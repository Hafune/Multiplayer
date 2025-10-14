using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ActionCompleteByStreamingLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] private float _minDuration;
        private EcsPool<ActionCompleteTag> _actionComplete;
        private EcsPool<EventActionCompleteStreaming> _completeStreaming;
        private float _startTime;
        private bool _eventCached;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _actionComplete = pools.ActionComplete;
            _completeStreaming = pools.EventActionCompleteStreaming;
        }

        public void SubStart(int entity)
        {
            _startTime = Time.time;
            _eventCached = false;
        }

        public override void Run(int entity)
        {
            if (_completeStreaming.Has(entity))
                _eventCached = true;

            if (_eventCached && Time.time - _startTime > _minDuration)
                _actionComplete.AddIfNotExist(entity);
        }
    }
}
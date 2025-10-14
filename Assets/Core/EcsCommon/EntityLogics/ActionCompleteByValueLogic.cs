using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ActionCompleteByValueLogic : AbstractEntityLogic
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private float _minAvailableValue;
        private EcsPool<ActionCompleteTag> _actionComplete;
        private ComponentPools _pools;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _actionComplete = _pools.ActionComplete;
        }

        public override void Run(int entity)
        {
            if (ValuePoolsUtility.GetValue(_pools, entity, _value) - _minAvailableValue <= 0)
                _actionComplete.AddIfNotExist(entity);
        }
    }
}
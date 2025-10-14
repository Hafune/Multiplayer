using System;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class EndFrameCircleCast2DLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityLogic _next;
        [SerializeField] private float _radius = 1f;
        private EcsPool<EventEndFrameCall> _endFrameCall;
        private Action<int> _setTargets;
        private int _mask;

        private readonly Collider2D[] _results = new Collider2D[64];

        private void Awake()
        {
            _mask = Physics2D.GetLayerCollisionMask(gameObject.layer);
            _endFrameCall = context.Resolve<ComponentPools>().EventEndFrameCall;
            _setTargets = SetTargets;
            Assert.IsNotNull(_next);
        }

        public override void Run(int entity) => _endFrameCall.GetOrInitialize(entity).call += _setTargets;

        private void SetTargets(int entity)
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _results, _mask);

            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
                _next.Run(TriggerCache.ExtractEntity(_results[i]).RawEntity);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.cyan;
            Handles.DrawWireArc(transform.position, Vector3.forward, Vector3.right, 360, _radius);
        }
#endif
    }
}
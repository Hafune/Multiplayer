using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class KnockbackAttraction2DLogic : AbstractEntityLogic
    {
        [SerializeField] private float _distance;
        private EcsPool<EventAttraction> _eventPool;
        private EcsPool<Rigidbody2DComponent> _positionPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _eventPool = pools.EventAttraction;
            _positionPool = pools.Rigidbody2D;
        }

        public override void Run(int target)
        {
            var position = _positionPool.Get(target).rigidbody2D.position;
            var dir = (position - (Vector2)transform.position).normalized;
            _eventPool.GetOrInitialize(target).position = position + dir * _distance;
        }
    }
}
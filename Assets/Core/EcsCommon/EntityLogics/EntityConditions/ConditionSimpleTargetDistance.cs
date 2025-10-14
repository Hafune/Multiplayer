using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ConditionSimpleTargetDistance : AbstractEntityCondition
    {
        [SerializeField] private float _distance;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<BodyRadiusComponent> _bodyRadiusPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>(); 
            _targetPool = pools.Target;
            _positionPool = pools.Position;
            _bodyRadiusPool = pools.BodyRadius;
        }

        public override bool Check(int entity)
        {
            if (!_targetPool.Has(entity))
                return false;

            int target = _targetPool.Get(entity).entity;
            var position = _positionPool.Get(target).transform.position;
            var radius = _bodyRadiusPool.Get(target).radius + _distance;
            
            var line = position - transform.position;

            return line.sqrMagnitude <= radius * radius;
        }
    }
}
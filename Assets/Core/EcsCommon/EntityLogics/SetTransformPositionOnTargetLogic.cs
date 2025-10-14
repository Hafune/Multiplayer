using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class SetTransformPositionOnTargetLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _transform;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<PositionComponent> _transformPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _targetPool = pools.Target;
            _transformPool = pools.Position;
            Assert.IsNotNull(_transform);
        }

        public override void Run(int entity)
        {
            if (_targetPool.Has(entity))
                _transform.position = _transformPool.Get(_targetPool.Get(entity).entity).transform.position;
        }
    }
}
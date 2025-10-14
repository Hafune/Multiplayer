using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateToTarget2DLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _defaultAngle;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<PositionComponent> _transformPool;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _targetPool = pools.Target;
            _transformPool = pools.Position;
        }

        public override void Run(int entity)
        {
            if (!_targetPool.Has(entity))
                return;

            var targetPosition = _transformPool.Get(_targetPool.Get(entity).entity).transform.position;
            float _angle = Vector2.SignedAngle(Vector2.right, targetPosition - transform.position) + _defaultAngle;
            _transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}
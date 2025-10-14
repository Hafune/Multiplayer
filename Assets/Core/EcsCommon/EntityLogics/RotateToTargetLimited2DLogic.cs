using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete("Не реализовано, доделать перед использованием !!")]
    public class RotateToTargetLimited2DLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _maxAngleDifference;
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

            var target = _transformPool.Get(_targetPool.Get(entity).entity).transform;
            var targetPosition = (Vector2)target.position;
            var position = (Vector2)_transform.position;
            var right = (Vector2)_transform.right;

            var totalRight = right.RotatedToward(targetPosition - position, _maxAngleDifference);
            _transform.right = totalRight;
        }
    }
}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class FollowToTarget2DLogic : AbstractEntityLogic
    {
        [SerializeField] private float _smooth = .5f;
        private EcsPool<TargetComponent> _targets;
        private EcsPool<Rigidbody2DComponent> _rigidbodies;
        private EcsPool<PositionComponent> _positions;
        private EcsPool<MoveSpeedValueComponent> _moveSpeed;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _targets = pools.Target;
            _positions = pools.Position;
            _rigidbodies = pools.Rigidbody2D;
            _moveSpeed = pools.MoveSpeedValue;
        }

        public override void Run(int entity)
        {
            if (!_targets.Has(entity))
                return;

            int target = _targets.Get(entity).entity;
            var direction = _positions.Get(target).transform.position - transform.position;
            var rigidbody = _rigidbodies.Get(entity).rigidbody;

            var velocity = direction.normalized * _moveSpeed.Get(entity).value;

            rigidbody.linearVelocity = Vector2.MoveTowards(
                rigidbody.linearVelocity,
                velocity,
                (1 / _smooth) * velocity.magnitude * Time.deltaTime);
        }
    }
}
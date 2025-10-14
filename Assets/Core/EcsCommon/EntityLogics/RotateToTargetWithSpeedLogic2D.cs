using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateToTargetWithSpeedLogic2D : AbstractEntityLogic
    {
        [SerializeField] private Transform _transform;
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity)
        {
            if (!_pools.Target.Has(entity))
                return;

            int targetEntity = _pools.Target.Get(entity).entity;
            var targetPosition = (Vector2)_pools.Position.Get(targetEntity).transform.position;
            var position = (Vector2)_transform.position;
            var angularSpeed = _pools.AngularSpeed.Get(entity).value * Time.deltaTime;
            var euler = _transform.eulerAngles;
            var angle = Vector2.SignedAngle(Vector2.right, targetPosition - position);
            euler.z = Mathf.MoveTowardsAngle(euler.z, angle, angularSpeed);
            _transform.rotation = Quaternion.Euler(euler);
        }
    }
}
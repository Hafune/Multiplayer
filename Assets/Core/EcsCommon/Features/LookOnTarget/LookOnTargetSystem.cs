using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class LookOnTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                TargetComponent,
                PositionComponent,
                AngularSpeedComponent,
                LookOnTargetTag
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                int targetEntity = _pools.Target.Get(entity).entity;
                var targetPosition = (Vector2)_pools.Position.Get(targetEntity).transform.position;
                var transform = _pools.Position.Get(entity).transform;
                var position = (Vector2)transform.position;
                var angularSpeed = _pools.AngularSpeed.Get(entity).value * Time.deltaTime;
                var euler = transform.eulerAngles;
                var angle = Vector2.SignedAngle(Vector2.right, targetPosition - position);
                euler.z = Mathf.MoveTowardsAngle(euler.z, angle, angularSpeed);
                transform.rotation = Quaternion.Euler(euler);
            }
        }
    }
}
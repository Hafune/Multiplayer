using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class DirectionUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                AngularSpeedComponent,
                AnimatorComponent,
                DirectionUpdateTag,
                MoveDestinationComponent,
                PositionComponent,
                MoveDirectionComponent,
                RigidbodyComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var dest = _pools.MoveDestination.Get(i);
                var position = (Vector2)_pools.Position.Get(i).transform.position;
                var line = dest.position - position;

                var rigidbody = _pools.Rigidbody.Get(i).rigidbody;
                rigidbody.rotation = Mathf.MoveTowardsAngle(rigidbody.rotation,
                    Vector2.SignedAngle(Vector2.right, _pools.MoveDirection.Get(i).direction = line.normalized),
                    _pools.AngularSpeed.Get(i).value * Time.deltaTime * _pools.Animator.Get(i).animancer.Graph.Speed);
                // Vector2.SignedAngle(Vector2.right, _pools.MoveDirection.Get(i).direction = line.normalized);
            }
        }
    }
}
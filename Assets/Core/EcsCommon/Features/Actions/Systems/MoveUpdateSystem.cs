using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class MoveUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                MoveUpdateComponent,
                PositionComponent,
                MoveDirectionComponent,
                MoveSpeedValueComponent,
                RigidbodyComponent
            >> _filter;

        private readonly ComponentPools _pools;
        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var scales = _pools.MoveUpdate.Get(i);
                var velocity = _pools.MoveDirection.Get(i).direction.ToVector3XZ();
                velocity.y = body.linearVelocity.y;

                var direction = Quaternion.Inverse(body.rotation) * velocity;
                var scale = Math.Abs(direction.z) < 0.1f ? scales.sidewaysSpeed :
                    direction.z > 0 ? scales.forwardSpeed :
                    scales.backwardSpeed;

                body.linearVelocity = velocity * _pools.MoveSpeedValue.Get(i).value * scale;
            }
        }
    }
}
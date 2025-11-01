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
        private readonly Collider[] _colliders = new Collider[4];
        private readonly int _mask = LayerMask.GetMask("UnitCollision");

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var scales = _pools.MoveUpdate.Get(i);
                var velocity = _pools.MoveDirection.Get(i).direction.ToVector3XZ();
                var y = body.linearVelocity.y;

                var direction = Quaternion.Inverse(body.rotation) * velocity;
                var scale = Math.Abs(direction.z) < 0.1f ? scales.sidewaysSpeed :
                    direction.z > 0 ? scales.forwardSpeed :
                    scales.backwardSpeed;

                var total = velocity * _pools.MoveSpeedValue.Get(i).value * scale;
                var horizontalVelocity = new Vector3(total.x, 0, total.z);
                const float sphereRadius = 0.18f;

                var position = body.position + new Vector3(0,.5f,0);
                int count = Physics.OverlapSphereNonAlloc(position, sphereRadius, _colliders, _mask);
                for (var index = 0; index < count; index++)
                {
                    var col = _colliders[index];
                    if (col.attachedRigidbody == body) 
                        continue;

                    var colliderCenter = col.bounds.center;
                    var currentDistance = Vector3.Distance(position, colliderCenter);
                    var nextPosition = position + horizontalVelocity * Time.deltaTime;
                    var nextDistance = Vector3.Distance(nextPosition, colliderCenter);

                    if (nextDistance < currentDistance)
                    {
                        total.x = 0;
                        total.z = 0;
                        break;
                    }
                }

                total.y = y;
                body.linearVelocity = total;
            }
        }
    }
}
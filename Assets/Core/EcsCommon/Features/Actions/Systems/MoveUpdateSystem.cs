using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class MoveUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                MoveUpdateTag,
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
                var rb = _pools.Rigidbody.Get(i).rigidbody;
                var dir = _pools.MoveDirection.Get(i).direction * _pools.MoveSpeedValue.Get(i).value;
                var velocity = rb.linearVelocity;
                velocity.x = dir.x;
                velocity.z = dir.y;
                rb.linearVelocity = velocity;
            }
        }
    }
}
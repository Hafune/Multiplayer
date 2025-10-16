using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core
{
    public class MultiplayerUpdateServerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                Player1UniqueTag,
                RigidbodyComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;
            
                MultiplayerManager.Instance.SendData("move", new()
                {
                    { "x", position.x },
                    { "z", position.z },
                    { "velocityX", velocity.x },
                    { "velocityZ", velocity.z },
                });
            }
        }
    }
}
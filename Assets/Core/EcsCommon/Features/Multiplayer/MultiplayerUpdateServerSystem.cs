using System.Collections.Generic;
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
        private readonly Dictionary<string, object> _message = new();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var body = _pools.Rigidbody.Get(i).rigidbody;
                var position = body.position;
                var velocity = body.linearVelocity;
                _message.Clear();
                _message["x"] = position.x;
                _message["z"] = position.z;
                _message["velocityX"] = velocity.x;
                _message["velocityZ"] = velocity.z;

                MultiplayerManager.Instance.SendData("move", _message);
            }
        }
    }
}
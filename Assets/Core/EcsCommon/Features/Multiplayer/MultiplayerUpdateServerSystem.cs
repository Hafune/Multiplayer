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
                PositionComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var position = _pools.Position.Get(i).transform.position;
            
                MultiplayerManager.Instance.SendData("move", new()
                {
                    { "x", position.x },
                    { "z", position.z },
                });
            }
        }
    }
}
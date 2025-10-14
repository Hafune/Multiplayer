using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventMoveCompleteSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventMoveComplete,
                ActionOnMoveCompleteComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pools.ActionOnMoveComplete.Get(i).action?.Invoke(i);
                _pools.EventMoveComplete.Del(i);
            }
        }
    }
}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventEndFrameCallSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventEndFrameCall
            >> _filter;

        private readonly EcsPoolInject<EventEndFrameCall> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pool.Value.Get(i).call.Invoke(i);
                _pool.Value.Del(i);
            }
        }
    }
}
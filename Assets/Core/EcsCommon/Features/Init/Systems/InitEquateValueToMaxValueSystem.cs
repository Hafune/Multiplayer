using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class InitEquateValueToMaxValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventInit,
                V,
                M
            >> _filter;

        private readonly EcsPoolInject<EventValueUpdated<V>> _eventRefreshValuePool;
        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _maxValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _valuePool.Value.Get(i).value = _maxValuePool.Value.Get(i).value;
                _eventRefreshValuePool.Value.AddIfNotExist(i);
            }
        }
    }
}
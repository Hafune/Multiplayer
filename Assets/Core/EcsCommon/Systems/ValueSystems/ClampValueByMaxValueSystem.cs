using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ClampValueByMaxValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventValueUpdated<V>,
                M
            >> _valueFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                EventValueUpdated<M>
            >> _maxValueFilter;

        private readonly EcsPoolInject<EventValueUpdated<V>> _eventRefreshValuePool;
        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _maxValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueFilter.Value)
            {
                ref var value = ref _valuePool.Value.Get(i);
                var maxValue = _maxValuePool.Value.Get(i).value;
                
                if (value.value <= maxValue)
                    continue;
                
                value.value = _maxValuePool.Value.Get(i).value;
            }
            
            foreach (var i in _maxValueFilter.Value)
            {
                ref var value = ref _valuePool.Value.Get(i);
                var maxValue = _maxValuePool.Value.Get(i).value;
                
                if (value.value <= maxValue)
                    continue;
                
                value.value = _maxValuePool.Value.Get(i).value;
                _eventRefreshValuePool.Value.AddIfNotExist(i);
            }
        }
    }
}
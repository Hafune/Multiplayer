using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class EventResourceGeneratedSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventResourceGenerated,
                V,
                M
            >> _filter;

        private readonly EcsPoolInject<EventResourceGenerated> _eventGeneratePool;
        private readonly EcsPoolInject<EventValueUpdated<V>> _eventUpdatedPool;
        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _valueMaxPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var value = _eventGeneratePool.Value.Get(i).value;
                ref var resource = ref _valuePool.Value.Get(i);
                resource.value = Math.Min(_valueMaxPool.Value.Get(i).value, resource.value + value);
                _eventUpdatedPool.Value.AddIfNotExist(i);
            }
        }
    }
}
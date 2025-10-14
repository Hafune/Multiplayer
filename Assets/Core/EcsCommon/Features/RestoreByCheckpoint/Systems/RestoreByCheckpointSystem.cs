using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class RestoreByCheckpointSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventRestoreConsumables,
                V,
                M
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventRestoreConsumables
            >> _eventFilter;

        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _valueMaxPool;
        private readonly EcsPoolInject<EventValueUpdated<V>> _eventUpdatedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _valuePool.Value.Get(i).value = _valueMaxPool.Value.Get(i).value;
                _eventUpdatedPool.Value.AddIfNotExist(i);
            }
        }
    }
}
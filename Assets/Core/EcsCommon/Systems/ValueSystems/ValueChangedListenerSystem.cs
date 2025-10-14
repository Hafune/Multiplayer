using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ValueChangeListenerSystem<V, L> : IEcsRunSystem
        where V : struct, IValue
        where L : struct, IValueChangeListener
    {
        private readonly EcsFilterInject<
            Inc<
                V,
                L,
                EventValueUpdated<V>
            >> _filter;

        private readonly EcsPoolInject<V> _pool;
        private readonly EcsPoolInject<L> _listenerPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _listenerPool.Value.Get(i).UpdateByValue(_pool.Value.Get(i).value);
        }
    }
}
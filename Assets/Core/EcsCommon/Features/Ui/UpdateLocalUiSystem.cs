using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateLocalUiSystem<T> : IEcsRunSystem
        where T : struct
    {
        private readonly EcsFilterInject<
            Inc<
                T, 
                EventValueUpdated<T>, 
                LocalUiValue<T>
            >> _valueRefreshFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<LocalUiValue<T>> _uiValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
                _uiValuePool.Value.Get(i).update.Invoke(_valuePool.Value.Get(i));
        }
    }
}
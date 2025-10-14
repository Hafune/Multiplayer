using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateGlobalUiSystem<ByTag, T> : IEcsRunSystem
        where ByTag : struct //компонент для связи
        where T : struct //изменившийся компонент
    {
        private readonly EcsFilterInject<
            Inc<
                ByTag,
                T,
                EventValueUpdated<T>
            >> _valueRefreshFilter;

        private readonly EcsFilterInject<
            Inc<
                GlobalUiLink<ByTag>,
                UiValue<T>
            >> _uiFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<UiValue<T>> _uiValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
            foreach (var ui in _uiFilter.Value)
                _uiValuePool.Value.Get(ui).update(i, _valuePool.Value.Get(i));
        }
    }
}
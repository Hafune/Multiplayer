using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateGlobalUiByEventSystem<ByTag, E> : IEcsRunSystem
        where ByTag : struct //компонент для связи
        where E : struct //ивент
    {
        private readonly EcsFilterInject<
            Inc<
                ByTag,
                E
            >> _valueRefreshFilter;

        private readonly EcsFilterInject<
            Inc<
                GlobalUiLink<ByTag>,
                UiValue<E>
            >> _uiFilter;

        private readonly EcsPoolInject<E> _valuePool;
        private readonly EcsPoolInject<UiValue<E>> _uiValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
            foreach (var ui in _uiFilter.Value)
                _uiValuePool.Value.Get(ui).update(i, _valuePool.Value.Get(i));
        }
    }
}
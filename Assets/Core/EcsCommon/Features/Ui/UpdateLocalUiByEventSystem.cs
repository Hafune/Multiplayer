using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateLocalUiByEventSystem<E> : IEcsRunSystem
        where E : struct //ивент
    {
        private readonly EcsFilterInject<
            Inc<
                E,
                LocalUiValue<E>
            >> _eventFilter;

        private readonly EcsPoolInject<E> _valuePool;
        private readonly EcsPoolInject<LocalUiValue<E>> _uiValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventFilter.Value)
                _uiValuePool.Value.Get(i).update.Invoke(_valuePool.Value.Get(i));
        }
    }
}
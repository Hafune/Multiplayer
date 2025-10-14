using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionCancelBeforeRemoveEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                PositionComponent,
                ActionCurrentComponent,
                EventRemoveEntity
            >> _filter;

        private readonly EcsPoolInject<PositionComponent> _transformPool;
        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _actionCurrentPool.Value.Get(i).currentAction?.Cancel(i);
                // var current = _actionCurrentPool.Value.Get(i);

                // if (!_transformPool.Value.Get(i).transform.IsDestroyed())
                //     current.currentAction?.Cancel(i);
            }
        }
    }
}
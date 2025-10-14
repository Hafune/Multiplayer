using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionUpdateTag,
                ActionCurrentComponent
            >> _filter;

        // private readonly ComponentPools _pools;
        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _actionCurrentPool.Value.Get(i).logic?.UpdateLogic(i);
        }
    }
}
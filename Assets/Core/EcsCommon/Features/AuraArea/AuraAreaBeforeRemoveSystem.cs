using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class AuraAreaBeforeRemoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                AuraAreaComponent,
                AuraEventsComponent,
                EventRemoveEntity
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var area = _pools.AuraArea.Get(i).area;
                var auraEvents = _pools.AuraEvents.Get(i);

                area.ForEachLeavers(auraEvents.targetExitEvents.Run);
                area.ForEachPotentialLeavers(auraEvents.targetExitEvents.Run);
            }
        }
    }
}
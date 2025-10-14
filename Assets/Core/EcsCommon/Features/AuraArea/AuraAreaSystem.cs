using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class AuraAreaSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ReceiversInArea<AuraAreaComponent>,
                AuraAreaComponent,
                AuraEventsComponent
            >> _enterFilter;

        private readonly EcsFilterInject<
            Inc<
                LeaversFromArea<AuraAreaComponent>,
                AuraAreaComponent,
                AuraEventsComponent
            >> _exitFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _enterFilter.Value)
            {
                var area = _pools.AuraArea.Get(i).area;
                var auraEvents = _pools.AuraEvents.Get(i);

                area.WriteReceiversToPotentialLeavers();
                area.ForEachReceivers(auraEvents.targetEnterEvents.Run);

                auraEvents.selfEnterEvents?.Run(i);
            }

            foreach (var i in _exitFilter.Value)
            {
                var area = _pools.AuraArea.Get(i).area;
                var auraEvents = _pools.AuraEvents.Get(i);

                if (auraEvents.targetExitEvents)
                    area.ForEachLeavers(auraEvents.targetExitEvents.Run);

                auraEvents.selfExitEvents?.Run(i);
            }
        }
    }
}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventDeathByDealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DeathOnDealDamageTag,
                EventCausedDamage
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.EventDeathByDealDamage.Add(i);
        }
    }
}
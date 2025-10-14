using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class DeathOnDealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventDeathByDealDamage,
                EventCausedDamage
            >,
            Exc<
                ThroughProjectileSlotTag
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.EventStartActionDeath.AddIfNotExist(i);
        }
    }
}
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class EventIncomingDamageApplySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventIncomingDamage,
                HitPointValueComponent
            >,
            Exc<
                InvulnerabilityLifetimeComponent
            >> _filter;

        private readonly EcsPoolInject<EventIncomingDamage> _eventApplyDamagePool;
        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var eventApplyDamage = _pools.EventIncomingDamage.Get(i).data;

                if (eventApplyDamage.Count == 0)
                    return;

                var hitPosition = _pools.Position.Get(i).transform.position;
                ref var eventDamageTaken = ref _pools.EventDamageTaken.Add(i);

                foreach (ref var value in eventApplyDamage)
                {
                    eventDamageTaken.count++;

                    if (value.effect)
                        value.effect.Spawn(hitPosition, (int)value.damage);

                    _pools.EventCausedDamage.GetOrInitialize(value.owner).AddDamage(i, value.damage);
                }
            }
        }
    }
}
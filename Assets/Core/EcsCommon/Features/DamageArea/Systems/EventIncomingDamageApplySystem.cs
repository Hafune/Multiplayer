using System;
using Core.Components;
using Core.Generated;
using Core.Lib.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;
using Random = UnityEngine.Random;

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

                var totalPoint = Vector3.zero;
                var ownerPosition = Vector3.zero;
                ref var eventDamageTaken = ref _pools.EventDamageTaken.Add(i);

                foreach (ref var value in eventApplyDamage)
                {
                    eventDamageTaken.count++;
                    totalPoint += value.triggerPoint;
                    ownerPosition += value.ownerPosition;

                    if (value.effect)
                        value.effect.Spawn(value.triggerPoint + Vector3.back, (int)value.damage);

                    _pools.EventCausedDamage.GetOrInitialize(value.owner).AddDamage(i, value.damage);
                }
            }
        }
    }
}
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
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var eventApplyDamage = _eventApplyDamagePool.Value.Get(entity).data;

            if (eventApplyDamage.Count == 0)
                return;

            ref var hitPoint = ref _pools.HitPointValue.Get(entity);
            var totalPoint = Vector3.zero;
            var ownerPosition = Vector3.zero;
            float totalDamage = 0;
            ref var eventDamageTaken = ref _pools.EventDamageTaken.Add(entity);

            foreach (ref var value in eventApplyDamage)
            {
                eventDamageTaken.count++;
                hitPoint.value -= value.damage;
                totalDamage += value.damage;
                totalPoint += value.triggerPoint;
                ownerPosition += value.ownerPosition;

                if (value.effect)
                    value.effect.Spawn(value.triggerPoint + Vector3.back, (int)value.damage);

                ref var damages = ref _pools.EventCausedDamage.GetOrInitialize(value.owner);
                MyArrayUtility.Add(ref damages.damages, ref damages.damagesCount, value.damage);
            }

            totalPoint /= eventApplyDamage.Count;
            ownerPosition /= eventApplyDamage.Count;
            _pools.EventUpdatedHitPointValue.AddIfNotExist(entity);

            if (hitPoint.value <= 0)
            {
                _pools.EventStartActionDeath.GetOrInitialize(entity);
                ref var actionDeath = ref _pools.ActionDeath.Get(entity);
                actionDeath.impactPosition = ownerPosition;
                actionDeath.impactDirection =
                    (_pools.Position.Get(entity).transform.position - ownerPosition).normalized;
                actionDeath.impactDirection += Vector3.back * Random.value * 2;
                actionDeath.impactDirection.Normalize();
                actionDeath.impactForce =
                    Mathf.Clamp(Math.Abs(totalDamage / _pools.HitPointMaxValue.Get(entity).value * 4), .25f, 1f);
            }

            if (!_pools.HitTakenEffect.Has(entity))
                return;

            var hitEffectSpawner = _pools.HitTakenEffect.Get(entity).hitEffectOldSpawner;
            hitEffectSpawner.transform.position = totalPoint;
            hitEffectSpawner.Execute();
        }
    }
}
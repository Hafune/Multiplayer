using Core.Components;
using Core.Generated;
using DamageNumbersPro;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class DamagePerSecondSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                TargetComponent,
                DamagePerSecondComponent,
                HitImpactComponent
            >> _filter;

        private readonly EcsFilterInject<Inc<EventIncomingDamage>> _eventApplyDamageFilter;

        private readonly DamageNumber _damageTextEffectPrefab;

        // private readonly DamageNumber _criticalDamageTextEffectPrefab;
        private readonly ComponentPools _pools;

        public DamagePerSecondSystem(Context context)
        {
            var effectsTemplate = context.Resolve<CommonValues>();
            _damageTextEffectPrefab = effectsTemplate.DamageTextEffectPrefab;
            // _criticalDamageTextEffectPrefab = effectsTemplate.CriticalDamageTextEffectPrefab;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var value = ref _pools.DamagePerSecond.Get(i);

                if (value.duration != 0 && Time.time - value.startTime > value.duration)
                {
                    _pools.EventRemoveEntity.AddIfNotExist(i);
                    continue;
                }

                float deltaTime = Time.fixedTime - value.lastTickTime;
                if (deltaTime < value.tickDelay)
                    continue;

                value.lastTickTime = Time.fixedTime;
                float damage = value.damagePerSecond * deltaTime;
                damage += Random.value * damage * .1f;
                int target = _pools.Target.Get(i).entity;
                int owner = _pools.Parent.Has(i) ? _pools.Parent.Get(i).entity : i;
                var eventIncomingDamage = _pools.EventIncomingDamage.GetOrInitialize(target);

                var impacts = _pools.HitImpact.Get(i);
                impacts.targetEvents?.Run(target);
                impacts.selfEvents?.Run(i);
                var position = _pools.Position.Get(target).transform.position;
                
                eventIncomingDamage.data.Add((
                    damage,
                    position,
                    position + (Vector3)Random.insideUnitCircle,
                    owner,
                    value.showDamage ? _damageTextEffectPrefab : null
                ));
            }
        }
    }
}
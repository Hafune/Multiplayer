using System;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Core.Lib;
using DamageNumbersPro;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Systems
{
    public class DamageAreaSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ReceiversInArea<DamageAreaComponent>,
                DamageAreaComponent,
                DamageValueComponent,
                HitImpactComponent,
                PositionComponent
            >,
            Exc<
                CriticalChanceValueComponent
            >> _enemyFilter;

        private readonly EcsFilterInject<
            Inc<
                ReceiversInArea<DamageAreaComponent>,
                DamageAreaComponent,
                DamageMinValueComponent,
                DamageMaxValueComponent,
                CriticalChanceValueComponent,
                CriticalDamageValueComponent,
                HitImpactComponent,
                PositionComponent
            >> _playerFilter;

        private readonly DamageNumber _damageTextEffectPrefab;
        private readonly DamageNumber _criticalDamageTextEffectPrefab;
        private readonly ComponentPools _pools;
        private readonly Action<int, float> _forEachEntity;

        private int _entity;
        private float _damage;
        private Vector3 _position;
        private DamageArea _area;
        private AbstractEntityLogic _targetEvents;
        private bool _canBeCritical;

        public DamageAreaSystem(Context context)
        {
            var effectsTemplate = context.Resolve<CommonValues>();
            _damageTextEffectPrefab = effectsTemplate.DamageTextEffectPrefab;
            _criticalDamageTextEffectPrefab = effectsTemplate.CriticalDamageTextEffectPrefab;
            _forEachEntity = ForEachEntity;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _enemyFilter.Value)
            {
                _canBeCritical = false;
                UpdateEntity(i, _pools.DamageArea.Get(i).area, _pools.DamageValue.Get(i).value);
            }

            foreach (var i in _playerFilter.Value)
            {
                var damage = _pools.DamageMaxValue.Get(i).value;
                damage += (_pools.DamageMaxValue.Get(i).value - damage); // * Random.value;
                _canBeCritical = true;

                UpdateEntity(i, _pools.DamageArea.Get(i).area, damage);
            }
        }

        private void UpdateEntity(int entity, DamageArea area, float damage)
        {
            var hitImpactsNode = _pools.HitImpact.Get(entity);
            var position = _pools.Position.Get(entity).transform.position;

            _damage = damage;
            _area = area;
            _position = position;
            _entity = entity;
            _targetEvents = hitImpactsNode.targetEvents;
            //этот ивент не нужен ни одному фильтру, возможно стоит объеденить с каким нибудь компонентоа этой системы  
            _pools.EventDamageAreaSelfImpactInfo.GetOrInitialize(entity) = default;

            area.WriteReceiversToHits();
            area.ForEachReceivers(_forEachEntity, true);

            if (area.ReceiversClearCount == 0)
                hitImpactsNode.selfEvents?.Run(entity);
        }

        private void ForEachEntity(int targetEntity, float damageScale)
        {
            DamageNumber damageNumberPrefab = null;
            var damage = _damage * damageScale;
            damage += Random.value * damage * .2f;

            ref var info = ref _pools.EventDamageAreaSelfImpactInfo.Get(_entity);
            info.isCriticalHit = false;

            if (_canBeCritical)
            {
                damageNumberPrefab = _damageTextEffectPrefab;
                var chance = _pools.CriticalChanceValue.Get(_entity).value;

                chance += _pools.HitAdditionalCriticalChance.Get(_entity).value;
                chance += _pools.VulnerabilityCriticalChanceValue.Get(targetEntity).value;

                if (chance >= Random.value)
                {
                    damageNumberPrefab = _criticalDamageTextEffectPrefab;
                    damage += damage * _pools.CriticalDamageValue.Get(_entity).value;
                    info.criticalHitCount++;
                    info.isCriticalHit = true;
                }
            }

            _targetEvents?.Run(targetEntity);

            if (damage == 0)
                return;

            var eventIncomingDamage = _pools.EventIncomingDamage.GetOrInitialize(targetEntity);
            eventIncomingDamage.data.Add((
                damage,
                _area.TriggerPoint,
                _position,
                _entity,
                damageNumberPrefab));
        }
    }
}
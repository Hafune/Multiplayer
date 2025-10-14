using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Systems
{
    public class AbilityHealingPotionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventAbilityStart<HealingPotionValueComponent>,
                HealingPotionValueComponent,
                HitPointValueComponent,
                HitPointMaxValueComponent
            >,
            Exc<
                InProgressTag<CooldownValueComponent<HealingPotionValueComponent>>,
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventAbilityStart<HealingPotionValueComponent>
            >> _eventFilter;

        private readonly EcsPoolInject<EventStartCooldown<CooldownValueComponent<HealingPotionValueComponent>>> _startCooldownPool;
        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pools.CooldownValueHealingPotionValue.Get(i).startTime = Time.time;
                _startCooldownPool.Value.Add(i);
                ref var hp = ref _pools.HitPointValue.Get(i);
                var maxValue = _pools.HitPointMaxValue.Get(i).value;
                hp.value = math.min(hp.value + maxValue * _pools.HealingPotionValue.Get(i).value, maxValue);
                _pools.EventUpdatedHitPointValue.AddIfNotExist(i);
            }

            foreach (var i in _eventFilter.Value)
                _pools.EventAbilityStartHealingPotionValue.Del(i);
        }
    }
}
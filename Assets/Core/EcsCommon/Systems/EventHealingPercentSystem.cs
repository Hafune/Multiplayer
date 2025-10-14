using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Systems
{
    public class EventHealingPercentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventHealingPercent,
                HitPointValueComponent,
                HitPointMaxValueComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var hp = ref _pools.HitPointValue.Get(i);
                var hpMax = _pools.HitPointMaxValue.Get(i).value;
                var percent = _pools.EventHealingPercent.Get(i).value;
                hp.value = math.clamp(hp.value + hpMax * percent, 0, hpMax);
                _pools.EventUpdatedHitPointValue.AddIfNotExist(i);
                _pools.EventHealingPercent.Del(i);
            }
        }
    }
}
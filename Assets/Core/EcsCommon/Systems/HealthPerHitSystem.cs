using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;

namespace Core.Systems
{
    public class HealthPerHitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ReceiversInArea<DamageAreaComponent>,
                DamageAreaComponent,
                HealingPerHitValueComponent,
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
                if (_pools.DamageArea.Get(i).area.ReceiversClearCount != 0)
                    continue;

                ref var hp = ref _pools.HitPointValue.Get(i);
                hp.value += _pools.HealingPerHitValue.Get(i).value;
                hp.value = math.clamp(hp.value, 0, _pools.HitPointMaxValue.Get(i).value);
                _pools.EventUpdatedHitPointValue.AddIfNotExist(i);
            }
        }
    }
}
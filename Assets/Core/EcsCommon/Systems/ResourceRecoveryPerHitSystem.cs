using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;

namespace Core.Systems
{
    public class ResourceRecoveryPerHitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ReceiversInArea<DamageAreaComponent>,
                DamageAreaComponent,
                ResourceRecoveryPerHitComponent,
                ManaPointValueComponent,
                ManaPointMaxValueComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                if (_pools.DamageArea.Get(i).area.ReceiversClearCount != 0)
                    continue;
                
                ref var value = ref _pools.ManaPointValue.Get(i);
                value.value += _pools.ResourceRecoveryPerHit.Get(i).value;
                value.value = math.clamp(value.value, 0, _pools.ManaPointMaxValue.Get(i).value);
                _pools.EventUpdatedManaPointValue.AddIfNotExist(i);
            }
        }
    }
}
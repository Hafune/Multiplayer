using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.Mathematics;

namespace Core.Systems
{
    public class ResourceRecoveryPerDamageTakenSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventDamageTaken,
                ResourceRecoveryPerDamageTakenValueComponent,
                ManaPointValueComponent,
                ManaPointMaxValueComponent
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var value = ref _pools.ManaPointValue.Get(i);
                value.value += _pools.ResourceRecoveryPerDamageTakenValue.Get(i).value * _pools.EventDamageTaken.Get(i).count;
                value.value = math.clamp(value.value, 0, _pools.ManaPointMaxValue.Get(i).value);
                _pools.EventUpdatedManaPointValue.AddIfNotExist(i);
            }
        }
    }
}
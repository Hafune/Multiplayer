using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Core.Systems
{
    public class BarbRevengeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                BarbRevengeComponent,
                EventHitTaken
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var value = _pools.BarbRevenge.Get(i);

                if (value.chargeChance < Random.value)
                    continue;

                float charge = math.clamp(value.getCharge(i) + 1, 0, value.getChargeMax(i));
                value.setCharge(i, charge);
            }
        }
    }
}
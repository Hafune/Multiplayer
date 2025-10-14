using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class ValuePerSecondSystemsNode
    {
        public static IEcsSystem[] BuildSystems()
        {
            //Обновление UI
            return new IEcsSystem[]
            {
                new ValuePerSecondSystem<
                    HitPointValueComponent,
                    HitPointMaxValueComponent,
                    HealingPerSecondValueComponent>(),

                new ValuePercentPerSecondSystem<
                    HitPointValueComponent,
                    HitPointMaxValueComponent,
                    HealingPercentPerSecondValueComponent>(),

                new ValuePerSecondSystem<
                    ManaPointValueComponent,
                    ManaPointMaxValueComponent,
                    ManaPointRecoveryValueComponent>(),

                new ValuePerSecondSystem<
                    ManaPointValueComponent,
                    ManaPointMaxValueComponent,
                    ActionCostPerSecondValueComponent>()
            };
        }
    }
}
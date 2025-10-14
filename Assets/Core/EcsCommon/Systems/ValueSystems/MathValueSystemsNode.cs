using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class MathValueSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            //еденица силы = 1% урона
            new SumValueByValueSystem<DamageMinValueComponent, DamagePropertyMinValueComponent>(),
            new SumValueByValueSystem<DamageMaxValueComponent, DamagePropertyMaxValueComponent>(),
            new SumValueByPercentSystem<DamageMinValueComponent, StrengthValueComponent>(.01f),
            new SumValueByPercentSystem<DamageMaxValueComponent, StrengthValueComponent>(.01f),
            new SumValueByPercentSystem<DamageMinValueComponent, DamagePercentValueComponent>(),
            new SumValueByPercentSystem<DamageMaxValueComponent, DamagePercentValueComponent>(),
            //
            new SumValueByPercentSystem<AttackSpeedValueComponent, AttackSpeedPercentValueComponent>(),
            //
            new SumValueByPercentSystem<VitalityValueComponent, VitalityPercentValueComponent>(),
            new SumValueByPercentSystem<HitPointMaxValueComponent, HitPointPercentValueComponent>(),
            //
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkButton1Component>, RecoveryTimeReductionValueComponent>(-1),
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkButton2Component>, RecoveryTimeReductionValueComponent>(-1),
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkButton3Component>, RecoveryTimeReductionValueComponent>(-1),
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkButton4Component>, RecoveryTimeReductionValueComponent>(-1),
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkMouseLeftComponent>, RecoveryTimeReductionValueComponent>(-1),
            new SumValueByPercentSystem<CooldownValueComponent<ActionLinkMouseRightComponent>, RecoveryTimeReductionValueComponent>(-1),
            //Снижение стоимости
            new SumValueByPercentSystem<CostValueComponent<ActionLinkButton1Component>, ResourceCostsReductionValueComponent>(-1),
            new SumValueByPercentSystem<CostValueComponent<ActionLinkButton2Component>, ResourceCostsReductionValueComponent>(-1),
            new SumValueByPercentSystem<CostValueComponent<ActionLinkButton3Component>, ResourceCostsReductionValueComponent>(-1),
            new SumValueByPercentSystem<CostValueComponent<ActionLinkButton4Component>, ResourceCostsReductionValueComponent>(-1),
            new SumValueByPercentSystem<CostValueComponent<ActionLinkMouseLeftComponent>, ResourceCostsReductionValueComponent>(-1),
            new SumValueByPercentSystem<CostValueComponent<ActionLinkMouseRightComponent>, ResourceCostsReductionValueComponent>(-1),
            //
            new SumValueByPercentSystem<ActionCostPerSecondValueComponent, ResourceCostsReductionValueComponent>(-1),
            //
            new SumValueByPercentSystem<MoveSpeedValueComponent, MoveSpeedPercentValueComponent>(),
            new SumValueByPercentSystem<MoveSpeedValueComponent, SlowdownMoveValueComponent>(-1),
            new SumValueByPercentSystem<MoveSpeedValueComponent, SlowdownAnimationValueComponent>(-1),
            //
            new SumValueByValueSystem<ArmorValueComponent, StrengthValueComponent>(),
            new SumValueByValueSystem<ArmorValueComponent, ArmorPropertyValueComponent>(),
            new SumValueByPercentSystem<ArmorValueComponent, ArmorPercentValueComponent>(),
            //
            new SumValueByValueSystem<HitPointMaxValueComponent, VitalityValueComponent>(10),
            //
            //new ValueChangeListenerSystem<ExplosionScaleValueComponent, ScaledByExplosionScaleValue>()
        };
    }
}
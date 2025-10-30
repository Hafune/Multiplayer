//Файл генерируется в GenEventStartRecalculateValueSystem
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class EventStartRecalculateValueSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventValueUpdated<ActionCostPerSecondValueComponent>>> _eventUpdatedActionCostPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<ActionCostPerSecondValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ActionCostPerSecondValueComponent>>> _baseActionCostPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AddScoreOnDeathValueComponent>>> _eventUpdatedAddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<AddScoreOnDeathValueComponent, EventStartRecalculateAllValues, BaseValueComponent<AddScoreOnDeathValueComponent>>> _baseAddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ArmorPercentValueComponent>>> _eventUpdatedArmorPercentValueFilter;
        private readonly EcsFilterInject<Inc<ArmorPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ArmorPercentValueComponent>>> _baseArmorPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ArmorPropertyValueComponent>>> _eventUpdatedArmorPropertyValueFilter;
        private readonly EcsFilterInject<Inc<ArmorPropertyValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ArmorPropertyValueComponent>>> _baseArmorPropertyValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ArmorValueComponent>>> _eventUpdatedArmorValueFilter;
        private readonly EcsFilterInject<Inc<ArmorValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ArmorValueComponent>>> _baseArmorValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AttackSpeedPercentValueComponent>>> _eventUpdatedAttackSpeedPercentValueFilter;
        private readonly EcsFilterInject<Inc<AttackSpeedPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<AttackSpeedPercentValueComponent>>> _baseAttackSpeedPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AttackSpeedValueComponent>>> _eventUpdatedAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<AttackSpeedValueComponent, EventStartRecalculateAllValues, BaseValueComponent<AttackSpeedValueComponent>>> _baseAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageBashValueComponent>>> _eventUpdatedBarbDamageBashValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageBashValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageBashValueComponent>>> _baseBarbDamageBashValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageCleaveValueComponent>>> _eventUpdatedBarbDamageCleaveValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageCleaveValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageCleaveValueComponent>>> _baseBarbDamageCleaveValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageFrenzyValueComponent>>> _eventUpdatedBarbDamageFrenzyValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageFrenzyValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageFrenzyValueComponent>>> _baseBarbDamageFrenzyValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageHammerOfTheAncientsValueComponent>>> _eventUpdatedBarbDamageHammerOfTheAncientsValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageHammerOfTheAncientsValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageHammerOfTheAncientsValueComponent>>> _baseBarbDamageHammerOfTheAncientsValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageOverPowerValueComponent>>> _eventUpdatedBarbDamageOverPowerValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageOverPowerValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageOverPowerValueComponent>>> _baseBarbDamageOverPowerValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageRendValueComponent>>> _eventUpdatedBarbDamageRendValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageRendValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageRendValueComponent>>> _baseBarbDamageRendValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageRevengeValueComponent>>> _eventUpdatedBarbDamageRevengeValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageRevengeValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageRevengeValueComponent>>> _baseBarbDamageRevengeValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageSeismicSlamValueComponent>>> _eventUpdatedBarbDamageSeismicSlamValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageSeismicSlamValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageSeismicSlamValueComponent>>> _baseBarbDamageSeismicSlamValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageWeaponThrowValueComponent>>> _eventUpdatedBarbDamageWeaponThrowValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageWeaponThrowValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageWeaponThrowValueComponent>>> _baseBarbDamageWeaponThrowValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbDamageWhirlwindValueComponent>>> _eventUpdatedBarbDamageWhirlwindValueFilter;
        private readonly EcsFilterInject<Inc<BarbDamageWhirlwindValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbDamageWhirlwindValueComponent>>> _baseBarbDamageWhirlwindValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BarbFrenzyStackValueComponent>>> _eventUpdatedBarbFrenzyStackValueFilter;
        private readonly EcsFilterInject<Inc<BarbFrenzyStackValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BarbFrenzyStackValueComponent>>> _baseBarbFrenzyStackValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BlockAmountMaxValueComponent>>> _eventUpdatedBlockAmountMaxValueFilter;
        private readonly EcsFilterInject<Inc<BlockAmountMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BlockAmountMaxValueComponent>>> _baseBlockAmountMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BlockAmountMinValueComponent>>> _eventUpdatedBlockAmountMinValueFilter;
        private readonly EcsFilterInject<Inc<BlockAmountMinValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BlockAmountMinValueComponent>>> _baseBlockAmountMinValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<BlockChanceValueComponent>>> _eventUpdatedBlockChanceValueFilter;
        private readonly EcsFilterInject<Inc<BlockChanceValueComponent, EventStartRecalculateAllValues, BaseValueComponent<BlockChanceValueComponent>>> _baseBlockChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalChanceValueComponent>>> _eventUpdatedCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<CriticalChanceValueComponent, EventStartRecalculateAllValues, BaseValueComponent<CriticalChanceValueComponent>>> _baseCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalDamageValueComponent>>> _eventUpdatedCriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<CriticalDamageValueComponent, EventStartRecalculateAllValues, BaseValueComponent<CriticalDamageValueComponent>>> _baseCriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageColdValueComponent>>> _eventUpdatedDamageColdValueFilter;
        private readonly EcsFilterInject<Inc<DamageColdValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageColdValueComponent>>> _baseDamageColdValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageFireValueComponent>>> _eventUpdatedDamageFireValueFilter;
        private readonly EcsFilterInject<Inc<DamageFireValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageFireValueComponent>>> _baseDamageFireValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageLightningValueComponent>>> _eventUpdatedDamageLightningValueFilter;
        private readonly EcsFilterInject<Inc<DamageLightningValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageLightningValueComponent>>> _baseDamageLightningValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageMaxValueComponent>>> _eventUpdatedDamageMaxValueFilter;
        private readonly EcsFilterInject<Inc<DamageMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageMaxValueComponent>>> _baseDamageMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageMinValueComponent>>> _eventUpdatedDamageMinValueFilter;
        private readonly EcsFilterInject<Inc<DamageMinValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageMinValueComponent>>> _baseDamageMinValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePercentValueComponent>>> _eventUpdatedDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<DamagePercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamagePercentValueComponent>>> _baseDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePhysicalValueComponent>>> _eventUpdatedDamagePhysicalValueFilter;
        private readonly EcsFilterInject<Inc<DamagePhysicalValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamagePhysicalValueComponent>>> _baseDamagePhysicalValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePropertyMaxValueComponent>>> _eventUpdatedDamagePropertyMaxValueFilter;
        private readonly EcsFilterInject<Inc<DamagePropertyMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamagePropertyMaxValueComponent>>> _baseDamagePropertyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePropertyMinValueComponent>>> _eventUpdatedDamagePropertyMinValueFilter;
        private readonly EcsFilterInject<Inc<DamagePropertyMinValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamagePropertyMinValueComponent>>> _baseDamagePropertyMinValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageReflectionPercentValueComponent>>> _eventUpdatedDamageReflectionPercentValueFilter;
        private readonly EcsFilterInject<Inc<DamageReflectionPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageReflectionPercentValueComponent>>> _baseDamageReflectionPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageValueComponent>>> _eventUpdatedDamageValueFilter;
        private readonly EcsFilterInject<Inc<DamageValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DamageValueComponent>>> _baseDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DexterityValueComponent>>> _eventUpdatedDexterityValueFilter;
        private readonly EcsFilterInject<Inc<DexterityValueComponent, EventStartRecalculateAllValues, BaseValueComponent<DexterityValueComponent>>> _baseDexterityValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyMaxValueComponent>>> _eventUpdatedEnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EnergyMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<EnergyMaxValueComponent>>> _baseEnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyValueComponent>>> _eventUpdatedEnergyValueFilter;
        private readonly EcsFilterInject<Inc<EnergyValueComponent, EventStartRecalculateAllValues, BaseValueComponent<EnergyValueComponent>>> _baseEnergyValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EvasionValueComponent>>> _eventUpdatedEvasionValueFilter;
        private readonly EcsFilterInject<Inc<EvasionValueComponent, EventStartRecalculateAllValues, BaseValueComponent<EvasionValueComponent>>> _baseEvasionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ExperienceValueComponent>>> _eventUpdatedExperienceValueFilter;
        private readonly EcsFilterInject<Inc<ExperienceValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ExperienceValueComponent>>> _baseExperienceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ExplosionScaleValueComponent>>> _eventUpdatedExplosionScaleValueFilter;
        private readonly EcsFilterInject<Inc<ExplosionScaleValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ExplosionScaleValueComponent>>> _baseExplosionScaleValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ExtraGoldWhenKillingValueComponent>>> _eventUpdatedExtraGoldWhenKillingValueFilter;
        private readonly EcsFilterInject<Inc<ExtraGoldWhenKillingValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ExtraGoldWhenKillingValueComponent>>> _baseExtraGoldWhenKillingValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPercentPerSecondValueComponent>>> _eventUpdatedHealingPercentPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<HealingPercentPerSecondValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HealingPercentPerSecondValueComponent>>> _baseHealingPercentPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPerHitValueComponent>>> _eventUpdatedHealingPerHitValueFilter;
        private readonly EcsFilterInject<Inc<HealingPerHitValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HealingPerHitValueComponent>>> _baseHealingPerHitValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPerSecondValueComponent>>> _eventUpdatedHealingPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<HealingPerSecondValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HealingPerSecondValueComponent>>> _baseHealingPerSecondValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPotionValueComponent>>> _eventUpdatedHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<HealingPotionValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HealingPotionValueComponent>>> _baseHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointMaxValueComponent>>> _eventUpdatedHitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<HitPointMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HitPointMaxValueComponent>>> _baseHitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointPercentValueComponent>>> _eventUpdatedHitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<HitPointPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HitPointPercentValueComponent>>> _baseHitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointValueComponent>>> _eventUpdatedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<HitPointValueComponent, EventStartRecalculateAllValues, BaseValueComponent<HitPointValueComponent>>> _baseHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<IncomingDamagePercentValueComponent>>> _eventUpdatedIncomingDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<IncomingDamagePercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<IncomingDamagePercentValueComponent>>> _baseIncomingDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<IntelligenceValueComponent>>> _eventUpdatedIntelligenceValueFilter;
        private readonly EcsFilterInject<Inc<IntelligenceValueComponent, EventStartRecalculateAllValues, BaseValueComponent<IntelligenceValueComponent>>> _baseIntelligenceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<LevelValueComponent>>> _eventUpdatedLevelValueFilter;
        private readonly EcsFilterInject<Inc<LevelValueComponent, EventStartRecalculateAllValues, BaseValueComponent<LevelValueComponent>>> _baseLevelValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointMaxValueComponent>>> _eventUpdatedManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<ManaPointMaxValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ManaPointMaxValueComponent>>> _baseManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointRecoveryValueComponent>>> _eventUpdatedManaPointRecoveryValueFilter;
        private readonly EcsFilterInject<Inc<ManaPointRecoveryValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ManaPointRecoveryValueComponent>>> _baseManaPointRecoveryValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointValueComponent>>> _eventUpdatedManaPointValueFilter;
        private readonly EcsFilterInject<Inc<ManaPointValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ManaPointValueComponent>>> _baseManaPointValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<MoveSpeedPercentValueComponent>>> _eventUpdatedMoveSpeedPercentValueFilter;
        private readonly EcsFilterInject<Inc<MoveSpeedPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<MoveSpeedPercentValueComponent>>> _baseMoveSpeedPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<MoveSpeedValueComponent>>> _eventUpdatedMoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<MoveSpeedValueComponent, EventStartRecalculateAllValues, BaseValueComponent<MoveSpeedValueComponent>>> _baseMoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<RecoveryTimeReductionValueComponent>>> _eventUpdatedRecoveryTimeReductionValueFilter;
        private readonly EcsFilterInject<Inc<RecoveryTimeReductionValueComponent, EventStartRecalculateAllValues, BaseValueComponent<RecoveryTimeReductionValueComponent>>> _baseRecoveryTimeReductionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResistanceAllValueComponent>>> _eventUpdatedResistanceAllValueFilter;
        private readonly EcsFilterInject<Inc<ResistanceAllValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ResistanceAllValueComponent>>> _baseResistanceAllValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceCostsReductionValueComponent>>> _eventUpdatedResourceCostsReductionValueFilter;
        private readonly EcsFilterInject<Inc<ResourceCostsReductionValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ResourceCostsReductionValueComponent>>> _baseResourceCostsReductionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerDamageTakenValueComponent>>> _eventUpdatedResourceRecoveryPerDamageTakenValueFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerDamageTakenValueComponent, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerDamageTakenValueComponent>>> _baseResourceRecoveryPerDamageTakenValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<SlowdownAnimationValueComponent>>> _eventUpdatedSlowdownAnimationValueFilter;
        private readonly EcsFilterInject<Inc<SlowdownAnimationValueComponent, EventStartRecalculateAllValues, BaseValueComponent<SlowdownAnimationValueComponent>>> _baseSlowdownAnimationValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<SlowdownMoveValueComponent>>> _eventUpdatedSlowdownMoveValueFilter;
        private readonly EcsFilterInject<Inc<SlowdownMoveValueComponent, EventStartRecalculateAllValues, BaseValueComponent<SlowdownMoveValueComponent>>> _baseSlowdownMoveValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<StrengthValueComponent>>> _eventUpdatedStrengthValueFilter;
        private readonly EcsFilterInject<Inc<StrengthValueComponent, EventStartRecalculateAllValues, BaseValueComponent<StrengthValueComponent>>> _baseStrengthValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<VitalityPercentValueComponent>>> _eventUpdatedVitalityPercentValueFilter;
        private readonly EcsFilterInject<Inc<VitalityPercentValueComponent, EventStartRecalculateAllValues, BaseValueComponent<VitalityPercentValueComponent>>> _baseVitalityPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<VitalityValueComponent>>> _eventUpdatedVitalityValueFilter;
        private readonly EcsFilterInject<Inc<VitalityValueComponent, EventStartRecalculateAllValues, BaseValueComponent<VitalityValueComponent>>> _baseVitalityValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<VulnerabilityCriticalChanceValueComponent>>> _eventUpdatedVulnerabilityCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<VulnerabilityCriticalChanceValueComponent, EventStartRecalculateAllValues, BaseValueComponent<VulnerabilityCriticalChanceValueComponent>>> _baseVulnerabilityCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<WeaponAttackSpeedValueComponent>>> _eventUpdatedWeaponAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<WeaponAttackSpeedValueComponent, EventStartRecalculateAllValues, BaseValueComponent<WeaponAttackSpeedValueComponent>>> _baseWeaponAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton1Component>>>> _eventUpdatedChargeCostValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkButton1Component>>>> _baseChargeCostValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton2Component>>>> _eventUpdatedChargeCostValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkButton2Component>>>> _baseChargeCostValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton3Component>>>> _eventUpdatedChargeCostValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkButton3Component>>>> _baseChargeCostValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton4Component>>>> _eventUpdatedChargeCostValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkButton4Component>>>> _baseChargeCostValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedChargeCostValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkForwardFComponent>>>> _baseChargeCostValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedChargeCostValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkMouseLeftComponent>>>> _baseChargeCostValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedChargeCostValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkMouseRightComponent>>>> _baseChargeCostValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedChargeCostValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkSpaceComponent>>>> _baseChargeCostValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedChargeCostValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>>>> _baseChargeCostValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeCostValueComponent<HealingPotionValueComponent>>>> _eventUpdatedChargeCostValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<ChargeCostValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeCostValueComponent<HealingPotionValueComponent>>>> _baseChargeCostValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton1Component>>>> _eventUpdatedChargeMaxValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton1Component>>>> _baseChargeMaxValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton2Component>>>> _eventUpdatedChargeMaxValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton2Component>>>> _baseChargeMaxValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton3Component>>>> _eventUpdatedChargeMaxValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton3Component>>>> _baseChargeMaxValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton4Component>>>> _eventUpdatedChargeMaxValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton4Component>>>> _baseChargeMaxValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedChargeMaxValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkForwardFComponent>>>> _baseChargeMaxValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedChargeMaxValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>>>> _baseChargeMaxValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedChargeMaxValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkMouseRightComponent>>>> _baseChargeMaxValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedChargeMaxValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkSpaceComponent>>>> _baseChargeMaxValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedChargeMaxValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>>>> _baseChargeMaxValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeMaxValueComponent<HealingPotionValueComponent>>>> _eventUpdatedChargeMaxValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<ChargeMaxValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeMaxValueComponent<HealingPotionValueComponent>>>> _baseChargeMaxValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkButton1Component>>>> _eventUpdatedChargeValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkButton1Component>>>> _baseChargeValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkButton2Component>>>> _eventUpdatedChargeValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkButton2Component>>>> _baseChargeValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkButton3Component>>>> _eventUpdatedChargeValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkButton3Component>>>> _baseChargeValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkButton4Component>>>> _eventUpdatedChargeValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkButton4Component>>>> _baseChargeValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedChargeValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkForwardFComponent>>>> _baseChargeValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedChargeValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkMouseLeftComponent>>>> _baseChargeValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedChargeValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkMouseRightComponent>>>> _baseChargeValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedChargeValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkSpaceComponent>>>> _baseChargeValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedChargeValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<ActionLinkSpaceForwardComponent>>>> _baseChargeValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ChargeValueComponent<HealingPotionValueComponent>>>> _eventUpdatedChargeValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<ChargeValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<ChargeValueComponent<HealingPotionValueComponent>>>> _baseChargeValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkButton1Component>>>> _eventUpdatedCooldownValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkButton1Component>>>> _baseCooldownValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkButton2Component>>>> _eventUpdatedCooldownValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkButton2Component>>>> _baseCooldownValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkButton3Component>>>> _eventUpdatedCooldownValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkButton3Component>>>> _baseCooldownValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkButton4Component>>>> _eventUpdatedCooldownValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkButton4Component>>>> _baseCooldownValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedCooldownValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkForwardFComponent>>>> _baseCooldownValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedCooldownValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkMouseLeftComponent>>>> _baseCooldownValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedCooldownValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkMouseRightComponent>>>> _baseCooldownValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedCooldownValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkSpaceComponent>>>> _baseCooldownValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedCooldownValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<ActionLinkSpaceForwardComponent>>>> _baseCooldownValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CooldownValueComponent<HealingPotionValueComponent>>>> _eventUpdatedCooldownValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<CooldownValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<CooldownValueComponent<HealingPotionValueComponent>>>> _baseCooldownValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkButton1Component>>>> _eventUpdatedCostValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkButton1Component>>>> _baseCostValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkButton2Component>>>> _eventUpdatedCostValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkButton2Component>>>> _baseCostValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkButton3Component>>>> _eventUpdatedCostValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkButton3Component>>>> _baseCostValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkButton4Component>>>> _eventUpdatedCostValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkButton4Component>>>> _baseCostValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedCostValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkForwardFComponent>>>> _baseCostValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedCostValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkMouseLeftComponent>>>> _baseCostValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedCostValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkMouseRightComponent>>>> _baseCostValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedCostValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkSpaceComponent>>>> _baseCostValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CostValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedCostValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<CostValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<CostValueComponent<ActionLinkSpaceForwardComponent>>>> _baseCostValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>>>> _baseResourceRecoveryPerHitValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>>>> _baseResourceRecoveryPerHitValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>>>> _baseResourceRecoveryPerHitValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>>>> _baseResourceRecoveryPerHitValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>>>> _baseResourceRecoveryPerHitValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>>>> _baseResourceRecoveryPerHitValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>>>> _baseResourceRecoveryPerHitValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>>>> _baseResourceRecoveryPerHitValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>>>> _baseResourceRecoveryPerHitValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>>>> _eventUpdatedResourceRecoveryPerHitValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>>>> _baseResourceRecoveryPerHitValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>>>> _baseResourceRecoveryPerUsingValueActionLinkButton1Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>>>> _baseResourceRecoveryPerUsingValueActionLinkButton2Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>>>> _baseResourceRecoveryPerUsingValueActionLinkButton3Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>>>> _baseResourceRecoveryPerUsingValueActionLinkButton4Filter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>>>> _baseResourceRecoveryPerUsingValueActionLinkForwardFFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>>>> _baseResourceRecoveryPerUsingValueActionLinkMouseLeftFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>>>> _baseResourceRecoveryPerUsingValueActionLinkMouseRightFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>>>> _baseResourceRecoveryPerUsingValueActionLinkSpaceFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>>>> _baseResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>>>> _eventUpdatedResourceRecoveryPerUsingValueHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>, EventStartRecalculateAllValues, BaseValueComponent<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>>>> _baseResourceRecoveryPerUsingValueHealingPotionValueFilter;
                
        private readonly EcsPoolInject<ActionCostPerSecondValueComponent> _ActionCostPerSecondValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ActionCostPerSecondValueComponent>> _baseActionCostPerSecondValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ActionCostPerSecondValueComponent>> _eventUpdatedActionCostPerSecondValuePool;
        private readonly EcsPoolInject<AddScoreOnDeathValueComponent> _AddScoreOnDeathValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AddScoreOnDeathValueComponent>> _baseAddScoreOnDeathValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AddScoreOnDeathValueComponent>> _eventUpdatedAddScoreOnDeathValuePool;
        private readonly EcsPoolInject<ArmorPercentValueComponent> _ArmorPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ArmorPercentValueComponent>> _baseArmorPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ArmorPercentValueComponent>> _eventUpdatedArmorPercentValuePool;
        private readonly EcsPoolInject<ArmorPropertyValueComponent> _ArmorPropertyValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ArmorPropertyValueComponent>> _baseArmorPropertyValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ArmorPropertyValueComponent>> _eventUpdatedArmorPropertyValuePool;
        private readonly EcsPoolInject<ArmorValueComponent> _ArmorValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ArmorValueComponent>> _baseArmorValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ArmorValueComponent>> _eventUpdatedArmorValuePool;
        private readonly EcsPoolInject<AttackSpeedPercentValueComponent> _AttackSpeedPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AttackSpeedPercentValueComponent>> _baseAttackSpeedPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AttackSpeedPercentValueComponent>> _eventUpdatedAttackSpeedPercentValuePool;
        private readonly EcsPoolInject<AttackSpeedValueComponent> _AttackSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AttackSpeedValueComponent>> _baseAttackSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AttackSpeedValueComponent>> _eventUpdatedAttackSpeedValuePool;
        private readonly EcsPoolInject<BarbDamageBashValueComponent> _BarbDamageBashValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageBashValueComponent>> _baseBarbDamageBashValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageBashValueComponent>> _eventUpdatedBarbDamageBashValuePool;
        private readonly EcsPoolInject<BarbDamageCleaveValueComponent> _BarbDamageCleaveValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageCleaveValueComponent>> _baseBarbDamageCleaveValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageCleaveValueComponent>> _eventUpdatedBarbDamageCleaveValuePool;
        private readonly EcsPoolInject<BarbDamageFrenzyValueComponent> _BarbDamageFrenzyValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageFrenzyValueComponent>> _baseBarbDamageFrenzyValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageFrenzyValueComponent>> _eventUpdatedBarbDamageFrenzyValuePool;
        private readonly EcsPoolInject<BarbDamageHammerOfTheAncientsValueComponent> _BarbDamageHammerOfTheAncientsValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageHammerOfTheAncientsValueComponent>> _baseBarbDamageHammerOfTheAncientsValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageHammerOfTheAncientsValueComponent>> _eventUpdatedBarbDamageHammerOfTheAncientsValuePool;
        private readonly EcsPoolInject<BarbDamageOverPowerValueComponent> _BarbDamageOverPowerValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageOverPowerValueComponent>> _baseBarbDamageOverPowerValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageOverPowerValueComponent>> _eventUpdatedBarbDamageOverPowerValuePool;
        private readonly EcsPoolInject<BarbDamageRendValueComponent> _BarbDamageRendValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageRendValueComponent>> _baseBarbDamageRendValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageRendValueComponent>> _eventUpdatedBarbDamageRendValuePool;
        private readonly EcsPoolInject<BarbDamageRevengeValueComponent> _BarbDamageRevengeValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageRevengeValueComponent>> _baseBarbDamageRevengeValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageRevengeValueComponent>> _eventUpdatedBarbDamageRevengeValuePool;
        private readonly EcsPoolInject<BarbDamageSeismicSlamValueComponent> _BarbDamageSeismicSlamValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageSeismicSlamValueComponent>> _baseBarbDamageSeismicSlamValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageSeismicSlamValueComponent>> _eventUpdatedBarbDamageSeismicSlamValuePool;
        private readonly EcsPoolInject<BarbDamageWeaponThrowValueComponent> _BarbDamageWeaponThrowValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageWeaponThrowValueComponent>> _baseBarbDamageWeaponThrowValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageWeaponThrowValueComponent>> _eventUpdatedBarbDamageWeaponThrowValuePool;
        private readonly EcsPoolInject<BarbDamageWhirlwindValueComponent> _BarbDamageWhirlwindValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbDamageWhirlwindValueComponent>> _baseBarbDamageWhirlwindValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbDamageWhirlwindValueComponent>> _eventUpdatedBarbDamageWhirlwindValuePool;
        private readonly EcsPoolInject<BarbFrenzyStackValueComponent> _BarbFrenzyStackValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BarbFrenzyStackValueComponent>> _baseBarbFrenzyStackValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BarbFrenzyStackValueComponent>> _eventUpdatedBarbFrenzyStackValuePool;
        private readonly EcsPoolInject<BlockAmountMaxValueComponent> _BlockAmountMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BlockAmountMaxValueComponent>> _baseBlockAmountMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BlockAmountMaxValueComponent>> _eventUpdatedBlockAmountMaxValuePool;
        private readonly EcsPoolInject<BlockAmountMinValueComponent> _BlockAmountMinValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BlockAmountMinValueComponent>> _baseBlockAmountMinValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BlockAmountMinValueComponent>> _eventUpdatedBlockAmountMinValuePool;
        private readonly EcsPoolInject<BlockChanceValueComponent> _BlockChanceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<BlockChanceValueComponent>> _baseBlockChanceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<BlockChanceValueComponent>> _eventUpdatedBlockChanceValuePool;
        private readonly EcsPoolInject<CriticalChanceValueComponent> _CriticalChanceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalChanceValueComponent>> _baseCriticalChanceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<CriticalChanceValueComponent>> _eventUpdatedCriticalChanceValuePool;
        private readonly EcsPoolInject<CriticalDamageValueComponent> _CriticalDamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalDamageValueComponent>> _baseCriticalDamageValuePool;
        private readonly EcsPoolInject<EventValueUpdated<CriticalDamageValueComponent>> _eventUpdatedCriticalDamageValuePool;
        private readonly EcsPoolInject<DamageColdValueComponent> _DamageColdValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageColdValueComponent>> _baseDamageColdValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageColdValueComponent>> _eventUpdatedDamageColdValuePool;
        private readonly EcsPoolInject<DamageFireValueComponent> _DamageFireValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageFireValueComponent>> _baseDamageFireValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageFireValueComponent>> _eventUpdatedDamageFireValuePool;
        private readonly EcsPoolInject<DamageLightningValueComponent> _DamageLightningValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageLightningValueComponent>> _baseDamageLightningValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageLightningValueComponent>> _eventUpdatedDamageLightningValuePool;
        private readonly EcsPoolInject<DamageMaxValueComponent> _DamageMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageMaxValueComponent>> _baseDamageMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageMaxValueComponent>> _eventUpdatedDamageMaxValuePool;
        private readonly EcsPoolInject<DamageMinValueComponent> _DamageMinValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageMinValueComponent>> _baseDamageMinValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageMinValueComponent>> _eventUpdatedDamageMinValuePool;
        private readonly EcsPoolInject<DamagePercentValueComponent> _DamagePercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePercentValueComponent>> _baseDamagePercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamagePercentValueComponent>> _eventUpdatedDamagePercentValuePool;
        private readonly EcsPoolInject<DamagePhysicalValueComponent> _DamagePhysicalValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePhysicalValueComponent>> _baseDamagePhysicalValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamagePhysicalValueComponent>> _eventUpdatedDamagePhysicalValuePool;
        private readonly EcsPoolInject<DamagePropertyMaxValueComponent> _DamagePropertyMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePropertyMaxValueComponent>> _baseDamagePropertyMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamagePropertyMaxValueComponent>> _eventUpdatedDamagePropertyMaxValuePool;
        private readonly EcsPoolInject<DamagePropertyMinValueComponent> _DamagePropertyMinValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePropertyMinValueComponent>> _baseDamagePropertyMinValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamagePropertyMinValueComponent>> _eventUpdatedDamagePropertyMinValuePool;
        private readonly EcsPoolInject<DamageReflectionPercentValueComponent> _DamageReflectionPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageReflectionPercentValueComponent>> _baseDamageReflectionPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageReflectionPercentValueComponent>> _eventUpdatedDamageReflectionPercentValuePool;
        private readonly EcsPoolInject<DamageValueComponent> _DamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageValueComponent>> _baseDamageValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageValueComponent>> _eventUpdatedDamageValuePool;
        private readonly EcsPoolInject<DexterityValueComponent> _DexterityValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DexterityValueComponent>> _baseDexterityValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DexterityValueComponent>> _eventUpdatedDexterityValuePool;
        private readonly EcsPoolInject<EnergyMaxValueComponent> _EnergyMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyMaxValueComponent>> _baseEnergyMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<EnergyMaxValueComponent>> _eventUpdatedEnergyMaxValuePool;
        private readonly EcsPoolInject<EnergyValueComponent> _EnergyValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyValueComponent>> _baseEnergyValuePool;
        private readonly EcsPoolInject<EventValueUpdated<EnergyValueComponent>> _eventUpdatedEnergyValuePool;
        private readonly EcsPoolInject<EvasionValueComponent> _EvasionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EvasionValueComponent>> _baseEvasionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<EvasionValueComponent>> _eventUpdatedEvasionValuePool;
        private readonly EcsPoolInject<ExperienceValueComponent> _ExperienceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ExperienceValueComponent>> _baseExperienceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ExperienceValueComponent>> _eventUpdatedExperienceValuePool;
        private readonly EcsPoolInject<ExplosionScaleValueComponent> _ExplosionScaleValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ExplosionScaleValueComponent>> _baseExplosionScaleValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ExplosionScaleValueComponent>> _eventUpdatedExplosionScaleValuePool;
        private readonly EcsPoolInject<ExtraGoldWhenKillingValueComponent> _ExtraGoldWhenKillingValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ExtraGoldWhenKillingValueComponent>> _baseExtraGoldWhenKillingValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ExtraGoldWhenKillingValueComponent>> _eventUpdatedExtraGoldWhenKillingValuePool;
        private readonly EcsPoolInject<HealingPercentPerSecondValueComponent> _HealingPercentPerSecondValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPercentPerSecondValueComponent>> _baseHealingPercentPerSecondValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPercentPerSecondValueComponent>> _eventUpdatedHealingPercentPerSecondValuePool;
        private readonly EcsPoolInject<HealingPerHitValueComponent> _HealingPerHitValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPerHitValueComponent>> _baseHealingPerHitValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPerHitValueComponent>> _eventUpdatedHealingPerHitValuePool;
        private readonly EcsPoolInject<HealingPerSecondValueComponent> _HealingPerSecondValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPerSecondValueComponent>> _baseHealingPerSecondValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPerSecondValueComponent>> _eventUpdatedHealingPerSecondValuePool;
        private readonly EcsPoolInject<HealingPotionValueComponent> _HealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPotionValueComponent>> _baseHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPotionValueComponent>> _eventUpdatedHealingPotionValuePool;
        private readonly EcsPoolInject<HitPointMaxValueComponent> _HitPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointMaxValueComponent>> _baseHitPointMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointMaxValueComponent>> _eventUpdatedHitPointMaxValuePool;
        private readonly EcsPoolInject<HitPointPercentValueComponent> _HitPointPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointPercentValueComponent>> _baseHitPointPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointPercentValueComponent>> _eventUpdatedHitPointPercentValuePool;
        private readonly EcsPoolInject<HitPointValueComponent> _HitPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointValueComponent>> _baseHitPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointValueComponent>> _eventUpdatedHitPointValuePool;
        private readonly EcsPoolInject<IncomingDamagePercentValueComponent> _IncomingDamagePercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<IncomingDamagePercentValueComponent>> _baseIncomingDamagePercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<IncomingDamagePercentValueComponent>> _eventUpdatedIncomingDamagePercentValuePool;
        private readonly EcsPoolInject<IntelligenceValueComponent> _IntelligenceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<IntelligenceValueComponent>> _baseIntelligenceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<IntelligenceValueComponent>> _eventUpdatedIntelligenceValuePool;
        private readonly EcsPoolInject<LevelValueComponent> _LevelValuePool;
        private readonly EcsPoolInject<BaseValueComponent<LevelValueComponent>> _baseLevelValuePool;
        private readonly EcsPoolInject<EventValueUpdated<LevelValueComponent>> _eventUpdatedLevelValuePool;
        private readonly EcsPoolInject<ManaPointMaxValueComponent> _ManaPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointMaxValueComponent>> _baseManaPointMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ManaPointMaxValueComponent>> _eventUpdatedManaPointMaxValuePool;
        private readonly EcsPoolInject<ManaPointRecoveryValueComponent> _ManaPointRecoveryValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointRecoveryValueComponent>> _baseManaPointRecoveryValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ManaPointRecoveryValueComponent>> _eventUpdatedManaPointRecoveryValuePool;
        private readonly EcsPoolInject<ManaPointValueComponent> _ManaPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointValueComponent>> _baseManaPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ManaPointValueComponent>> _eventUpdatedManaPointValuePool;
        private readonly EcsPoolInject<MoveSpeedPercentValueComponent> _MoveSpeedPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<MoveSpeedPercentValueComponent>> _baseMoveSpeedPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<MoveSpeedPercentValueComponent>> _eventUpdatedMoveSpeedPercentValuePool;
        private readonly EcsPoolInject<MoveSpeedValueComponent> _MoveSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<MoveSpeedValueComponent>> _baseMoveSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<MoveSpeedValueComponent>> _eventUpdatedMoveSpeedValuePool;
        private readonly EcsPoolInject<RecoveryTimeReductionValueComponent> _RecoveryTimeReductionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<RecoveryTimeReductionValueComponent>> _baseRecoveryTimeReductionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<RecoveryTimeReductionValueComponent>> _eventUpdatedRecoveryTimeReductionValuePool;
        private readonly EcsPoolInject<ResistanceAllValueComponent> _ResistanceAllValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ResistanceAllValueComponent>> _baseResistanceAllValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ResistanceAllValueComponent>> _eventUpdatedResistanceAllValuePool;
        private readonly EcsPoolInject<ResourceCostsReductionValueComponent> _ResourceCostsReductionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceCostsReductionValueComponent>> _baseResourceCostsReductionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceCostsReductionValueComponent>> _eventUpdatedResourceCostsReductionValuePool;
        private readonly EcsPoolInject<ResourceRecoveryPerDamageTakenValueComponent> _ResourceRecoveryPerDamageTakenValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerDamageTakenValueComponent>> _baseResourceRecoveryPerDamageTakenValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerDamageTakenValueComponent>> _eventUpdatedResourceRecoveryPerDamageTakenValuePool;
        private readonly EcsPoolInject<SlowdownAnimationValueComponent> _SlowdownAnimationValuePool;
        private readonly EcsPoolInject<BaseValueComponent<SlowdownAnimationValueComponent>> _baseSlowdownAnimationValuePool;
        private readonly EcsPoolInject<EventValueUpdated<SlowdownAnimationValueComponent>> _eventUpdatedSlowdownAnimationValuePool;
        private readonly EcsPoolInject<SlowdownMoveValueComponent> _SlowdownMoveValuePool;
        private readonly EcsPoolInject<BaseValueComponent<SlowdownMoveValueComponent>> _baseSlowdownMoveValuePool;
        private readonly EcsPoolInject<EventValueUpdated<SlowdownMoveValueComponent>> _eventUpdatedSlowdownMoveValuePool;
        private readonly EcsPoolInject<StrengthValueComponent> _StrengthValuePool;
        private readonly EcsPoolInject<BaseValueComponent<StrengthValueComponent>> _baseStrengthValuePool;
        private readonly EcsPoolInject<EventValueUpdated<StrengthValueComponent>> _eventUpdatedStrengthValuePool;
        private readonly EcsPoolInject<VitalityPercentValueComponent> _VitalityPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<VitalityPercentValueComponent>> _baseVitalityPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<VitalityPercentValueComponent>> _eventUpdatedVitalityPercentValuePool;
        private readonly EcsPoolInject<VitalityValueComponent> _VitalityValuePool;
        private readonly EcsPoolInject<BaseValueComponent<VitalityValueComponent>> _baseVitalityValuePool;
        private readonly EcsPoolInject<EventValueUpdated<VitalityValueComponent>> _eventUpdatedVitalityValuePool;
        private readonly EcsPoolInject<VulnerabilityCriticalChanceValueComponent> _VulnerabilityCriticalChanceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<VulnerabilityCriticalChanceValueComponent>> _baseVulnerabilityCriticalChanceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<VulnerabilityCriticalChanceValueComponent>> _eventUpdatedVulnerabilityCriticalChanceValuePool;
        private readonly EcsPoolInject<WeaponAttackSpeedValueComponent> _WeaponAttackSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<WeaponAttackSpeedValueComponent>> _baseWeaponAttackSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<WeaponAttackSpeedValueComponent>> _eventUpdatedWeaponAttackSpeedValuePool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkButton1Component>> _ChargeCostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkButton1Component>>> _baseChargeCostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton1Component>>> _eventUpdatedChargeCostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkButton2Component>> _ChargeCostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkButton2Component>>> _baseChargeCostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton2Component>>> _eventUpdatedChargeCostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkButton3Component>> _ChargeCostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkButton3Component>>> _baseChargeCostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton3Component>>> _eventUpdatedChargeCostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkButton4Component>> _ChargeCostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkButton4Component>>> _baseChargeCostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkButton4Component>>> _eventUpdatedChargeCostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkForwardFComponent>> _ChargeCostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkForwardFComponent>>> _baseChargeCostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedChargeCostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkMouseLeftComponent>> _ChargeCostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkMouseLeftComponent>>> _baseChargeCostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedChargeCostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkMouseRightComponent>> _ChargeCostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkMouseRightComponent>>> _baseChargeCostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedChargeCostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkSpaceComponent>> _ChargeCostValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkSpaceComponent>>> _baseChargeCostValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedChargeCostValueActionLinkSpacePool;
        private readonly EcsPoolInject<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>> _ChargeCostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>>> _baseChargeCostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedChargeCostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ChargeCostValueComponent<HealingPotionValueComponent>> _ChargeCostValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeCostValueComponent<HealingPotionValueComponent>>> _baseChargeCostValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeCostValueComponent<HealingPotionValueComponent>>> _eventUpdatedChargeCostValueHealingPotionValuePool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkButton1Component>> _ChargeMaxValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton1Component>>> _baseChargeMaxValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton1Component>>> _eventUpdatedChargeMaxValueActionLinkButton1Pool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkButton2Component>> _ChargeMaxValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton2Component>>> _baseChargeMaxValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton2Component>>> _eventUpdatedChargeMaxValueActionLinkButton2Pool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkButton3Component>> _ChargeMaxValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton3Component>>> _baseChargeMaxValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton3Component>>> _eventUpdatedChargeMaxValueActionLinkButton3Pool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkButton4Component>> _ChargeMaxValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkButton4Component>>> _baseChargeMaxValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkButton4Component>>> _eventUpdatedChargeMaxValueActionLinkButton4Pool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkForwardFComponent>> _ChargeMaxValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkForwardFComponent>>> _baseChargeMaxValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedChargeMaxValueActionLinkForwardFPool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>> _ChargeMaxValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>>> _baseChargeMaxValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedChargeMaxValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkMouseRightComponent>> _ChargeMaxValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkMouseRightComponent>>> _baseChargeMaxValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedChargeMaxValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkSpaceComponent>> _ChargeMaxValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkSpaceComponent>>> _baseChargeMaxValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedChargeMaxValueActionLinkSpacePool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>> _ChargeMaxValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>>> _baseChargeMaxValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedChargeMaxValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<HealingPotionValueComponent>> _ChargeMaxValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeMaxValueComponent<HealingPotionValueComponent>>> _baseChargeMaxValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeMaxValueComponent<HealingPotionValueComponent>>> _eventUpdatedChargeMaxValueHealingPotionValuePool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkButton1Component>> _ChargeValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkButton1Component>>> _baseChargeValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkButton1Component>>> _eventUpdatedChargeValueActionLinkButton1Pool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkButton2Component>> _ChargeValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkButton2Component>>> _baseChargeValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkButton2Component>>> _eventUpdatedChargeValueActionLinkButton2Pool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkButton3Component>> _ChargeValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkButton3Component>>> _baseChargeValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkButton3Component>>> _eventUpdatedChargeValueActionLinkButton3Pool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkButton4Component>> _ChargeValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkButton4Component>>> _baseChargeValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkButton4Component>>> _eventUpdatedChargeValueActionLinkButton4Pool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkForwardFComponent>> _ChargeValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkForwardFComponent>>> _baseChargeValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedChargeValueActionLinkForwardFPool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkMouseLeftComponent>> _ChargeValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkMouseLeftComponent>>> _baseChargeValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedChargeValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkMouseRightComponent>> _ChargeValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkMouseRightComponent>>> _baseChargeValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedChargeValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkSpaceComponent>> _ChargeValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkSpaceComponent>>> _baseChargeValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedChargeValueActionLinkSpacePool;
        private readonly EcsPoolInject<ChargeValueComponent<ActionLinkSpaceForwardComponent>> _ChargeValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<ActionLinkSpaceForwardComponent>>> _baseChargeValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedChargeValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ChargeValueComponent<HealingPotionValueComponent>> _ChargeValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ChargeValueComponent<HealingPotionValueComponent>>> _baseChargeValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<HealingPotionValueComponent>>> _eventUpdatedChargeValueHealingPotionValuePool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkButton1Component>> _CooldownValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkButton1Component>>> _baseCooldownValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkButton1Component>>> _eventUpdatedCooldownValueActionLinkButton1Pool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkButton2Component>> _CooldownValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkButton2Component>>> _baseCooldownValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkButton2Component>>> _eventUpdatedCooldownValueActionLinkButton2Pool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkButton3Component>> _CooldownValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkButton3Component>>> _baseCooldownValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkButton3Component>>> _eventUpdatedCooldownValueActionLinkButton3Pool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkButton4Component>> _CooldownValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkButton4Component>>> _baseCooldownValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkButton4Component>>> _eventUpdatedCooldownValueActionLinkButton4Pool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkForwardFComponent>> _CooldownValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkForwardFComponent>>> _baseCooldownValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedCooldownValueActionLinkForwardFPool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkMouseLeftComponent>> _CooldownValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkMouseLeftComponent>>> _baseCooldownValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedCooldownValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkMouseRightComponent>> _CooldownValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkMouseRightComponent>>> _baseCooldownValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedCooldownValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkSpaceComponent>> _CooldownValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkSpaceComponent>>> _baseCooldownValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedCooldownValueActionLinkSpacePool;
        private readonly EcsPoolInject<CooldownValueComponent<ActionLinkSpaceForwardComponent>> _CooldownValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<ActionLinkSpaceForwardComponent>>> _baseCooldownValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedCooldownValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<CooldownValueComponent<HealingPotionValueComponent>> _CooldownValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CooldownValueComponent<HealingPotionValueComponent>>> _baseCooldownValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<HealingPotionValueComponent>>> _eventUpdatedCooldownValueHealingPotionValuePool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkButton1Component>> _CostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkButton1Component>>> _baseCostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkButton1Component>>> _eventUpdatedCostValueActionLinkButton1Pool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkButton2Component>> _CostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkButton2Component>>> _baseCostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkButton2Component>>> _eventUpdatedCostValueActionLinkButton2Pool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkButton3Component>> _CostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkButton3Component>>> _baseCostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkButton3Component>>> _eventUpdatedCostValueActionLinkButton3Pool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkButton4Component>> _CostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkButton4Component>>> _baseCostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkButton4Component>>> _eventUpdatedCostValueActionLinkButton4Pool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkForwardFComponent>> _CostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkForwardFComponent>>> _baseCostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedCostValueActionLinkForwardFPool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkMouseLeftComponent>> _CostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkMouseLeftComponent>>> _baseCostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedCostValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkMouseRightComponent>> _CostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkMouseRightComponent>>> _baseCostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedCostValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkSpaceComponent>> _CostValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkSpaceComponent>>> _baseCostValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedCostValueActionLinkSpacePool;
        private readonly EcsPoolInject<CostValueComponent<ActionLinkSpaceForwardComponent>> _CostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<CostValueComponent<ActionLinkSpaceForwardComponent>>> _baseCostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<CostValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedCostValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>> _ResourceRecoveryPerHitValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>>> _baseResourceRecoveryPerHitValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>> _ResourceRecoveryPerHitValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>>> _baseResourceRecoveryPerHitValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>> _ResourceRecoveryPerHitValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>>> _baseResourceRecoveryPerHitValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>> _ResourceRecoveryPerHitValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>>> _baseResourceRecoveryPerHitValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>> _ResourceRecoveryPerHitValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>>> _baseResourceRecoveryPerHitValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFPool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>> _ResourceRecoveryPerHitValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>>> _baseResourceRecoveryPerHitValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>> _ResourceRecoveryPerHitValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>>> _baseResourceRecoveryPerHitValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>> _ResourceRecoveryPerHitValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>>> _baseResourceRecoveryPerHitValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkSpacePool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>> _ResourceRecoveryPerHitValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>>> _baseResourceRecoveryPerHitValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>> _ResourceRecoveryPerHitValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>>> _baseResourceRecoveryPerHitValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>>> _eventUpdatedResourceRecoveryPerHitValueHealingPotionValuePool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>> _ResourceRecoveryPerUsingValueActionLinkButton1Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>>> _baseResourceRecoveryPerUsingValueActionLinkButton1Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>> _ResourceRecoveryPerUsingValueActionLinkButton2Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>>> _baseResourceRecoveryPerUsingValueActionLinkButton2Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>> _ResourceRecoveryPerUsingValueActionLinkButton3Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>>> _baseResourceRecoveryPerUsingValueActionLinkButton3Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>> _ResourceRecoveryPerUsingValueActionLinkButton4Pool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>>> _baseResourceRecoveryPerUsingValueActionLinkButton4Pool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Pool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>> _ResourceRecoveryPerUsingValueActionLinkForwardFPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>>> _baseResourceRecoveryPerUsingValueActionLinkForwardFPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkForwardFComponent>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFPool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>> _ResourceRecoveryPerUsingValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>>> _baseResourceRecoveryPerUsingValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftPool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>> _ResourceRecoveryPerUsingValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>>> _baseResourceRecoveryPerUsingValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightPool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>> _ResourceRecoveryPerUsingValueActionLinkSpacePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>>> _baseResourceRecoveryPerUsingValueActionLinkSpacePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceComponent>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpacePool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>> _ResourceRecoveryPerUsingValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>>> _baseResourceRecoveryPerUsingValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<ActionLinkSpaceForwardComponent>>> _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardPool;
        private readonly EcsPoolInject<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>> _ResourceRecoveryPerUsingValueHealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>>> _baseResourceRecoveryPerUsingValueHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>>> _eventUpdatedResourceRecoveryPerUsingValueHealingPotionValuePool;

        public void Run(IEcsSystems systems)
        {
            if (_eventUpdatedActionCostPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedActionCostPerSecondValueFilter.Value) _eventUpdatedActionCostPerSecondValuePool.Value.Del(i);
            if (_baseActionCostPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseActionCostPerSecondValueFilter.Value) { _ActionCostPerSecondValuePool.Value.Get(i).value = _baseActionCostPerSecondValuePool.Value.Get(i).baseValue; _eventUpdatedActionCostPerSecondValuePool.Value.Add(i); }
            if (_eventUpdatedAddScoreOnDeathValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedAddScoreOnDeathValueFilter.Value) _eventUpdatedAddScoreOnDeathValuePool.Value.Del(i);
            if (_baseAddScoreOnDeathValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseAddScoreOnDeathValueFilter.Value) { _AddScoreOnDeathValuePool.Value.Get(i).value = _baseAddScoreOnDeathValuePool.Value.Get(i).baseValue; _eventUpdatedAddScoreOnDeathValuePool.Value.Add(i); }
            if (_eventUpdatedArmorPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedArmorPercentValueFilter.Value) _eventUpdatedArmorPercentValuePool.Value.Del(i);
            if (_baseArmorPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseArmorPercentValueFilter.Value) { _ArmorPercentValuePool.Value.Get(i).value = _baseArmorPercentValuePool.Value.Get(i).baseValue; _eventUpdatedArmorPercentValuePool.Value.Add(i); }
            if (_eventUpdatedArmorPropertyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedArmorPropertyValueFilter.Value) _eventUpdatedArmorPropertyValuePool.Value.Del(i);
            if (_baseArmorPropertyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseArmorPropertyValueFilter.Value) { _ArmorPropertyValuePool.Value.Get(i).value = _baseArmorPropertyValuePool.Value.Get(i).baseValue; _eventUpdatedArmorPropertyValuePool.Value.Add(i); }
            if (_eventUpdatedArmorValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedArmorValueFilter.Value) _eventUpdatedArmorValuePool.Value.Del(i);
            if (_baseArmorValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseArmorValueFilter.Value) { _ArmorValuePool.Value.Get(i).value = _baseArmorValuePool.Value.Get(i).baseValue; _eventUpdatedArmorValuePool.Value.Add(i); }
            if (_eventUpdatedAttackSpeedPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedAttackSpeedPercentValueFilter.Value) _eventUpdatedAttackSpeedPercentValuePool.Value.Del(i);
            if (_baseAttackSpeedPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseAttackSpeedPercentValueFilter.Value) { _AttackSpeedPercentValuePool.Value.Get(i).value = _baseAttackSpeedPercentValuePool.Value.Get(i).baseValue; _eventUpdatedAttackSpeedPercentValuePool.Value.Add(i); }
            if (_eventUpdatedAttackSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedAttackSpeedValueFilter.Value) _eventUpdatedAttackSpeedValuePool.Value.Del(i);
            if (_baseAttackSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseAttackSpeedValueFilter.Value) { _AttackSpeedValuePool.Value.Get(i).value = _baseAttackSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedAttackSpeedValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageBashValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageBashValueFilter.Value) _eventUpdatedBarbDamageBashValuePool.Value.Del(i);
            if (_baseBarbDamageBashValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageBashValueFilter.Value) { _BarbDamageBashValuePool.Value.Get(i).value = _baseBarbDamageBashValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageBashValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageCleaveValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageCleaveValueFilter.Value) _eventUpdatedBarbDamageCleaveValuePool.Value.Del(i);
            if (_baseBarbDamageCleaveValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageCleaveValueFilter.Value) { _BarbDamageCleaveValuePool.Value.Get(i).value = _baseBarbDamageCleaveValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageCleaveValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageFrenzyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageFrenzyValueFilter.Value) _eventUpdatedBarbDamageFrenzyValuePool.Value.Del(i);
            if (_baseBarbDamageFrenzyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageFrenzyValueFilter.Value) { _BarbDamageFrenzyValuePool.Value.Get(i).value = _baseBarbDamageFrenzyValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageFrenzyValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageHammerOfTheAncientsValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageHammerOfTheAncientsValueFilter.Value) _eventUpdatedBarbDamageHammerOfTheAncientsValuePool.Value.Del(i);
            if (_baseBarbDamageHammerOfTheAncientsValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageHammerOfTheAncientsValueFilter.Value) { _BarbDamageHammerOfTheAncientsValuePool.Value.Get(i).value = _baseBarbDamageHammerOfTheAncientsValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageHammerOfTheAncientsValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageOverPowerValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageOverPowerValueFilter.Value) _eventUpdatedBarbDamageOverPowerValuePool.Value.Del(i);
            if (_baseBarbDamageOverPowerValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageOverPowerValueFilter.Value) { _BarbDamageOverPowerValuePool.Value.Get(i).value = _baseBarbDamageOverPowerValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageOverPowerValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageRendValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageRendValueFilter.Value) _eventUpdatedBarbDamageRendValuePool.Value.Del(i);
            if (_baseBarbDamageRendValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageRendValueFilter.Value) { _BarbDamageRendValuePool.Value.Get(i).value = _baseBarbDamageRendValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageRendValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageRevengeValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageRevengeValueFilter.Value) _eventUpdatedBarbDamageRevengeValuePool.Value.Del(i);
            if (_baseBarbDamageRevengeValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageRevengeValueFilter.Value) { _BarbDamageRevengeValuePool.Value.Get(i).value = _baseBarbDamageRevengeValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageRevengeValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageSeismicSlamValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageSeismicSlamValueFilter.Value) _eventUpdatedBarbDamageSeismicSlamValuePool.Value.Del(i);
            if (_baseBarbDamageSeismicSlamValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageSeismicSlamValueFilter.Value) { _BarbDamageSeismicSlamValuePool.Value.Get(i).value = _baseBarbDamageSeismicSlamValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageSeismicSlamValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageWeaponThrowValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageWeaponThrowValueFilter.Value) _eventUpdatedBarbDamageWeaponThrowValuePool.Value.Del(i);
            if (_baseBarbDamageWeaponThrowValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageWeaponThrowValueFilter.Value) { _BarbDamageWeaponThrowValuePool.Value.Get(i).value = _baseBarbDamageWeaponThrowValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageWeaponThrowValuePool.Value.Add(i); }
            if (_eventUpdatedBarbDamageWhirlwindValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbDamageWhirlwindValueFilter.Value) _eventUpdatedBarbDamageWhirlwindValuePool.Value.Del(i);
            if (_baseBarbDamageWhirlwindValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbDamageWhirlwindValueFilter.Value) { _BarbDamageWhirlwindValuePool.Value.Get(i).value = _baseBarbDamageWhirlwindValuePool.Value.Get(i).baseValue; _eventUpdatedBarbDamageWhirlwindValuePool.Value.Add(i); }
            if (_eventUpdatedBarbFrenzyStackValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBarbFrenzyStackValueFilter.Value) _eventUpdatedBarbFrenzyStackValuePool.Value.Del(i);
            if (_baseBarbFrenzyStackValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBarbFrenzyStackValueFilter.Value) { _BarbFrenzyStackValuePool.Value.Get(i).value = _baseBarbFrenzyStackValuePool.Value.Get(i).baseValue; _eventUpdatedBarbFrenzyStackValuePool.Value.Add(i); }
            if (_eventUpdatedBlockAmountMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBlockAmountMaxValueFilter.Value) _eventUpdatedBlockAmountMaxValuePool.Value.Del(i);
            if (_baseBlockAmountMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBlockAmountMaxValueFilter.Value) { _BlockAmountMaxValuePool.Value.Get(i).value = _baseBlockAmountMaxValuePool.Value.Get(i).baseValue; _eventUpdatedBlockAmountMaxValuePool.Value.Add(i); }
            if (_eventUpdatedBlockAmountMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBlockAmountMinValueFilter.Value) _eventUpdatedBlockAmountMinValuePool.Value.Del(i);
            if (_baseBlockAmountMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBlockAmountMinValueFilter.Value) { _BlockAmountMinValuePool.Value.Get(i).value = _baseBlockAmountMinValuePool.Value.Get(i).baseValue; _eventUpdatedBlockAmountMinValuePool.Value.Add(i); }
            if (_eventUpdatedBlockChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedBlockChanceValueFilter.Value) _eventUpdatedBlockChanceValuePool.Value.Del(i);
            if (_baseBlockChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseBlockChanceValueFilter.Value) { _BlockChanceValuePool.Value.Get(i).value = _baseBlockChanceValuePool.Value.Get(i).baseValue; _eventUpdatedBlockChanceValuePool.Value.Add(i); }
            if (_eventUpdatedCriticalChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCriticalChanceValueFilter.Value) _eventUpdatedCriticalChanceValuePool.Value.Del(i);
            if (_baseCriticalChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCriticalChanceValueFilter.Value) { _CriticalChanceValuePool.Value.Get(i).value = _baseCriticalChanceValuePool.Value.Get(i).baseValue; _eventUpdatedCriticalChanceValuePool.Value.Add(i); }
            if (_eventUpdatedCriticalDamageValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCriticalDamageValueFilter.Value) _eventUpdatedCriticalDamageValuePool.Value.Del(i);
            if (_baseCriticalDamageValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCriticalDamageValueFilter.Value) { _CriticalDamageValuePool.Value.Get(i).value = _baseCriticalDamageValuePool.Value.Get(i).baseValue; _eventUpdatedCriticalDamageValuePool.Value.Add(i); }
            if (_eventUpdatedDamageColdValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageColdValueFilter.Value) _eventUpdatedDamageColdValuePool.Value.Del(i);
            if (_baseDamageColdValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageColdValueFilter.Value) { _DamageColdValuePool.Value.Get(i).value = _baseDamageColdValuePool.Value.Get(i).baseValue; _eventUpdatedDamageColdValuePool.Value.Add(i); }
            if (_eventUpdatedDamageFireValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageFireValueFilter.Value) _eventUpdatedDamageFireValuePool.Value.Del(i);
            if (_baseDamageFireValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageFireValueFilter.Value) { _DamageFireValuePool.Value.Get(i).value = _baseDamageFireValuePool.Value.Get(i).baseValue; _eventUpdatedDamageFireValuePool.Value.Add(i); }
            if (_eventUpdatedDamageLightningValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageLightningValueFilter.Value) _eventUpdatedDamageLightningValuePool.Value.Del(i);
            if (_baseDamageLightningValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageLightningValueFilter.Value) { _DamageLightningValuePool.Value.Get(i).value = _baseDamageLightningValuePool.Value.Get(i).baseValue; _eventUpdatedDamageLightningValuePool.Value.Add(i); }
            if (_eventUpdatedDamageMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageMaxValueFilter.Value) _eventUpdatedDamageMaxValuePool.Value.Del(i);
            if (_baseDamageMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageMaxValueFilter.Value) { _DamageMaxValuePool.Value.Get(i).value = _baseDamageMaxValuePool.Value.Get(i).baseValue; _eventUpdatedDamageMaxValuePool.Value.Add(i); }
            if (_eventUpdatedDamageMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageMinValueFilter.Value) _eventUpdatedDamageMinValuePool.Value.Del(i);
            if (_baseDamageMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageMinValueFilter.Value) { _DamageMinValuePool.Value.Get(i).value = _baseDamageMinValuePool.Value.Get(i).baseValue; _eventUpdatedDamageMinValuePool.Value.Add(i); }
            if (_eventUpdatedDamagePercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamagePercentValueFilter.Value) _eventUpdatedDamagePercentValuePool.Value.Del(i);
            if (_baseDamagePercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamagePercentValueFilter.Value) { _DamagePercentValuePool.Value.Get(i).value = _baseDamagePercentValuePool.Value.Get(i).baseValue; _eventUpdatedDamagePercentValuePool.Value.Add(i); }
            if (_eventUpdatedDamagePhysicalValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamagePhysicalValueFilter.Value) _eventUpdatedDamagePhysicalValuePool.Value.Del(i);
            if (_baseDamagePhysicalValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamagePhysicalValueFilter.Value) { _DamagePhysicalValuePool.Value.Get(i).value = _baseDamagePhysicalValuePool.Value.Get(i).baseValue; _eventUpdatedDamagePhysicalValuePool.Value.Add(i); }
            if (_eventUpdatedDamagePropertyMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamagePropertyMaxValueFilter.Value) _eventUpdatedDamagePropertyMaxValuePool.Value.Del(i);
            if (_baseDamagePropertyMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamagePropertyMaxValueFilter.Value) { _DamagePropertyMaxValuePool.Value.Get(i).value = _baseDamagePropertyMaxValuePool.Value.Get(i).baseValue; _eventUpdatedDamagePropertyMaxValuePool.Value.Add(i); }
            if (_eventUpdatedDamagePropertyMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamagePropertyMinValueFilter.Value) _eventUpdatedDamagePropertyMinValuePool.Value.Del(i);
            if (_baseDamagePropertyMinValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamagePropertyMinValueFilter.Value) { _DamagePropertyMinValuePool.Value.Get(i).value = _baseDamagePropertyMinValuePool.Value.Get(i).baseValue; _eventUpdatedDamagePropertyMinValuePool.Value.Add(i); }
            if (_eventUpdatedDamageReflectionPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageReflectionPercentValueFilter.Value) _eventUpdatedDamageReflectionPercentValuePool.Value.Del(i);
            if (_baseDamageReflectionPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageReflectionPercentValueFilter.Value) { _DamageReflectionPercentValuePool.Value.Get(i).value = _baseDamageReflectionPercentValuePool.Value.Get(i).baseValue; _eventUpdatedDamageReflectionPercentValuePool.Value.Add(i); }
            if (_eventUpdatedDamageValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDamageValueFilter.Value) _eventUpdatedDamageValuePool.Value.Del(i);
            if (_baseDamageValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDamageValueFilter.Value) { _DamageValuePool.Value.Get(i).value = _baseDamageValuePool.Value.Get(i).baseValue; _eventUpdatedDamageValuePool.Value.Add(i); }
            if (_eventUpdatedDexterityValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedDexterityValueFilter.Value) _eventUpdatedDexterityValuePool.Value.Del(i);
            if (_baseDexterityValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseDexterityValueFilter.Value) { _DexterityValuePool.Value.Get(i).value = _baseDexterityValuePool.Value.Get(i).baseValue; _eventUpdatedDexterityValuePool.Value.Add(i); }
            if (_eventUpdatedEnergyMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedEnergyMaxValueFilter.Value) _eventUpdatedEnergyMaxValuePool.Value.Del(i);
            if (_baseEnergyMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseEnergyMaxValueFilter.Value) { _EnergyMaxValuePool.Value.Get(i).value = _baseEnergyMaxValuePool.Value.Get(i).baseValue; _eventUpdatedEnergyMaxValuePool.Value.Add(i); }
            if (_eventUpdatedEnergyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedEnergyValueFilter.Value) _eventUpdatedEnergyValuePool.Value.Del(i);
            if (_baseEnergyValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseEnergyValueFilter.Value) { _EnergyValuePool.Value.Get(i).value = _baseEnergyValuePool.Value.Get(i).baseValue; _eventUpdatedEnergyValuePool.Value.Add(i); }
            if (_eventUpdatedEvasionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedEvasionValueFilter.Value) _eventUpdatedEvasionValuePool.Value.Del(i);
            if (_baseEvasionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseEvasionValueFilter.Value) { _EvasionValuePool.Value.Get(i).value = _baseEvasionValuePool.Value.Get(i).baseValue; _eventUpdatedEvasionValuePool.Value.Add(i); }
            if (_eventUpdatedExperienceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedExperienceValueFilter.Value) _eventUpdatedExperienceValuePool.Value.Del(i);
            if (_baseExperienceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseExperienceValueFilter.Value) { _ExperienceValuePool.Value.Get(i).value = _baseExperienceValuePool.Value.Get(i).baseValue; _eventUpdatedExperienceValuePool.Value.Add(i); }
            if (_eventUpdatedExplosionScaleValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedExplosionScaleValueFilter.Value) _eventUpdatedExplosionScaleValuePool.Value.Del(i);
            if (_baseExplosionScaleValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseExplosionScaleValueFilter.Value) { _ExplosionScaleValuePool.Value.Get(i).value = _baseExplosionScaleValuePool.Value.Get(i).baseValue; _eventUpdatedExplosionScaleValuePool.Value.Add(i); }
            if (_eventUpdatedExtraGoldWhenKillingValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedExtraGoldWhenKillingValueFilter.Value) _eventUpdatedExtraGoldWhenKillingValuePool.Value.Del(i);
            if (_baseExtraGoldWhenKillingValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseExtraGoldWhenKillingValueFilter.Value) { _ExtraGoldWhenKillingValuePool.Value.Get(i).value = _baseExtraGoldWhenKillingValuePool.Value.Get(i).baseValue; _eventUpdatedExtraGoldWhenKillingValuePool.Value.Add(i); }
            if (_eventUpdatedHealingPercentPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHealingPercentPerSecondValueFilter.Value) _eventUpdatedHealingPercentPerSecondValuePool.Value.Del(i);
            if (_baseHealingPercentPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHealingPercentPerSecondValueFilter.Value) { _HealingPercentPerSecondValuePool.Value.Get(i).value = _baseHealingPercentPerSecondValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPercentPerSecondValuePool.Value.Add(i); }
            if (_eventUpdatedHealingPerHitValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHealingPerHitValueFilter.Value) _eventUpdatedHealingPerHitValuePool.Value.Del(i);
            if (_baseHealingPerHitValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHealingPerHitValueFilter.Value) { _HealingPerHitValuePool.Value.Get(i).value = _baseHealingPerHitValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPerHitValuePool.Value.Add(i); }
            if (_eventUpdatedHealingPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHealingPerSecondValueFilter.Value) _eventUpdatedHealingPerSecondValuePool.Value.Del(i);
            if (_baseHealingPerSecondValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHealingPerSecondValueFilter.Value) { _HealingPerSecondValuePool.Value.Get(i).value = _baseHealingPerSecondValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPerSecondValuePool.Value.Add(i); }
            if (_eventUpdatedHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHealingPotionValueFilter.Value) _eventUpdatedHealingPotionValuePool.Value.Del(i);
            if (_baseHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHealingPotionValueFilter.Value) { _HealingPotionValuePool.Value.Get(i).value = _baseHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedHitPointMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHitPointMaxValueFilter.Value) _eventUpdatedHitPointMaxValuePool.Value.Del(i);
            if (_baseHitPointMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHitPointMaxValueFilter.Value) { _HitPointMaxValuePool.Value.Get(i).value = _baseHitPointMaxValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointMaxValuePool.Value.Add(i); }
            if (_eventUpdatedHitPointPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHitPointPercentValueFilter.Value) _eventUpdatedHitPointPercentValuePool.Value.Del(i);
            if (_baseHitPointPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHitPointPercentValueFilter.Value) { _HitPointPercentValuePool.Value.Get(i).value = _baseHitPointPercentValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointPercentValuePool.Value.Add(i); }
            if (_eventUpdatedHitPointValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedHitPointValueFilter.Value) _eventUpdatedHitPointValuePool.Value.Del(i);
            if (_baseHitPointValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseHitPointValueFilter.Value) { _HitPointValuePool.Value.Get(i).value = _baseHitPointValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointValuePool.Value.Add(i); }
            if (_eventUpdatedIncomingDamagePercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedIncomingDamagePercentValueFilter.Value) _eventUpdatedIncomingDamagePercentValuePool.Value.Del(i);
            if (_baseIncomingDamagePercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseIncomingDamagePercentValueFilter.Value) { _IncomingDamagePercentValuePool.Value.Get(i).value = _baseIncomingDamagePercentValuePool.Value.Get(i).baseValue; _eventUpdatedIncomingDamagePercentValuePool.Value.Add(i); }
            if (_eventUpdatedIntelligenceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedIntelligenceValueFilter.Value) _eventUpdatedIntelligenceValuePool.Value.Del(i);
            if (_baseIntelligenceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseIntelligenceValueFilter.Value) { _IntelligenceValuePool.Value.Get(i).value = _baseIntelligenceValuePool.Value.Get(i).baseValue; _eventUpdatedIntelligenceValuePool.Value.Add(i); }
            if (_eventUpdatedLevelValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedLevelValueFilter.Value) _eventUpdatedLevelValuePool.Value.Del(i);
            if (_baseLevelValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseLevelValueFilter.Value) { _LevelValuePool.Value.Get(i).value = _baseLevelValuePool.Value.Get(i).baseValue; _eventUpdatedLevelValuePool.Value.Add(i); }
            if (_eventUpdatedManaPointMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedManaPointMaxValueFilter.Value) _eventUpdatedManaPointMaxValuePool.Value.Del(i);
            if (_baseManaPointMaxValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseManaPointMaxValueFilter.Value) { _ManaPointMaxValuePool.Value.Get(i).value = _baseManaPointMaxValuePool.Value.Get(i).baseValue; _eventUpdatedManaPointMaxValuePool.Value.Add(i); }
            if (_eventUpdatedManaPointRecoveryValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedManaPointRecoveryValueFilter.Value) _eventUpdatedManaPointRecoveryValuePool.Value.Del(i);
            if (_baseManaPointRecoveryValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseManaPointRecoveryValueFilter.Value) { _ManaPointRecoveryValuePool.Value.Get(i).value = _baseManaPointRecoveryValuePool.Value.Get(i).baseValue; _eventUpdatedManaPointRecoveryValuePool.Value.Add(i); }
            if (_eventUpdatedManaPointValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedManaPointValueFilter.Value) _eventUpdatedManaPointValuePool.Value.Del(i);
            if (_baseManaPointValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseManaPointValueFilter.Value) { _ManaPointValuePool.Value.Get(i).value = _baseManaPointValuePool.Value.Get(i).baseValue; _eventUpdatedManaPointValuePool.Value.Add(i); }
            if (_eventUpdatedMoveSpeedPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedMoveSpeedPercentValueFilter.Value) _eventUpdatedMoveSpeedPercentValuePool.Value.Del(i);
            if (_baseMoveSpeedPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseMoveSpeedPercentValueFilter.Value) { _MoveSpeedPercentValuePool.Value.Get(i).value = _baseMoveSpeedPercentValuePool.Value.Get(i).baseValue; _eventUpdatedMoveSpeedPercentValuePool.Value.Add(i); }
            if (_eventUpdatedMoveSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedMoveSpeedValueFilter.Value) _eventUpdatedMoveSpeedValuePool.Value.Del(i);
            if (_baseMoveSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseMoveSpeedValueFilter.Value) { _MoveSpeedValuePool.Value.Get(i).value = _baseMoveSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedMoveSpeedValuePool.Value.Add(i); }
            if (_eventUpdatedRecoveryTimeReductionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedRecoveryTimeReductionValueFilter.Value) _eventUpdatedRecoveryTimeReductionValuePool.Value.Del(i);
            if (_baseRecoveryTimeReductionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseRecoveryTimeReductionValueFilter.Value) { _RecoveryTimeReductionValuePool.Value.Get(i).value = _baseRecoveryTimeReductionValuePool.Value.Get(i).baseValue; _eventUpdatedRecoveryTimeReductionValuePool.Value.Add(i); }
            if (_eventUpdatedResistanceAllValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResistanceAllValueFilter.Value) _eventUpdatedResistanceAllValuePool.Value.Del(i);
            if (_baseResistanceAllValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResistanceAllValueFilter.Value) { _ResistanceAllValuePool.Value.Get(i).value = _baseResistanceAllValuePool.Value.Get(i).baseValue; _eventUpdatedResistanceAllValuePool.Value.Add(i); }
            if (_eventUpdatedResourceCostsReductionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceCostsReductionValueFilter.Value) _eventUpdatedResourceCostsReductionValuePool.Value.Del(i);
            if (_baseResourceCostsReductionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceCostsReductionValueFilter.Value) { _ResourceCostsReductionValuePool.Value.Get(i).value = _baseResourceCostsReductionValuePool.Value.Get(i).baseValue; _eventUpdatedResourceCostsReductionValuePool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerDamageTakenValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerDamageTakenValueFilter.Value) _eventUpdatedResourceRecoveryPerDamageTakenValuePool.Value.Del(i);
            if (_baseResourceRecoveryPerDamageTakenValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerDamageTakenValueFilter.Value) { _ResourceRecoveryPerDamageTakenValuePool.Value.Get(i).value = _baseResourceRecoveryPerDamageTakenValuePool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerDamageTakenValuePool.Value.Add(i); }
            if (_eventUpdatedSlowdownAnimationValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedSlowdownAnimationValueFilter.Value) _eventUpdatedSlowdownAnimationValuePool.Value.Del(i);
            if (_baseSlowdownAnimationValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseSlowdownAnimationValueFilter.Value) { _SlowdownAnimationValuePool.Value.Get(i).value = _baseSlowdownAnimationValuePool.Value.Get(i).baseValue; _eventUpdatedSlowdownAnimationValuePool.Value.Add(i); }
            if (_eventUpdatedSlowdownMoveValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedSlowdownMoveValueFilter.Value) _eventUpdatedSlowdownMoveValuePool.Value.Del(i);
            if (_baseSlowdownMoveValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseSlowdownMoveValueFilter.Value) { _SlowdownMoveValuePool.Value.Get(i).value = _baseSlowdownMoveValuePool.Value.Get(i).baseValue; _eventUpdatedSlowdownMoveValuePool.Value.Add(i); }
            if (_eventUpdatedStrengthValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedStrengthValueFilter.Value) _eventUpdatedStrengthValuePool.Value.Del(i);
            if (_baseStrengthValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseStrengthValueFilter.Value) { _StrengthValuePool.Value.Get(i).value = _baseStrengthValuePool.Value.Get(i).baseValue; _eventUpdatedStrengthValuePool.Value.Add(i); }
            if (_eventUpdatedVitalityPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedVitalityPercentValueFilter.Value) _eventUpdatedVitalityPercentValuePool.Value.Del(i);
            if (_baseVitalityPercentValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseVitalityPercentValueFilter.Value) { _VitalityPercentValuePool.Value.Get(i).value = _baseVitalityPercentValuePool.Value.Get(i).baseValue; _eventUpdatedVitalityPercentValuePool.Value.Add(i); }
            if (_eventUpdatedVitalityValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedVitalityValueFilter.Value) _eventUpdatedVitalityValuePool.Value.Del(i);
            if (_baseVitalityValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseVitalityValueFilter.Value) { _VitalityValuePool.Value.Get(i).value = _baseVitalityValuePool.Value.Get(i).baseValue; _eventUpdatedVitalityValuePool.Value.Add(i); }
            if (_eventUpdatedVulnerabilityCriticalChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedVulnerabilityCriticalChanceValueFilter.Value) _eventUpdatedVulnerabilityCriticalChanceValuePool.Value.Del(i);
            if (_baseVulnerabilityCriticalChanceValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseVulnerabilityCriticalChanceValueFilter.Value) { _VulnerabilityCriticalChanceValuePool.Value.Get(i).value = _baseVulnerabilityCriticalChanceValuePool.Value.Get(i).baseValue; _eventUpdatedVulnerabilityCriticalChanceValuePool.Value.Add(i); }
            if (_eventUpdatedWeaponAttackSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedWeaponAttackSpeedValueFilter.Value) _eventUpdatedWeaponAttackSpeedValuePool.Value.Del(i);
            if (_baseWeaponAttackSpeedValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseWeaponAttackSpeedValueFilter.Value) { _WeaponAttackSpeedValuePool.Value.Get(i).value = _baseWeaponAttackSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedWeaponAttackSpeedValuePool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkButton1Filter.Value) _eventUpdatedChargeCostValueActionLinkButton1Pool.Value.Del(i);
            if (_baseChargeCostValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkButton1Filter.Value) { _ChargeCostValueActionLinkButton1Pool.Value.Get(i).value = _baseChargeCostValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkButton2Filter.Value) _eventUpdatedChargeCostValueActionLinkButton2Pool.Value.Del(i);
            if (_baseChargeCostValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkButton2Filter.Value) { _ChargeCostValueActionLinkButton2Pool.Value.Get(i).value = _baseChargeCostValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkButton3Filter.Value) _eventUpdatedChargeCostValueActionLinkButton3Pool.Value.Del(i);
            if (_baseChargeCostValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkButton3Filter.Value) { _ChargeCostValueActionLinkButton3Pool.Value.Get(i).value = _baseChargeCostValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkButton4Filter.Value) _eventUpdatedChargeCostValueActionLinkButton4Pool.Value.Del(i);
            if (_baseChargeCostValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkButton4Filter.Value) { _ChargeCostValueActionLinkButton4Pool.Value.Get(i).value = _baseChargeCostValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkForwardFFilter.Value) _eventUpdatedChargeCostValueActionLinkForwardFPool.Value.Del(i);
            if (_baseChargeCostValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkForwardFFilter.Value) { _ChargeCostValueActionLinkForwardFPool.Value.Get(i).value = _baseChargeCostValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkMouseLeftFilter.Value) _eventUpdatedChargeCostValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseChargeCostValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkMouseLeftFilter.Value) { _ChargeCostValueActionLinkMouseLeftPool.Value.Get(i).value = _baseChargeCostValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkMouseRightFilter.Value) _eventUpdatedChargeCostValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseChargeCostValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkMouseRightFilter.Value) { _ChargeCostValueActionLinkMouseRightPool.Value.Get(i).value = _baseChargeCostValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkSpaceFilter.Value) _eventUpdatedChargeCostValueActionLinkSpacePool.Value.Del(i);
            if (_baseChargeCostValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkSpaceFilter.Value) { _ChargeCostValueActionLinkSpacePool.Value.Get(i).value = _baseChargeCostValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueActionLinkSpaceForwardFilter.Value) _eventUpdatedChargeCostValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseChargeCostValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueActionLinkSpaceForwardFilter.Value) { _ChargeCostValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseChargeCostValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedChargeCostValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeCostValueHealingPotionValueFilter.Value) _eventUpdatedChargeCostValueHealingPotionValuePool.Value.Del(i);
            if (_baseChargeCostValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeCostValueHealingPotionValueFilter.Value) { _ChargeCostValueHealingPotionValuePool.Value.Get(i).value = _baseChargeCostValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedChargeCostValueHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkButton1Filter.Value) _eventUpdatedChargeMaxValueActionLinkButton1Pool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkButton1Filter.Value) { _ChargeMaxValueActionLinkButton1Pool.Value.Get(i).value = _baseChargeMaxValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkButton2Filter.Value) _eventUpdatedChargeMaxValueActionLinkButton2Pool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkButton2Filter.Value) { _ChargeMaxValueActionLinkButton2Pool.Value.Get(i).value = _baseChargeMaxValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkButton3Filter.Value) _eventUpdatedChargeMaxValueActionLinkButton3Pool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkButton3Filter.Value) { _ChargeMaxValueActionLinkButton3Pool.Value.Get(i).value = _baseChargeMaxValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkButton4Filter.Value) _eventUpdatedChargeMaxValueActionLinkButton4Pool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkButton4Filter.Value) { _ChargeMaxValueActionLinkButton4Pool.Value.Get(i).value = _baseChargeMaxValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkForwardFFilter.Value) _eventUpdatedChargeMaxValueActionLinkForwardFPool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkForwardFFilter.Value) { _ChargeMaxValueActionLinkForwardFPool.Value.Get(i).value = _baseChargeMaxValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkMouseLeftFilter.Value) _eventUpdatedChargeMaxValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkMouseLeftFilter.Value) { _ChargeMaxValueActionLinkMouseLeftPool.Value.Get(i).value = _baseChargeMaxValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkMouseRightFilter.Value) _eventUpdatedChargeMaxValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkMouseRightFilter.Value) { _ChargeMaxValueActionLinkMouseRightPool.Value.Get(i).value = _baseChargeMaxValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkSpaceFilter.Value) _eventUpdatedChargeMaxValueActionLinkSpacePool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkSpaceFilter.Value) { _ChargeMaxValueActionLinkSpacePool.Value.Get(i).value = _baseChargeMaxValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueActionLinkSpaceForwardFilter.Value) _eventUpdatedChargeMaxValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseChargeMaxValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueActionLinkSpaceForwardFilter.Value) { _ChargeMaxValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseChargeMaxValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedChargeMaxValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeMaxValueHealingPotionValueFilter.Value) _eventUpdatedChargeMaxValueHealingPotionValuePool.Value.Del(i);
            if (_baseChargeMaxValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeMaxValueHealingPotionValueFilter.Value) { _ChargeMaxValueHealingPotionValuePool.Value.Get(i).value = _baseChargeMaxValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedChargeMaxValueHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkButton1Filter.Value) _eventUpdatedChargeValueActionLinkButton1Pool.Value.Del(i);
            if (_baseChargeValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkButton1Filter.Value) { _ChargeValueActionLinkButton1Pool.Value.Get(i).value = _baseChargeValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkButton2Filter.Value) _eventUpdatedChargeValueActionLinkButton2Pool.Value.Del(i);
            if (_baseChargeValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkButton2Filter.Value) { _ChargeValueActionLinkButton2Pool.Value.Get(i).value = _baseChargeValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkButton3Filter.Value) _eventUpdatedChargeValueActionLinkButton3Pool.Value.Del(i);
            if (_baseChargeValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkButton3Filter.Value) { _ChargeValueActionLinkButton3Pool.Value.Get(i).value = _baseChargeValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkButton4Filter.Value) _eventUpdatedChargeValueActionLinkButton4Pool.Value.Del(i);
            if (_baseChargeValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkButton4Filter.Value) { _ChargeValueActionLinkButton4Pool.Value.Get(i).value = _baseChargeValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkForwardFFilter.Value) _eventUpdatedChargeValueActionLinkForwardFPool.Value.Del(i);
            if (_baseChargeValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkForwardFFilter.Value) { _ChargeValueActionLinkForwardFPool.Value.Get(i).value = _baseChargeValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkMouseLeftFilter.Value) _eventUpdatedChargeValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseChargeValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkMouseLeftFilter.Value) { _ChargeValueActionLinkMouseLeftPool.Value.Get(i).value = _baseChargeValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkMouseRightFilter.Value) _eventUpdatedChargeValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseChargeValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkMouseRightFilter.Value) { _ChargeValueActionLinkMouseRightPool.Value.Get(i).value = _baseChargeValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkSpaceFilter.Value) _eventUpdatedChargeValueActionLinkSpacePool.Value.Del(i);
            if (_baseChargeValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkSpaceFilter.Value) { _ChargeValueActionLinkSpacePool.Value.Get(i).value = _baseChargeValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedChargeValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueActionLinkSpaceForwardFilter.Value) _eventUpdatedChargeValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseChargeValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueActionLinkSpaceForwardFilter.Value) { _ChargeValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseChargeValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedChargeValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedChargeValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedChargeValueHealingPotionValueFilter.Value) _eventUpdatedChargeValueHealingPotionValuePool.Value.Del(i);
            if (_baseChargeValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseChargeValueHealingPotionValueFilter.Value) { _ChargeValueHealingPotionValuePool.Value.Get(i).value = _baseChargeValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedChargeValueHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkButton1Filter.Value) _eventUpdatedCooldownValueActionLinkButton1Pool.Value.Del(i);
            if (_baseCooldownValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkButton1Filter.Value) { _CooldownValueActionLinkButton1Pool.Value.Get(i).value = _baseCooldownValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkButton2Filter.Value) _eventUpdatedCooldownValueActionLinkButton2Pool.Value.Del(i);
            if (_baseCooldownValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkButton2Filter.Value) { _CooldownValueActionLinkButton2Pool.Value.Get(i).value = _baseCooldownValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkButton3Filter.Value) _eventUpdatedCooldownValueActionLinkButton3Pool.Value.Del(i);
            if (_baseCooldownValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkButton3Filter.Value) { _CooldownValueActionLinkButton3Pool.Value.Get(i).value = _baseCooldownValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkButton4Filter.Value) _eventUpdatedCooldownValueActionLinkButton4Pool.Value.Del(i);
            if (_baseCooldownValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkButton4Filter.Value) { _CooldownValueActionLinkButton4Pool.Value.Get(i).value = _baseCooldownValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkForwardFFilter.Value) _eventUpdatedCooldownValueActionLinkForwardFPool.Value.Del(i);
            if (_baseCooldownValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkForwardFFilter.Value) { _CooldownValueActionLinkForwardFPool.Value.Get(i).value = _baseCooldownValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkMouseLeftFilter.Value) _eventUpdatedCooldownValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseCooldownValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkMouseLeftFilter.Value) { _CooldownValueActionLinkMouseLeftPool.Value.Get(i).value = _baseCooldownValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkMouseRightFilter.Value) _eventUpdatedCooldownValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseCooldownValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkMouseRightFilter.Value) { _CooldownValueActionLinkMouseRightPool.Value.Get(i).value = _baseCooldownValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkSpaceFilter.Value) _eventUpdatedCooldownValueActionLinkSpacePool.Value.Del(i);
            if (_baseCooldownValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkSpaceFilter.Value) { _CooldownValueActionLinkSpacePool.Value.Get(i).value = _baseCooldownValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedCooldownValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueActionLinkSpaceForwardFilter.Value) _eventUpdatedCooldownValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseCooldownValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueActionLinkSpaceForwardFilter.Value) { _CooldownValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseCooldownValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedCooldownValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedCooldownValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCooldownValueHealingPotionValueFilter.Value) _eventUpdatedCooldownValueHealingPotionValuePool.Value.Del(i);
            if (_baseCooldownValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCooldownValueHealingPotionValueFilter.Value) { _CooldownValueHealingPotionValuePool.Value.Get(i).value = _baseCooldownValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedCooldownValueHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkButton1Filter.Value) _eventUpdatedCostValueActionLinkButton1Pool.Value.Del(i);
            if (_baseCostValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkButton1Filter.Value) { _CostValueActionLinkButton1Pool.Value.Get(i).value = _baseCostValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkButton2Filter.Value) _eventUpdatedCostValueActionLinkButton2Pool.Value.Del(i);
            if (_baseCostValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkButton2Filter.Value) { _CostValueActionLinkButton2Pool.Value.Get(i).value = _baseCostValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkButton3Filter.Value) _eventUpdatedCostValueActionLinkButton3Pool.Value.Del(i);
            if (_baseCostValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkButton3Filter.Value) { _CostValueActionLinkButton3Pool.Value.Get(i).value = _baseCostValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkButton4Filter.Value) _eventUpdatedCostValueActionLinkButton4Pool.Value.Del(i);
            if (_baseCostValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkButton4Filter.Value) { _CostValueActionLinkButton4Pool.Value.Get(i).value = _baseCostValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkForwardFFilter.Value) _eventUpdatedCostValueActionLinkForwardFPool.Value.Del(i);
            if (_baseCostValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkForwardFFilter.Value) { _CostValueActionLinkForwardFPool.Value.Get(i).value = _baseCostValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkMouseLeftFilter.Value) _eventUpdatedCostValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseCostValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkMouseLeftFilter.Value) { _CostValueActionLinkMouseLeftPool.Value.Get(i).value = _baseCostValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkMouseRightFilter.Value) _eventUpdatedCostValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseCostValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkMouseRightFilter.Value) { _CostValueActionLinkMouseRightPool.Value.Get(i).value = _baseCostValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkSpaceFilter.Value) _eventUpdatedCostValueActionLinkSpacePool.Value.Del(i);
            if (_baseCostValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkSpaceFilter.Value) { _CostValueActionLinkSpacePool.Value.Get(i).value = _baseCostValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedCostValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedCostValueActionLinkSpaceForwardFilter.Value) _eventUpdatedCostValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseCostValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseCostValueActionLinkSpaceForwardFilter.Value) { _CostValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseCostValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedCostValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Filter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Pool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkButton1Filter.Value) { _ResourceRecoveryPerHitValueActionLinkButton1Pool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Filter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Pool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkButton2Filter.Value) { _ResourceRecoveryPerHitValueActionLinkButton2Pool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Filter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Pool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkButton3Filter.Value) { _ResourceRecoveryPerHitValueActionLinkButton3Pool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Filter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Pool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkButton4Filter.Value) { _ResourceRecoveryPerHitValueActionLinkButton4Pool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFFilter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFPool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkForwardFFilter.Value) { _ResourceRecoveryPerHitValueActionLinkForwardFPool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftFilter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkMouseLeftFilter.Value) { _ResourceRecoveryPerHitValueActionLinkMouseLeftPool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightFilter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkMouseRightFilter.Value) { _ResourceRecoveryPerHitValueActionLinkMouseRightPool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceFilter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkSpacePool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkSpaceFilter.Value) { _ResourceRecoveryPerHitValueActionLinkSpacePool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardFilter.Value) _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueActionLinkSpaceForwardFilter.Value) { _ResourceRecoveryPerHitValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseResourceRecoveryPerHitValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerHitValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerHitValueHealingPotionValueFilter.Value) _eventUpdatedResourceRecoveryPerHitValueHealingPotionValuePool.Value.Del(i);
            if (_baseResourceRecoveryPerHitValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerHitValueHealingPotionValueFilter.Value) { _ResourceRecoveryPerHitValueHealingPotionValuePool.Value.Get(i).value = _baseResourceRecoveryPerHitValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerHitValueHealingPotionValuePool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Filter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Pool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkButton1Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkButton1Filter.Value) { _ResourceRecoveryPerUsingValueActionLinkButton1Pool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkButton1Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton1Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Filter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Pool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkButton2Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkButton2Filter.Value) { _ResourceRecoveryPerUsingValueActionLinkButton2Pool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkButton2Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton2Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Filter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Pool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkButton3Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkButton3Filter.Value) { _ResourceRecoveryPerUsingValueActionLinkButton3Pool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkButton3Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton3Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Filter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Pool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkButton4Filter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkButton4Filter.Value) { _ResourceRecoveryPerUsingValueActionLinkButton4Pool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkButton4Pool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkButton4Pool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFPool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkForwardFFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkForwardFFilter.Value) { _ResourceRecoveryPerUsingValueActionLinkForwardFPool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkForwardFPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkForwardFPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftPool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkMouseLeftFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkMouseLeftFilter.Value) { _ResourceRecoveryPerUsingValueActionLinkMouseLeftPool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkMouseLeftPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeftPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightPool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkMouseRightFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkMouseRightFilter.Value) { _ResourceRecoveryPerUsingValueActionLinkMouseRightPool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkMouseRightPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRightPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpacePool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkSpaceFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkSpaceFilter.Value) { _ResourceRecoveryPerUsingValueActionLinkSpacePool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkSpacePool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpacePool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardPool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueActionLinkSpaceForwardFilter.Value) { _ResourceRecoveryPerUsingValueActionLinkSpaceForwardPool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueActionLinkSpaceForwardPool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueActionLinkSpaceForwardPool.Value.Add(i); }
            if (_eventUpdatedResourceRecoveryPerUsingValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _eventUpdatedResourceRecoveryPerUsingValueHealingPotionValueFilter.Value) _eventUpdatedResourceRecoveryPerUsingValueHealingPotionValuePool.Value.Del(i);
            if (_baseResourceRecoveryPerUsingValueHealingPotionValueFilter.Value.GetEntitiesCount() != 0) foreach (var i in _baseResourceRecoveryPerUsingValueHealingPotionValueFilter.Value) { _ResourceRecoveryPerUsingValueHealingPotionValuePool.Value.Get(i).value = _baseResourceRecoveryPerUsingValueHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedResourceRecoveryPerUsingValueHealingPotionValuePool.Value.Add(i); }
        }
    }
}
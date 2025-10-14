//Файл генерируется в GenValuePoolsUtility
using System;
using System.Runtime.CompilerServices;
using Core.Components;
using Lib;

namespace Core.Generated
{    
    // @formatter:off
    public static class ValuePoolsUtility
    {
        public static void Sum(ComponentPools pools, int entity, ValueEnum @enum, float byValue)
        {
            switch (@enum)
            {
                case ValueEnum.ActionCostPerSecondValue: pools.ActionCostPerSecondValue.Get(entity).value += byValue; pools.EventUpdatedActionCostPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value += byValue; pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPercentValue: pools.ArmorPercentValue.Get(entity).value += byValue; pools.EventUpdatedArmorPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPropertyValue: pools.ArmorPropertyValue.Get(entity).value += byValue; pools.EventUpdatedArmorPropertyValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorValue: pools.ArmorValue.Get(entity).value += byValue; pools.EventUpdatedArmorValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedPercentValue: pools.AttackSpeedPercentValue.Get(entity).value += byValue; pools.EventUpdatedAttackSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value += byValue; pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageBashValue: pools.BarbDamageBashValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageBashValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageCleaveValue: pools.BarbDamageCleaveValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageCleaveValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageFrenzyValue: pools.BarbDamageFrenzyValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageFrenzyValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageHammerOfTheAncientsValue: pools.BarbDamageHammerOfTheAncientsValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageHammerOfTheAncientsValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageOverPowerValue: pools.BarbDamageOverPowerValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageOverPowerValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRendValue: pools.BarbDamageRendValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageRendValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRevengeValue: pools.BarbDamageRevengeValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageRevengeValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageSeismicSlamValue: pools.BarbDamageSeismicSlamValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageSeismicSlamValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWeaponThrowValue: pools.BarbDamageWeaponThrowValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageWeaponThrowValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWhirlwindValue: pools.BarbDamageWhirlwindValue.Get(entity).value += byValue; pools.EventUpdatedBarbDamageWhirlwindValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbFrenzyStackValue: pools.BarbFrenzyStackValue.Get(entity).value += byValue; pools.EventUpdatedBarbFrenzyStackValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMaxValue: pools.BlockAmountMaxValue.Get(entity).value += byValue; pools.EventUpdatedBlockAmountMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMinValue: pools.BlockAmountMinValue.Get(entity).value += byValue; pools.EventUpdatedBlockAmountMinValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockChanceValue: pools.BlockChanceValue.Get(entity).value += byValue; pools.EventUpdatedBlockChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value += byValue; pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value += byValue; pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageColdValue: pools.DamageColdValue.Get(entity).value += byValue; pools.EventUpdatedDamageColdValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageFireValue: pools.DamageFireValue.Get(entity).value += byValue; pools.EventUpdatedDamageFireValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageLightningValue: pools.DamageLightningValue.Get(entity).value += byValue; pools.EventUpdatedDamageLightningValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMaxValue: pools.DamageMaxValue.Get(entity).value += byValue; pools.EventUpdatedDamageMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMinValue: pools.DamageMinValue.Get(entity).value += byValue; pools.EventUpdatedDamageMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value += byValue; pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePhysicalValue: pools.DamagePhysicalValue.Get(entity).value += byValue; pools.EventUpdatedDamagePhysicalValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMaxValue: pools.DamagePropertyMaxValue.Get(entity).value += byValue; pools.EventUpdatedDamagePropertyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMinValue: pools.DamagePropertyMinValue.Get(entity).value += byValue; pools.EventUpdatedDamagePropertyMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageReflectionPercentValue: pools.DamageReflectionPercentValue.Get(entity).value += byValue; pools.EventUpdatedDamageReflectionPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value += byValue; pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DexterityValue: pools.DexterityValue.Get(entity).value += byValue; pools.EventUpdatedDexterityValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value += byValue; pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value += byValue; pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.EvasionValue: pools.EvasionValue.Get(entity).value += byValue; pools.EventUpdatedEvasionValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value += byValue; pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.ExplosionScaleValue: pools.ExplosionScaleValue.Get(entity).value += byValue; pools.EventUpdatedExplosionScaleValue.AddIfNotExist(entity); break;
                case ValueEnum.ExtraGoldWhenKillingValue: pools.ExtraGoldWhenKillingValue.Get(entity).value += byValue; pools.EventUpdatedExtraGoldWhenKillingValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPercentPerSecondValue: pools.HealingPercentPerSecondValue.Get(entity).value += byValue; pools.EventUpdatedHealingPercentPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerHitValue: pools.HealingPerHitValue.Get(entity).value += byValue; pools.EventUpdatedHealingPerHitValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerSecondValue: pools.HealingPerSecondValue.Get(entity).value += byValue; pools.EventUpdatedHealingPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value += byValue; pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value += byValue; pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value += byValue; pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.IncomingDamagePercentValue: pools.IncomingDamagePercentValue.Get(entity).value += byValue; pools.EventUpdatedIncomingDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.IntelligenceValue: pools.IntelligenceValue.Get(entity).value += byValue; pools.EventUpdatedIntelligenceValue.AddIfNotExist(entity); break;
                case ValueEnum.LevelValue: pools.LevelValue.Get(entity).value += byValue; pools.EventUpdatedLevelValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value += byValue; pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointRecoveryValue: pools.ManaPointRecoveryValue.Get(entity).value += byValue; pools.EventUpdatedManaPointRecoveryValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value += byValue; pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedPercentValue: pools.MoveSpeedPercentValue.Get(entity).value += byValue; pools.EventUpdatedMoveSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value += byValue; pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoveryTimeReductionValue: pools.RecoveryTimeReductionValue.Get(entity).value += byValue; pools.EventUpdatedRecoveryTimeReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResistanceAllValue: pools.ResistanceAllValue.Get(entity).value += byValue; pools.EventUpdatedResistanceAllValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceCostsReductionValue: pools.ResourceCostsReductionValue.Get(entity).value += byValue; pools.EventUpdatedResourceCostsReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerDamageTakenValue: pools.ResourceRecoveryPerDamageTakenValue.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerDamageTakenValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownAnimationValue: pools.SlowdownAnimationValue.Get(entity).value += byValue; pools.EventUpdatedSlowdownAnimationValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownMoveValue: pools.SlowdownMoveValue.Get(entity).value += byValue; pools.EventUpdatedSlowdownMoveValue.AddIfNotExist(entity); break;
                case ValueEnum.StrengthValue: pools.StrengthValue.Get(entity).value += byValue; pools.EventUpdatedStrengthValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityPercentValue: pools.VitalityPercentValue.Get(entity).value += byValue; pools.EventUpdatedVitalityPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityValue: pools.VitalityValue.Get(entity).value += byValue; pools.EventUpdatedVitalityValue.AddIfNotExist(entity); break;
                case ValueEnum.VulnerabilityCriticalChanceValue: pools.VulnerabilityCriticalChanceValue.Get(entity).value += byValue; pools.EventUpdatedVulnerabilityCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.WeaponAttackSpeedValue: pools.WeaponAttackSpeedValue.Get(entity).value += byValue; pools.EventUpdatedWeaponAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton1: pools.ChargeCostValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton2: pools.ChargeCostValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton3: pools.ChargeCostValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton4: pools.ChargeCostValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseLeft: pools.ChargeCostValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseRight: pools.ChargeCostValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueHealingPotionValue: pools.ChargeCostValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedChargeCostValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton1: pools.ChargeMaxValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton2: pools.ChargeMaxValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton3: pools.ChargeMaxValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton4: pools.ChargeMaxValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseLeft: pools.ChargeMaxValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseRight: pools.ChargeMaxValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueHealingPotionValue: pools.ChargeMaxValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedChargeMaxValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton1: pools.ChargeValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton2: pools.ChargeValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton3: pools.ChargeValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton4: pools.ChargeValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseLeft: pools.ChargeValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseRight: pools.ChargeValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedChargeValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueHealingPotionValue: pools.ChargeValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedChargeValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton1: pools.CooldownValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton2: pools.CooldownValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton3: pools.CooldownValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton4: pools.CooldownValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseLeft: pools.CooldownValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseRight: pools.CooldownValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedCooldownValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueHealingPotionValue: pools.CooldownValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedCooldownValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton1: pools.CostValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton2: pools.CostValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton3: pools.CostValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton4: pools.CostValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseLeft: pools.CostValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseRight: pools.CostValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1: pools.ResourceRecoveryPerHitValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2: pools.ResourceRecoveryPerHitValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3: pools.ResourceRecoveryPerHitValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4: pools.ResourceRecoveryPerHitValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft: pools.ResourceRecoveryPerHitValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight: pools.ResourceRecoveryPerHitValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue: pools.ResourceRecoveryPerHitValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerHitValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1: pools.ResourceRecoveryPerUsingValueActionLinkButton1.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2: pools.ResourceRecoveryPerUsingValueActionLinkButton2.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3: pools.ResourceRecoveryPerUsingValueActionLinkButton3.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4: pools.ResourceRecoveryPerUsingValueActionLinkButton4.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft: pools.ResourceRecoveryPerUsingValueActionLinkMouseLeft.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight: pools.ResourceRecoveryPerUsingValueActionLinkMouseRight.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue: pools.ResourceRecoveryPerUsingValueHealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedResourceRecoveryPerUsingValueHealingPotionValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }
        
        public static void Sum(ComponentPools pools, int entity, ValueEnum @enum, ValueEnum byValue)
        {
            switch (@enum)
            {
                case ValueEnum.ActionCostPerSecondValue: pools.ActionCostPerSecondValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedActionCostPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPercentValue: pools.ArmorPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedArmorPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPropertyValue: pools.ArmorPropertyValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedArmorPropertyValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorValue: pools.ArmorValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedArmorValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedPercentValue: pools.AttackSpeedPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAttackSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageBashValue: pools.BarbDamageBashValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageBashValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageCleaveValue: pools.BarbDamageCleaveValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageCleaveValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageFrenzyValue: pools.BarbDamageFrenzyValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageFrenzyValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageHammerOfTheAncientsValue: pools.BarbDamageHammerOfTheAncientsValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageHammerOfTheAncientsValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageOverPowerValue: pools.BarbDamageOverPowerValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageOverPowerValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRendValue: pools.BarbDamageRendValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageRendValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRevengeValue: pools.BarbDamageRevengeValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageRevengeValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageSeismicSlamValue: pools.BarbDamageSeismicSlamValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageSeismicSlamValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWeaponThrowValue: pools.BarbDamageWeaponThrowValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageWeaponThrowValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWhirlwindValue: pools.BarbDamageWhirlwindValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbDamageWhirlwindValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbFrenzyStackValue: pools.BarbFrenzyStackValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBarbFrenzyStackValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMaxValue: pools.BlockAmountMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBlockAmountMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMinValue: pools.BlockAmountMinValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBlockAmountMinValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockChanceValue: pools.BlockChanceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedBlockChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageColdValue: pools.DamageColdValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageColdValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageFireValue: pools.DamageFireValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageFireValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageLightningValue: pools.DamageLightningValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageLightningValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMaxValue: pools.DamageMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMinValue: pools.DamageMinValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePhysicalValue: pools.DamagePhysicalValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamagePhysicalValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMaxValue: pools.DamagePropertyMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamagePropertyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMinValue: pools.DamagePropertyMinValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamagePropertyMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageReflectionPercentValue: pools.DamageReflectionPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageReflectionPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DexterityValue: pools.DexterityValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDexterityValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.EvasionValue: pools.EvasionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedEvasionValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.ExplosionScaleValue: pools.ExplosionScaleValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedExplosionScaleValue.AddIfNotExist(entity); break;
                case ValueEnum.ExtraGoldWhenKillingValue: pools.ExtraGoldWhenKillingValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedExtraGoldWhenKillingValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPercentPerSecondValue: pools.HealingPercentPerSecondValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPercentPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerHitValue: pools.HealingPerHitValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPerHitValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerSecondValue: pools.HealingPerSecondValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.IncomingDamagePercentValue: pools.IncomingDamagePercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedIncomingDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.IntelligenceValue: pools.IntelligenceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedIntelligenceValue.AddIfNotExist(entity); break;
                case ValueEnum.LevelValue: pools.LevelValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedLevelValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointRecoveryValue: pools.ManaPointRecoveryValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedManaPointRecoveryValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedPercentValue: pools.MoveSpeedPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedMoveSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoveryTimeReductionValue: pools.RecoveryTimeReductionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedRecoveryTimeReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResistanceAllValue: pools.ResistanceAllValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResistanceAllValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceCostsReductionValue: pools.ResourceCostsReductionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceCostsReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerDamageTakenValue: pools.ResourceRecoveryPerDamageTakenValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerDamageTakenValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownAnimationValue: pools.SlowdownAnimationValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedSlowdownAnimationValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownMoveValue: pools.SlowdownMoveValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedSlowdownMoveValue.AddIfNotExist(entity); break;
                case ValueEnum.StrengthValue: pools.StrengthValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedStrengthValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityPercentValue: pools.VitalityPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedVitalityPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityValue: pools.VitalityValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedVitalityValue.AddIfNotExist(entity); break;
                case ValueEnum.VulnerabilityCriticalChanceValue: pools.VulnerabilityCriticalChanceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedVulnerabilityCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.WeaponAttackSpeedValue: pools.WeaponAttackSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedWeaponAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton1: pools.ChargeCostValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton2: pools.ChargeCostValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton3: pools.ChargeCostValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton4: pools.ChargeCostValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseLeft: pools.ChargeCostValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseRight: pools.ChargeCostValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueHealingPotionValue: pools.ChargeCostValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeCostValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton1: pools.ChargeMaxValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton2: pools.ChargeMaxValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton3: pools.ChargeMaxValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton4: pools.ChargeMaxValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseLeft: pools.ChargeMaxValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseRight: pools.ChargeMaxValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueHealingPotionValue: pools.ChargeMaxValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeMaxValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton1: pools.ChargeValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton2: pools.ChargeValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton3: pools.ChargeValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton4: pools.ChargeValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseLeft: pools.ChargeValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseRight: pools.ChargeValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueHealingPotionValue: pools.ChargeValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedChargeValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton1: pools.CooldownValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton2: pools.CooldownValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton3: pools.CooldownValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton4: pools.CooldownValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseLeft: pools.CooldownValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseRight: pools.CooldownValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueHealingPotionValue: pools.CooldownValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCooldownValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton1: pools.CostValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton2: pools.CostValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton3: pools.CostValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton4: pools.CostValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseLeft: pools.CostValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseRight: pools.CostValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1: pools.ResourceRecoveryPerHitValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2: pools.ResourceRecoveryPerHitValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3: pools.ResourceRecoveryPerHitValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4: pools.ResourceRecoveryPerHitValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft: pools.ResourceRecoveryPerHitValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight: pools.ResourceRecoveryPerHitValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue: pools.ResourceRecoveryPerHitValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerHitValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1: pools.ResourceRecoveryPerUsingValueActionLinkButton1.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2: pools.ResourceRecoveryPerUsingValueActionLinkButton2.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3: pools.ResourceRecoveryPerUsingValueActionLinkButton3.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4: pools.ResourceRecoveryPerUsingValueActionLinkButton4.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft: pools.ResourceRecoveryPerUsingValueActionLinkMouseLeft.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight: pools.ResourceRecoveryPerUsingValueActionLinkMouseRight.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue: pools.ResourceRecoveryPerUsingValueHealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedResourceRecoveryPerUsingValueHealingPotionValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }
        
        public static float GetValue(ComponentPools pools, int entity, ValueEnum @enum) => @enum switch
        {
            ValueEnum.ActionCostPerSecondValue => pools.ActionCostPerSecondValue.Get(entity).value,
            ValueEnum.AddScoreOnDeathValue => pools.AddScoreOnDeathValue.Get(entity).value,
            ValueEnum.ArmorPercentValue => pools.ArmorPercentValue.Get(entity).value,
            ValueEnum.ArmorPropertyValue => pools.ArmorPropertyValue.Get(entity).value,
            ValueEnum.ArmorValue => pools.ArmorValue.Get(entity).value,
            ValueEnum.AttackSpeedPercentValue => pools.AttackSpeedPercentValue.Get(entity).value,
            ValueEnum.AttackSpeedValue => pools.AttackSpeedValue.Get(entity).value,
            ValueEnum.BarbDamageBashValue => pools.BarbDamageBashValue.Get(entity).value,
            ValueEnum.BarbDamageCleaveValue => pools.BarbDamageCleaveValue.Get(entity).value,
            ValueEnum.BarbDamageFrenzyValue => pools.BarbDamageFrenzyValue.Get(entity).value,
            ValueEnum.BarbDamageHammerOfTheAncientsValue => pools.BarbDamageHammerOfTheAncientsValue.Get(entity).value,
            ValueEnum.BarbDamageOverPowerValue => pools.BarbDamageOverPowerValue.Get(entity).value,
            ValueEnum.BarbDamageRendValue => pools.BarbDamageRendValue.Get(entity).value,
            ValueEnum.BarbDamageRevengeValue => pools.BarbDamageRevengeValue.Get(entity).value,
            ValueEnum.BarbDamageSeismicSlamValue => pools.BarbDamageSeismicSlamValue.Get(entity).value,
            ValueEnum.BarbDamageWeaponThrowValue => pools.BarbDamageWeaponThrowValue.Get(entity).value,
            ValueEnum.BarbDamageWhirlwindValue => pools.BarbDamageWhirlwindValue.Get(entity).value,
            ValueEnum.BarbFrenzyStackValue => pools.BarbFrenzyStackValue.Get(entity).value,
            ValueEnum.BlockAmountMaxValue => pools.BlockAmountMaxValue.Get(entity).value,
            ValueEnum.BlockAmountMinValue => pools.BlockAmountMinValue.Get(entity).value,
            ValueEnum.BlockChanceValue => pools.BlockChanceValue.Get(entity).value,
            ValueEnum.CriticalChanceValue => pools.CriticalChanceValue.Get(entity).value,
            ValueEnum.CriticalDamageValue => pools.CriticalDamageValue.Get(entity).value,
            ValueEnum.DamageColdValue => pools.DamageColdValue.Get(entity).value,
            ValueEnum.DamageFireValue => pools.DamageFireValue.Get(entity).value,
            ValueEnum.DamageLightningValue => pools.DamageLightningValue.Get(entity).value,
            ValueEnum.DamageMaxValue => pools.DamageMaxValue.Get(entity).value,
            ValueEnum.DamageMinValue => pools.DamageMinValue.Get(entity).value,
            ValueEnum.DamagePercentValue => pools.DamagePercentValue.Get(entity).value,
            ValueEnum.DamagePhysicalValue => pools.DamagePhysicalValue.Get(entity).value,
            ValueEnum.DamagePropertyMaxValue => pools.DamagePropertyMaxValue.Get(entity).value,
            ValueEnum.DamagePropertyMinValue => pools.DamagePropertyMinValue.Get(entity).value,
            ValueEnum.DamageReflectionPercentValue => pools.DamageReflectionPercentValue.Get(entity).value,
            ValueEnum.DamageValue => pools.DamageValue.Get(entity).value,
            ValueEnum.DexterityValue => pools.DexterityValue.Get(entity).value,
            ValueEnum.EnergyMaxValue => pools.EnergyMaxValue.Get(entity).value,
            ValueEnum.EnergyValue => pools.EnergyValue.Get(entity).value,
            ValueEnum.EvasionValue => pools.EvasionValue.Get(entity).value,
            ValueEnum.ExperienceValue => pools.ExperienceValue.Get(entity).value,
            ValueEnum.ExplosionScaleValue => pools.ExplosionScaleValue.Get(entity).value,
            ValueEnum.ExtraGoldWhenKillingValue => pools.ExtraGoldWhenKillingValue.Get(entity).value,
            ValueEnum.HealingPercentPerSecondValue => pools.HealingPercentPerSecondValue.Get(entity).value,
            ValueEnum.HealingPerHitValue => pools.HealingPerHitValue.Get(entity).value,
            ValueEnum.HealingPerSecondValue => pools.HealingPerSecondValue.Get(entity).value,
            ValueEnum.HealingPotionValue => pools.HealingPotionValue.Get(entity).value,
            ValueEnum.HitPointMaxValue => pools.HitPointMaxValue.Get(entity).value,
            ValueEnum.HitPointPercentValue => pools.HitPointPercentValue.Get(entity).value,
            ValueEnum.HitPointValue => pools.HitPointValue.Get(entity).value,
            ValueEnum.IncomingDamagePercentValue => pools.IncomingDamagePercentValue.Get(entity).value,
            ValueEnum.IntelligenceValue => pools.IntelligenceValue.Get(entity).value,
            ValueEnum.LevelValue => pools.LevelValue.Get(entity).value,
            ValueEnum.ManaPointMaxValue => pools.ManaPointMaxValue.Get(entity).value,
            ValueEnum.ManaPointRecoveryValue => pools.ManaPointRecoveryValue.Get(entity).value,
            ValueEnum.ManaPointValue => pools.ManaPointValue.Get(entity).value,
            ValueEnum.MoveSpeedPercentValue => pools.MoveSpeedPercentValue.Get(entity).value,
            ValueEnum.MoveSpeedValue => pools.MoveSpeedValue.Get(entity).value,
            ValueEnum.RecoveryTimeReductionValue => pools.RecoveryTimeReductionValue.Get(entity).value,
            ValueEnum.ResistanceAllValue => pools.ResistanceAllValue.Get(entity).value,
            ValueEnum.ResourceCostsReductionValue => pools.ResourceCostsReductionValue.Get(entity).value,
            ValueEnum.ResourceRecoveryPerDamageTakenValue => pools.ResourceRecoveryPerDamageTakenValue.Get(entity).value,
            ValueEnum.SlowdownAnimationValue => pools.SlowdownAnimationValue.Get(entity).value,
            ValueEnum.SlowdownMoveValue => pools.SlowdownMoveValue.Get(entity).value,
            ValueEnum.StrengthValue => pools.StrengthValue.Get(entity).value,
            ValueEnum.VitalityPercentValue => pools.VitalityPercentValue.Get(entity).value,
            ValueEnum.VitalityValue => pools.VitalityValue.Get(entity).value,
            ValueEnum.VulnerabilityCriticalChanceValue => pools.VulnerabilityCriticalChanceValue.Get(entity).value,
            ValueEnum.WeaponAttackSpeedValue => pools.WeaponAttackSpeedValue.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkButton1 => pools.ChargeCostValueActionLinkButton1.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkButton2 => pools.ChargeCostValueActionLinkButton2.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkButton3 => pools.ChargeCostValueActionLinkButton3.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkButton4 => pools.ChargeCostValueActionLinkButton4.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkMouseLeft => pools.ChargeCostValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.ChargeCostValueActionLinkMouseRight => pools.ChargeCostValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ChargeCostValueHealingPotionValue => pools.ChargeCostValueHealingPotionValue.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkButton1 => pools.ChargeMaxValueActionLinkButton1.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkButton2 => pools.ChargeMaxValueActionLinkButton2.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkButton3 => pools.ChargeMaxValueActionLinkButton3.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkButton4 => pools.ChargeMaxValueActionLinkButton4.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkMouseLeft => pools.ChargeMaxValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.ChargeMaxValueActionLinkMouseRight => pools.ChargeMaxValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ChargeMaxValueHealingPotionValue => pools.ChargeMaxValueHealingPotionValue.Get(entity).value,
            ValueEnum.ChargeValueActionLinkButton1 => pools.ChargeValueActionLinkButton1.Get(entity).value,
            ValueEnum.ChargeValueActionLinkButton2 => pools.ChargeValueActionLinkButton2.Get(entity).value,
            ValueEnum.ChargeValueActionLinkButton3 => pools.ChargeValueActionLinkButton3.Get(entity).value,
            ValueEnum.ChargeValueActionLinkButton4 => pools.ChargeValueActionLinkButton4.Get(entity).value,
            ValueEnum.ChargeValueActionLinkMouseLeft => pools.ChargeValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.ChargeValueActionLinkMouseRight => pools.ChargeValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ChargeValueHealingPotionValue => pools.ChargeValueHealingPotionValue.Get(entity).value,
            ValueEnum.CooldownValueActionLinkButton1 => pools.CooldownValueActionLinkButton1.Get(entity).value,
            ValueEnum.CooldownValueActionLinkButton2 => pools.CooldownValueActionLinkButton2.Get(entity).value,
            ValueEnum.CooldownValueActionLinkButton3 => pools.CooldownValueActionLinkButton3.Get(entity).value,
            ValueEnum.CooldownValueActionLinkButton4 => pools.CooldownValueActionLinkButton4.Get(entity).value,
            ValueEnum.CooldownValueActionLinkMouseLeft => pools.CooldownValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.CooldownValueActionLinkMouseRight => pools.CooldownValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.CooldownValueHealingPotionValue => pools.CooldownValueHealingPotionValue.Get(entity).value,
            ValueEnum.CostValueActionLinkButton1 => pools.CostValueActionLinkButton1.Get(entity).value,
            ValueEnum.CostValueActionLinkButton2 => pools.CostValueActionLinkButton2.Get(entity).value,
            ValueEnum.CostValueActionLinkButton3 => pools.CostValueActionLinkButton3.Get(entity).value,
            ValueEnum.CostValueActionLinkButton4 => pools.CostValueActionLinkButton4.Get(entity).value,
            ValueEnum.CostValueActionLinkMouseLeft => pools.CostValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.CostValueActionLinkMouseRight => pools.CostValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1 => pools.ResourceRecoveryPerHitValueActionLinkButton1.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2 => pools.ResourceRecoveryPerHitValueActionLinkButton2.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3 => pools.ResourceRecoveryPerHitValueActionLinkButton3.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4 => pools.ResourceRecoveryPerHitValueActionLinkButton4.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft => pools.ResourceRecoveryPerHitValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight => pools.ResourceRecoveryPerHitValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue => pools.ResourceRecoveryPerHitValueHealingPotionValue.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1 => pools.ResourceRecoveryPerUsingValueActionLinkButton1.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2 => pools.ResourceRecoveryPerUsingValueActionLinkButton2.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3 => pools.ResourceRecoveryPerUsingValueActionLinkButton3.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4 => pools.ResourceRecoveryPerUsingValueActionLinkButton4.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft => pools.ResourceRecoveryPerUsingValueActionLinkMouseLeft.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight => pools.ResourceRecoveryPerUsingValueActionLinkMouseRight.Get(entity).value,
            ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue => pools.ResourceRecoveryPerUsingValueHealingPotionValue.Get(entity).value,
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };

        public static void SetValue(ComponentPools pools, int entity, ValueEnum @enum, float value)
        {
            switch (@enum)
            {
                case ValueEnum.ActionCostPerSecondValue: pools.ActionCostPerSecondValue.Get(entity).value = value; pools.EventUpdatedActionCostPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value = value; pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPercentValue: pools.ArmorPercentValue.Get(entity).value = value; pools.EventUpdatedArmorPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorPropertyValue: pools.ArmorPropertyValue.Get(entity).value = value; pools.EventUpdatedArmorPropertyValue.AddIfNotExist(entity); break;
                case ValueEnum.ArmorValue: pools.ArmorValue.Get(entity).value = value; pools.EventUpdatedArmorValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedPercentValue: pools.AttackSpeedPercentValue.Get(entity).value = value; pools.EventUpdatedAttackSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value = value; pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageBashValue: pools.BarbDamageBashValue.Get(entity).value = value; pools.EventUpdatedBarbDamageBashValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageCleaveValue: pools.BarbDamageCleaveValue.Get(entity).value = value; pools.EventUpdatedBarbDamageCleaveValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageFrenzyValue: pools.BarbDamageFrenzyValue.Get(entity).value = value; pools.EventUpdatedBarbDamageFrenzyValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageHammerOfTheAncientsValue: pools.BarbDamageHammerOfTheAncientsValue.Get(entity).value = value; pools.EventUpdatedBarbDamageHammerOfTheAncientsValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageOverPowerValue: pools.BarbDamageOverPowerValue.Get(entity).value = value; pools.EventUpdatedBarbDamageOverPowerValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRendValue: pools.BarbDamageRendValue.Get(entity).value = value; pools.EventUpdatedBarbDamageRendValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageRevengeValue: pools.BarbDamageRevengeValue.Get(entity).value = value; pools.EventUpdatedBarbDamageRevengeValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageSeismicSlamValue: pools.BarbDamageSeismicSlamValue.Get(entity).value = value; pools.EventUpdatedBarbDamageSeismicSlamValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWeaponThrowValue: pools.BarbDamageWeaponThrowValue.Get(entity).value = value; pools.EventUpdatedBarbDamageWeaponThrowValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbDamageWhirlwindValue: pools.BarbDamageWhirlwindValue.Get(entity).value = value; pools.EventUpdatedBarbDamageWhirlwindValue.AddIfNotExist(entity); break;
                case ValueEnum.BarbFrenzyStackValue: pools.BarbFrenzyStackValue.Get(entity).value = value; pools.EventUpdatedBarbFrenzyStackValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMaxValue: pools.BlockAmountMaxValue.Get(entity).value = value; pools.EventUpdatedBlockAmountMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockAmountMinValue: pools.BlockAmountMinValue.Get(entity).value = value; pools.EventUpdatedBlockAmountMinValue.AddIfNotExist(entity); break;
                case ValueEnum.BlockChanceValue: pools.BlockChanceValue.Get(entity).value = value; pools.EventUpdatedBlockChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value = value; pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value = value; pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageColdValue: pools.DamageColdValue.Get(entity).value = value; pools.EventUpdatedDamageColdValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageFireValue: pools.DamageFireValue.Get(entity).value = value; pools.EventUpdatedDamageFireValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageLightningValue: pools.DamageLightningValue.Get(entity).value = value; pools.EventUpdatedDamageLightningValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMaxValue: pools.DamageMaxValue.Get(entity).value = value; pools.EventUpdatedDamageMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageMinValue: pools.DamageMinValue.Get(entity).value = value; pools.EventUpdatedDamageMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value = value; pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePhysicalValue: pools.DamagePhysicalValue.Get(entity).value = value; pools.EventUpdatedDamagePhysicalValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMaxValue: pools.DamagePropertyMaxValue.Get(entity).value = value; pools.EventUpdatedDamagePropertyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePropertyMinValue: pools.DamagePropertyMinValue.Get(entity).value = value; pools.EventUpdatedDamagePropertyMinValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageReflectionPercentValue: pools.DamageReflectionPercentValue.Get(entity).value = value; pools.EventUpdatedDamageReflectionPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value = value; pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DexterityValue: pools.DexterityValue.Get(entity).value = value; pools.EventUpdatedDexterityValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value = value; pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value = value; pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.EvasionValue: pools.EvasionValue.Get(entity).value = value; pools.EventUpdatedEvasionValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value = value; pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.ExplosionScaleValue: pools.ExplosionScaleValue.Get(entity).value = value; pools.EventUpdatedExplosionScaleValue.AddIfNotExist(entity); break;
                case ValueEnum.ExtraGoldWhenKillingValue: pools.ExtraGoldWhenKillingValue.Get(entity).value = value; pools.EventUpdatedExtraGoldWhenKillingValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPercentPerSecondValue: pools.HealingPercentPerSecondValue.Get(entity).value = value; pools.EventUpdatedHealingPercentPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerHitValue: pools.HealingPerHitValue.Get(entity).value = value; pools.EventUpdatedHealingPerHitValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPerSecondValue: pools.HealingPerSecondValue.Get(entity).value = value; pools.EventUpdatedHealingPerSecondValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value = value; pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value = value; pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value = value; pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value = value; pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.IncomingDamagePercentValue: pools.IncomingDamagePercentValue.Get(entity).value = value; pools.EventUpdatedIncomingDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.IntelligenceValue: pools.IntelligenceValue.Get(entity).value = value; pools.EventUpdatedIntelligenceValue.AddIfNotExist(entity); break;
                case ValueEnum.LevelValue: pools.LevelValue.Get(entity).value = value; pools.EventUpdatedLevelValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value = value; pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointRecoveryValue: pools.ManaPointRecoveryValue.Get(entity).value = value; pools.EventUpdatedManaPointRecoveryValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value = value; pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedPercentValue: pools.MoveSpeedPercentValue.Get(entity).value = value; pools.EventUpdatedMoveSpeedPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value = value; pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoveryTimeReductionValue: pools.RecoveryTimeReductionValue.Get(entity).value = value; pools.EventUpdatedRecoveryTimeReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResistanceAllValue: pools.ResistanceAllValue.Get(entity).value = value; pools.EventUpdatedResistanceAllValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceCostsReductionValue: pools.ResourceCostsReductionValue.Get(entity).value = value; pools.EventUpdatedResourceCostsReductionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerDamageTakenValue: pools.ResourceRecoveryPerDamageTakenValue.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerDamageTakenValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownAnimationValue: pools.SlowdownAnimationValue.Get(entity).value = value; pools.EventUpdatedSlowdownAnimationValue.AddIfNotExist(entity); break;
                case ValueEnum.SlowdownMoveValue: pools.SlowdownMoveValue.Get(entity).value = value; pools.EventUpdatedSlowdownMoveValue.AddIfNotExist(entity); break;
                case ValueEnum.StrengthValue: pools.StrengthValue.Get(entity).value = value; pools.EventUpdatedStrengthValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityPercentValue: pools.VitalityPercentValue.Get(entity).value = value; pools.EventUpdatedVitalityPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.VitalityValue: pools.VitalityValue.Get(entity).value = value; pools.EventUpdatedVitalityValue.AddIfNotExist(entity); break;
                case ValueEnum.VulnerabilityCriticalChanceValue: pools.VulnerabilityCriticalChanceValue.Get(entity).value = value; pools.EventUpdatedVulnerabilityCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.WeaponAttackSpeedValue: pools.WeaponAttackSpeedValue.Get(entity).value = value; pools.EventUpdatedWeaponAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton1: pools.ChargeCostValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton2: pools.ChargeCostValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton3: pools.ChargeCostValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkButton4: pools.ChargeCostValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseLeft: pools.ChargeCostValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueActionLinkMouseRight: pools.ChargeCostValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedChargeCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeCostValueHealingPotionValue: pools.ChargeCostValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedChargeCostValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton1: pools.ChargeMaxValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton2: pools.ChargeMaxValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton3: pools.ChargeMaxValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkButton4: pools.ChargeMaxValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseLeft: pools.ChargeMaxValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseRight: pools.ChargeMaxValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedChargeMaxValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeMaxValueHealingPotionValue: pools.ChargeMaxValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedChargeMaxValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton1: pools.ChargeValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton2: pools.ChargeValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton3: pools.ChargeValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkButton4: pools.ChargeValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseLeft: pools.ChargeValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueActionLinkMouseRight: pools.ChargeValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedChargeValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ChargeValueHealingPotionValue: pools.ChargeValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedChargeValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton1: pools.CooldownValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton2: pools.CooldownValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton3: pools.CooldownValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkButton4: pools.CooldownValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseLeft: pools.CooldownValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueActionLinkMouseRight: pools.CooldownValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedCooldownValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.CooldownValueHealingPotionValue: pools.CooldownValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedCooldownValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton1: pools.CostValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton2: pools.CostValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton3: pools.CostValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkButton4: pools.CostValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseLeft: pools.CostValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.CostValueActionLinkMouseRight: pools.CostValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedCostValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1: pools.ResourceRecoveryPerHitValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2: pools.ResourceRecoveryPerHitValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3: pools.ResourceRecoveryPerHitValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4: pools.ResourceRecoveryPerHitValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft: pools.ResourceRecoveryPerHitValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight: pools.ResourceRecoveryPerHitValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue: pools.ResourceRecoveryPerHitValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerHitValueHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1: pools.ResourceRecoveryPerUsingValueActionLinkButton1.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton1.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2: pools.ResourceRecoveryPerUsingValueActionLinkButton2.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton2.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3: pools.ResourceRecoveryPerUsingValueActionLinkButton3.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton3.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4: pools.ResourceRecoveryPerUsingValueActionLinkButton4.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkButton4.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft: pools.ResourceRecoveryPerUsingValueActionLinkMouseLeft.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseLeft.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight: pools.ResourceRecoveryPerUsingValueActionLinkMouseRight.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueActionLinkMouseRight.AddIfNotExist(entity); break;
                case ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue: pools.ResourceRecoveryPerUsingValueHealingPotionValue.Get(entity).value = value; pools.EventUpdatedResourceRecoveryPerUsingValueHealingPotionValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }

        public static void CallGenericPool<T>(T poolContext, ComponentPools pools, ValueEnum @enum) where T : IValueGenericPoolContext
        {
            switch(@enum)
            {
                case ValueEnum.ActionCostPerSecondValue: poolContext.GenericRun(pools.ActionCostPerSecondValue); break;
                case ValueEnum.AddScoreOnDeathValue: poolContext.GenericRun(pools.AddScoreOnDeathValue); break;
                case ValueEnum.ArmorPercentValue: poolContext.GenericRun(pools.ArmorPercentValue); break;
                case ValueEnum.ArmorPropertyValue: poolContext.GenericRun(pools.ArmorPropertyValue); break;
                case ValueEnum.ArmorValue: poolContext.GenericRun(pools.ArmorValue); break;
                case ValueEnum.AttackSpeedPercentValue: poolContext.GenericRun(pools.AttackSpeedPercentValue); break;
                case ValueEnum.AttackSpeedValue: poolContext.GenericRun(pools.AttackSpeedValue); break;
                case ValueEnum.BarbDamageBashValue: poolContext.GenericRun(pools.BarbDamageBashValue); break;
                case ValueEnum.BarbDamageCleaveValue: poolContext.GenericRun(pools.BarbDamageCleaveValue); break;
                case ValueEnum.BarbDamageFrenzyValue: poolContext.GenericRun(pools.BarbDamageFrenzyValue); break;
                case ValueEnum.BarbDamageHammerOfTheAncientsValue: poolContext.GenericRun(pools.BarbDamageHammerOfTheAncientsValue); break;
                case ValueEnum.BarbDamageOverPowerValue: poolContext.GenericRun(pools.BarbDamageOverPowerValue); break;
                case ValueEnum.BarbDamageRendValue: poolContext.GenericRun(pools.BarbDamageRendValue); break;
                case ValueEnum.BarbDamageRevengeValue: poolContext.GenericRun(pools.BarbDamageRevengeValue); break;
                case ValueEnum.BarbDamageSeismicSlamValue: poolContext.GenericRun(pools.BarbDamageSeismicSlamValue); break;
                case ValueEnum.BarbDamageWeaponThrowValue: poolContext.GenericRun(pools.BarbDamageWeaponThrowValue); break;
                case ValueEnum.BarbDamageWhirlwindValue: poolContext.GenericRun(pools.BarbDamageWhirlwindValue); break;
                case ValueEnum.BarbFrenzyStackValue: poolContext.GenericRun(pools.BarbFrenzyStackValue); break;
                case ValueEnum.BlockAmountMaxValue: poolContext.GenericRun(pools.BlockAmountMaxValue); break;
                case ValueEnum.BlockAmountMinValue: poolContext.GenericRun(pools.BlockAmountMinValue); break;
                case ValueEnum.BlockChanceValue: poolContext.GenericRun(pools.BlockChanceValue); break;
                case ValueEnum.CriticalChanceValue: poolContext.GenericRun(pools.CriticalChanceValue); break;
                case ValueEnum.CriticalDamageValue: poolContext.GenericRun(pools.CriticalDamageValue); break;
                case ValueEnum.DamageColdValue: poolContext.GenericRun(pools.DamageColdValue); break;
                case ValueEnum.DamageFireValue: poolContext.GenericRun(pools.DamageFireValue); break;
                case ValueEnum.DamageLightningValue: poolContext.GenericRun(pools.DamageLightningValue); break;
                case ValueEnum.DamageMaxValue: poolContext.GenericRun(pools.DamageMaxValue); break;
                case ValueEnum.DamageMinValue: poolContext.GenericRun(pools.DamageMinValue); break;
                case ValueEnum.DamagePercentValue: poolContext.GenericRun(pools.DamagePercentValue); break;
                case ValueEnum.DamagePhysicalValue: poolContext.GenericRun(pools.DamagePhysicalValue); break;
                case ValueEnum.DamagePropertyMaxValue: poolContext.GenericRun(pools.DamagePropertyMaxValue); break;
                case ValueEnum.DamagePropertyMinValue: poolContext.GenericRun(pools.DamagePropertyMinValue); break;
                case ValueEnum.DamageReflectionPercentValue: poolContext.GenericRun(pools.DamageReflectionPercentValue); break;
                case ValueEnum.DamageValue: poolContext.GenericRun(pools.DamageValue); break;
                case ValueEnum.DexterityValue: poolContext.GenericRun(pools.DexterityValue); break;
                case ValueEnum.EnergyMaxValue: poolContext.GenericRun(pools.EnergyMaxValue); break;
                case ValueEnum.EnergyValue: poolContext.GenericRun(pools.EnergyValue); break;
                case ValueEnum.EvasionValue: poolContext.GenericRun(pools.EvasionValue); break;
                case ValueEnum.ExperienceValue: poolContext.GenericRun(pools.ExperienceValue); break;
                case ValueEnum.ExplosionScaleValue: poolContext.GenericRun(pools.ExplosionScaleValue); break;
                case ValueEnum.ExtraGoldWhenKillingValue: poolContext.GenericRun(pools.ExtraGoldWhenKillingValue); break;
                case ValueEnum.HealingPercentPerSecondValue: poolContext.GenericRun(pools.HealingPercentPerSecondValue); break;
                case ValueEnum.HealingPerHitValue: poolContext.GenericRun(pools.HealingPerHitValue); break;
                case ValueEnum.HealingPerSecondValue: poolContext.GenericRun(pools.HealingPerSecondValue); break;
                case ValueEnum.HealingPotionValue: poolContext.GenericRun(pools.HealingPotionValue); break;
                case ValueEnum.HitPointMaxValue: poolContext.GenericRun(pools.HitPointMaxValue); break;
                case ValueEnum.HitPointPercentValue: poolContext.GenericRun(pools.HitPointPercentValue); break;
                case ValueEnum.HitPointValue: poolContext.GenericRun(pools.HitPointValue); break;
                case ValueEnum.IncomingDamagePercentValue: poolContext.GenericRun(pools.IncomingDamagePercentValue); break;
                case ValueEnum.IntelligenceValue: poolContext.GenericRun(pools.IntelligenceValue); break;
                case ValueEnum.LevelValue: poolContext.GenericRun(pools.LevelValue); break;
                case ValueEnum.ManaPointMaxValue: poolContext.GenericRun(pools.ManaPointMaxValue); break;
                case ValueEnum.ManaPointRecoveryValue: poolContext.GenericRun(pools.ManaPointRecoveryValue); break;
                case ValueEnum.ManaPointValue: poolContext.GenericRun(pools.ManaPointValue); break;
                case ValueEnum.MoveSpeedPercentValue: poolContext.GenericRun(pools.MoveSpeedPercentValue); break;
                case ValueEnum.MoveSpeedValue: poolContext.GenericRun(pools.MoveSpeedValue); break;
                case ValueEnum.RecoveryTimeReductionValue: poolContext.GenericRun(pools.RecoveryTimeReductionValue); break;
                case ValueEnum.ResistanceAllValue: poolContext.GenericRun(pools.ResistanceAllValue); break;
                case ValueEnum.ResourceCostsReductionValue: poolContext.GenericRun(pools.ResourceCostsReductionValue); break;
                case ValueEnum.ResourceRecoveryPerDamageTakenValue: poolContext.GenericRun(pools.ResourceRecoveryPerDamageTakenValue); break;
                case ValueEnum.SlowdownAnimationValue: poolContext.GenericRun(pools.SlowdownAnimationValue); break;
                case ValueEnum.SlowdownMoveValue: poolContext.GenericRun(pools.SlowdownMoveValue); break;
                case ValueEnum.StrengthValue: poolContext.GenericRun(pools.StrengthValue); break;
                case ValueEnum.VitalityPercentValue: poolContext.GenericRun(pools.VitalityPercentValue); break;
                case ValueEnum.VitalityValue: poolContext.GenericRun(pools.VitalityValue); break;
                case ValueEnum.VulnerabilityCriticalChanceValue: poolContext.GenericRun(pools.VulnerabilityCriticalChanceValue); break;
                case ValueEnum.WeaponAttackSpeedValue: poolContext.GenericRun(pools.WeaponAttackSpeedValue); break;
                case ValueEnum.ChargeCostValueActionLinkButton1: poolContext.GenericRun(pools.ChargeCostValueActionLinkButton1); break;
                case ValueEnum.ChargeCostValueActionLinkButton2: poolContext.GenericRun(pools.ChargeCostValueActionLinkButton2); break;
                case ValueEnum.ChargeCostValueActionLinkButton3: poolContext.GenericRun(pools.ChargeCostValueActionLinkButton3); break;
                case ValueEnum.ChargeCostValueActionLinkButton4: poolContext.GenericRun(pools.ChargeCostValueActionLinkButton4); break;
                case ValueEnum.ChargeCostValueActionLinkMouseLeft: poolContext.GenericRun(pools.ChargeCostValueActionLinkMouseLeft); break;
                case ValueEnum.ChargeCostValueActionLinkMouseRight: poolContext.GenericRun(pools.ChargeCostValueActionLinkMouseRight); break;
                case ValueEnum.ChargeCostValueHealingPotionValue: poolContext.GenericRun(pools.ChargeCostValueHealingPotionValue); break;
                case ValueEnum.ChargeMaxValueActionLinkButton1: poolContext.GenericRun(pools.ChargeMaxValueActionLinkButton1); break;
                case ValueEnum.ChargeMaxValueActionLinkButton2: poolContext.GenericRun(pools.ChargeMaxValueActionLinkButton2); break;
                case ValueEnum.ChargeMaxValueActionLinkButton3: poolContext.GenericRun(pools.ChargeMaxValueActionLinkButton3); break;
                case ValueEnum.ChargeMaxValueActionLinkButton4: poolContext.GenericRun(pools.ChargeMaxValueActionLinkButton4); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseLeft: poolContext.GenericRun(pools.ChargeMaxValueActionLinkMouseLeft); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseRight: poolContext.GenericRun(pools.ChargeMaxValueActionLinkMouseRight); break;
                case ValueEnum.ChargeMaxValueHealingPotionValue: poolContext.GenericRun(pools.ChargeMaxValueHealingPotionValue); break;
                case ValueEnum.ChargeValueActionLinkButton1: poolContext.GenericRun(pools.ChargeValueActionLinkButton1); break;
                case ValueEnum.ChargeValueActionLinkButton2: poolContext.GenericRun(pools.ChargeValueActionLinkButton2); break;
                case ValueEnum.ChargeValueActionLinkButton3: poolContext.GenericRun(pools.ChargeValueActionLinkButton3); break;
                case ValueEnum.ChargeValueActionLinkButton4: poolContext.GenericRun(pools.ChargeValueActionLinkButton4); break;
                case ValueEnum.ChargeValueActionLinkMouseLeft: poolContext.GenericRun(pools.ChargeValueActionLinkMouseLeft); break;
                case ValueEnum.ChargeValueActionLinkMouseRight: poolContext.GenericRun(pools.ChargeValueActionLinkMouseRight); break;
                case ValueEnum.ChargeValueHealingPotionValue: poolContext.GenericRun(pools.ChargeValueHealingPotionValue); break;
                case ValueEnum.CooldownValueActionLinkButton1: poolContext.GenericRun(pools.CooldownValueActionLinkButton1); break;
                case ValueEnum.CooldownValueActionLinkButton2: poolContext.GenericRun(pools.CooldownValueActionLinkButton2); break;
                case ValueEnum.CooldownValueActionLinkButton3: poolContext.GenericRun(pools.CooldownValueActionLinkButton3); break;
                case ValueEnum.CooldownValueActionLinkButton4: poolContext.GenericRun(pools.CooldownValueActionLinkButton4); break;
                case ValueEnum.CooldownValueActionLinkMouseLeft: poolContext.GenericRun(pools.CooldownValueActionLinkMouseLeft); break;
                case ValueEnum.CooldownValueActionLinkMouseRight: poolContext.GenericRun(pools.CooldownValueActionLinkMouseRight); break;
                case ValueEnum.CooldownValueHealingPotionValue: poolContext.GenericRun(pools.CooldownValueHealingPotionValue); break;
                case ValueEnum.CostValueActionLinkButton1: poolContext.GenericRun(pools.CostValueActionLinkButton1); break;
                case ValueEnum.CostValueActionLinkButton2: poolContext.GenericRun(pools.CostValueActionLinkButton2); break;
                case ValueEnum.CostValueActionLinkButton3: poolContext.GenericRun(pools.CostValueActionLinkButton3); break;
                case ValueEnum.CostValueActionLinkButton4: poolContext.GenericRun(pools.CostValueActionLinkButton4); break;
                case ValueEnum.CostValueActionLinkMouseLeft: poolContext.GenericRun(pools.CostValueActionLinkMouseLeft); break;
                case ValueEnum.CostValueActionLinkMouseRight: poolContext.GenericRun(pools.CostValueActionLinkMouseRight); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkButton1); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkButton2); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkButton3); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkButton4); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkMouseLeft); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueActionLinkMouseRight); break;
                case ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue: poolContext.GenericRun(pools.ResourceRecoveryPerHitValueHealingPotionValue); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkButton1); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkButton2); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkButton3); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkButton4); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkMouseLeft); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueActionLinkMouseRight); break;
                case ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue: poolContext.GenericRun(pools.ResourceRecoveryPerUsingValueHealingPotionValue); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }

        public static void CallGenericValue<T>(ref T valueContext, ValueEnum @enum) where T : IValueContext
        {
            switch(@enum)
            {
                case ValueEnum.ActionCostPerSecondValue: valueContext.GenericValueRun<ActionCostPerSecondValueComponent>(); break;
                case ValueEnum.AddScoreOnDeathValue: valueContext.GenericValueRun<AddScoreOnDeathValueComponent>(); break;
                case ValueEnum.ArmorPercentValue: valueContext.GenericValueRun<ArmorPercentValueComponent>(); break;
                case ValueEnum.ArmorPropertyValue: valueContext.GenericValueRun<ArmorPropertyValueComponent>(); break;
                case ValueEnum.ArmorValue: valueContext.GenericValueRun<ArmorValueComponent>(); break;
                case ValueEnum.AttackSpeedPercentValue: valueContext.GenericValueRun<AttackSpeedPercentValueComponent>(); break;
                case ValueEnum.AttackSpeedValue: valueContext.GenericValueRun<AttackSpeedValueComponent>(); break;
                case ValueEnum.BarbDamageBashValue: valueContext.GenericValueRun<BarbDamageBashValueComponent>(); break;
                case ValueEnum.BarbDamageCleaveValue: valueContext.GenericValueRun<BarbDamageCleaveValueComponent>(); break;
                case ValueEnum.BarbDamageFrenzyValue: valueContext.GenericValueRun<BarbDamageFrenzyValueComponent>(); break;
                case ValueEnum.BarbDamageHammerOfTheAncientsValue: valueContext.GenericValueRun<BarbDamageHammerOfTheAncientsValueComponent>(); break;
                case ValueEnum.BarbDamageOverPowerValue: valueContext.GenericValueRun<BarbDamageOverPowerValueComponent>(); break;
                case ValueEnum.BarbDamageRendValue: valueContext.GenericValueRun<BarbDamageRendValueComponent>(); break;
                case ValueEnum.BarbDamageRevengeValue: valueContext.GenericValueRun<BarbDamageRevengeValueComponent>(); break;
                case ValueEnum.BarbDamageSeismicSlamValue: valueContext.GenericValueRun<BarbDamageSeismicSlamValueComponent>(); break;
                case ValueEnum.BarbDamageWeaponThrowValue: valueContext.GenericValueRun<BarbDamageWeaponThrowValueComponent>(); break;
                case ValueEnum.BarbDamageWhirlwindValue: valueContext.GenericValueRun<BarbDamageWhirlwindValueComponent>(); break;
                case ValueEnum.BarbFrenzyStackValue: valueContext.GenericValueRun<BarbFrenzyStackValueComponent>(); break;
                case ValueEnum.BlockAmountMaxValue: valueContext.GenericValueRun<BlockAmountMaxValueComponent>(); break;
                case ValueEnum.BlockAmountMinValue: valueContext.GenericValueRun<BlockAmountMinValueComponent>(); break;
                case ValueEnum.BlockChanceValue: valueContext.GenericValueRun<BlockChanceValueComponent>(); break;
                case ValueEnum.CriticalChanceValue: valueContext.GenericValueRun<CriticalChanceValueComponent>(); break;
                case ValueEnum.CriticalDamageValue: valueContext.GenericValueRun<CriticalDamageValueComponent>(); break;
                case ValueEnum.DamageColdValue: valueContext.GenericValueRun<DamageColdValueComponent>(); break;
                case ValueEnum.DamageFireValue: valueContext.GenericValueRun<DamageFireValueComponent>(); break;
                case ValueEnum.DamageLightningValue: valueContext.GenericValueRun<DamageLightningValueComponent>(); break;
                case ValueEnum.DamageMaxValue: valueContext.GenericValueRun<DamageMaxValueComponent>(); break;
                case ValueEnum.DamageMinValue: valueContext.GenericValueRun<DamageMinValueComponent>(); break;
                case ValueEnum.DamagePercentValue: valueContext.GenericValueRun<DamagePercentValueComponent>(); break;
                case ValueEnum.DamagePhysicalValue: valueContext.GenericValueRun<DamagePhysicalValueComponent>(); break;
                case ValueEnum.DamagePropertyMaxValue: valueContext.GenericValueRun<DamagePropertyMaxValueComponent>(); break;
                case ValueEnum.DamagePropertyMinValue: valueContext.GenericValueRun<DamagePropertyMinValueComponent>(); break;
                case ValueEnum.DamageReflectionPercentValue: valueContext.GenericValueRun<DamageReflectionPercentValueComponent>(); break;
                case ValueEnum.DamageValue: valueContext.GenericValueRun<DamageValueComponent>(); break;
                case ValueEnum.DexterityValue: valueContext.GenericValueRun<DexterityValueComponent>(); break;
                case ValueEnum.EnergyMaxValue: valueContext.GenericValueRun<EnergyMaxValueComponent>(); break;
                case ValueEnum.EnergyValue: valueContext.GenericValueRun<EnergyValueComponent>(); break;
                case ValueEnum.EvasionValue: valueContext.GenericValueRun<EvasionValueComponent>(); break;
                case ValueEnum.ExperienceValue: valueContext.GenericValueRun<ExperienceValueComponent>(); break;
                case ValueEnum.ExplosionScaleValue: valueContext.GenericValueRun<ExplosionScaleValueComponent>(); break;
                case ValueEnum.ExtraGoldWhenKillingValue: valueContext.GenericValueRun<ExtraGoldWhenKillingValueComponent>(); break;
                case ValueEnum.HealingPercentPerSecondValue: valueContext.GenericValueRun<HealingPercentPerSecondValueComponent>(); break;
                case ValueEnum.HealingPerHitValue: valueContext.GenericValueRun<HealingPerHitValueComponent>(); break;
                case ValueEnum.HealingPerSecondValue: valueContext.GenericValueRun<HealingPerSecondValueComponent>(); break;
                case ValueEnum.HealingPotionValue: valueContext.GenericValueRun<HealingPotionValueComponent>(); break;
                case ValueEnum.HitPointMaxValue: valueContext.GenericValueRun<HitPointMaxValueComponent>(); break;
                case ValueEnum.HitPointPercentValue: valueContext.GenericValueRun<HitPointPercentValueComponent>(); break;
                case ValueEnum.HitPointValue: valueContext.GenericValueRun<HitPointValueComponent>(); break;
                case ValueEnum.IncomingDamagePercentValue: valueContext.GenericValueRun<IncomingDamagePercentValueComponent>(); break;
                case ValueEnum.IntelligenceValue: valueContext.GenericValueRun<IntelligenceValueComponent>(); break;
                case ValueEnum.LevelValue: valueContext.GenericValueRun<LevelValueComponent>(); break;
                case ValueEnum.ManaPointMaxValue: valueContext.GenericValueRun<ManaPointMaxValueComponent>(); break;
                case ValueEnum.ManaPointRecoveryValue: valueContext.GenericValueRun<ManaPointRecoveryValueComponent>(); break;
                case ValueEnum.ManaPointValue: valueContext.GenericValueRun<ManaPointValueComponent>(); break;
                case ValueEnum.MoveSpeedPercentValue: valueContext.GenericValueRun<MoveSpeedPercentValueComponent>(); break;
                case ValueEnum.MoveSpeedValue: valueContext.GenericValueRun<MoveSpeedValueComponent>(); break;
                case ValueEnum.RecoveryTimeReductionValue: valueContext.GenericValueRun<RecoveryTimeReductionValueComponent>(); break;
                case ValueEnum.ResistanceAllValue: valueContext.GenericValueRun<ResistanceAllValueComponent>(); break;
                case ValueEnum.ResourceCostsReductionValue: valueContext.GenericValueRun<ResourceCostsReductionValueComponent>(); break;
                case ValueEnum.ResourceRecoveryPerDamageTakenValue: valueContext.GenericValueRun<ResourceRecoveryPerDamageTakenValueComponent>(); break;
                case ValueEnum.SlowdownAnimationValue: valueContext.GenericValueRun<SlowdownAnimationValueComponent>(); break;
                case ValueEnum.SlowdownMoveValue: valueContext.GenericValueRun<SlowdownMoveValueComponent>(); break;
                case ValueEnum.StrengthValue: valueContext.GenericValueRun<StrengthValueComponent>(); break;
                case ValueEnum.VitalityPercentValue: valueContext.GenericValueRun<VitalityPercentValueComponent>(); break;
                case ValueEnum.VitalityValue: valueContext.GenericValueRun<VitalityValueComponent>(); break;
                case ValueEnum.VulnerabilityCriticalChanceValue: valueContext.GenericValueRun<VulnerabilityCriticalChanceValueComponent>(); break;
                case ValueEnum.WeaponAttackSpeedValue: valueContext.GenericValueRun<WeaponAttackSpeedValueComponent>(); break;
                case ValueEnum.ChargeCostValueActionLinkButton1: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.ChargeCostValueActionLinkButton2: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.ChargeCostValueActionLinkButton3: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.ChargeCostValueActionLinkButton4: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.ChargeCostValueActionLinkMouseLeft: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.ChargeCostValueActionLinkMouseRight: valueContext.GenericValueRun<ChargeCostValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ChargeCostValueHealingPotionValue: valueContext.GenericValueRun<ChargeCostValueComponent<HealingPotionValueComponent>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkButton1: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkButton2: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkButton3: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkButton4: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseLeft: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.ChargeMaxValueActionLinkMouseRight: valueContext.GenericValueRun<ChargeMaxValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ChargeMaxValueHealingPotionValue: valueContext.GenericValueRun<ChargeMaxValueComponent<HealingPotionValueComponent>>(); break;
                case ValueEnum.ChargeValueActionLinkButton1: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.ChargeValueActionLinkButton2: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.ChargeValueActionLinkButton3: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.ChargeValueActionLinkButton4: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.ChargeValueActionLinkMouseLeft: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.ChargeValueActionLinkMouseRight: valueContext.GenericValueRun<ChargeValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ChargeValueHealingPotionValue: valueContext.GenericValueRun<ChargeValueComponent<HealingPotionValueComponent>>(); break;
                case ValueEnum.CooldownValueActionLinkButton1: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.CooldownValueActionLinkButton2: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.CooldownValueActionLinkButton3: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.CooldownValueActionLinkButton4: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.CooldownValueActionLinkMouseLeft: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.CooldownValueActionLinkMouseRight: valueContext.GenericValueRun<CooldownValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.CooldownValueHealingPotionValue: valueContext.GenericValueRun<CooldownValueComponent<HealingPotionValueComponent>>(); break;
                case ValueEnum.CostValueActionLinkButton1: valueContext.GenericValueRun<CostValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.CostValueActionLinkButton2: valueContext.GenericValueRun<CostValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.CostValueActionLinkButton3: valueContext.GenericValueRun<CostValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.CostValueActionLinkButton4: valueContext.GenericValueRun<CostValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.CostValueActionLinkMouseLeft: valueContext.GenericValueRun<CostValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.CostValueActionLinkMouseRight: valueContext.GenericValueRun<CostValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue: valueContext.GenericValueRun<ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>>(); break;
                case ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue: valueContext.GenericValueRun<ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>>(); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueEnum GetEnumByType<T>() where T : struct, IValue => 
            ValueEnumCache<T>.Value;
    }
    
    public static class ValueEnumCache<T> where T : struct, IValue
    {
        public static readonly ValueEnum Value = GetValueEnum();
        
        private static ValueEnum GetValueEnum()
        {
            if (typeof(T) == typeof(ActionCostPerSecondValueComponent)) return ValueEnum.ActionCostPerSecondValue;
            if (typeof(T) == typeof(AddScoreOnDeathValueComponent)) return ValueEnum.AddScoreOnDeathValue;
            if (typeof(T) == typeof(ArmorPercentValueComponent)) return ValueEnum.ArmorPercentValue;
            if (typeof(T) == typeof(ArmorPropertyValueComponent)) return ValueEnum.ArmorPropertyValue;
            if (typeof(T) == typeof(ArmorValueComponent)) return ValueEnum.ArmorValue;
            if (typeof(T) == typeof(AttackSpeedPercentValueComponent)) return ValueEnum.AttackSpeedPercentValue;
            if (typeof(T) == typeof(AttackSpeedValueComponent)) return ValueEnum.AttackSpeedValue;
            if (typeof(T) == typeof(BarbDamageBashValueComponent)) return ValueEnum.BarbDamageBashValue;
            if (typeof(T) == typeof(BarbDamageCleaveValueComponent)) return ValueEnum.BarbDamageCleaveValue;
            if (typeof(T) == typeof(BarbDamageFrenzyValueComponent)) return ValueEnum.BarbDamageFrenzyValue;
            if (typeof(T) == typeof(BarbDamageHammerOfTheAncientsValueComponent)) return ValueEnum.BarbDamageHammerOfTheAncientsValue;
            if (typeof(T) == typeof(BarbDamageOverPowerValueComponent)) return ValueEnum.BarbDamageOverPowerValue;
            if (typeof(T) == typeof(BarbDamageRendValueComponent)) return ValueEnum.BarbDamageRendValue;
            if (typeof(T) == typeof(BarbDamageRevengeValueComponent)) return ValueEnum.BarbDamageRevengeValue;
            if (typeof(T) == typeof(BarbDamageSeismicSlamValueComponent)) return ValueEnum.BarbDamageSeismicSlamValue;
            if (typeof(T) == typeof(BarbDamageWeaponThrowValueComponent)) return ValueEnum.BarbDamageWeaponThrowValue;
            if (typeof(T) == typeof(BarbDamageWhirlwindValueComponent)) return ValueEnum.BarbDamageWhirlwindValue;
            if (typeof(T) == typeof(BarbFrenzyStackValueComponent)) return ValueEnum.BarbFrenzyStackValue;
            if (typeof(T) == typeof(BlockAmountMaxValueComponent)) return ValueEnum.BlockAmountMaxValue;
            if (typeof(T) == typeof(BlockAmountMinValueComponent)) return ValueEnum.BlockAmountMinValue;
            if (typeof(T) == typeof(BlockChanceValueComponent)) return ValueEnum.BlockChanceValue;
            if (typeof(T) == typeof(CriticalChanceValueComponent)) return ValueEnum.CriticalChanceValue;
            if (typeof(T) == typeof(CriticalDamageValueComponent)) return ValueEnum.CriticalDamageValue;
            if (typeof(T) == typeof(DamageColdValueComponent)) return ValueEnum.DamageColdValue;
            if (typeof(T) == typeof(DamageFireValueComponent)) return ValueEnum.DamageFireValue;
            if (typeof(T) == typeof(DamageLightningValueComponent)) return ValueEnum.DamageLightningValue;
            if (typeof(T) == typeof(DamageMaxValueComponent)) return ValueEnum.DamageMaxValue;
            if (typeof(T) == typeof(DamageMinValueComponent)) return ValueEnum.DamageMinValue;
            if (typeof(T) == typeof(DamagePercentValueComponent)) return ValueEnum.DamagePercentValue;
            if (typeof(T) == typeof(DamagePhysicalValueComponent)) return ValueEnum.DamagePhysicalValue;
            if (typeof(T) == typeof(DamagePropertyMaxValueComponent)) return ValueEnum.DamagePropertyMaxValue;
            if (typeof(T) == typeof(DamagePropertyMinValueComponent)) return ValueEnum.DamagePropertyMinValue;
            if (typeof(T) == typeof(DamageReflectionPercentValueComponent)) return ValueEnum.DamageReflectionPercentValue;
            if (typeof(T) == typeof(DamageValueComponent)) return ValueEnum.DamageValue;
            if (typeof(T) == typeof(DexterityValueComponent)) return ValueEnum.DexterityValue;
            if (typeof(T) == typeof(EnergyMaxValueComponent)) return ValueEnum.EnergyMaxValue;
            if (typeof(T) == typeof(EnergyValueComponent)) return ValueEnum.EnergyValue;
            if (typeof(T) == typeof(EvasionValueComponent)) return ValueEnum.EvasionValue;
            if (typeof(T) == typeof(ExperienceValueComponent)) return ValueEnum.ExperienceValue;
            if (typeof(T) == typeof(ExplosionScaleValueComponent)) return ValueEnum.ExplosionScaleValue;
            if (typeof(T) == typeof(ExtraGoldWhenKillingValueComponent)) return ValueEnum.ExtraGoldWhenKillingValue;
            if (typeof(T) == typeof(HealingPercentPerSecondValueComponent)) return ValueEnum.HealingPercentPerSecondValue;
            if (typeof(T) == typeof(HealingPerHitValueComponent)) return ValueEnum.HealingPerHitValue;
            if (typeof(T) == typeof(HealingPerSecondValueComponent)) return ValueEnum.HealingPerSecondValue;
            if (typeof(T) == typeof(HealingPotionValueComponent)) return ValueEnum.HealingPotionValue;
            if (typeof(T) == typeof(HitPointMaxValueComponent)) return ValueEnum.HitPointMaxValue;
            if (typeof(T) == typeof(HitPointPercentValueComponent)) return ValueEnum.HitPointPercentValue;
            if (typeof(T) == typeof(HitPointValueComponent)) return ValueEnum.HitPointValue;
            if (typeof(T) == typeof(IncomingDamagePercentValueComponent)) return ValueEnum.IncomingDamagePercentValue;
            if (typeof(T) == typeof(IntelligenceValueComponent)) return ValueEnum.IntelligenceValue;
            if (typeof(T) == typeof(LevelValueComponent)) return ValueEnum.LevelValue;
            if (typeof(T) == typeof(ManaPointMaxValueComponent)) return ValueEnum.ManaPointMaxValue;
            if (typeof(T) == typeof(ManaPointRecoveryValueComponent)) return ValueEnum.ManaPointRecoveryValue;
            if (typeof(T) == typeof(ManaPointValueComponent)) return ValueEnum.ManaPointValue;
            if (typeof(T) == typeof(MoveSpeedPercentValueComponent)) return ValueEnum.MoveSpeedPercentValue;
            if (typeof(T) == typeof(MoveSpeedValueComponent)) return ValueEnum.MoveSpeedValue;
            if (typeof(T) == typeof(RecoveryTimeReductionValueComponent)) return ValueEnum.RecoveryTimeReductionValue;
            if (typeof(T) == typeof(ResistanceAllValueComponent)) return ValueEnum.ResistanceAllValue;
            if (typeof(T) == typeof(ResourceCostsReductionValueComponent)) return ValueEnum.ResourceCostsReductionValue;
            if (typeof(T) == typeof(ResourceRecoveryPerDamageTakenValueComponent)) return ValueEnum.ResourceRecoveryPerDamageTakenValue;
            if (typeof(T) == typeof(SlowdownAnimationValueComponent)) return ValueEnum.SlowdownAnimationValue;
            if (typeof(T) == typeof(SlowdownMoveValueComponent)) return ValueEnum.SlowdownMoveValue;
            if (typeof(T) == typeof(StrengthValueComponent)) return ValueEnum.StrengthValue;
            if (typeof(T) == typeof(VitalityPercentValueComponent)) return ValueEnum.VitalityPercentValue;
            if (typeof(T) == typeof(VitalityValueComponent)) return ValueEnum.VitalityValue;
            if (typeof(T) == typeof(VulnerabilityCriticalChanceValueComponent)) return ValueEnum.VulnerabilityCriticalChanceValue;
            if (typeof(T) == typeof(WeaponAttackSpeedValueComponent)) return ValueEnum.WeaponAttackSpeedValue;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkButton1Component>)) return ValueEnum.ChargeCostValueActionLinkButton1;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkButton2Component>)) return ValueEnum.ChargeCostValueActionLinkButton2;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkButton3Component>)) return ValueEnum.ChargeCostValueActionLinkButton3;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkButton4Component>)) return ValueEnum.ChargeCostValueActionLinkButton4;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.ChargeCostValueActionLinkMouseLeft;
            if (typeof(T) == typeof(ChargeCostValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.ChargeCostValueActionLinkMouseRight;
            if (typeof(T) == typeof(ChargeCostValueComponent<HealingPotionValueComponent>)) return ValueEnum.ChargeCostValueHealingPotionValue;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkButton1Component>)) return ValueEnum.ChargeMaxValueActionLinkButton1;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkButton2Component>)) return ValueEnum.ChargeMaxValueActionLinkButton2;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkButton3Component>)) return ValueEnum.ChargeMaxValueActionLinkButton3;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkButton4Component>)) return ValueEnum.ChargeMaxValueActionLinkButton4;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.ChargeMaxValueActionLinkMouseLeft;
            if (typeof(T) == typeof(ChargeMaxValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.ChargeMaxValueActionLinkMouseRight;
            if (typeof(T) == typeof(ChargeMaxValueComponent<HealingPotionValueComponent>)) return ValueEnum.ChargeMaxValueHealingPotionValue;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkButton1Component>)) return ValueEnum.ChargeValueActionLinkButton1;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkButton2Component>)) return ValueEnum.ChargeValueActionLinkButton2;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkButton3Component>)) return ValueEnum.ChargeValueActionLinkButton3;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkButton4Component>)) return ValueEnum.ChargeValueActionLinkButton4;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.ChargeValueActionLinkMouseLeft;
            if (typeof(T) == typeof(ChargeValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.ChargeValueActionLinkMouseRight;
            if (typeof(T) == typeof(ChargeValueComponent<HealingPotionValueComponent>)) return ValueEnum.ChargeValueHealingPotionValue;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkButton1Component>)) return ValueEnum.CooldownValueActionLinkButton1;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkButton2Component>)) return ValueEnum.CooldownValueActionLinkButton2;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkButton3Component>)) return ValueEnum.CooldownValueActionLinkButton3;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkButton4Component>)) return ValueEnum.CooldownValueActionLinkButton4;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.CooldownValueActionLinkMouseLeft;
            if (typeof(T) == typeof(CooldownValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.CooldownValueActionLinkMouseRight;
            if (typeof(T) == typeof(CooldownValueComponent<HealingPotionValueComponent>)) return ValueEnum.CooldownValueHealingPotionValue;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkButton1Component>)) return ValueEnum.CostValueActionLinkButton1;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkButton2Component>)) return ValueEnum.CostValueActionLinkButton2;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkButton3Component>)) return ValueEnum.CostValueActionLinkButton3;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkButton4Component>)) return ValueEnum.CostValueActionLinkButton4;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.CostValueActionLinkMouseLeft;
            if (typeof(T) == typeof(CostValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.CostValueActionLinkMouseRight;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkButton1Component>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkButton1;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkButton2Component>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkButton2;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkButton3Component>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkButton3;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkButton4Component>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkButton4;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseLeft;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.ResourceRecoveryPerHitValueActionLinkMouseRight;
            if (typeof(T) == typeof(ResourceRecoveryPerHitValueComponent<HealingPotionValueComponent>)) return ValueEnum.ResourceRecoveryPerHitValueHealingPotionValue;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkButton1Component>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton1;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkButton2Component>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton2;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkButton3Component>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton3;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkButton4Component>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkButton4;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkMouseLeftComponent>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseLeft;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<ActionLinkMouseRightComponent>)) return ValueEnum.ResourceRecoveryPerUsingValueActionLinkMouseRight;
            if (typeof(T) == typeof(ResourceRecoveryPerUsingValueComponent<HealingPotionValueComponent>)) return ValueEnum.ResourceRecoveryPerUsingValueHealingPotionValue;
            
            throw new ArgumentException($"Unknown value type: {typeof(T)}");
        }
    }
}
using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionCostPerSecondValueComponent : IValue
    {
        public float value { get; set; }
    }
    
    [Serializable]
    public struct ArmorValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ArmorPercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ArmorPropertyValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct AttackSpeedPercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct AttackSpeedValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ExperienceValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct BlockAmountMaxValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BlockAmountMinValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BlockChanceValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct CriticalChanceValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct CriticalDamageValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamageMaxValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamageMinValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamagePropertyMaxValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamagePropertyMinValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamagePercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamageReflectionPercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamageValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamagePhysicalValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamageColdValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamageFireValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamageLightningValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DexterityValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct EnergyMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct EnergyValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct EvasionValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ExplosionScaleValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ExtraGoldWhenKillingValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct HealingPerHitValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HealingPerSecondValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct HealingPercentPerSecondValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct HealingPotionValueComponent : IValue, IAbilityComponent, IGenCooldownValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointPercentValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct IntelligenceValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct IncomingDamagePercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct LevelValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ManaPointMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ManaPointRecoveryValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ManaPointValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct MoveSpeedPercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct MoveSpeedValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct RecoveryTimeReductionValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ResistanceAllValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ResourceCostsReductionValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ResourceRecoveryPerDamageTakenValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct SlowdownMoveValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct SlowdownAnimationValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct StrengthValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct VitalityPercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct VitalityValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct VulnerabilityCriticalChanceValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct WeaponAttackSpeedValueComponent : IValue
    {
        public float value { get; set; }
    }

    //---------------------------------------------

    [Serializable]
    public struct CooldownValueComponent<T> : IValue, ICooldownComponent
    {
        public float value { get; set; }
        public float startTime { get; set; }
        public float lastChargeAddTime { get; set; }
    }

    [Serializable]
    public struct CostValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    public struct ChargeValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    public struct ChargeMaxValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    public struct ChargeCostValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ResourceRecoveryPerUsingValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ResourceRecoveryPerHitValueComponent<T> : IValue
    {
        public float value { get; set; }
    }

    //Интерфейс маркер по которому кодогенерация (GenActionValueLinks) сопоставит дженерик компоненты значений
    //с компонентом носящим интерфейс
    public interface IGenValue
    {
    }

    public interface IGenCooldownValue : IGenValue
    {
    }

    public interface IGenCostValue : IGenValue
    {
    }
}
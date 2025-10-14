using Core.Generated;
using Core.Lib;
using Unity.Mathematics;

namespace Core.Components
{
    public static class ValueFormatUtility
    {
        private static readonly Glossary<Glossary<string>> _cache = new();

        public static string Format(ValueEnum valueEnum, float value)
        {
            if (!_cache.TryGetValue((int)valueEnum, out var glossary))
                _cache.Add((int)valueEnum, glossary = new());

            var key = (int)math.round(value * 1000);
            if (glossary.TryGetValue(key, out var v))
                return v;

            v = valueEnum switch
            {
                ValueEnum.AddScoreOnDeathValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.ArmorValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.AttackSpeedValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.CriticalChanceValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.CriticalDamageValue => FormatFloatToStringUtility.ToPercentInt(value) + "%",
                ValueEnum.DamagePercentValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.DamageValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.EnergyMaxValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.EnergyValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.ExperienceValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.ExtraGoldWhenKillingValue => FormatFloatToStringUtility.ToPercentInt(value) + "%",
                ValueEnum.HealingPotionValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.HealingPerSecondValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.HitPointMaxValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.HitPointPercentValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.HitPointValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.ManaPointMaxValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.ManaPointValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.MoveSpeedValue => FormatFloatToStringUtility.ToPercentInt(value) + "%",
                ValueEnum.RecoveryTimeReductionValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.ResourceCostsReductionValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.ResistanceAllValue => FormatFloatToStringUtility.ToInt(value),
                ValueEnum.VitalityPercentValue => FormatFloatToStringUtility.FloatDim1(value * 100) + "%",
                ValueEnum.WeaponAttackSpeedValue => FormatFloatToStringUtility.FloatDim2(value),
                //
                ValueEnum.BarbDamageBashValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageCleaveValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageFrenzyValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageHammerOfTheAncientsValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageOverPowerValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageRendValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageRevengeValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageSeismicSlamValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageWeaponThrowValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.BarbDamageWhirlwindValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                //
                ValueEnum.DamagePhysicalValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.DamageColdValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.DamageFireValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                ValueEnum.DamageLightningValue => FormatFloatToStringUtility.ToInt(value * 100) + "%",
                _ => FormatFloatToStringUtility.ToInt(value)
            };

            glossary.Add(key, v);
            return v;
        }

        public static string Format<T>(T component)
            where T : struct, IValue =>
            Format(ValuePoolsUtility.GetEnumByType<T>(), component.value);
    }
}
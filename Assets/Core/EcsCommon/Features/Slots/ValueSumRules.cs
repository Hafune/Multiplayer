using Core.Components;
using Core.Generated;
using System;
using System.Runtime.CompilerServices;
using Core.Lib;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    public static class ValueSumRules
    {
        private static readonly Glossary<Func<float, float, float>> _cache = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SumValueByRule<T>(float v1, float v2) where T : struct, IValue =>
            SumRuleCache<T>.Rule(v1, v2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SumValueByRule(ValueEnum @enum, float v1, float v2)
        {
            if (!_cache.TryGetValue((int)@enum, out var foo))
                _cache.Add((int)@enum, foo = GetSumRule(@enum));

            return foo(v1, v2);
        }

        private static Func<float, float, float> GetSumRule(ValueEnum vEnum) => vEnum switch
        {
            //Если это сокращения чего либо оно никогда не должно достигнуть 100%, скращается оставшаяся часть
            ValueEnum.ResourceCostsReductionValue or ValueEnum.RecoveryTimeReductionValue => (v1, v2) => v1 + (1 - v1) * v2,
            //Замедление движения
            ValueEnum.SlowdownMoveValue => (v1, v2) => math.min(v1 + v2, .8f),
            //Замедление заморозка
            ValueEnum.SlowdownAnimationValue => (v1, v2) => Mathf.Clamp01(math.max(v1, v2)),
            //Перемножение бонусов к урону
            ValueEnum.DamagePercentValue or ValueEnum.IncomingDamagePercentValue => (v1, v2) =>
            {
                if (v1 == 0)
                    return v2;
                
                return v1 * (1 + v2);
            },
            //Прочее
            _ => (v1, v2) => v1 + v2
        };

        private static class SumRuleCache<T> where T : struct, IValue
        {
            public static readonly Func<float, float, float> Rule = GetSumRule();

            private static Func<float, float, float> GetSumRule() => ValueSumRules.GetSumRule(ValuePoolsUtility.GetEnumByType<T>());
        }
    }
}
using System;
using Random = Unity.Mathematics.Random;

namespace Core.Lib
{
    public struct ProgressiveChance
    {
        private int failCount; // Количество неудач подряд

        public bool TryTrigger(Random random, float baseChance, float failMultiplier)
        {
            float currentChance = Math.Min(baseChance * (Math.Max(1, failCount) * failMultiplier), 1.0f);
            float roll = random.NextFloat();

            if (roll < currentChance)
            {
                failCount = 0; // Успех → сброс
                return true;
            }

            failCount++; // Неудача → увеличиваем счётчик
            return false;
        }

        public int GetFailCount() => failCount;
    }
}
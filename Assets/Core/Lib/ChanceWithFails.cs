using Random = UnityEngine.Random;

namespace Core.Lib
{
    public struct ChanceWithFails
    {
        private int failCount; // Количество неудач подряд

        public bool TryTrigger(float baseChance, float failMultiplier)
        {
            if (baseChance + baseChance * (failCount * failMultiplier) > Random.value)
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
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;
using RandomDefault = UnityEngine.Random;

namespace Lib
{
    public static class MyMathematicsRandomExtensions
    {
        public static Vector2 InsideUnitCircle(this ref Random r)
        {
            float angle = r.NextFloat(0, math.PI * 2f);
            float radius = math.sqrt(r.NextFloat()); // Корень нужен для равномерного распределения
            return new Vector2(math.cos(angle), math.sin(angle)) * radius;
        }

        public static float IndependentValue(this ref Random _) => RandomDefault.value;
        
        public static float NextFloatInclusive(this ref Random r, float min, float max) => min + r.NextFloat() * (max - min);
    }
}
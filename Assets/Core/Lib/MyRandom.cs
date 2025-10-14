using Lib;
using UnityEngine;
using MathematicsRandom = Unity.Mathematics.Random;
using Random = System.Random;

namespace Core.Lib
{
    public class MyRandom
    {
        private static readonly Random SystemRandom = new();
        private MathematicsRandom _random;

        public MyRandom(uint seed = 0) => SetupState(seed);
        public uint Seed { get; private set; }

        public MyRandom SetupState(uint seed = 0)
        {
            if (seed == 0)
                seed = (uint)(SystemRandom.NextDouble() * uint.MaxValue + 1);

            Seed = seed;
            _random.InitState(seed);
            return this;
        }

        public float IndependentValue() => _random.IndependentValue();

        public int NextInt(int min, int max) => _random.NextInt(min, max);
        public int NextInt(int max) => _random.NextInt(max);
        public int NextInt() => _random.NextInt();

        public float NextFloat() => _random.NextFloat();
        public float NextFloat(float value) => _random.NextFloat(value);
        public float NextFloat(float v1, float v2) => _random.NextFloat(v1, v2);

        public Vector2 InsideUnitCircle() => _random.InsideUnitCircle();
    }
}
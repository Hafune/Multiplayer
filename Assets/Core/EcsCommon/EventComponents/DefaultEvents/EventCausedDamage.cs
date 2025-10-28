using System.Runtime.CompilerServices;
using Core.Lib.Utils;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct EventCausedDamage : IEcsAutoReset<EventCausedDamage>
    {
        public float[] damages;
        public int[] targets;
        public int damagesCount;
        public int targetsCount;

        public void AutoReset(ref EventCausedDamage c)
        {
            c.damages ??= new float[1];
            c.damagesCount = 0;

            c.targets ??= new int[1];
            c.targetsCount = 0;
        }

        public void AddDamage(int target, float damage)
        {
            MyArrayUtility.Add(ref damages, ref damagesCount, damage);
            MyArrayUtility.Add(ref targets, ref targetsCount, target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new(ref this);

        public ref struct Enumerator
        {
            private readonly EventCausedDamage _data;
            private int _idx;

            public Enumerator(ref EventCausedDamage data)
            {
                _data = data;
                _idx = -1;
            }

            public (int, float) Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => (_data.targets[_idx], _data.damages[_idx]);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() => ++_idx < _data.targetsCount;
        }
    }
}
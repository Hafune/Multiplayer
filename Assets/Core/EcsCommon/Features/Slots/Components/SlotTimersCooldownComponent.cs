using System;
using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    [Serializable]
    public struct SlotTimersCooldownComponent : IEcsAutoReset<SlotTimersCooldownComponent>
    {
        public MyList<float> times;
        public MyList<short> ids;

        public void AutoReset(ref SlotTimersCooldownComponent c)
        {
            c.times ??= new();
            c.times.Clear();

            c.ids ??= new();
            c.ids.Clear();
        }
    }
}
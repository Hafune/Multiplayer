using System;
using Core.Components;

namespace Core
{
    [Serializable]
    public struct SlotTimerData
    {
        public float cooldown;
        public float duration;
        public SlotComponent value;
    }
}
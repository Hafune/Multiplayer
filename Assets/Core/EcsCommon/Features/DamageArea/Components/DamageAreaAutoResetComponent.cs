using System;
using Core.Lib;

namespace Core.Components
{
    [Serializable]
    public struct DamageAreaAutoResetComponent
    {
        public DamageArea area;
        public float delay;
        [NonSerialized] public float lastTime;
    }
}
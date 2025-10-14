using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct BarbRevengeComponent
    {
        [SerializeField] public float chargeChance;
        public Func<int, float> getCharge;
        public Func<int, float> getChargeMax;
        public Action<int, float> setCharge;
    }
}
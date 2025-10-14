using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct LifetimeComponent
    {
        public float currentTime { get; set; }
        [field: SerializeField] public float maxTime { get; set; }
    }
}
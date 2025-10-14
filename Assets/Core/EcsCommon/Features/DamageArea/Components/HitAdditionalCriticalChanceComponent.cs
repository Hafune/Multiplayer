using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct HitAdditionalCriticalChanceComponent
    {
        [field: SerializeField] public float value { get; set; }
    }
}
using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct EventRemoveTimer
    {
        [field: SerializeField] public float maxTime { get; set; }
    }
}
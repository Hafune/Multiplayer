using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionHitStunComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
        [field: NonSerialized] public float lastStartTime;
    }
}
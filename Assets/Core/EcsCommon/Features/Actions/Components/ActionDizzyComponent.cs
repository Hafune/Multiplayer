using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionDizzyComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
        [field: NonSerialized] public float duration;
        [field: NonSerialized] public float startTime;
    }
}
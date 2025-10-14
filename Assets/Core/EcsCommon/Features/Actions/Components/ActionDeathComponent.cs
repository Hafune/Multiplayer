using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionDeathComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }

        [NonSerialized] public Vector3 impactPosition;
        [NonSerialized] public Vector3 impactDirection;
        [NonSerialized] public float impactForce;
    }
}
using System;
using Core.ExternalEntityLogics;
using Core.Generated;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionReviveComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
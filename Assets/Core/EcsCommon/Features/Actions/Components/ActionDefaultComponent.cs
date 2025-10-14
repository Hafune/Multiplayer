using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionDefaultComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
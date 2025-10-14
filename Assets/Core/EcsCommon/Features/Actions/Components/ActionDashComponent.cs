using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionDashComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
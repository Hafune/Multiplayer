using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct NpcActionComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
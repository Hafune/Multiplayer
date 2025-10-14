using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionAttackComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
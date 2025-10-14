using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionDefaultAttackComponent
    {
        [field: SerializeField] public AbstractEntityActionStateful logic { get; set; }
    }
}
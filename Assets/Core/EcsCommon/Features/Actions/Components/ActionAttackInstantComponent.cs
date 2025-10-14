using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionAttackInstantComponent
    {
        [field: SerializeField] public AbstractEntityActionInstant logic { get; set; }
    }
}
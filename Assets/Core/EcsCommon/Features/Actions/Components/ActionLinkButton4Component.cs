using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionLinkButton4Component : IEntityActionComponent, IGenCooldownValue, IGenCostValue
    {
        [field: SerializeField] public AbstractEntityAction logic { get; set; }
    }
}
using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionLinkSpaceForwardComponent : IEntityActionComponent, IGenCooldownValue, IGenCostValue
    {
        [field: SerializeField] public AbstractEntityAction logic { get; set; }
    }
}
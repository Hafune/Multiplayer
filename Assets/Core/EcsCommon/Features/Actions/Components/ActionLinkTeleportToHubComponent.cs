using System;
using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ActionLinkTeleportToHubComponent : IEntityActionComponent
    {
        [field: SerializeField] public AbstractEntityAction logic { get; set; }
    }
}
using System;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AuraAreaComponent : IEcsAutoReset<AuraAreaComponent>
    {
        [field: SerializeField] public Area area { get; set; }

        public void AutoReset(ref AuraAreaComponent c) => c.area?.Init(c);
    }
}
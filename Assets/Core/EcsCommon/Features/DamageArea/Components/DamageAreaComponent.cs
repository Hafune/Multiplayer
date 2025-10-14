using System;
using Core.Lib;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct DamageAreaComponent : IEcsAutoReset<DamageAreaComponent>
    {
        [field: SerializeField] public DamageArea area { get; set; }

        public void AutoReset(ref DamageAreaComponent c) => c.area?.Init(c);
    }
}
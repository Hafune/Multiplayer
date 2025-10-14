using Core.Lib;
using DamageNumbersPro;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Components
{
    public struct EventIncomingDamage : IEcsAutoReset<EventIncomingDamage>
    {
        public MyList<(float damage, Vector3 triggerPoint, Vector3 ownerPosition, int owner, DamageNumber effect)> data;

        public void AutoReset(ref EventIncomingDamage c)
        {
            c.data ??= new();
            c.data.Clear();
        }
    }
}
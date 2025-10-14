using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct TargetResetByDelayComponent : IEcsAutoReset<TargetResetByDelayComponent>
    {
        public float delay;
        [NonSerialized] public float startTime;

        public void AutoReset(ref TargetResetByDelayComponent c) => c.startTime = Time.time;
    }
}
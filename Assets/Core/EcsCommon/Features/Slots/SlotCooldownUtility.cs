using System.Runtime.CompilerServices;
using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Core.Generated;
using UnityEngine;

namespace Core.EcsCommon.CompositionClasses
{
    public static class SlotCooldownUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnCooldown(int entity, SlotTimerData data, ComponentPools pools)
        {
            var id = SlotUtility.GetIdByKey(data.value.key);
            if (id == 0)
                return false;

            var cooldownComponent = pools.SlotTimersCooldown.Get(entity);
            var index = cooldownComponent.ids.IndexOf(id);

            if (index < 0)
                return false;

            return Time.time - cooldownComponent.times[index] < data.cooldown;
        }
    }
}
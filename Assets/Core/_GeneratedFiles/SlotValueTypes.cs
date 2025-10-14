//Файл генерируется в GenSlotValueTypes
using System;
using Leopotam.EcsLite;

namespace Core.Generated
{
    public static class SlotValueTypes
    {
        // @formatter:off
        public static IEcsPool GetSlotTagPool(SlotTagEnum @enum, ComponentPools pools) => @enum switch
        {
            SlotTagEnum.ThroughProjectileSlotTag => pools.ThroughProjectileSlotTag,
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };

        public static IEcsPool GetEventUpdatedSlotTagPool(SlotTagEnum @enum, ComponentPools pools) => @enum switch
        {
            SlotTagEnum.ThroughProjectileSlotTag => pools.EventUpdatedThroughProjectileSlotTag,
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };
        // @formatter:on
    }
}

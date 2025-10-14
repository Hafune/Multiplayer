using System.Collections.Generic;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class SlotTagSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems(Context context) => new IEcsSystem[]
        {
            // new ThroughProjectileSlotTagSystem(context),
        };
    }
}
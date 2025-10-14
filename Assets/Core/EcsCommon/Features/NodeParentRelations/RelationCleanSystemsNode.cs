using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public static class RelationCleanSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildRemoveWithRelationSystems(Context context) => new IEcsSystem[]
        {
            new RemoveWithRelationSystem<SlotNodeComponent, SlotTargetComponent, RemoveWithSlotTargetTag>(context),
            new RemoveWithRelationSystem<NodeComponent, ParentComponent, RemoveWithParentTag>(context),
            new RemoveWithRelationSystem<AimComponent, TargetComponent, RemoveWithTargetTag>(context)
        };
        
        public static IEnumerable<IEcsSystem> BuildCascadeCleanSystems(Context context) => new IEcsSystem[]
        {
            new CascadeCleanBeforeRemoveEntitySystem<SlotNodeComponent, SlotTargetComponent>(context),
            new CascadeCleanBeforeRemoveEntitySystem<NodeComponent, ParentComponent>(context),
            new CascadeCleanBeforeRemoveEntitySystem<AimComponent, TargetComponent>(context),
        };
    }
}
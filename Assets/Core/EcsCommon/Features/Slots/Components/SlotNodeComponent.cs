using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct SlotTargetComponent : IParentComponent
    {
        public int entity { get; set; }
    }

    public struct SlotNodeComponent : IEcsAutoReset<SlotNodeComponent>, INodeComponent<SlotTargetComponent>
    {
        public MyList<int> children { get; set; }
        public MyList<IEcsPool> tagPools;

        public void AutoReset(ref SlotNodeComponent c)
        {
            c.children ??= new();
            c.children.Clear();

            c.tagPools ??= new();
            c.tagPools.Clear();
        }
    }

    public struct RemoveWithSlotTargetTag
    {
    }
}
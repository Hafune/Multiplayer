using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct TargetComponent : IParentComponent
    {
        public int entity { get; set; }
    }

    public struct AimComponent : IEcsAutoReset<AimComponent>, INodeComponent<TargetComponent>
    {
        public MyList<int> children { get; set; }

        public void AutoReset(ref AimComponent c)
        {
            c.children ??= new();
            c.children.Clear();
        }
    }

    public struct RemoveWithTargetTag
    {
    }
}
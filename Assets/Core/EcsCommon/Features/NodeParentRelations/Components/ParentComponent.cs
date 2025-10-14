using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct ParentComponent : IParentComponent
    {
        public int entity { get; set; }
    }
    
    public struct NodeComponent : IEcsAutoReset<NodeComponent>, INodeComponent<ParentComponent>
    {
        public MyList<int> children  { get; set; }

        public void AutoReset(ref NodeComponent c)
        {
            c.children ??= new();
            c.children.Clear();
        }
    }
    
    public struct RemoveWithParentTag
    {
        
    }
}
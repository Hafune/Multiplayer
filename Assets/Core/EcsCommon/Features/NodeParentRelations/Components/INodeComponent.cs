using Core.Lib;

namespace Core.Components
{
    public interface INodeComponent<P> where P : struct, IParentComponent
    {
        public MyList<int> children { get; set; }
    }
}
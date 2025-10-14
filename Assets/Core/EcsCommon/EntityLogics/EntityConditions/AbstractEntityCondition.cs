using Lib;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractEntityCondition : MonoConstruct
    {
        public abstract bool Check(int entity);
    }
}
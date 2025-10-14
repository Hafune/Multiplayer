using Lib;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractEntityLogic : MonoConstruct
    {
        public abstract void Run(int entity);
    }
}
using Core.Components;

namespace Core.ExternalEntityLogics
{
    public interface IActionGenericLogic
    {
        void GenericRun<A>(int entity);
    }
    
    public interface IButtonGenericLogic
    {
        void GenericRun<A>(int entity) where A : struct, IButtonComponent;
    }
}
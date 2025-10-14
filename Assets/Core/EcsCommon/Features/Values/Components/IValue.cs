using Leopotam.EcsLite;

namespace Core.Components
{
    public interface IValue
    {
        public float value { get; set; }
    }

    public interface IBaseValue
    {
        public float baseValue { get; set; }
    }

    public interface IValueGenericPoolContext
    {
        void GenericRun<V>(EcsPool<V> pool) where V : struct, IValue;
    }
    
    public interface IValueContext
    {
        void GenericValueRun<V>() where V : struct, IValue;
    }
}
using Leopotam.EcsLite;
using Lib;

namespace Core.Lib
{
    public class BaseProvider<T> : AbstractBaseProvider where T : struct
    {
        private enum ValueType
        {
            Simple,
            Auto,
        }
        
        private T value;
        private EcsPool<T> _pool;
        private IEcsAutoReset<T> _iValue;
        private ValueType valueType = ValueType.Simple;
        private EcsWorld _world;

        public BaseProvider(T v) => value = v;

        public override void Attach(int entity, EcsWorld world)
        {
            if (_world != world)
                Init(world);

            if (valueType == ValueType.Auto)
                _iValue.AutoReset(ref value);

            _pool.GetOrInitialize(entity) = value;
        }

        public override void Del(int entity, EcsWorld world)
        {
            if (_world != world)
                Init(world);

            _pool.DelIfExist(entity);
        }

        public override bool Has(int entity, EcsWorld world)
        {
            if (_world != world)
                Init(world);

            return _pool.Has(entity);
        }

        private void Init(EcsWorld world)
        {
            _world = world;
            _pool = world.GetPool<T>();

            if (value is not IEcsAutoReset<T> iValue) 
                return;
            
            _iValue = iValue;
            valueType = ValueType.Auto;
        }
    }
}
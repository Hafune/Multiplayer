using UnityEngine;
using Leopotam.EcsLite;
using Lib;

namespace Core.Lib
{
    public abstract class MonoProvider<T> : BaseMonoProvider where T : struct
    {
        [SerializeField] protected T value;
        private EcsPool<T> _pool;
        private IEcsAutoReset<T> _iValue;
        private bool _isAutoReset;
        private EcsWorld _world;

        public override void Attach(int entity, EcsWorld world)
        {
            if (_world != world)
                Init(world);

            if (_isAutoReset)
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
            _isAutoReset = true;
        }
    }
}
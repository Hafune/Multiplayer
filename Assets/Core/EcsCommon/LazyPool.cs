using System;
using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using Reflex;

namespace Core.ExternalEntityLogics
{
    public struct LazyPool
    {
        private readonly EcsWorld _world;
        private IEcsPool _pool;
        private Type _lastType;

        public LazyPool(Context context)
        {
            _world = context.Resolve<EcsWorld>();
            _pool = null;
            _lastType = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EcsPool<T> GetPool<T>() where T : struct
        {
            if (ReferenceEquals(_lastType, typeof(T)))
                return (EcsPool<T>)_pool;

            _lastType = typeof(T);
            _pool = _world.GetPool<T>();
            return (EcsPool<T>)_pool;
        }

        public void Del(int i) => _pool.Del(i);
    }
}
using System;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class MyPool
    {
        private readonly Glossary<IPool> _pools = new();
        private readonly Glossary<IPool> _isolatedPools = new();
        private readonly Context _context;
        private readonly bool _dontDestroyOnLoad;

        public MyPool(Context context, bool dontDestroyOnLoad = false)
        {
            _context = context;
            _dontDestroyOnLoad = dontDestroyOnLoad;
        }

        public T GetInstanceByPrefab<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : Component
        {
#if UNITY_EDITOR
            CheckPrefab(prefab);
#endif
            var id = prefab.GetInstanceID();

            if (!_pools.TryGetValue(id, out var pool))
                _pools.Add(id, pool = new PrefabPool<T>(_context, prefab, _dontDestroyOnLoad));

            return (T)pool.GetInstanceUntyped(position, rotation, parent);
        }

        public PrefabPool<T> GetPullByPrefab<T>(T prefab) where T : Component
        {
            BuildPoolIfNotExist(prefab);
            return (PrefabPool<T>)_pools.GetValue(prefab.GetInstanceID());
        }

        internal PrefabPool<T> GetIsolatedPullByPrefab<T>(T prefab) where T : Component
        {
            var pool = new PrefabPool<T>(_context, prefab, _dontDestroyOnLoad);
            _isolatedPools.Add(prefab.GetInstanceID(), pool);
            return (PrefabPool<T>)_isolatedPools.GetValue(prefab.GetInstanceID());
        }

        public void ForceDisable()
        {
            foreach (var e in _pools)
                e.Value.ForceDisable();
            
            foreach (var e in _isolatedPools)
                e.Value.ForceDisable();
        }

        public void ReturnDisabledInContainer()
        {
            foreach (var e in _pools)
                e.Value.ReturnDisabledInContainer();
            
            foreach (var e in _isolatedPools)
                e.Value.ReturnDisabledInContainer();
        }

        public void Clear()
        {
            foreach (var e in _pools)
                e.Value.Dispose();

            _pools.Clear();
            
            foreach (var e in _isolatedPools)
                e.Value.Dispose();

            _isolatedPools.Clear();
        }

        private void BuildPoolIfNotExist<T>(T prefab) where T : Component
        {
#if UNITY_EDITOR
            CheckPrefab(prefab);
#endif

            if (_pools.ContainsKey(prefab.GetInstanceID()))
                return;

            var pool = new PrefabPool<T>(_context, prefab, _dontDestroyOnLoad);
            _pools.Add(prefab.GetInstanceID(), pool);
        }

        private static void CheckPrefab(Component c)
        {
            if (!c)
                throw new Exception("missing reference префаб");
        }
    }
}
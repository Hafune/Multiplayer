using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Core.Lib
{
    public readonly struct KeyValueCache<T, V>
    {
        private readonly Dictionary<T, V> _cache;
        private readonly Func<T, V> _getValue;

        public KeyValueCache(Func<T, V> getValue)
        {
            _getValue = getValue;
            _cache = new Dictionary<T, V>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public V Get(T key)
        {
            if (!_cache.TryGetValue(key, out var fullKey))
                _cache[key] = fullKey = _getValue(key);

            return fullKey;
        }
    }

    public readonly struct IntValueCache<V>
    {
        private readonly Glossary<V> _cache;
        private readonly Func<int, V> _getValue;

        public IntValueCache(Func<int, V> getValue)
        {
            _getValue = getValue;
            _cache = new Glossary<V>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public V Get(int key)
        {
            if (!_cache.TryGetValue(key, out var fullKey))
                _cache.Add(key, fullKey = _getValue(key));

            return fullKey;
        }

        public void Clear() => _cache.Clear();
    }
}
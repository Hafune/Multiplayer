using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Payload : IDisposable
    {
        private static readonly Stack<Payload> _pool = new();

        public static Payload GetPooled()
        {
            if (!_pool.TryPop(out var payload))
                return new Payload();

            payload._inPool = false;
            return payload;
        }

        private Payload _parent;
        private readonly List<Payload> _children = new();
        private object _key;
        private object _value;
        private bool _inPool;

        private Payload()
        {
        }

        public Payload With<T>(PropKey<T> key, T value)
        {
            var p = GetPooled();
            p._parent = this;
            p._key = key;
            p._value = value;
            _children.Add(p);

            return p;
        }

        public T Get<T>(PropKey<T> key)
        {
            for (var p = this; p != null; p = p._parent)
            {
#if UNITY_EDITOR
                p.DebugCheck();
#endif
                if (ReferenceEquals(p._key, key))
                    return (T)p._value;
            }

#if UNITY_EDITOR
            throw new NullReferenceException();
#endif
            Debug.LogError(nameof(NullReferenceException) + ": " + key);
            return default;
        }

        public bool TryGet<T>(PropKey<T> key, out T value)
        {
            for (var p = this; p != null; p = p._parent)
            {
#if UNITY_EDITOR
                p.DebugCheck();
#endif
                if (ReferenceEquals(p._key, key))
                {
                    value = (T)p._value;
                    return true;
                }
            }

            value = default;
            return false;
        }

        public void Dispose()
        {
            if (_inPool)
            {
                Debug.LogError("Дублирующий Dispose");
                return;
            }

            _parent = null;

            foreach (var child in _children)
                child.Dispose();

            _key = null;
            _value = null;
            _children.Clear();
            _pool.Push(this);
            _inPool = true;
        }

#if UNITY_EDITOR
        private void DebugCheck()
        {
            if (_inPool)
                throw new Exception($"{nameof(Payload)} не должен быть активен т.к. находится в пуле");
        }
#endif
    }
}
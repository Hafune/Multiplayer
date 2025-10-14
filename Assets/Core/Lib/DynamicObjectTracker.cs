using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Core.Lib
{
    public class DynamicObjectTracker<T> where T : class
    {
        private readonly T[] _components;
        private readonly BitArray _componentStates;
        private readonly int _componentCount;

        public DynamicObjectTracker(T[] components)
        {
            _components = components;
            _componentCount = _components.Length;
            _componentStates = new BitArray(_componentCount);

            for (var i = 0; i < _componentCount; i++)
            {
                var go = (_components[i] as MonoBehaviour)!.gameObject;
                _componentStates[i] = go.activeInHierarchy;

                var dispatcher = go.GetComponent<EnableDispatcher>() ?? go.AddComponent<EnableDispatcher>();
                var index = i;
                dispatcher.OnEnabled += () => _componentStates[index] = true;
                dispatcher.OnDisabled += () => _componentStates[index] = false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEachActive(Action<T> action)
        {
            for (var i = 0; i < _componentCount; i++)
                if (_componentStates[i])
                    action(_components[i]);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetFirstActive()
        {
            for (var i = 0; i < _componentCount; i++)
                if (_componentStates[i])
                    return _components[i];

            throw new Exception($"{nameof(DynamicObjectTracker<T>)} нет ни одного активного объекта");
        }
    }
} 
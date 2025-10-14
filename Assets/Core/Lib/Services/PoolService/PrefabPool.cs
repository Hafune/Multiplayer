using System;
using System.Collections.Generic;
using Core.Lib.Utils;
using Reflex;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Lib
{
    public class PrefabPool<T> : IPool where T : Component
    {
        private readonly T _prefab;
        private int _denseItemsCurrentCount;
        private T[] _denseItems;
        private int _denseItemsCount;
        private int[] _sparseItemIndexes;
        private int _sparseItemIndexesCount;
        private readonly Context _context;
        private readonly bool _dontDestroyOnLoad;
        private readonly GameObject _container;
        private bool _disposeInProgress;
        private static readonly HashSet<Material> _warmUpCache = new();

        internal PrefabPool(Context context, T prefab, bool dontDestroyOnLoad = false)
        {
            _denseItems = new T[1];
            _sparseItemIndexes = new int[1];
            _context = context;
            _prefab = prefab;
            _dontDestroyOnLoad = dontDestroyOnLoad;
            _container = new GameObject(_dontDestroyOnLoad
                ? "Pool_d: " + prefab.name
                : "Pool: " + prefab.name);

            Object.DontDestroyOnLoad(_container);
            
            // Создаем временный объект для поиска материалов
            var renderers = prefab.GetComponentsInChildren<Renderer>(true);

            foreach (var renderer in renderers.AsSpan())
            foreach (var material in renderer.sharedMaterials.AsSpan())
                if (material != null && !_warmUpCache.Contains(material))
                {
                    _warmUpCache.Add(material);
                    for (int pass = 0; pass < material.passCount; pass++)
                        material.SetPass(pass); // Прогреваем каждый проход шейдера
                }
        }

        public T GetInstance(Vector3 position, Quaternion quaternion, Transform parent = null) =>
            (T)GetInstanceUntyped(position, quaternion, parent);

        public object GetInstanceUntyped(Vector3 position, Quaternion quaternion, Transform parent = null)
        {
            T effect = null;

            if (_denseItemsCurrentCount > 0)
                effect = _denseItems[_sparseItemIndexes[--_denseItemsCurrentCount]];

            effect ??= BuildInstance(position, quaternion, parent);
            effect.transform.SetParent(parent ? parent : _container.transform, false);
            effect.transform.SetPositionAndRotation(position, quaternion);
            effect.gameObject.SetActive(true);

            return effect;
        }

        private T BuildInstance(Vector3 position, Quaternion quaternion, Transform parent = null)
        {
            var obj = _context.Instantiate(_prefab, position, quaternion, parent);
            var dispatcher = obj.gameObject.AddComponent<EnableDispatcher>();
            var index = _denseItemsCount;
            dispatcher.OnDisabled += () => ReturnInPull(index);

            MyArrayUtility.Add(ref _denseItems, ref _denseItemsCount, obj);

            if (_dontDestroyOnLoad && !parent)
                obj.transform.SetParent(_container.transform, false);

            return obj;
        }

        private void ReturnInPull(int index)
        {
            if (_disposeInProgress)
                return;

            if (_denseItemsCurrentCount >= _sparseItemIndexesCount)
                MyArrayUtility.Add(ref _sparseItemIndexes, ref _sparseItemIndexesCount, index);
            else
                _sparseItemIndexes[_denseItemsCurrentCount] = index;

            _denseItems[_sparseItemIndexes[_denseItemsCurrentCount]].gameObject.SetActive(false);
            _denseItemsCurrentCount++;
        }
        
        public void ForceDisable()
        {
            for (var i = 0; i < _denseItemsCount; i++) 
                _denseItems[i].gameObject.SetActive(false);
        }

        public void ReturnDisabledInContainer()
        {
            for (var i = 0; i < _denseItemsCount; i++)
            {
                var item = _denseItems[i];
                if (!item.gameObject.activeSelf)
                    item.transform.SetParent(_container.transform);
            }
        }

        public void Dispose()
        {
            _disposeInProgress = true;

            foreach (var item in _denseItems)
                Object.Destroy(item.gameObject);

            _denseItemsCount = 0;
            _sparseItemIndexesCount = 0;
            _denseItemsCurrentCount = 0;
            _disposeInProgress = false;
        }
    }
}
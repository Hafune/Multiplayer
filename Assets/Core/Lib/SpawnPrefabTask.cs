using System;
using Core.Lib.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace Core.Lib
{
    [ExecuteInEditMode]
    public class SpawnPrefabTask : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _runSelfOnStart;
        [SerializeField] private bool _useDontDestroyPool;
        [SerializeField] private bool _applySpawnerName;
        public bool InProgress => false;

#if UNITY_EDITOR
        private Transform _lastPrefab;

        public static GameObject InstantiatePrefabDontSave(Transform prefab, Transform parent)
        {
            var instance = PrefabUtility.InstantiatePrefab(prefab, parent);
            var prefabGo = instance.GameObject();
            prefabGo.hideFlags = HideFlags.DontSave;
            return prefabGo;
        }

        [MyButton]
        private void AutoRename()
        {
            if (_prefab)
                name = _prefab.name + "_spawn";
        }

        private void Refresh()
        {
            if (MyEditorUtility.IsUnsafeEditorCall())
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            if (this.IsDestroyed() || Application.isPlaying || !_prefab)
                return;

            _lastPrefab = _prefab;

            transform.DestroyChildren();
            InstantiatePrefabDontSave(_prefab, transform);
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
                return;

            transform.DestroyChildren(true);
            _lastPrefab = null;
        }
#endif

        private void Start()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            if (_runSelfOnStart)
                Begin(null);
        }

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            var effectPool = _useDontDestroyPool
                ? context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab)
                : context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);

            var instance = effectPool.GetInstance(
                transform.position,
                transform.rotation,
                transform
            );

            instance.localScale = Vector3.one;

            if (_useDontDestroyPool)
            {
                instance.parent = null;
                DontDestroyOnLoad(instance.gameObject);
            }

            if (_applySpawnerName)
                instance.name = name;

            onComplete?.Invoke();
        }
    }
}
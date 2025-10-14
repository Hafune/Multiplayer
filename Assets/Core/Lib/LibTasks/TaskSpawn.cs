using System;
using Core.Lib;
using Core.Lib.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Core.Tasks
{
    [ExecuteInEditMode]
    public class TaskSpawn : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _useAsParent;
        private PrefabPool<Transform> _pool;

        public bool InProgress { get; private set; }

#if UNITY_EDITOR
        private void Refresh()
        {
            if (MyEditorUtility.IsUnsafeEditorCall())
                return;

            if (this.IsDestroyed() || !enabled)
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (MyEditorUtility.IsUnsafeEditorCall())
            {
                OnDisable();
                return;
            }

            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            if (this.IsDestroyed() || Application.isPlaying || !_prefab)
                return;

            transform.DestroyChildren();
            SpawnPrefabTask.InstantiatePrefabDontSave(_prefab.transform, transform);
        }

        private void OnDisable()
        {
            if (!Application.isPlaying)
                transform.DestroyChildren();
        }
#endif

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _pool ??= context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);
            var instance = _pool.GetInstance(transform.position, transform.rotation, _useAsParent ? transform : null);

            if (!_useAsParent)
                instance.transform.localScale = Vector3.Scale(_prefab.transform.localScale, transform.lossyScale);

            onComplete?.Invoke();
        }
    }
}
using System;
using Core.Lib;
using Core.Lib.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Tasks
{
    [ExecuteInEditMode, Obsolete("Должна быть сиквенсом или иметь базовую функцию для этого")]
    public class TaskSpawnEntity : MonoConstruct, IMyTask
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private bool _waitRemove;
        [SerializeField] private bool _useAsParent;

        private Action _onComplete;
        private PrefabPool<ConvertToEntity> _pool;

        public bool InProgress { get; private set; }

#if UNITY_EDITOR
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
#if UNITY_EDITOR
            if (InProgress)
                throw new Exception("Новый вызов таски до её завершения");
#endif

            _pool ??= context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);
            _onComplete = onComplete;
            InProgress = _waitRemove;

            var entityRef = _pool.GetInstance(transform.position, transform.rotation, _useAsParent ? transform : null);

            if (!_useAsParent)
                entityRef.transform.localScale = Vector3.Scale(_prefab.transform.localScale, transform.lossyScale);

            payload.With(CommonPayloadKeys.ConvertToEntity, entityRef);

            if (_waitRemove)
                entityRef.BeforeEntityDeleted += EntityRemoved;
            else
                onComplete?.Invoke();
        }

        private void EntityRemoved(ConvertToEntity entityRef)
        {
            entityRef.BeforeEntityDeleted -= EntityRemoved;
            InProgress = false;
            _onComplete?.Invoke();
        }
    }
}
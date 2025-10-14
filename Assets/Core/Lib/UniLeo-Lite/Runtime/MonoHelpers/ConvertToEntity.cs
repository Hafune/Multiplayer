using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    [SelectionBase]
    public class ConvertToEntity : MonoConstruct
    {
        [Tooltip("Вызов перед удалением RawEntity")]
        public ActionNonAlloc<ConvertToEntity> BeforeEntityDeleted = new();

        private ActionNonAlloc _onInitialized = new();

        private Action<int, EcsWorld>[] _cache;

        // @formatter:off
        [SerializeField, Tooltip("Объект не выключается при уничтожении сущности, важно когда активность объекта контролируется через иерархию")]
        private bool _dontDisableGameObject;
        // @formatter:on

        private EcsWorld _world;
        private EcsPool<EventRemoveEntity> _eventRemovePool;
        private EcsPool<EventWaitInit> _eventWaitInit;
        private EcsPool<ConvertToEntityComponent> _convertToEntity;
        private bool _cacheIsApplied;
        private Rigidbody2D _rigidBody;

        [field: SerializeField, ReadOnly]
        [Tooltip("Уникальный Id префаба")]
        public int TemplateId { get; private set; }

        public int RawEntity { get; private set; } = -1;
        public Context GetContext() => context;

        private void OnValidate() => gameObject.UpdateTemplateId(TemplateId, id => TemplateId = id);

        private void Awake()
        {
            _world = context.Resolve<EcsWorld>();
            var componentPools = context.Resolve<ComponentPools>();
            _eventRemovePool = componentPools.EventRemoveEntity;
            _eventWaitInit = componentPools.EventWaitInit;
            _convertToEntity = componentPools.ConvertToEntity;
            _rigidBody = GetComponent<Rigidbody2D>();

#if UNITY_EDITOR
            if (_rigidBody && _rigidBody.simulated)
                Debug.LogError("Тело включено на префабе");
#endif

            MakeCache();
        }

        private void OnEnable() => ConnectToWorld();

        private void OnDisable() => DisconnectFromWorld();

        public void RemoveConnectionInfo()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            BeforeEntityDeleted.Invoke(this);
            BeforeEntityDeleted.Clear();

            if (!_dontDisableGameObject)
                gameObject.SetActive(false);

            if (_rigidBody)
                _rigidBody.simulated = false;

            RawEntity = -1;
        }

        public void RegisterInitializeCall(Action callback)
        {
            _onInitialized += callback;

            if (_cacheIsApplied)
                callback.Invoke();
        }

        public void UnRegisterInitializeCall(Action callback) => _onInitialized -= callback;

        public void ApplyCache()
        {
            _convertToEntity.Add(RawEntity).convertToEntity = this;

            for (int i = 0, iMax = _cache.Length; i < iMax; i++)
                _cache[i].Invoke(RawEntity, _world);

            _onInitialized.Invoke();
            _cacheIsApplied = true;

            if (_rigidBody)
                _rigidBody.simulated = true;
        }

        private void MakeCache()
        {
            var list = gameObject.GetComponents<BaseMonoProvider>();
            _cache = new Action<int, EcsWorld>[list.Length];

            for (int i = 0, iMax = list.Length; i < iMax; i++)
            {
                var entityComponent = list[i];
                _cache[i] = entityComponent.Attach;
                Destroy(entityComponent);
            }
        }

        private void ConnectToWorld()
        {
            RawEntity = _world.NewEntity();
            _eventWaitInit.Add(RawEntity).convertToEntity = this;
            _cacheIsApplied = false;
        }

        private void DisconnectFromWorld()
        {
            if (RawEntity == -1)
                return;
            
            //Если выключение происходит в том же кадре
            if (_eventWaitInit.Has(RawEntity))
            {
                _world.DelEntity(RawEntity);
                return;
            }

            _eventRemovePool.AddIfNotExist(RawEntity);
        }
    }
}
using System;
using Core.ExternalEntityLogics;
using Core.Generated;
using Core.Lib;
using Lib;
using UnityEngine;

namespace Core
{
    [DisallowMultipleComponent, RequireComponent(typeof(Animator))]
    public class AnimatorEventHandler : MonoConstruct
    {
        private enum Parameters
        {
            CanBeCanceled,
            CannotBeCanceled,
            Completed,
            ResetTargets,
        }

        [SerializeField] private Transform _root;
        [SerializeField] private ConvertToEntity _convertToEntity;
        [SerializeField] private DamageArea _damageArea;
        private ComponentPools _pools;
        private KeyValueCache<ReferencePath, Action<int>> _logicCache;
        private KeyValueCache<ReferencePath, AbstractEffect> _effectCache;

        private void OnValidate()
        {
            _convertToEntity = _convertToEntity ? _convertToEntity : GetComponentInParent<ConvertToEntity>();
            _root = _root ? _root : transform.root;
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (!_convertToEntity)
            {
                Debug.LogWarning("Не задан ConvertToEntity", this);
                return;
            }
#endif
            _pools = context.Resolve<ComponentPools>();
            _logicCache = new(path => path.Find(_root).GetComponent<AbstractEntityLogic>().Run);
            _effectCache = new(path => path.Find(_root).GetComponent<AbstractEffect>());
        }

        private void ExecuteEffect(ReferencePath pathRef)
        {
#if UNITY_EDITOR
            if (!_convertToEntity)
                return;
#endif
            _effectCache.Get(pathRef).Execute();
        }

        private void PutActionEvent(Parameters value)
        {
#if UNITY_EDITOR
            if (!_convertToEntity)
                return;
#endif
            switch (value)
            {
                case Parameters.CanBeCanceled:
                    _pools.ActionCanBeCanceled.AddIfNotExist(_convertToEntity.RawEntity);
                    break;
                case Parameters.CannotBeCanceled:
                    _pools.ActionCanBeCanceled.DelIfExist(_convertToEntity.RawEntity);
                    break;
                case Parameters.Completed:
                    _pools.ActionComplete.AddIfNotExist(_convertToEntity.RawEntity);
                    break;
                case Parameters.ResetTargets:
                    _damageArea.ResetTargets();
                    break;
                default:
                    Debug.LogError("Неизвестный параметер " + value);
                    break;
            }
        }

        private void PutTimelineAction(ReferencePath pathRef)
        {
#if UNITY_EDITOR
            if (!_convertToEntity)
                return;
#endif
            _pools.EventTimelineAction.GetOrInitialize(_convertToEntity.RawEntity).logic += _logicCache.Get(pathRef);
        }
    }
}
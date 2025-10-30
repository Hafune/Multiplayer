using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class AnimatorRootMotion : MonoConstruct
    {
        private Animator _animator;
        private ConvertToEntity _convertToEntity;
        private EcsPool<AnimatorRootMotionComponent> _rootMotionPool;
        private Func<Vector3, Vector3> _movePostProcessing;

        private void Awake()
        {
            _convertToEntity = GetComponentInParent<ConvertToEntity>();
            _animator = GetComponentInParent<Animator>();
            _animator.applyRootMotion = false;
            _rootMotionPool = context.Resolve<ComponentPools>().AnimatorRootMotion;
        }

        private void OnEnable() => _animator.applyRootMotion = true;

        private void OnDisable()
        {
            _animator.applyRootMotion = false;
            
            if (_convertToEntity.RawEntity != -1)
                _rootMotionPool.DelIfExist(_convertToEntity.RawEntity);
        }

        public void SetDeltaPostProcessing(Func<Vector3, Vector3> action) => _movePostProcessing = action;

        private void OnAnimatorMove()
        {
            _rootMotionPool.GetOrInitialize(_convertToEntity.RawEntity).deltaPosition +=
                _movePostProcessing?.Invoke(_animator.deltaPosition) ?? _animator.deltaPosition;
        }
    }
}
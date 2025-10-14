using System;
using Core.Lib;
// using Cinemachine;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskNodeNewCinemachineTarget : TaskSequence
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        private Action _onComplete;
        // private CinemachineVirtualCamera _virtualCamera;
        private Transform _oldTarget;
        private Vector3 _oldOffset;
        // private CinemachineFramingTransposer _transposer;

        private void OnValidate() => _target = _target ? _target : transform;

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            // _virtualCamera = context.Resolve<CinemachineVirtualCamera>();
            // _oldTarget = _virtualCamera.Follow;
            // _transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            // _oldOffset = _transposer.m_TrackedObjectOffset;
            // _virtualCamera.Follow = _target;
            // _transposer.m_TrackedObjectOffset = _offset;

            _onComplete = onComplete;
            base.Begin(payload, Callback);
        }

        private void Callback()
        {
            // _virtualCamera.Follow = _oldTarget;
            // _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = _oldOffset;
            _onComplete?.Invoke();
        }
    }
}
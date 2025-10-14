using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;


namespace Core.Tasks
{
    [ExecuteAlways]
    public class TaskSetWorldPositionX : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _target;
        [SerializeField] private FloatValue _value;
        [SerializeField] private float _offset;

        public bool InProgress => false;

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (!Application.isPlaying && _target)
                _target.localPosition = _target.localPosition.Copy(x: transform.localPosition.x);
        }

        [MyButton]
        private void SetupSelfAsTarget() => _target = transform;
#endif

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _target.position = _target.position.Copy(x: _value.Value + _offset);
            onComplete?.Invoke();
        }
    }
}
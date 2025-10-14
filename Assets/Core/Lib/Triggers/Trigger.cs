using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    [SelectionBase]
    public class Trigger : MonoConstruct, ITrigger
    {
        [SerializeField] private bool _reusable;
        [CanBeNull, SerializeField] private MonoBehaviour _task;
        private bool _isCompleted;
        private Payload _payload;

        private void OnValidate()
        {
            if (_task is not IMyTask)
                _task = null;

            _task = _task ? _task : (MonoBehaviour)GetComponentInChildren<IMyTask>();

            if (_task == this)
                _task = null;
        }

        private void OnEnable() => _isCompleted = false;

        private void OnTriggerEnter2D(Collider2D _)
        {
            if (_isCompleted)
                return;

            _isCompleted = !_reusable;

            if (_task)
                (_task as IMyTask)?.Begin(_payload = Payload.GetPooled(), OnComplete);
        }

        private void OnDestroy() => _payload?.Dispose();

        private void OnComplete()
        {
            _payload?.Dispose();
            _payload = null;
        }
    }
}
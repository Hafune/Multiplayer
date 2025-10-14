using System;
using UnityEngine;

namespace Core.Lib
{
    public class AnimatorRootMotion : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _deltaPosition;
        private Func<Vector2, Vector2> _movePostProcessing;

        private void OnValidate()
        {
            _animator = _animator ? _animator : GetComponent<Animator>();
            _rigidbody = _rigidbody ? _rigidbody : GetComponent<Rigidbody2D>();
        }

        private void Awake() => _animator.applyRootMotion = false;

        private void OnEnable()
        {
            _animator.applyRootMotion = true;
            _rigidbody.linearVelocity = -Physics2D.gravity * _rigidbody.gravityScale;
            _deltaPosition = Vector2.zero;
        }

        private void OnDisable() => _animator.applyRootMotion = false;

        public void SetDeltaPostProcessing(Func<Vector2, Vector2> action) => _movePostProcessing = action;

        private void OnAnimatorMove()
        {
            if (!enabled)
                return;
            
            var delta = _movePostProcessing?.Invoke(_animator.deltaPosition) ?? _animator.deltaPosition;

            _deltaPosition += delta;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _deltaPosition);
            // var velocity = _deltaPosition / Time.deltaTime;
            //
            // _rigidbody.velocity = Vector2.MoveTowards(
            //     _rigidbody.velocity,
            //     velocity,
            //     velocity.magnitude);

            _deltaPosition = Vector2.zero;
        }
    }
}
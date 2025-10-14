using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimateRotation : AbstractAnimateFloat
    {
        [SerializeField] public Axis axis;
        [SerializeField] public float speed;
        [SerializeField] public float smooth = 0.75f;
        [SerializeField, CanBeNull] public RotationVelocityInfo info;
        [SerializeField] public Transform target;
        private Vector3 _direction;
        private float _velocity;
        private float _currentSpeed;

        private void Awake()
        {
            _direction = axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override void SetValue(float value) => speed = value;

        private void OnEnable() => _currentSpeed = info?.GetVelocity(axis) ?? speed;

        private void FixedUpdate()
        {
            _currentSpeed = MathfExtensions.SmoothDampWithOvershoot(_currentSpeed, speed, ref _velocity, smooth);
            target.localRotation *= Quaternion.AngleAxis(_currentSpeed * Time.deltaTime, _direction);
        }
    }
}
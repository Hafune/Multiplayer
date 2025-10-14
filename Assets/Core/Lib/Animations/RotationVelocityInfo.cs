using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class RotationVelocityInfo : AbstractVelocityInfo
    {
        private Quaternion _previousRotation;
        private Quaternion _currentRotation;
        private float _calculateTime;
        private Vector3 _velocity;

        private void OnEnable()
        {
            _previousRotation = transform.localRotation;
            _currentRotation = _previousRotation;
        }

        public override Vector3 GetVelocity()
        {
            if (_calculateTime == Time.time)
                return _velocity;

            // Разница между предыдущей и текущей ориентацией
            var deltaRotation = Quaternion.Inverse(_previousRotation) * _currentRotation;

            // Угол поворота за кадр
            deltaRotation.ToAngleAxis(out float result, out var direction);
            _velocity = direction * result / Time.fixedDeltaTime;
            _calculateTime = Time.time;
            return _velocity;
        }

        public override float GetVelocity(Axis axis)
        {
            GetVelocity();
            return axis switch
            {
                Axis.X => _velocity.x,
                Axis.Y => _velocity.y,
                Axis.Z => _velocity.z,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void FixedUpdate()
        {
            _previousRotation = _currentRotation;
            _currentRotation = transform.localRotation;
        }
    }
}
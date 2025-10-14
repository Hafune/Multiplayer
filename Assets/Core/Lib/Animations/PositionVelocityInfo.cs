using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class PositionVelocityInfo : AbstractVelocityInfo
    {
        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
        private float _calculateTime;
        private Vector3 _velocity;
        
        private void OnEnable()
        {
            _previousPosition = transform.position;
            _currentPosition = _previousPosition;
        }


        public override Vector3 GetVelocity()
        {
            if (_calculateTime == Time.time)
                return _velocity;
                
            var localPreviousPosition = transform.InverseTransformPoint(_previousPosition);
            var localCurrentPosition = transform.InverseTransformPoint(_currentPosition);

            var deltaPosition = localCurrentPosition - localPreviousPosition;
            _calculateTime = Time.time;
            _velocity = deltaPosition / Time.fixedDeltaTime;
                    
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
            _previousPosition = _currentPosition;
            _currentPosition = transform.position;
        }
    }
}
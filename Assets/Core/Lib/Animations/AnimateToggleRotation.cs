using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimateToggleRotation : AbstractAnimateToggle
    {
        [SerializeField] public Axis axis;
        [SerializeField] public bool toggle;
        [SerializeField] public float toggleValue;
        [SerializeField] public float smooth = 0.1f;
        
        private float _velocity;
        private Vector3 _startEuler;

        private void Awake() => _startEuler = transform.localEulerAngles;
        
        public override void SetValue(bool value) => toggle = value;

        private void Update()
        {
            var euler = transform.localEulerAngles;

            float value = axis switch
            {
                Axis.X => euler.x,
                Axis.Y => euler.y,
                Axis.Z => euler.z,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            float target = axis switch
            {
                Axis.X => toggle ? toggleValue : _startEuler.x,
                Axis.Y => toggle ? toggleValue : _startEuler.y,
                Axis.Z => toggle ? toggleValue : _startEuler.z,
                _ => throw new ArgumentOutOfRangeException()
            };

            value = Mathf.SmoothDampAngle(value, target, ref _velocity, smooth);
            
            switch (axis)
            {
                case Axis.X: euler.x = value; break;
                case Axis.Y: euler.y = value; break;
                case Axis.Z: euler.z = value; break;
                default: throw new ArgumentOutOfRangeException();
            }

            transform.localEulerAngles = euler;
        }
    }
}
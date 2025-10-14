using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class SmoothLockOnPositionLogic : AbstractEntityLogic, IActionSubStartLogic
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _smooth;
        [SerializeField] private float _minVelocity = 1f;
        private Vector2 _startPosition;

        public void SubStart(int entity) => _startPosition = _transform.position;

        public override void Run(int entity)
        {
            var velocity = _rigidbody.linearVelocity;
            MathfExtensions.SmoothDampWithOvershoot((Vector2)_transform.position, _startPosition, ref velocity,
                _smooth);
            
            _rigidbody.linearVelocity = MathfExtensions.ClampMagnitudeMin(velocity, _minVelocity);
        }
    }
}
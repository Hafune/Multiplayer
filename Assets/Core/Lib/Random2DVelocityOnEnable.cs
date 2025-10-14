using UnityEngine;

namespace Core.Lib
{
    public class Random2DVelocityOnEnable : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _minForce;
        [SerializeField] private float _randomValue;

        private void OnValidate() => _rigidbody = _rigidbody ? _rigidbody : GetComponent<Rigidbody2D>();

        private void OnEnable() => _rigidbody.linearVelocity =
            Random.insideUnitCircle.normalized * (_minForce + Random.value * _randomValue);
    }
}
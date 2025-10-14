using System;
using UnityEngine;

namespace Core.Lib
{
    public class TurretLookController : MonoBehaviour
    {
        [SerializeField] private Transform _baseTurret; // Основание турели
        [SerializeField] private Transform _gunBarrel; // Пушки турели
        [SerializeField] private Transform _target; // Цель
        [SerializeField] private float _baseRotationSpeed; // Цель
        [SerializeField] private float _gunRotationSpeed; // Цель
        private const float _defaultAngle = 90f;
        private const float _gunMaxAngle = 90f;
        private const float _gunMinAngle = 0f;

        private void Update()
        {
            RotateBase();
            RotateGun();
        }

        private void RotateBase()
        {
            var localTargetPosition = _baseTurret.InverseTransformPoint(_target.position);

            float angle = Mathf.Atan2(localTargetPosition.x, localTargetPosition.y) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360;

            var localRotation = _baseTurret.localRotation;
            _baseTurret.localRotation = Quaternion.RotateTowards(localRotation,
                Quaternion.Euler(0, 0, localRotation.eulerAngles.z - angle + _defaultAngle),
                _baseRotationSpeed * Time.deltaTime);
        }

        private void RotateGun()
        {
            var localTargetPosition = _gunBarrel.InverseTransformPoint(_target.position);

            float angle = Mathf.Atan2(localTargetPosition.x, localTargetPosition.y) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360;

            var localRotation = _gunBarrel.localRotation;
            _gunBarrel.localRotation = Quaternion
                .RotateTowards(localRotation,
                    Quaternion.Euler(0, 0, Math.Clamp(localRotation.eulerAngles.z - angle + _defaultAngle, _gunMinAngle,
                        _gunMaxAngle)), _gunRotationSpeed * Time.deltaTime);
        }
    }
}
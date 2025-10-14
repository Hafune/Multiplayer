using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimateRotationSpring : MonoBehaviour
    {
        [SerializeField] public Axis axis;
        [SerializeField] public float step = 15f;
        [SerializeField] public float smooth = 0.3f;
        [SerializeField] public RotationVelocityInfo info;
        [SerializeField] public Transform target;
        [SerializeField, CanBeNull] public Transform baseRotation;
        private float _velocity;
        private Quaternion _startRotation;
        private Quaternion _defaultRotation => baseRotation ? baseRotation.localRotation : _startRotation;

        private void OnValidate()
        {
            target = target ? target : transform;
            info = info ? info : GetComponentInParent<RotationVelocityInfo>();
        }

        private void Awake() => _startRotation = transform.localRotation;

        private void OnEnable() => _velocity = info.GetVelocity(axis);

        private void FixedUpdate()
        {
            // Получаем текущий кватернион
            var currentRotation = target.localRotation;

            // Вычисляем разницу между текущим и стартовым кватернионами
            var deltaRotation = Quaternion.Inverse(_defaultRotation) * currentRotation;

            // Определяем угол поворота для выбранной оси
            float currentValue = axis switch
            {
                Axis.X => deltaRotation.eulerAngles.x,
                Axis.Y => deltaRotation.eulerAngles.y,
                Axis.Z => deltaRotation.eulerAngles.z,
                _ => throw new ArgumentOutOfRangeException()
            };

            // Приведение угла к диапазону -180..180
            currentValue = Mathf.DeltaAngle(0, currentValue);

            // Округляем до ближайшего значения, кратного step
            float targetAngle = step == 0 ? 0 : Mathf.Round(currentValue / step) * step;

            // Плавное изменение угла с помощью расширенного метода с overshoot
            float smoothedValue = MathfExtensions.SmoothDampAngleWithOvershoot(currentValue, targetAngle, ref _velocity, smooth);

            // Вычисляем вращение для смещения на основе разницы между углом текущим и сглаженным
            var offsetRotation = axis switch
            {
                Axis.X => Quaternion.AngleAxis(smoothedValue - currentValue, Vector3.right),
                Axis.Y => Quaternion.AngleAxis(smoothedValue - currentValue, Vector3.up),
                Axis.Z => Quaternion.AngleAxis(smoothedValue - currentValue, Vector3.forward),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Применяем смещение к текущему вращению
            target.localRotation *= offsetRotation;
        }
    }
}
/* решение без углов элера от гпт 01
using UnityEngine.Animations;
using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Lib
{
    public class AnimateRotationSpring : MonoBehaviour
    {
        [SerializeField] public Axis axis;
        [SerializeField] public float step = 15f;
        [SerializeField] public float smooth = 0.3f;
        [SerializeField] public RotationVelocityInfo info;
        [SerializeField] public Transform target;
        [SerializeField, CanBeNull] public Transform baseRotation;
        private float _velocity;
        private Quaternion _startRotation;
        private Quaternion _defaultRotation => baseRotation ? baseRotation.localRotation : _startRotation;

        private void OnValidate()
        {
            target = target ? target : transform;
            info = info ? info : GetComponentInParent<RotationVelocityInfo>();
        }

        private void Awake() => _startRotation = transform.localRotation;

        private void OnEnable() => _velocity = info.GetVelocity(axis);

        private void FixedUpdate()
        {
            // Получаем текущий кватернион
            var currentRotation = target.localRotation;

            // Вычисляем разницу между текущим и базовым кватернионами
            var deltaRotation = Quaternion.Inverse(_defaultRotation) * currentRotation;

            // Определяем ось вращения для выбранной оси
            Vector3 rotationAxis = axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => throw new ArgumentOutOfRangeException()
            };

            // Получаем кватернион твиста (вращение вокруг оси)
            Quaternion twist = GetTwist(deltaRotation, rotationAxis);

            // Извлекаем угол из твиста
            twist.ToAngleAxis(out float angle, out Vector3 twistAxis);

            // Корректируем знак угла в соответствии с направлением оси
            if (Vector3.Dot(twistAxis, rotationAxis) < 0)
            {
                angle = -angle;
            }

            // Приведение угла к диапазону -180..180
            angle = Mathf.DeltaAngle(0, angle);

            // Округляем до ближайшего значения, кратного step
            float targetAngle = step == 0 ? 0 : Mathf.Round(angle / step) * step;

            // Плавное изменение угла с помощью расширенного метода с overshoot
            float smoothedValue = MathfExtensions.SmoothDampAngleWithOvershoot(angle, targetAngle, ref _velocity, smooth);

            // Вычисляем вращение для смещения на основе разницы между углом текущим и сглаженным
            float deltaAngle = smoothedValue - angle;
            Quaternion offsetRotation = Quaternion.AngleAxis(deltaAngle, rotationAxis);

            // Применяем смещение к текущему вращению
            target.localRotation *= offsetRotation;
        }

        private Quaternion GetTwist(Quaternion rotation, Vector3 axis)
        {
            axis.Normalize();
            Vector3 p = new Vector3(rotation.x, rotation.y, rotation.z);
            Vector3 projection = Vector3.Dot(p, axis) * axis;
            Quaternion twist = new Quaternion(projection.x, projection.y, projection.z, rotation.w);
            return twist.normalized;
        }
    }
}

*/
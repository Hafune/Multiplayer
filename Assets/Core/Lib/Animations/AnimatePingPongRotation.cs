using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimatePingPongRotation : MonoBehaviour
    {
        [SerializeField] private Axis axis; // Ось вращения
        [SerializeField] public float speed; // Скорость вращения
        [SerializeField] private float minAngle; // Минимальный угол
        [SerializeField] private float maxAngle; // Максимальный угол

        private Vector3 _startEuler;
        private float _currentTime;

        private void Awake() => _startEuler = transform.localEulerAngles;

        private void OnEnable()
        {
            transform.localEulerAngles = _startEuler;
            _currentTime = 0f;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            var euler = _startEuler;
            float angle = GetAngle(minAngle, maxAngle, _currentTime, speed);

            // Выбираем ось вращения
            switch (axis)
            {
                case Axis.X:
                    euler.x += angle;
                    break;
                case Axis.Y:
                    euler.y += angle;
                    break;
                case Axis.Z:
                    euler.z += angle;
                    break;
                default: throw new System.ArgumentOutOfRangeException();
            }

            transform.localEulerAngles = euler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetAngle(float min, float max, float time, float speed) =>
            Mathf.SmoothStep(min, max, Mathf.PingPong(time * speed, 1f));
    }
}
using UnityEngine;

namespace Core.Lib
{
    public class RandomRotation : MonoBehaviour
    {
        public Vector3 startDirection = Vector3.up; // Начальное направление
        public float maxAngleDeviation = 45f; // Максимальный угол отклонения в градусах
        public float rotationSpeed = 1f; // Скорость вращения

        private Quaternion _targetRotation;
        private float _timeToChangeDirection = 2f; // Время между сменой направления
        private float _timePassed;

        void Start()
        {
            // Устанавливаем начальное направление
            transform.rotation = Quaternion.LookRotation(startDirection);
            SetRandomTargetRotation();
        }

        void Update()
        {
            // Плавно вращаем к новому направлению
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);

            // Обновляем время
            _timePassed += Time.deltaTime;
            if (_timePassed >= _timeToChangeDirection)
            {
                // Сбрасываем таймер и задаём новое случайное направление
                _timePassed = 0f;
                SetRandomTargetRotation();
            }
        }

        private void SetRandomTargetRotation()
        {
            // Случайное отклонение от текущего направления
            Vector3 randomDeviation = Random.insideUnitSphere * maxAngleDeviation;

            // Новое направление с учетом максимального отклонения
            Vector3 newDirection = Quaternion.Euler(randomDeviation) * startDirection;

            // Устанавливаем новую целевую ротацию
            _targetRotation = Quaternion.LookRotation(newDirection);
        }
    }
}
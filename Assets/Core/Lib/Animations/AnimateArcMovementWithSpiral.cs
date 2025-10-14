using UnityEngine;

using Random = UnityEngine.Random;

namespace Core.Lib
{
    public class AnimateArcMovementWithSpiral : MonoBehaviour
    {
        [SerializeField] private float _arcRotationMin;
        [SerializeField] private float _arcRotationMax;
        [SerializeField] private Transform _startPoint; // Начальная точка
        [SerializeField] private Transform _endPoint; // Конечная точка
        [SerializeField] private float _arcHeight = 5f; // Высота дуги
        [SerializeField] private float _speed = .5f; // Скорость движения
        [SerializeField] private float _spiralFrequency = 3f; // Частота спирали (количество витков)
        [SerializeField] private float _spiralRadius = 1.5f; // Радиус спирали

        private float _progress; // Прогресс по дуге от 0 до 1
        private float _angle;
        private Quaternion _offsetRotation;

        [MyButton]
        private void Restore()
        {
            _progress = 0;
            _offsetRotation = Quaternion.LookRotation((_endPoint.position - _startPoint.position).normalized) *
                              Quaternion.Euler(0, 0, 360 - Random.Range(_arcRotationMin, _arcRotationMax));
        }

        [MyButton]
        private void OnEnable() => _angle = Random.value * 360;

        private void Update()
        {
            // Увеличиваем прогресс по времени
            _progress += Time.deltaTime * _speed;

            if (_progress > 1f)
            {
                _progress = 1f; // Останавливаем движение в конечной точке
            }

            // Вычисляем положение объекта на дуге с учетом высоты
            var positionOnArc = GetArcPosition(_startPoint.position, _endPoint.position, _arcHeight, _progress);

            // Вычисляем смещение по спирали, ориентированное относительно дуги
            var spiralOffset = GetSpiralOffset(_startPoint.position, _endPoint.position, _arcHeight, _progress);

            // Применяем новое положение к объекту (позиция на дуге + смещение по спирали)
            transform.position = positionOnArc + spiralOffset;
        }

        // Метод для вычисления положения на дуге
        private Vector3 GetArcPosition(Vector3 start, Vector3 end, float height, float t)
        {
            // Интерполяция по оси X и Z (линейное движение)
            var midPoint = Vector3.Lerp(start, end, t);

            // Добавляем высоту по оси Y для создания дуги
            float arc = Mathf.Sin(t * 2 * Mathf.PI) * height;

            var offset = _offsetRotation * (Vector3.right * arc);

            return midPoint - offset;
        }

        // Метод для вычисления смещения по спирали вокруг дуги
        private Vector3 GetSpiralOffset(Vector3 start, Vector3 end, float height, float t)
        {
            // Позиция текущей точки на дуге
            var positionOnArc = GetArcPosition(start, end, height, t);

            // Позиция следующей точки на дуге (для расчета направления)
            var nextPositionOnArc = GetArcPosition(start, end, height, t + 0.01f);

            // Направление движения по дуге (касательная)
            var direction = (nextPositionOnArc - positionOnArc).normalized;

            // Ортогональный вектор к направлению движения (нормаль к дуге)
            var orthogonal = Vector3.Cross(direction, Vector3.up).normalized;

            // Смещение по спирали (вращение вокруг касательной)
            var sin = Mathf.Sin(t * Mathf.PI);
            var coef = sin + sin * (1 - sin);
            float radius = _spiralRadius * coef;
            float angle = t * _spiralFrequency * Mathf.PI * 2; // угол вращения по спирали
            float xOffset = Mathf.Cos(angle) * radius; // смещение по одной оси
            float yOffset = Mathf.Sin(angle) * radius; // смещение по другой оси

            // Создаем локальное смещение по спирали относительно дуги
            var spiralOffset = orthogonal * xOffset + Vector3.Cross(orthogonal, direction) * yOffset;

            return spiralOffset;
        }
    }
}
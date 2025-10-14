using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Core.Lib
{
    public abstract class AbstractMultiCurve : ScriptableObject
    {
#if UNITY_EDITOR
        [field: SerializeField] public Vector2 EditorSteps { get; private set; } = new Vector2(1, 0.1f);
        [field: SerializeField] public Rect GraphLength { get; private set; } = new Rect(0, 0, 10, 10);
        [field: SerializeField] public int PreviewHeight { get; private set; } = 200;

        private (MemberInfo member, string displayName)[] _cachedCurveMembers;

        public void SetGraphLengthFromCurves()
        {
            var minTime = float.MaxValue;
            var maxTime = float.MinValue;
            var minValue = float.MaxValue;
            var maxValue = float.MinValue;

            foreach (var (_, curve) in EditorGetCurves())
            {
                if (curve == null || curve.length == 0)
                    continue;

                foreach (var key in curve.keys)
                {
                    minTime = Mathf.Min(minTime, key.time);
                    maxTime = Mathf.Max(maxTime, key.time);
                    minValue = Mathf.Min(minValue, key.value);
                    maxValue = Mathf.Max(maxValue, key.value);
                }
            }

            if (minTime < float.MaxValue)
            {
                // Вычисляем размеры
                var timeRange = maxTime - minTime;
                var valueRange = maxValue - minValue;

                // Добавляем 10% отступы
                var timePadding = timeRange * 0.1f;
                var valuePadding = valueRange * 0.1f;

                // Если диапазон очень маленький, устанавливаем минимальный отступ
                if (Mathf.Approximately(timeRange, 0f))
                {
                    timePadding = 1f;
                }

                if (Mathf.Approximately(valueRange, 0f))
                {
                    valuePadding = 1f;
                }

                // Вычисляем границы с отступами
                var newMinTime = minTime - timePadding;
                var newMaxTime = maxTime + timePadding;
                var newMinValue = minValue - valuePadding;
                var newMaxValue = maxValue + valuePadding;

                // Округляем до значений, кратных EditorSteps
                var timeStep = EditorSteps.x;
                var valueStep = EditorSteps.y;

                if (timeStep > 0)
                {
                    newMinTime = Mathf.Floor(newMinTime / timeStep) * timeStep;
                    newMaxTime = Mathf.Ceil(newMaxTime / timeStep) * timeStep;
                }

                if (valueStep > 0)
                {
                    newMinValue = Mathf.Floor(newMinValue / valueStep) * valueStep;
                    newMaxValue = Mathf.Ceil(newMaxValue / valueStep) * valueStep;
                }

                GraphLength = new Rect(
                    newMinTime,
                    newMinValue,
                    newMaxTime - newMinTime,
                    newMaxValue - newMinValue
                );
            }
        }

        public IEnumerable<(string name, AnimationCurve curve)> EditorGetCurves()
        {
            if (_cachedCurveMembers == null)
            {
                var data = new List<(MemberInfo, string)>();

                var fields = GetType().GetFields(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance);

                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(AnimationCurve))
                    {
                        var attr = field.GetCustomAttributes(typeof(CurveSetEditorAttribute), true);
                        if (attr.Length > 0)
                        {
                            var displayName = ObjectNames.NicifyVariableName(field.Name);
                            data.Add((field, displayName));
                        }
                    }
                }

                var properties = GetType().GetProperties(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance);

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(AnimationCurve) && property.CanRead)
                    {
                        var attr = property.GetCustomAttributes(typeof(CurveSetEditorAttribute), true);
                        if (attr.Length > 0)
                        {
                            var displayName = ObjectNames.NicifyVariableName(property.Name);
                            data.Add((property, displayName));
                        }
                    }
                }

                _cachedCurveMembers = data.ToArray();
            }

            foreach (var (member, displayName) in _cachedCurveMembers)
            {
                var curve = member switch
                {
                    FieldInfo field => (AnimationCurve)field.GetValue(this),
                    PropertyInfo property => (AnimationCurve)property.GetValue(this),
                    _ => null
                };

                if (curve != null)
                    yield return (displayName, curve);
            }
        }
#endif
    }
}
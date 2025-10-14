using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    // Базовый класс для функционала кнопок
    public abstract class BaseButtonAttributeDrawer : UnityEditor.Editor
    {
        private const BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        protected readonly Dictionary<Type, Dictionary<string, MethodInfo>> _cachedMethods = new();
        protected readonly Dictionary<string, List<UnityEngine.Object>> _methodMap = new();

        protected void DrawButtons()
        {
            _methodMap.Clear();

            // Перебираем все выбранные объекты
            foreach (var targetObj in targets)
            {
                var type = targetObj.GetType();

                if (!_cachedMethods.TryGetValue(type, out var methods))
                {
                    methods = new Dictionary<string, MethodInfo>();
                    _cachedMethods[type] = methods;

                    var currentType = type;
                    while (currentType != null && 
                          currentType != typeof(MonoBehaviour) && 
                          currentType != typeof(ScriptableObject))
                    {
                        foreach (var method in currentType.GetMethods(_flags))
                        {
                            var attr = method.GetCustomAttribute<MyButtonAttribute>();

                            if (attr == null) 
                                continue;
                            
                            var customName = string.IsNullOrEmpty(attr.customName) ? method.Name : attr.customName;
                            if (!methods.ContainsKey(customName))
                                methods[customName] = method;
                        }

                        currentType = currentType.BaseType; // Переход к родительскому классу
                    }
                }

                foreach (var method in methods)
                {
                    if (!_methodMap.ContainsKey(method.Key)) 
                        _methodMap[method.Key] = new List<UnityEngine.Object>();
                    
                    _methodMap[method.Key].Add(targetObj);
                }
            }

            // Создаём кнопки по группам методов
            foreach (var entry in _methodMap)
                if (GUILayout.Button(entry.Key))
                    foreach (var obj in entry.Value)
                        _cachedMethods[obj.GetType()][entry.Key].Invoke(obj, null);
        }
    }

    // Редактор для MonoBehaviour
    [CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
    public class ButtonAttributeDrawer : BaseButtonAttributeDrawer
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Отображает стандартные поля инспектора
            DrawButtons(); // Рисуем кнопки
        }
    }

    // Редактор для ScriptableObject
    [CustomEditor(typeof(ScriptableObject), true), CanEditMultipleObjects]
    public class ScriptableObjectButtonAttributeDrawer : BaseButtonAttributeDrawer
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Отображает стандартные поля инспектора
            DrawButtons(); // Рисуем кнопки
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    [CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
    public class ButtonAttributeDrawer : UnityEditor.Editor
    {
        private const BindingFlags _flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private readonly Dictionary<Type, Dictionary<string, MethodInfo>> _cachedMethods = new();
        private readonly Dictionary<string, List<MonoBehaviour>> _methodMap = new();

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Отображает стандартные поля инспектора

            _methodMap.Clear();

            // Перебираем все выбранные объекты
            foreach (var targetObj in targets)
            {
                var script = (MonoBehaviour)targetObj;
                var type = script.GetType();

                if (!_cachedMethods.TryGetValue(type, out var methods))
                {
                    methods = new Dictionary<string, MethodInfo>();
                    _cachedMethods[type] = methods;

                    while (type != null && type != typeof(MonoBehaviour))
                    {
                        foreach (var method in type.GetMethods(_flags))
                            if (method.GetCustomAttribute<MyButtonAttribute>() != null)
                                if (!methods.ContainsKey(method.Name))
                                    methods[method.Name] = method;

                        type = type.BaseType; // Переход к родительскому классу
                    }
                }

                foreach (var method in methods)
                {
                    if (!_methodMap.ContainsKey(method.Key)) 
                        _methodMap[method.Key] = new List<MonoBehaviour>();
                    
                    _methodMap[method.Key].Add(script);
                }
            }

            // Создаём кнопки по группам методов
            foreach (var entry in _methodMap)
                if (GUILayout.Button(entry.Key))
                    foreach (var script in entry.Value)
                        _cachedMethods[script.GetType()][entry.Key].Invoke(script, null);
        }
    }
}

*/
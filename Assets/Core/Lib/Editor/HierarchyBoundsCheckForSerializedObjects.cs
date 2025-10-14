using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Lib.Editor
{
    public static class HierarchyBoundsCheckForSerializedObjects
    {
/*         
Как использовать:
Скопируйте в буфер полное имя класса (например: Core.ExternalEntityLogics.AbstractEntityAction)
Выберите объекты в иерархии, которые хотите проверить
Вызовите меню: Tools → Hierarchy Check → Validate Selected Objects
Что делает скрипт:
✅ Находит все сериализованные поля типа UnityEngine.Object и его наследников
✅ Проверяет массивы и отдельные объекты
✅ Показывает точные нарушения с путем к полю
✅ Использует тип из буфера как ограничение иерархии
Поддерживаемые форматы имен классов:
Полное имя: Core.ExternalEntityLogics.AbstractEntityAction
Короткое имя: AbstractEntityAction
Теперь можно глобально проверять корректность ссылок в иерархии!
*/
        
        [MenuItem("Auto/Validate/Hierarchy Bounds Check For Serialized Objects")]
        public static void ValidateSelectedObjectsHierarchy()
        {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("Hierarchy Check", "No objects selected", "OK");
                return;
            }

            string clipboardText = EditorGUIUtility.systemCopyBuffer;
            if (string.IsNullOrEmpty(clipboardText))
            {
                EditorUtility.DisplayDialog("Hierarchy Check", "Clipboard is empty. Copy class name first.", "OK");
                return;
            }

            Type limitType = GetTypeFromString(clipboardText.Trim());
            if (limitType == null)
            {
                EditorUtility.DisplayDialog("Hierarchy Check", 
                    $"Cannot find type: '{clipboardText}'\nMake sure to copy full class name.", "OK");
                return;
            }

            int violationCount = 0;
            var violationSummary = new List<string>();

            foreach (var selectedObject in selectedObjects)
            {
                var components = selectedObject.GetComponents<MonoBehaviour>();
                
                foreach (var component in components)
                {
                    var fields = GetSerializableObjectFields(component.GetType());
                    
                    foreach (var field in fields)
                    {
                        var fieldValue = field.GetValue(component);
                        
                        if (fieldValue == null) continue;
                        
                        if (fieldValue is UnityEngine.Object[] array)
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i] != null && !IsWithinAllowedHierarchy(array[i], component.transform, limitType))
                                {
                                    var violation = $"{component.name}.{component.GetType().Name}.{field.Name}[{i}] -> {array[i].name}";
                                    violationSummary.Add(violation);
                                    
                                    Debug.LogError($"Element {i} in {field.Name} array '{array[i].name}' is outside allowed hierarchy. " +
                                                 $"Objects must be within {limitType.Name} hierarchy of '{component.name}'", component);
                                    violationCount++;
                                }
                            }
                        }
                        else if (fieldValue is UnityEngine.Object obj)
                        {
                            if (!IsWithinAllowedHierarchy(obj, component.transform, limitType))
                            {
                                var violation = $"{component.name}.{component.GetType().Name}.{field.Name} -> {obj.name}";
                                violationSummary.Add(violation);
                                
                                Debug.LogError($"Field {field.Name} '{obj.name}' is outside allowed hierarchy. " +
                                             $"Objects must be within {limitType.Name} hierarchy of '{component.name}'", component);
                                violationCount++;
                            }
                        }
                    }
                }
            }

            if (violationCount == 0)
            {
                Debug.Log($"<color=green>Hierarchy Check: All {GetTotalFieldsChecked(selectedObjects)} references are valid within {limitType.Name} hierarchy!</color>");
            }
            else
            {
                var message = $"Found {violationCount} violation(s) outside {limitType.Name} hierarchy:\n\n" +
                             string.Join("\n", violationSummary.Take(10));
                
                if (violationSummary.Count > 10)
                    message += $"\n... and {violationSummary.Count - 10} more";
                    
                Debug.LogError($"<color=red>Hierarchy Check completed: {violationCount} violations found. Check console for clickable references.</color>");
            }
        }

        private static int GetTotalFieldsChecked(GameObject[] selectedObjects)
        {
            int count = 0;
            foreach (var selectedObject in selectedObjects)
            {
                var components = selectedObject.GetComponents<MonoBehaviour>();
                foreach (var component in components)
                {
                    var fields = GetSerializableObjectFields(component.GetType());
                    foreach (var field in fields)
                    {
                        var fieldValue = field.GetValue(component);
                        if (fieldValue == null) continue;
                        
                        if (fieldValue is UnityEngine.Object[] array)
                            count += array.Length;
                        else if (fieldValue is UnityEngine.Object)
                            count++;
                    }
                }
            }
            return count;
        }

        private static Type GetTypeFromString(string typeName)
        {
            // Пробуем найти тип в текущей сборке
            var assemblies = new[]
            {
                typeof(MonoBehaviour).Assembly, // UnityEngine
                Assembly.GetExecutingAssembly(), // Текущая сборка
                Assembly.Load("Assembly-CSharp"), // Основная сборка проекта
            };

            foreach (var assembly in assemblies)
            {
                try
                {
                    var type = assembly.GetType(typeName);
                    if (type != null) return type;
                    
                    // Пробуем найти по имени без namespace
                    var types = assembly.GetTypes().Where(t => t.Name == typeName || t.FullName.EndsWith("." + typeName));
                    var foundType = types.FirstOrDefault();
                    if (foundType != null) return foundType;
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Error loading assembly {assembly.FullName}: {e.Message}");
                }
            }

            return null;
        }

        private static FieldInfo[] GetSerializableObjectFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => 
                {
                    // Проверяем, что поле сериализуемо
                    bool isSerializable = field.IsPublic || field.GetCustomAttribute<SerializeField>() != null;
                    if (!isSerializable) return false;
                    
                    // Проверяем, что тип поля наследуется от UnityEngine.Object
                    var fieldType = field.FieldType;
                    
                    // Для массивов получаем тип элемента
                    if (fieldType.IsArray)
                        fieldType = fieldType.GetElementType();
                    
                    return typeof(UnityEngine.Object).IsAssignableFrom(fieldType);
                })
                .ToArray();
        }

        private static bool IsWithinAllowedHierarchy(UnityEngine.Object target, Transform scriptTransform, Type limitType)
        {
            // Для не-GameObject объектов (например, ScriptableObject) разрешаем
            if (!(target is GameObject) && !(target is Component))
                return true;
                
            Transform targetTransform = null;
            
            if (target is GameObject go)
                targetTransform = go.transform;
            else if (target is Component comp)
                targetTransform = comp.transform;
                
            if (targetTransform == null)
                return true;

            // Ищем компонент limitType вверх по иерархии от скрипта
            var limitComponent = scriptTransform.GetComponentInParent(limitType, true);
            var maxLevelTransform = limitComponent ? ((Component)limitComponent).transform : null;
            
            var currentTransform = scriptTransform;
            
            // Проверяем вверх по иерархии до limitType
            while (currentTransform != null)
            {
                if (IsTargetInSubtree(currentTransform, targetTransform))
                    return true;
                    
                // Если достигли максимального уровня, останавливаемся
                if (currentTransform == maxLevelTransform)
                    break;
                    
                currentTransform = currentTransform.parent;
            }
            
            return false;
        }

        private static bool IsTargetInSubtree(Transform root, Transform target)
        {
            if (root == target)
                return true;
                
            for (int i = 0; i < root.childCount; i++)
            {
                if (IsTargetInSubtree(root.GetChild(i), target))
                    return true;
            }
            
            return false;
        }
    }
}
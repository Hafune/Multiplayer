#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Core.Lib
{
    [ExecuteAlways]
    public class HierarchyNavigatorEditor : MonoBehaviour
    {
        public Object editorValue;
        public object value;
        public string fromPath = "";
        public string toPath = "";

        private void Awake() => hideFlags = HideFlags.DontSave;

        public void MoveUpHierarchy() => Navigate(transform.parent?.gameObject);
        public void MoveDownHierarchy() => Navigate(transform.childCount > 0 ? transform.GetChild(0).gameObject : null);
        public void SwitchToPreviousSibling() => NavigateToSibling(-1);
        public void SwitchToNextSibling() => NavigateToSibling(1);

        public void CreateGO(int offset)
        {
            var go = new GameObject();
            go.transform.SetParent(transform.parent, false);
            go.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1 + offset);
        }

        public void CreateChildGO()
        {
            var go = new GameObject();
            go.transform.SetParent(transform, false);
        }

        public void DeleteGO()
        {
            var go = gameObject;
            SwitchToPreviousSibling();
            DestroyImmediate(go);
        }

        public void FindComponentValue()
        {
            if (string.IsNullOrEmpty(fromPath))
            {
                value = gameObject;
                editorValue = gameObject;
                return;
            }

            var (componentName, memberPath) = ParsePath(fromPath);
            var components = gameObject.GetComponents<Component>();

            if (string.IsNullOrEmpty(componentName))
            {
                var targetName = memberPath;
                foreach (var component in components)
                {
                    var type = component.GetType();
                    if (type.Name == targetName)
                    {
                        value = component;
                        editorValue = value as Object;
                        return;
                    }
                }

                value = null;
                editorValue = null;
            }
            else
            {
                Component target = null;
                foreach (var component in components)
                {
                    var type = component.GetType();
                    if (type.Name == componentName)
                    {
                        target = component;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(memberPath))
                {
                    value = target;
                    editorValue = value as Object;
                    return;
                }

                var segments = memberPath.Split('.');
                value = DynamicUtility.GetValueByPath(target, segments);
                editorValue = value as Object;
            }
        }

        public void ApplyComponentValue()
        {
            var (componentName, memberPath) = ParsePath(toPath);
            var components = gameObject.GetComponents<Component>();
            Component target = null;
            foreach (var component in components)
            {
                var type = component.GetType();
                if (type.Name == componentName)
                {
                    target = component;
                    break;
                }
            }

            var segments = memberPath.Split('.');
            DynamicUtility.SetValueByPath(target, value, segments);
            EditorUtility.SetDirty(gameObject);
        }

        private void NavigateToSibling(int direction)
        {
            var parent = transform.parent;
            if (parent == null)
                return;

            var currentIndex = transform.GetSiblingIndex();
            var newIndex = direction > 0
                ? (currentIndex + 1) % parent.childCount
                : (currentIndex - 1 + parent.childCount) % parent.childCount;

            if (newIndex != currentIndex)
                Navigate(parent.GetChild(newIndex).gameObject);
        }

        private void Navigate(GameObject target)
        {
            if (target == null)
                return;

            var currentObject = gameObject;
            var navigator = target.AddComponent<HierarchyNavigatorEditor>();
            navigator.hideFlags = HideFlags.DontSave;
            navigator.editorValue = editorValue;
            navigator.value = value;
            navigator.fromPath = fromPath;
            navigator.toPath = toPath;

            DestroyImmediate(this);
            SelectionHelper.UpdateSelection(currentObject, target);
        }

        private (string componentName, string fieldName) ParsePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return ("", path);

            var separatorIndex = path.IndexOf('.');
            return separatorIndex < 0 ? ("", path) : (path.Substring(0, separatorIndex), path.Substring(separatorIndex + 1));
        }
    }

    public static class ComponentReflectionHelper
    {
        public static bool TryGetValue(Component component, string fieldName, out object value)
        {
            var type = component.GetType();
            var fields = type.GetFields(System.Reflection.BindingFlags.Public |
                                        System.Reflection.BindingFlags.NonPublic |
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                if (field.Name.IndexOf(fieldName, System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    value = field.GetValue(component);
                    return true;
                }
            }

            var properties = type.GetProperties(System.Reflection.BindingFlags.Public |
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.Instance |
                                                System.Reflection.BindingFlags.FlattenHierarchy);

            foreach (var property in properties)
            {
                if (property.Name.IndexOf(fieldName, System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    value = property.GetValue(component);
                    return true;
                }
            }

            value = null;
            return false;
        }

        public static bool TrySetValue(Component component, string fieldName, object value)
        {
            var type = component.GetType();
            var fields = type.GetFields(System.Reflection.BindingFlags.Public |
                                        System.Reflection.BindingFlags.NonPublic |
                                        System.Reflection.BindingFlags.Instance |
                                        System.Reflection.BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                if (field.Name.IndexOf(fieldName, System.StringComparison.OrdinalIgnoreCase) >= 0 &&
                    IsAssignable(field.FieldType, value))
                {
                    field.SetValue(component, value);
                    return true;
                }
            }

            var properties = type.GetProperties(System.Reflection.BindingFlags.Public |
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.Instance |
                                                System.Reflection.BindingFlags.FlattenHierarchy);

            foreach (var property in properties)
            {
                if (property.Name.IndexOf(fieldName, System.StringComparison.OrdinalIgnoreCase) >= 0 &&
                    property.CanWrite && IsAssignable(property.PropertyType, value))
                {
                    property.SetValue(component, value, null);
                    return true;
                }
            }

            return false;
        }

        private static bool IsAssignable(System.Type targetType, object value)
        {
            if (value == null)
                return !targetType.IsValueType ||
                       (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(System.Nullable<>));

            var valueType = value.GetType();
            return targetType.IsAssignableFrom(valueType) ||
                   (IsNumericType(targetType) && IsNumericType(valueType)) ||
                   targetType == typeof(string);
        }

        private static bool IsNumericType(System.Type type)
        {
            if (type == typeof(byte) || type == typeof(sbyte) ||
                type == typeof(short) || type == typeof(ushort) ||
                type == typeof(int) || type == typeof(uint) ||
                type == typeof(long) || type == typeof(ulong) ||
                type == typeof(float) || type == typeof(double) ||
                type == typeof(decimal))
                return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>))
                return IsNumericType(System.Nullable.GetUnderlyingType(type));

            return false;
        }
    }

    public static class SelectionHelper
    {
        public static void UpdateSelection(GameObject oldObject, GameObject newObject)
        {
            EditorApplication.delayCall += () =>
            {
                var selection = Selection.gameObjects;
                var newSelection = new List<GameObject>();

                foreach (var obj in selection)
                {
                    if (obj != oldObject)
                        newSelection.Add(obj);
                }

                newSelection.Add(newObject);
                Selection.objects = newSelection.ToArray();
            };
        }
    }

    [CustomEditor(typeof(HierarchyNavigatorEditor)), CanEditMultipleObjects]
    public class HierarchyNavigatorEditorInspector : Editor
    {
        SerializedProperty valueProp;
        SerializedProperty fromPathProp;
        SerializedProperty toPathProp;
        private double _pressedTime;

        private void OnEnable()
        {
            valueProp = serializedObject.FindProperty("editorValue");
            fromPathProp = serializedObject.FindProperty("fromPath");
            toPathProp = serializedObject.FindProperty("toPath");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Навигация по иерархии", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            DrawNavigationButtons();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Работа с компонентами", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(valueProp, new GUIContent("Компонент"));
            EditorGUILayout.PropertyField(fromPathProp, new GUIContent("Исходный путь"));
            EditorGUILayout.PropertyField(toPathProp, new GUIContent("Целевой путь"));

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            DrawComponentButtons();
        }

        private void DrawNavigationButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Влево")) ExecuteForAllTargets(t => t.MoveUpHierarchy());
            if (GUILayout.Button("Вправо")) ExecuteForAllTargets(t => t.MoveDownHierarchy());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Вверх")) ExecuteForAllTargets(t => t.SwitchToPreviousSibling());
            if (GUILayout.Button("Вниз")) ExecuteForAllTargets(t => t.SwitchToNextSibling());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Создать выше")) ExecuteForAllTargets(t => t.CreateGO(-1));
            if (GUILayout.Button("Создать ниже")) ExecuteForAllTargets(t => t.CreateGO(0));
            if (GUILayout.Button("Удалить")) ExecuteForAllTargets(t => t.DeleteGO());
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Создать ребёнка")) ExecuteForAllTargets(t => t.CreateChildGO());

            HandleKeyboardInput();
        }

        private void DrawComponentButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Найти значение")) ExecuteForAllTargets(t => t.FindComponentValue());
            if (GUILayout.Button("Применить значение")) ExecuteForAllTargets(t => t.ApplyComponentValue());
            EditorGUILayout.EndHorizontal();
        }

        private void ExecuteForAllTargets(System.Action<HierarchyNavigatorEditor> action)
        {
            foreach (var t in targets)
                action((HierarchyNavigatorEditor)t);
        }

        private void HandleKeyboardInput()
        {
            if (EditorGUIUtility.editingTextField)
                return;

            var k = Keyboard.current;
            if (k == null || EditorApplication.timeSinceStartup - _pressedTime < 0.25f)
                return;

            var currentTime = EditorApplication.timeSinceStartup;

            if (k.leftArrowKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.MoveUpHierarchy());
            }
            else if (k.rightArrowKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.MoveDownHierarchy());
            }
            else if (k.upArrowKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.SwitchToPreviousSibling());
            }
            else if (k.downArrowKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.SwitchToNextSibling());
            }
            else if (k.numpadPlusKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.CreateGO(0));
            }
            else if (k.numpadMinusKey.isPressed)
            {
                _pressedTime = currentTime;
                ExecuteForAllTargets(t => t.DeleteGO());
            }
        }
    }
}
#endif
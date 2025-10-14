using System;
using UnityEditor;
using UnityEngine;

namespace Lib
{
    [CustomPropertyDrawer(typeof(TypeCheckAttribute))]
    internal class TypeCheckAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo != null && fieldInfo.DeclaringType != null && fieldInfo.DeclaringType.IsValueType)
                throw new InvalidOperationException("TypeCheckAttribute is not allowed on struct members");

            EditorGUI.PropertyField(position, property, label, true);

            var requiredType = ((TypeCheckAttribute)attribute).type;

            if (property.isArray)
            {
                var size = property.arraySize;
                for (var i = 0; i < size; i++)
                {
                    var element = property.GetArrayElementAtIndex(i);
                    if (element.propertyType != SerializedPropertyType.ObjectReference)
                        continue;

                    var value = element.objectReferenceValue;
                    if (!value)
                        continue;

                    element.objectReferenceValue = requiredType.IsInstanceOfType(value) ? value : null;
                }
                return;
            }

            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return;

            if (!property.objectReferenceValue)
                return;

            property.objectReferenceValue = requiredType.IsInstanceOfType(property.objectReferenceValue)
                ? property.objectReferenceValue
                : null;
        }
    }
}
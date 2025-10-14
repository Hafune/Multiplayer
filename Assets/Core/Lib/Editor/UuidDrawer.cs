using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

/// <summary>
/// PropertyDrawer для UuidAttribute.
/// Поддерживает:
/// - [SerializeField] private string field
/// - public string field  
/// - [field: SerializeField] string property
/// Отображает поле как ReadOnly с кнопкой генерации UUID
/// </summary>
[CustomPropertyDrawer(typeof(UuidAttribute))]
public class UuidDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Проверяем что это строковое поле или свойство
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, label.text, "UuidAttribute can only be used with string fields/properties");
            return;
        }

        // Определяем тип поля/свойства для лучшего отображения
        var currentFieldInfo = this.fieldInfo ?? GetFieldInfo(property);
        var isProperty = IsBackingField(property.name);
        var displayName = isProperty ? GetPropertyDisplayName(property.name) : label.text;
        
        // Используем правильное имя для отображения
        var displayLabel = new GUIContent(displayName, label.tooltip);

        // Кнопка занимает фиксированное маленькое пространство (квадрат)
        var buttonWidth = position.height; // Квадратная кнопка
        var fieldRect = new Rect(position.x, position.y, position.width - buttonWidth - 5, position.height);
        var buttonRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, position.height);

        // Отключаем возможность редактирования (ReadOnly функция)
        //GUI.enabled = false;
        EditorGUI.PropertyField(fieldRect, property, displayLabel);
        //GUI.enabled = true;

        // Кнопка для генерации нового UUID с иконкой обновления
        var refreshIcon = EditorGUIUtility.IconContent("KnobCShape");
        refreshIcon.tooltip = "Generate new UUID";
        
        if (GUI.Button(buttonRect, refreshIcon))
        {
            property.stringValue = Guid.NewGuid().ToString();
            property.serializedObject.ApplyModifiedProperties();
            
            // Помечаем объект как измененный для сохранения
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }

    /// <summary>
    /// Получает FieldInfo для SerializedProperty
    /// </summary>
    private FieldInfo GetFieldInfo(SerializedProperty property)
    {
        var targetObject = property.serializedObject.targetObject;
        var targetType = targetObject.GetType();
        
        return targetType.GetField(property.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    /// Проверяет является ли поле backing field для свойства
    /// </summary>
    private bool IsBackingField(string fieldName)
    {
        return fieldName.StartsWith("<") && fieldName.Contains(">k__BackingField");
    }

    /// <summary>
    /// Получает отображаемое имя свойства из backing field
    /// </summary>
    private string GetPropertyDisplayName(string backingFieldName)
    {
        if (!IsBackingField(backingFieldName))
            return backingFieldName;

        // Извлекаем имя свойства из "<PropertyName>k__BackingField"
        var startIndex = 1; // После '<'
        var endIndex = backingFieldName.IndexOf('>');
        
        if (endIndex > startIndex)
            return backingFieldName.Substring(startIndex, endIndex - startIndex);
        
        return backingFieldName;
    }
}

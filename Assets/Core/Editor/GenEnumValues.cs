using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Lib;
using UnityEditor;
using UnityEngine;

namespace Core.Editor
{
    public static class GenEnumValues
    {
        // Запуск генерации для всех enum-ов с атрибутом [GenEnumValues]
        public static bool GenAll()
        {
            var anyChanges = false;
            
            // Находим все типы enum с атрибутом GenEnumValues
            var enumTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsEnum && Attribute.IsDefined(t, typeof(GenEnumValuesAttribute)))
                .ToList();

            foreach (var enumType in enumTypes)
            {
                if (UpdateEnum(enumType))
                {
                    anyChanges = true;
                    Debug.Log($"Updated enum: {enumType.FullName}");
                }
            }
            
            return anyChanges;
        }

        // Обновление конкретного enum-а
        private static bool UpdateEnum(Type enumType)
        {
            // Получаем атрибут для кастомной конфигурации
            var attribute = enumType.GetCustomAttribute<GenEnumValuesAttribute>();
            if (attribute == null)
                return false;

            // Получаем путь к файлу с enum-ом
            var scriptPath = FindScriptPathForType(enumType);
            if (string.IsNullOrEmpty(scriptPath))
            {
                Debug.LogError($"Could not find script file for enum: {enumType.FullName}");
                return false;
            }

            // Получаем содержимое файла
            var fileContent = File.ReadAllText(scriptPath);
            
            // Получаем имена полей enum-а
            var enumFieldNames = Enum.GetNames(enumType);
            
            // Генерируем словарь имя -> значение только для существующих полей
            var keyValues = GetEnumValues(enumFieldNames, attribute);
            
            // Генерируем новое содержимое enum-а
            var updatedContent = UpdateEnumContent(fileContent, enumType.Name, keyValues);
            
            // Если содержимое изменилось, записываем в файл
            if (fileContent != updatedContent)
            {
                File.WriteAllText(scriptPath, updatedContent);
                AssetDatabase.Refresh();
                return true;
            }
            
            return false;
        }

        // Получение значений для существующих полей enum-а
        private static Dictionary<string, int> GetEnumValues(string[] enumFieldNames, GenEnumValuesAttribute attribute)
        {
            var keyValues = new Dictionary<string, int>();
            
            // Для каждого существующего поля enum-а вычисляем hash-значение
            foreach (var fieldName in enumFieldNames)
            {
                keyValues[fieldName] = Animator.StringToHash(fieldName);
            }
            
            return keyValues;
        }

        // Улучшенный поиск пути к файлу с enum-ом
        private static string FindScriptPathForType(Type type)
        {
            var typeName = type.Name;
            var nameSpace = type.Namespace;
            var potentialPaths = new List<string>();
            
            // Сначала попробуем найти по имени типа (быстрее)
            var guidsByName = AssetDatabase.FindAssets($"t:Script {typeName}");
            foreach (var guid in guidsByName)
            {
                potentialPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }
            
            // Ищем все скрипты в проекте, если не нашли по имени
            if (potentialPaths.Count == 0)
            {
                var allScripts = AssetDatabase.FindAssets("t:Script");
                foreach (var guid in allScripts)
                {
                    potentialPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
                }
            }
            
            // Паттерны для разных сценариев размещения enum-а
            string[] patterns = {
                // Enum верхнего уровня
                $@"(public|internal|private)?\s+enum\s+{Regex.Escape(typeName)}\b\s*\{{",
                
                // Enum внутри класса
                $@"(public|internal|private)?\s+class\s+\w+[\s\S]*?(public|internal|private)?\s+enum\s+{Regex.Escape(typeName)}\b\s*\{{",
                
                // Enum внутри структуры
                $@"(public|internal|private)?\s+struct\s+\w+[\s\S]*?(public|internal|private)?\s+enum\s+{Regex.Escape(typeName)}\b\s*\{{"
            };
            
            foreach (var path in potentialPaths)
            {
                try
                {
                    var content = File.ReadAllText(path);
                    
                    // Проверяем содержит ли файл необходимый namespace
                    if (!string.IsNullOrEmpty(nameSpace) && !content.Contains($"namespace {nameSpace}"))
                        continue;
                    
                    // Проверяем каждый паттерн
                    foreach (var pattern in patterns)
                    {
                        if (Regex.IsMatch(content, pattern))
                        {
                            return path;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error reading file {path}: {ex.Message}");
                }
            }
            
            // Более глубокий поиск, если предыдущие методы не сработали
            foreach (var path in potentialPaths)
            {
                try 
                {
                    var content = File.ReadAllText(path);
                    
                    // Ищем полное имя типа (с namespace)
                    var fullTypeName = string.IsNullOrEmpty(nameSpace) ? typeName : $"{nameSpace}.{typeName}";
                    if (content.Contains(fullTypeName))
                    {
                        // Более тщательная проверка через парсинг содержимого
                        using (var reader = new StringReader(content))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains($"enum {typeName}") || line.Contains($"enum {fullTypeName}"))
                                {
                                    return path;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error in deep search for file {path}: {ex.Message}");
                }
            }
            
            // Последняя попытка - поиск по всем C# файлам в проекте
            var allCSharpFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
            foreach (var filePath in allCSharpFiles)
            {
                try
                {
                    var content = File.ReadAllText(filePath);
                    if (content.Contains($"enum {typeName}") && 
                        (string.IsNullOrEmpty(nameSpace) || content.Contains($"namespace {nameSpace}")))
                    {
                        // Превращаем абсолютный путь в относительный путь Unity
                        var relativePath = "Assets" + filePath.Substring(Application.dataPath.Length);
                        return relativePath;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error reading file {filePath}: {ex.Message}");
                }
            }
            
            return null;
        }

        // Обновление содержимого файла, сохраняя существующие поля enum-а и только обновляя их значения
        private static string UpdateEnumContent(string fileContent, string enumName, Dictionary<string, int> keyValues)
        {
            // Шаблон для поиска блока enum-а
            var pattern = $@"(public|internal|private)?\s+enum\s+{enumName}\s*\{{([\s\S]*?)\}}";
            
            return Regex.Replace(fileContent, pattern, match =>
            {
                // Получаем весь блок enum-а
                var enumBlock = match.Value;
                
                // Полностью пересобираем тело enum-а из известных имён (без частичных совпадений)
                var openIdx = enumBlock.IndexOf('{') + 1;
                var closeIdx = enumBlock.LastIndexOf('}');
                if (openIdx <= 0 || closeIdx <= openIdx)
                    return enumBlock; // не нашли корректные скобки — ничего не меняем

                var header = enumBlock.Substring(0, openIdx);
                var inside = enumBlock.Substring(openIdx, closeIdx - openIdx);
                var footer = enumBlock.Substring(closeIdx);

                // Индентация строки с закрывающей скобкой в оригинальном коде
                var newlineBeforeClose = enumBlock.LastIndexOf('\n', closeIdx);
                var closingIndent = newlineBeforeClose >= 0
                    ? enumBlock.Substring(newlineBeforeClose + 1, closeIdx - (newlineBeforeClose + 1))
                    : string.Empty;

                // Определяем отступ по первой строке содержимого
                var indentMatch = Regex.Match(inside, @"\n([\t ]*)");
                var indent = indentMatch.Success ? indentMatch.Groups[1].Value : "        ";

                // Сохраняем исходный порядок полей по их первому вхождению в исходном теле
                var orderedNames = keyValues.Keys
                    .Select(n => new { Name = n, Index = inside.IndexOf(n, StringComparison.Ordinal) })
                    .OrderBy(p => p.Index < 0 ? int.MaxValue : p.Index)
                    .ThenBy(p => p.Name)
                    .Select(p => p.Name)
                    .ToList();

                var sb = new System.Text.StringBuilder();
                sb.Append('\n');
                sb.Append(indent).Append("// Автоматически сгенерированные значения");
                foreach (var name in orderedNames)
                {
                    sb.Append('\n');
                    sb.Append(indent).Append(name).Append(" = ").Append(keyValues[name]).Append(',');
                }
                sb.Append('\n');

                return header + sb.ToString() + closingIndent + footer;
            });
        }
    }

    //Расширение для интеграции с существующим генератором
    public static class GenEnumValuesExtension
    {
        [MenuItem("Auto/Gen/Update Enum Values")]
        public static void GenerateEnumValues()
        {
            GenEnumValues.GenAll();
        }
    }
}
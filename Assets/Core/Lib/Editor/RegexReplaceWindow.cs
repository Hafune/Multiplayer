using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Core.Lib
{
    public class RegexReplaceWindow : EditorWindow
    {
        private string inputText;
        private string regexPattern;
        private string replacementText;
        private string resultText;

        private const string InputKey = "RegexReplace_Input";
        private const string PatternKey = "RegexReplace_Pattern";
        private const string ReplacementKey = "RegexReplace_Replacement";
        private const string ResultKey = "RegexReplace_Result";
        private const string PatternHistoryKey = "RegexReplace_PatternHistory";
        private const string ReplacementHistoryKey = "RegexReplace_ReplacementHistory";

        private List<string> patternHistory = new();
        private List<string> replacementHistory = new();

        private Vector2 inputScrollPos;
        private Vector2 resultScrollPos;
        private bool showPatternHistory = false;
        private bool showReplacementHistory = false;

        private const int MaxHistory = 20;

        [MenuItem("Auto/Regex Replace Tool")]
        public static void ShowWindow()
        {
            GetWindow<RegexReplaceWindow>("Regex Replacer");
        }

        private void OnEnable()
        {
            inputText = EditorPrefs.GetString(InputKey, "Бла 50% бла");
            regexPattern = EditorPrefs.GetString(PatternKey, @"\d+%");
            replacementText = EditorPrefs.GetString(ReplacementKey, "<color=\"green\">$0</color>");
            resultText = EditorPrefs.GetString(ResultKey, "");

            patternHistory = LoadHistory(PatternHistoryKey);
            replacementHistory = LoadHistory(ReplacementHistoryKey);
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(InputKey, inputText);
            EditorPrefs.SetString(PatternKey, regexPattern);
            EditorPrefs.SetString(ReplacementKey, replacementText);
            EditorPrefs.SetString(ResultKey, resultText);

            SaveHistory(PatternHistoryKey, patternHistory);
            SaveHistory(ReplacementHistoryKey, replacementHistory);
        }

        private void OnGUI()
        {
            // Общий контейнер с отступами
            EditorGUILayout.BeginVertical(GUI.skin.box);
            
            GUILayout.Label("Regex Replacer", EditorStyles.boldLabel);

            // Исходный текст в прокручиваемой области
            EditorGUILayout.LabelField("Исходный текст:");
            inputScrollPos = EditorGUILayout.BeginScrollView(inputScrollPos, GUILayout.Height(100));
            inputText = EditorGUILayout.TextArea(inputText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(10);

            // Блок с регулярным выражением
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Регулярное выражение:", EditorStyles.boldLabel);
            regexPattern = EditorGUILayout.TextField(regexPattern);
            
            // Фолдаут для истории паттернов
            showPatternHistory = EditorGUILayout.Foldout(showPatternHistory, "История паттернов", true);
            if (showPatternHistory && patternHistory.Count > 0)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < patternHistory.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    
                    // Используем кнопку вместо выпадающего списка
                    if (GUILayout.Button(patternHistory[i], EditorStyles.label, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 60)))
                    {
                        regexPattern = patternHistory[i];
                    }
                    
                    // Кнопка удаления из истории
                    if (GUILayout.Button("×", GUILayout.Width(20)))
                    {
                        patternHistory.RemoveAt(i);
                        break;
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(5);

            // Блок с текстом замены
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Строка замены:", EditorStyles.boldLabel);
            replacementText = EditorGUILayout.TextField(replacementText);
            
            // Фолдаут для истории замен
            showReplacementHistory = EditorGUILayout.Foldout(showReplacementHistory, "История замен", true);
            if (showReplacementHistory && replacementHistory.Count > 0)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < replacementHistory.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    
                    // Используем кнопку вместо выпадающего списка
                    if (GUILayout.Button(replacementHistory[i], EditorStyles.label, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 60)))
                    {
                        replacementText = replacementHistory[i];
                    }
                    
                    // Кнопка удаления из истории
                    if (GUILayout.Button("×", GUILayout.Width(20)))
                    {
                        replacementHistory.RemoveAt(i);
                        break;
                    }
                    
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);

            // Центрированная кнопка применения
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Применить", GUILayout.Width(120), GUILayout.Height(30)))
            {
                try
                {
                    resultText = Regex.Replace(inputText, regexPattern, replacementText);
                    AddToHistory(patternHistory, regexPattern);
                    AddToHistory(replacementHistory, replacementText);
                    EditorGUIUtility.systemCopyBuffer = resultText;
                }
                catch (System.Exception ex)
                {
                    resultText = $"Ошибка: {ex.Message}";
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            // Результат в прокручиваемой области
            EditorGUILayout.LabelField("Результат:");
            resultScrollPos = EditorGUILayout.BeginScrollView(resultScrollPos, GUILayout.Height(100));
            resultText = EditorGUILayout.TextArea(resultText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();

            // Кнопки копирования и вставки
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Копировать результат", GUILayout.Height(24)))
            {
                EditorGUIUtility.systemCopyBuffer = resultText;
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }

        // ---------- История ----------
        private void AddToHistory(List<string> list, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            list.Remove(value); // Удалим, если уже есть
            list.Insert(0, value); // Вставим в начало

            if (list.Count > MaxHistory)
                list.RemoveAt(list.Count - 1);
        }

        private void SaveHistory(string key, List<string> list)
        {
            var json = JsonUtility.ToJson(new StringListWrapper { list = list });
            EditorPrefs.SetString(key, json);
        }

        private List<string> LoadHistory(string key)
        {
            var json = EditorPrefs.GetString(key, "");
            if (string.IsNullOrEmpty(json)) return new List<string>();

            try
            {
                return JsonUtility.FromJson<StringListWrapper>(json)?.list ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        [System.Serializable]
        private class StringListWrapper
        {
            public List<string> list = new();
        }
    }
}
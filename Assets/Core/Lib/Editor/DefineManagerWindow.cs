using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class DefineManagerWindow : EditorWindow
    {
        // Предопределенный список дефайнов
        private static readonly List<string> DefaultDefines = new List<string>
        {
            "VK_GAMES",
            "VK_GAMES_MOBILE",
            "YANDEX_GAMES",
            "CRAZY_GAMES",
            "DEBUG_IGNORE_SAVE",
            // Добавьте здесь свои дефайны
        };

        // Список всех дефайнов (предопределенные + пользовательские)
        private List<string> _allDefines = new List<string>();

        // Состояние включения дефайнов
        private Dictionary<string, bool> _defineStates = new Dictionary<string, bool>();

        // Прокрутка для списка дефайнов
        private Vector2 _scrollPosition;

        // Новый дефайн для добавления
        private string _newDefine = "";

        // Для фильтрации дефайнов
        private string _searchFilter = "";

        // Ключ для хранения пользовательских дефайнов
        private const string UserDefinesKey = "UserDefinedSymbols";

        [MenuItem("Auto/Define Manager Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<DefineManagerWindow>("Define Manager");
            window.minSize = new Vector2(300, 300);
            window.Show();
        }

        private void OnEnable()
        {
            LoadUserDefines();
            LoadCurrentDefines();
        }

        private void LoadUserDefines()
        {
            // Загружаем пользовательские дефайны
            string userDefinesString = EditorPrefs.GetString(UserDefinesKey, "");
            List<string> userDefines = new List<string>();

            if (!string.IsNullOrEmpty(userDefinesString))
            {
                userDefines = userDefinesString.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            // Объединяем предопределенные и пользовательские дефайны
            _allDefines = new List<string>(DefaultDefines);

            foreach (var define in userDefines)
            {
                if (!_allDefines.Contains(define))
                {
                    _allDefines.Add(define);
                }
            }
        }

        private void SaveUserDefines()
        {
            // Сохраняем только пользовательские дефайны (не входящие в предопределенный список)
            List<string> userDefines = _allDefines.Where(d => !DefaultDefines.Contains(d)).ToList();
            string userDefinesString = string.Join(";", userDefines);
            EditorPrefs.SetString(UserDefinesKey, userDefinesString);
        }

        private void LoadCurrentDefines()
        {
            // Получаем текущие дефайны
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            List<string> currentDefines = defineSymbols.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            // Инициализируем состояние для всех наших дефайнов
            _defineStates.Clear();
            foreach (var define in _allDefines)
            {
                _defineStates[define] = currentDefines.Contains(define);
            }

            // Проверяем, есть ли в текущих дефайнах те, которых нет в нашем списке
            foreach (var define in currentDefines)
            {
                if (!_allDefines.Contains(define) && !string.IsNullOrEmpty(define))
                {
                    _allDefines.Add(define);
                    _defineStates[define] = true;
                }
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Управление дефайнами", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);

            // Кнопки для предустановленных наборов
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("VK Games"))
            {
                SetPreset("VK_GAMES");
            }

            if (GUILayout.Button("Yandex Games"))
            {
                SetPreset("YANDEX_GAMES");
            }

            if (GUILayout.Button("Crazy Games"))
            {
                SetPreset("CRAZY_GAMES");
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            // Поле для фильтрации дефайнов
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Фильтр:", GUILayout.Width(50));
            _searchFilter = EditorGUILayout.TextField(_searchFilter);
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                _searchFilter = "";
                GUI.FocusControl(null);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            // Скролл для списка дефайнов
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandHeight(true));

            // Отображаем список всех дефайнов с галочками
            for (int i = 0; i < _allDefines.Count; i++)
            {
                string define = _allDefines[i];

                // Применяем фильтр
                if (!string.IsNullOrEmpty(_searchFilter) && !define.ToLower().Contains(_searchFilter.ToLower()))
                {
                    continue;
                }

                EditorGUILayout.BeginHorizontal();

                bool currentState = _defineStates[define];
                bool newState = EditorGUILayout.ToggleLeft(define, currentState, GUILayout.ExpandWidth(true));

                if (newState != currentState)
                {
                    _defineStates[define] = newState;
                }

                bool isDefault = DefaultDefines.Contains(define);
                GUI.enabled = !isDefault;
                if (GUILayout.Button("Удалить", GUILayout.Width(70)))
                {
                    if (EditorUtility.DisplayDialog("Удаление дефайна",
                            $"Вы уверены, что хотите удалить дефайн '{define}'?",
                            "Удалить", "Отмена"))
                    {
                        _allDefines.RemoveAt(i);
                        _defineStates.Remove(define);
                        SaveUserDefines();
                        i--; // Компенсируем удаление элемента из списка
                    }
                }

                GUI.enabled = true;

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(5);

            // Поле для добавления нового дефайна
            EditorGUILayout.BeginHorizontal();
            _newDefine = EditorGUILayout.TextField(_newDefine);

            bool canAdd = !string.IsNullOrWhiteSpace(_newDefine) && !_allDefines.Contains(_newDefine);
            GUI.enabled = canAdd;

            if (GUILayout.Button("Добавить", GUILayout.Width(80)))
            {
                if (canAdd)
                {
                    _allDefines.Add(_newDefine);
                    _defineStates[_newDefine] = false;
                    SaveUserDefines();
                    _newDefine = "";
                    GUI.FocusControl(null);
                }
            }

            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();

            if (!canAdd && !string.IsNullOrWhiteSpace(_newDefine) && _allDefines.Contains(_newDefine))
            {
                EditorGUILayout.HelpBox("Такой дефайн уже существует!", MessageType.Warning);
            }

            EditorGUILayout.Space(10);

            // Кнопка применения изменений
            if (GUILayout.Button("Применить", GUILayout.Height(30)))
            {
                ApplyDefines();
            }
        }

        private void SetPreset(string mainDefine)
        {
            // Сначала отключаем все дефайны
            foreach (var define in _allDefines)
            {
                _defineStates[define] = false;
            }

            // Включаем только выбранный дефайн
            _defineStates[mainDefine] = true;

            // Если это VK_GAMES, то включаем также VK_GAMES_MOBILE
            if (mainDefine == "VK_GAMES" && _defineStates.ContainsKey("VK_GAMES_MOBILE"))
            {
                _defineStates["VK_GAMES_MOBILE"] = true;
            }

            // Автоматически применяем изменения
            ApplyDefines();
        }

        private void ApplyDefines()
        {
            // Собираем включенные дефайны
            List<string> enabledDefines = new List<string>();
            foreach (var pair in _defineStates)
            {
                if (pair.Value)
                {
                    enabledDefines.Add(pair.Key);
                }
            }

            // Получаем все текущие дефайны (включая те, которые не в нашем списке)
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            List<string> currentDefines = defineSymbols.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            // Удаляем наши управляемые дефайны из текущего списка
            currentDefines = currentDefines.Where(d => !_allDefines.Contains(d)).ToList();

            // Добавляем выбранные дефайны
            currentDefines.AddRange(enabledDefines);

            // Сохраняем результат
            string newDefines = string.Join(";", currentDefines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefines);

            Debug.Log($"Применены дефайны: {string.Join(", ", enabledDefines)}");
        }
    }
}
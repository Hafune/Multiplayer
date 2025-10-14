using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Редакторское окно для фильтрации выделенных объектов
/// Позволяет открывать несколько экземпляров с разными фильтрами
/// </summary>
public class FilterSelectionWindow : EditorWindow
{
    // Хранит текущие выделенные объекты при создании окна
    private Object[] savedSelectionObjects;

    // Хранит отфильтрованные объекты для отображения в списке
    private Object[] displayedObjects;

    // Статический счетчик для уникальной нумерации окон
    private static int windowCounter = 0;

    // Наименование окна
    private string windowName = "Selection Filter";

    // Для прокрутки списка объектов
    private Vector2 objectListScrollPosition;
    private List<SelectionFilterBase> filters = new()
    {
        new PathFilter(),
        // новые фильтры добавляйте сюда
        new TextureFilter()
    };

    // Создание нового окна через меню Unity
    [MenuItem("Auto/Selection Filter Window")]
    public static void ShowWindow()
    {
        // Создаем новый экземпляр окна с уникальным идентификатором
        windowCounter++;
        var window = CreateInstance<FilterSelectionWindow>();
        window.titleContent = new GUIContent($"Selection Filter #{windowCounter}");
        window.windowName = $"Selection Filter #{windowCounter}";
        window.Initialize();
        window.Show();
    }

    // Инициализация окна при создании
    private void Initialize()
    {
        // Сохраняем текущие выделенные объекты
        savedSelectionObjects = Selection.objects;
        // Изначально отображаемые объекты совпадают с сохраненными
        displayedObjects = savedSelectionObjects;
    }

    // Отрисовка окна
    private void OnGUI()
    {
        GUILayout.Label(windowName, EditorStyles.boldLabel);

        EditorGUILayout.Space();

        // Кнопка для обновления списка выделенных объектов
        if (GUILayout.Button("Обновить список выделенных объектов"))
        {
            savedSelectionObjects = Selection.objects;
            displayedObjects = savedSelectionObjects;
            Repaint();
        }

        EditorGUILayout.Space();
        DrawFilters();
        EditorGUILayout.Space();

        // Кнопки для фильтрации и выделения
        EditorGUILayout.BeginHorizontal();

        // Кнопка для фильтрации объектов в списке
        if (GUILayout.Button("Отфильтровать объекты"))
        {
            FilterObjects();
        }

        // Кнопка для выделения текущих объектов из списка
        if (GUILayout.Button("Выделить текущие объекты"))
        {
            SelectDisplayedObjects();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Список объектов занимает все оставшееся пространство
        DrawObjectList();
    }

    // Отрисовка и обработка всех фильтров
    private void DrawFilters()
    {
        foreach (var filter in filters)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            filter.IsExpanded = EditorGUILayout.Foldout(filter.IsExpanded, filter.Title, true);
            if (filter.IsExpanded)
                filter.DrawUI();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }
    }

    // Отрисовка списка объектов с возможностью удаления
    private void DrawObjectList()
    {
        EditorGUILayout.LabelField("Список объектов:", EditorStyles.boldLabel);

        if (displayedObjects == null || displayedObjects.Length == 0)
        {
            EditorGUILayout.HelpBox("Нет объектов для отображения", MessageType.Info);
            return;
        }

        // Начало области прокрутки с растягиванием на все доступное место
        objectListScrollPosition = EditorGUILayout.BeginScrollView(
            objectListScrollPosition,
            GUILayout.ExpandHeight(true));

        // Создаем список для отслеживания объектов, которые нужно удалить
        List<Object> objectsToRemove = new List<Object>();

        // Отображаем каждый объект с кнопкой удаления
        foreach (var obj in displayedObjects)
        {
            if (obj == null)
            {
                objectsToRemove.Add(obj);
                continue;
            }

            EditorGUILayout.BeginHorizontal();

            // Отображаем иконку и имя объекта
            EditorGUILayout.ObjectField(obj, typeof(Object), false);

            // Добавляем кнопку удаления
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                objectsToRemove.Add(obj);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        // Удаляем объекты из списка, если были нажаты кнопки удаления
        if (objectsToRemove.Count > 0)
        {
            var newList = new List<Object>(displayedObjects);
            foreach (var obj in objectsToRemove)
            {
                newList.Remove(obj);
            }

            displayedObjects = newList.ToArray();

            // Если мы отображаем все сохраненные объекты, то также обновляем и их
            if (object.ReferenceEquals(displayedObjects, savedSelectionObjects))
            {
                savedSelectionObjects = displayedObjects;
            }

            Repaint();
        }
    }

    // Фильтрует объекты согласно активным фильтрам
    private void FilterObjects()
    {
        if (savedSelectionObjects == null || savedSelectionObjects.Length == 0)
        {
            Debug.Log("Нет сохраненных объектов для фильтрации");
            return;
        }

        IEnumerable<Object> result = savedSelectionObjects;
        foreach (var filter in filters)
        {
            result = filter.Apply(result);
        }

        displayedObjects = result.ToArray();
        Debug.Log($"Отфильтровано {displayedObjects.Length} объектов из {savedSelectionObjects.Length}");
        // savedSelectionObjects = displayedObjects.ToArray();
    }

    // Выделяет текущие отображаемые объекты
    private void SelectDisplayedObjects()
    {
        if (displayedObjects == null || displayedObjects.Length == 0)
        {
            Debug.Log("Нет объектов для выделения");
            return;
        }

        // Выделяем отображаемые объекты
        Selection.objects = displayedObjects;

        Debug.Log($"Выделено {displayedObjects.Length} объектов");
    }
}

public abstract class SelectionFilterBase
{
    public abstract string Title { get; }
    public bool IsExpanded { get; set; } = true;

    // UI и логика фильтрации
    public abstract void DrawUI();
    public abstract IEnumerable<Object> Apply(IEnumerable<Object> objects);
}

public class PathFilter : SelectionFilterBase
{
    public override string Title => "Фильтр по пути ассета";
    public string PathPart = "";
    public bool IgnoreCase = false;
    public bool Exclude = false;

    public override void DrawUI()
    {
        EditorGUI.indentLevel++;
        PathPart = EditorGUILayout.TextField("Часть пути:", PathPart);
        IgnoreCase = EditorGUILayout.Toggle("Ignore Case:", IgnoreCase);
        Exclude = EditorGUILayout.Toggle("Исключить:", Exclude);
        EditorGUI.indentLevel--;
    }

    public override IEnumerable<Object> Apply(IEnumerable<Object> objects)
    {
        if (string.IsNullOrEmpty(PathPart))
            return objects;

        var filtered = objects.Where(obj =>
            AssetDatabase.GetAssetPath(obj).ToLower().Contains(IgnoreCase ? PathPart.ToLower() : PathPart));

        return Exclude ? objects.Except(filtered) : filtered;
    }
}

public class TextureFilter : SelectionFilterBase
{
    public override string Title => "Фильтр по текстурам";

    public enum ComparisonType { GreaterThan, LessThan, EqualTo }

    public bool filterWidth = false;
    public bool filterHeight = false;

    public int widthValue = 1024;
    public int heightValue = 1024;

    public ComparisonType widthComparison = ComparisonType.GreaterThan;
    public ComparisonType heightComparison = ComparisonType.GreaterThan;

    public override void DrawUI()
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.LabelField("Фильтрация по типу", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Оставляет только объекты типа Texture", MessageType.Info);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Фильтрация по Max Size (из импортера)", EditorStyles.boldLabel);

        filterWidth = EditorGUILayout.Toggle("Фильтровать по ширине", filterWidth);
        if (filterWidth)
        {
            widthComparison = (ComparisonType)EditorGUILayout.EnumPopup("Условие ширины", widthComparison);
            widthValue = EditorGUILayout.IntField("Max Width", widthValue);
        }

        filterHeight = EditorGUILayout.Toggle("Фильтровать по высоте", filterHeight);
        if (filterHeight)
        {
            heightComparison = (ComparisonType)EditorGUILayout.EnumPopup("Условие высоты", heightComparison);
            heightValue = EditorGUILayout.IntField("Max Height", heightValue);
        }

        EditorGUI.indentLevel--;
    }

    public override IEnumerable<Object> Apply(IEnumerable<Object> objects)
    {
        var textures = objects.OfType<Texture>();
        var result = new List<Object>();

        foreach (var tex in textures)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
                continue;

            int maxWidth = importer.maxTextureSize;
            int maxHeight = importer.maxTextureSize; // Unity использует квадратное ограничение max size

            if (filterWidth && !Compare(maxWidth, widthValue, widthComparison))
                continue;

            if (filterHeight && !Compare(maxHeight, heightValue, heightComparison))
                continue;

            result.Add(tex);
        }

        return result;
    }

    private bool Compare(int actual, int target, ComparisonType type)
    {
        switch (type)
        {
            case ComparisonType.GreaterThan: return actual > target;
            case ComparisonType.LessThan: return actual < target;
            case ComparisonType.EqualTo: return actual == target;
            default: return false;
        }
    }
}


public class TextureTypeFilter : SelectionFilterBase
{
    public override string Title => "Фильтр по типу текстуры";

    public TextureImporterType SelectedType = TextureImporterType.Default;
    public bool Exclude = false;

    public override void DrawUI()
    {
        EditorGUI.indentLevel++;
        SelectedType = (TextureImporterType)EditorGUILayout.EnumPopup("Тип текстуры:", SelectedType);
        Exclude = EditorGUILayout.Toggle("Исключить:", Exclude);
        EditorGUI.indentLevel--;
    }

    public override IEnumerable<Object> Apply(IEnumerable<Object> objects)
    {
        List<Object> matched = new List<Object>();

        foreach (var obj in objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path))
                continue;

            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
                continue;

            if (importer.textureType == SelectedType)
                matched.Add(obj);
        }

        return Exclude ? objects.Except(matched) : matched;
    }
}

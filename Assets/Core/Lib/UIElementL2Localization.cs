using System.Collections.Generic;
using I2.Loc;
using UnityEngine.UIElements;

public class UIElementL2Localization
{
    private readonly Dictionary<VisualElement, string> _originalTexts = new();

    private string _table;
    private VisualElement _rootEle;
    private Dictionary<string, string> _keysCache = new();

    public UIElementL2Localization(VisualElement rootEle, string tableName)
    {
        _rootEle = rootEle;
        _table = tableName;
        LocalizationManager.OnLocalizeEvent += OnLanguageChange;
        OnLanguageChange();
    }

    private void OnLanguageChange()
    {
        LocalizeChildrenRecursively(_rootEle, _table);
        _rootEle.MarkDirtyRepaint();
    }

    private void LocalizeChildrenRecursively(VisualElement element, string table)
    {
        var elementHierarchy = element.hierarchy;
        int numChildren = elementHierarchy.childCount;

        for (int i = 0; i < numChildren; i++)
        {
            var child = elementHierarchy.ElementAt(i);
            Localize(child, table);
        }

        for (int i = 0; i < numChildren; i++)
        {
            var child = elementHierarchy.ElementAt(i);
            var childHierarchy = child.hierarchy;

            if (childHierarchy.childCount != 0)
                LocalizeChildrenRecursively(child, table);
        }
    }

    private void Localize(VisualElement next, string table)
    {
        if (_originalTexts.TryGetValue(next, out var text))
            ((TextElement)next).text = text;

        if (next is not TextElement textElement)
            return;

        string key = textElement.text;

        if (string.IsNullOrEmpty(key) || key[0] != '#')
            return;

        if (!_keysCache.TryGetValue(key, out var validKey))
            _keysCache[key] = validKey = table + "/" + key.TrimStart('#');
        
        LocalizationManager.TryGetTranslation(validKey, out var localizedText);
        
        if (string.IsNullOrEmpty(localizedText) || localizedText[0] == '#')
            return;
        
        if (!_originalTexts.ContainsKey(textElement))
            _originalTexts.Add(textElement, textElement.text);
        
        textElement.text = localizedText;
    }
}
using System;
using Core.InputSprites;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class AbstractUIDocumentView : MonoConstruct
    {
        [SerializeField] private UIDocument _document;
        public VisualElement RootVisualElement => _document.rootVisualElement;
        private void OnValidate() => _document = _document ? _document : GetComponent<UIDocument>();

        [Obsolete("использовать OnAwake")]
        protected virtual void Awake()
        {
            _document.rootVisualElement.DisplayNone();
            RootVisualElement.Query<VisualElement>()
                .Where(ele => ele is IContextElement)
                .ForEach(ele => ((IContextElement)ele).SetupContext(context));

            OnAwake();
            SetPickingModeIgnore(_document.rootVisualElement);
        }
        
        protected virtual void OnAwake(){}

        protected void DisplayFlex() => _document.rootVisualElement.DisplayFlex();

        protected void DisplayNone() => _document.rootVisualElement.DisplayNone();
        
        /// <summary>
        /// Устанавливает <b>PickingMode.Ignore</b> для всех элементов дерева 
        /// </summary>
        private static void SetPickingModeIgnore(VisualElement element)
        {
            element.pickingMode = PickingMode.Ignore;
            SetPickingModeIgnoreRecursive(element);
        }

        private static void SetPickingModeIgnoreRecursive(VisualElement element)
        {
            if (element.tabIndex == -1)
                return;
                
            element.pickingMode = PickingMode.Ignore;

            foreach (var child in element.Children())
                SetPickingModeIgnoreRecursive(child);
        }
    }
}
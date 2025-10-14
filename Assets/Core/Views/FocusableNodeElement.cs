using Core.InputSprites;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableNodeElement : VisualElement, IContextElement
    {
        private UiFocusableService _service;

        public bool IsActive { get; private set; }

        public FocusableNodeElement()
        {
            RegisterCallback<PointerDownEvent>(evt =>
            {
                evt.PreventDefault();
                evt.StopPropagation();
            });
            RegisterCallback<PointerUpEvent>(evt =>
            {
                evt.PreventDefault();
                evt.StopPropagation();
            });

            style.position = Position.Absolute;
            style.left = 0f;
            style.right = 0f;
            style.top = 0f;
            style.bottom = 0f;
        }

        public void SetupContext(Context context) => _service = context.Resolve<UiFocusableService>();

        public void AddLayer(bool focusFirstFocusable = false)
        {
            _service.AddLayer(this);
            
            if (!IsActive)
                Debug.LogError("Нарушена очередь добавления слоев");
                
            if (focusFirstFocusable)
                schedule.Execute(() => this.FocusFirstFocusableElement());
        }

        public void RemoveLayer()
        {
            _service.RemoveLayer();
            
            if (IsActive)
                Debug.LogError("Нарушена очередь удаления слоев");
        }
        
        public void RefreshActiveChildren() => RefreshFocusableRecursive(this);

        public void SetActiveInternal(bool active)
        {
            IsActive = active;
            pickingMode = active ? PickingMode.Position : PickingMode.Ignore;
            RefreshFocusableRecursive(this);
        }

        private void RefreshFocusableRecursive(VisualElement ele)
        {
            foreach (var child in ele.Children())
            {
                if (child is FocusableNodeElement)
                    continue;

                RefreshFocusableRecursive(child);

                if (child is IFocusableElement element)
                    element.FocusableElement.SetActive(IsActive);
            }
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableNodeElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}
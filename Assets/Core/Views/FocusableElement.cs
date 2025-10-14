using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableElement
    {
        public event Action OnSubmit;
        public event Action OnContextMenu;
        public event Action OnReset;
        public event Action OnFocus;
        public event Action OnFocusLost;

        public bool IsMouseEnter { get; private set; }
        public Vector2 mouseWarpOffset = new(.5f, 0);

        private readonly VisualElement _ele;
        private readonly List<VisualElement> _pickingElements = new();

        public FocusableElement(VisualElement ele)
        {
            _ele = ele;
            _ele.tabIndex = 0;

            void FindPickings(VisualElement element)
            {
                if (element.pickingMode == PickingMode.Position)
                    _pickingElements.Add(element);

                foreach (var child in element.hierarchy.Children())
                    FindPickings(child);
            }

            FindPickings(ele);

            ele.RegisterCallback<MyVisualElementExtensions.NavigationResetEvent>(evt =>
            {
                if (ele.focusController.focusedElement != ele)
                    return;

                evt.StopPropagation();
                OnReset?.Invoke();
            });
            ele.RegisterCallback<MyVisualElementExtensions.NavigationContextEvent>(evt =>
            {
                if (ele.focusController.focusedElement != ele)
                    return;

                evt.StopPropagation();
                OnContextMenu?.Invoke();
            });
            ele.RegisterCallback<PointerUpEvent>(evt =>
            {
                if (evt.button != 1)
                    return;

                evt.StopPropagation();
                OnContextMenu?.Invoke();
            });
            ele.RegisterCallback<ClickEvent>(evt =>
            {
                if (evt.button != 0)
                    return;

                evt.StopPropagation();
                OnSubmit?.Invoke();
            });
            ele.RegisterCallback<FocusEvent>(_ =>
            {
                if (IsMouseEnter)
                    return;

                ele.schedule.Execute(WarpCursorPosition);
                OnFocus?.Invoke();
            });
            ele.RegisterCallback<PointerEnterEvent>(_ =>
            {
                IsMouseEnter = true;
                ele.Focus();
                OnFocus?.Invoke();
            });
            ele.RegisterCallback<DetachFromPanelEvent>(_ => ele.Blur());
            ele.RegisterCallback<PointerLeaveEvent>(_ =>
            {
                IsMouseEnter = false;
                OnFocusLost?.Invoke();
            });
            ele.RegisterCallback<NavigationSubmitEvent>(_ => OnSubmit?.Invoke());
        }

        public void SetActive(bool isActive)
        {
            _ele.focusable = isActive;

            foreach (var element in _pickingElements)
                element.pickingMode = isActive ? PickingMode.Position : PickingMode.Ignore;
        }

        private void WarpCursorPosition() => _ele.WarpCursorPosition(mouseWarpOffset);
    }
}
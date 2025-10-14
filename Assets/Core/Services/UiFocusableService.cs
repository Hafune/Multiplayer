using System.Collections.Generic;
using JetBrains.Annotations;
using Lib;
using UnityEngine.UIElements;

namespace Core
{
    public class UiFocusableService
    {
        private readonly Stack<FocusableNodeElement> _focusableLayers = new();
        [ItemCanBeNull] private readonly Stack<Focusable> _focusableItems = new();

        public void AddLayer(FocusableNodeElement ele)
        {
            if (_focusableLayers.Count > 0)
            {
                var node = _focusableLayers.Peek();
                _focusableItems.Push(node.focusController.focusedElement);
                node.SetActiveInternal(false);
            }

            _focusableLayers.Push(ele);
            ele.SetActiveInternal(true);
        }

        public void RemoveLayer()
        {
            _focusableLayers.Pop().SetActiveInternal(false);

            if (_focusableLayers.Count > 0)
            {
                var node = _focusableLayers.Peek();
                node.SetActiveInternal(true);
                node.FocusFirstFocusableElement();
            }

            if (_focusableItems.Count > 0)
                _focusableItems.Pop()?.Focus();
        }
    }
}
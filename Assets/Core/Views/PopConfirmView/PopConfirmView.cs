using System;
using Core.Views;
using UnityEngine.UIElements;

namespace Core
{
    public class PopConfirmView : AbstractUIDocumentView
    {
        private Action _onSubmit;
        private Action _onCancel;
        private TextElement _messageElement;
        // private readonly UiFocusableService _uIFocusableService;
        private PopConfirmVT _root;

        protected override void OnAwake()
        {
            _root = new PopConfirmVT(RootVisualElement);
            _messageElement = _root.message;
            _root.submitButton.RegisterCallback<ClickEvent>(OnSubmit);
            _root.cancelButton.RegisterCallback<ClickEvent>(OnCancel);
            RootVisualElement.RegisterCallback<NavigationSubmitEvent>(OnSubmit);
            RootVisualElement.RegisterCallback<NavigationCancelEvent>(OnCancel);
        }

        public void Show(in string message, Action onSubmit, Action onCancel = null)
        {
            _onSubmit = onSubmit;
            _onCancel = onCancel;
            _messageElement.text = message;
            DisplayFlex();
            // _uIFocusableService.AddLayer(_rootVisualElement);
            _root.popConfirmWrapper.Focus();
        }

        private void OnSubmit(EventBase evt = null)
        {
            evt?.StopPropagation();
            DisplayNone();
            // _uIFocusableService.RemoveLayer();
            _onSubmit!();
        }

        private void OnCancel(EventBase evt = null)
        {
            evt?.StopPropagation();
            DisplayNone();
            // _uIFocusableService.RemoveLayer();
            _onCancel?.Invoke();
        }
    }
}
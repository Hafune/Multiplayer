using System;
using UnityEngine;

namespace Core
{
    public class DarkScreenService
    {
        public event Action OnFadeIn;
        public event Action OnFadeOut;

        private Action _callback;
        private bool _inProgress;

        public void FadeIn(Action callback = null)
        {
            _callback = callback;
            _inProgress = true;
            OnFadeIn?.Invoke();
        }

        public void FadeOut(Action callback = null)
        {
            if (_inProgress)
                _callback?.Invoke();

            _callback = callback;
            OnFadeOut?.Invoke();
        }

        public void Complete()
        {
            _inProgress = false;
            _callback?.Invoke();
        }
    }
}
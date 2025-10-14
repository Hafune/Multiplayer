using System.Collections;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.Views
{
    public class DarkScreenView : AbstractUIDocumentView
    {
        private readonly float _durationOut = .5f;
        private readonly float _durationIn = .5f;
        private float _duration = 1.5f;
        private readonly float _speed = 1f;
        private float _currentNormalizedTime;
        private int _direction;
        private DarkScreenService _service;
        [CanBeNull] private Coroutine _coroutine;

        protected override void OnAwake()
        {
            DisplayFlex();
            _service = context.Resolve<DarkScreenService>();
            _service.OnFadeIn += () =>
            {
                _currentNormalizedTime = 0;
                _direction = 1;
                _duration = _durationIn;

                if (_coroutine is not null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(Progress());
            };
            _service.OnFadeOut += () =>
            {
                _currentNormalizedTime = 1;
                _direction = -1;
                _duration = _durationOut;
                
                if (_coroutine is not null)
                    StopCoroutine(_coroutine);
                
                _coroutine = StartCoroutine(Progress());
            };
        }

        private IEnumerator Progress()
        {
            while (true)
            {
                _currentNormalizedTime += _direction * _speed * Time.deltaTime / _duration;
                _currentNormalizedTime = Mathf.Clamp01(_currentNormalizedTime);

                RootVisualElement.SetBackgroundColor(Color.Lerp(Color.clear, Color.black, _currentNormalizedTime));

                if (_currentNormalizedTime is 0 or 1)
                    break;

                yield return null;
            }

            _coroutine = null;
            _service.Complete();
        }
    }
}
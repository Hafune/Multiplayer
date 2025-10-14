using System;
using System.Collections;
using UnityEngine;

namespace Core.Lib
{
    public class PauseResumeCoroutineByCountWrapper
    {
        private int _usageCount;
        private readonly Func<IEnumerator> _pause;
        private readonly Func<IEnumerator> _resume;
        private readonly MonoBehaviour _coroutineOwner;

        public PauseResumeCoroutineByCountWrapper(MonoBehaviour coroutineOwner, Func<IEnumerator> pause, Func<IEnumerator> resume)
        {
            _pause = pause;
            _resume = resume;
            _coroutineOwner = coroutineOwner;
        }

        public void Pause()
        {
            if (++_usageCount == 1)
                _coroutineOwner.StartCoroutine(_pause());
        }

        public void Resume()
        {
            if (--_usageCount == 0)
                _coroutineOwner.StartCoroutine(_resume());

            if (_usageCount < 0)
                Debug.LogError("PauseResumeWrapper: " + nameof(_usageCount) + " < 0 !!");

#if UNITY_EDITOR
            if (_usageCount < 0)
                throw new Exception("PauseResumeWrapper: " + nameof(_usageCount) + " < 0 !!");
#endif
        }
    }
}
using System;
using UnityEngine;

namespace Core.Lib
{
    public class PauseResumeByCountWrapper
    {
        private int _usageCount;
        private readonly Action _pause;
        private readonly Action _resume;

        public bool IsPaused => _usageCount != 0;

        public PauseResumeByCountWrapper(Action pause = null, Action resume = null)
        {
            _pause = pause;
            _resume = resume;
        }

        public void Pause()
        {
            if (++_usageCount == 1)
                _pause?.Invoke();
        }

        public void Resume()
        {
            if (--_usageCount == 0)
                _resume?.Invoke();

            if (_usageCount < 0)
                Debug.LogError("PauseResumeWrapper: " + nameof(_usageCount) + " < 0 !!");

#if UNITY_EDITOR
            if (_usageCount < 0)
                throw new Exception("PauseResumeWrapper: " + nameof(_usageCount) + " < 0 !!");
#endif
        }
    }
}
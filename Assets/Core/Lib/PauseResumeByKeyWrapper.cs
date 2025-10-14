using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Lib
{
    public class PauseResumeByKeyWrapper
    {
        private readonly Action _pause;
        private readonly Action _resume;
        private readonly HashSet<object> _cash = new();

        public bool IsPaused => _cash.Count != 0;

        public PauseResumeByKeyWrapper(Action pause = null, Action resume = null)
        {
            _pause = pause;
            _resume = resume;
        }

        public void Pause(object key)
        {
#if UNITY_EDITOR
            if (_cash.Contains(key))
            {
                Debug.LogError($"[PauseResumeByKeyWrapper] Duplicate Pause call detected with key: {GetKeyName(key)}");
                return;
            }
#endif
            if (_cash.Add(key) && _cash.Count == 1)
                _pause?.Invoke();
        }

        public void Resume(object key)
        {
#if UNITY_EDITOR
            if (!_cash.Contains(key))
            {
                Debug.LogError($"[PauseResumeByKeyWrapper] Resume called with missing key: {GetKeyName(key)}");
                return;
            }
#endif
            if (_cash.Remove(key) && _cash.Count == 0)
                _resume?.Invoke();
        }

#if UNITY_EDITOR
        private string GetKeyName(object key)
        {
            if (key == null) return "null";
            return key is UnityEngine.Object unityObj ? unityObj.name : key.ToString();
        }
#endif
    }
}
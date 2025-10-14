using System;
using UnityEngine;

namespace Core.Lib
{
    public struct ThrowExceptionByCount
    {
        private readonly int _totalCount;
        private int _currentCount;

        public ThrowExceptionByCount(int totalCount)
        {
            _currentCount = 0;
            _totalCount = totalCount;
        }

        public void Run()
        {
            if (_currentCount++ > _totalCount)
                throw new Exception(nameof(ThrowExceptionByCount) + " " + _totalCount);
        }

        public void Log() => Debug.Log(nameof(ThrowExceptionByCount)+ " count: " + _currentCount);
        public int GetCurrentCount() => _currentCount;

        public void Reset() => _currentCount = 0;
    }
}
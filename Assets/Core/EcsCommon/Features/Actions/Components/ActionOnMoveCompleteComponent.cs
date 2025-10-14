using System;
using Core.Lib;

namespace Core.Components
{
    [MyDoc("действие при успешном достижении цели")]
    public struct ActionOnMoveCompleteComponent
    {
        public Action<int> action;
        public float actionDistance;
    }
}
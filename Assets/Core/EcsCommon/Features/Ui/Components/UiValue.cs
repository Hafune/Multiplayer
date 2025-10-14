using System;
using Core.Lib;

namespace Core.Components
{
    [MyDoc("Ui значение")]
    public struct UiValue<T> where T : struct
    {
        public Action<int, T> update;
    }
}
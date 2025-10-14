using System;

namespace Core.Lib
{
    using UnityEngine;

    /// <summary>
    /// Атрибут для добавления кнопки в инспекторе Unity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MyButtonAttribute : PropertyAttribute
    {
        public string customName;

        public MyButtonAttribute(string name = null) => customName = name;
    }
}
using System;
using UnityEngine;

namespace Core.Lib
{
    public interface IPool : IDisposable
    {
        public void ForceDisable();
        public void ReturnDisabledInContainer();
        public object GetInstanceUntyped(Vector3 position, Quaternion quaternion, Transform parent = null);
    }
}
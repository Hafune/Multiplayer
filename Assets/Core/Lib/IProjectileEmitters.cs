using System;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractProjectileEmitters : MonoBehaviour
    {
        public abstract void ForEachEmitters(Action<Transform> callback);
    }
}
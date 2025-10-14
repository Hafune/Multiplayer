using System;
using Lib;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lib
{
    public class TaskSetRandomRotation2D : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _transform;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _transform.rotation = Quaternion.Euler(0, 0, Random.value * 360f);
            onComplete?.Invoke();
        }
    }
}
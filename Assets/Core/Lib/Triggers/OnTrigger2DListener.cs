using System;
using UnityEngine;

namespace Core.Lib
{
    public class OnTrigger2DListener : MonoBehaviour
    {
        public Action OnEnter;
        public Action OnExit;

        public void OnTriggerEnter2D(Collider2D col) => OnEnter?.Invoke();
        public void OnTriggerExit2D(Collider2D col) => OnExit?.Invoke();
    }
}
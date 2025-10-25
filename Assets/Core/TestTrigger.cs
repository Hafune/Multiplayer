using Core.Lib;
using UnityEngine;

namespace Core
{
    public class TestTrigger : MonoBehaviour
    {
        private int _count;
        private Collider _coll;

        private void Awake()
        {
            _count = 0;
            _coll = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider col)
        {
            TriggerCallbacksCache.RegisterTrigger(this, col);
            Debug.Log(name + " ENTER " + _count++);
        }
        
        private void OnTriggerExit(Collider col)
        {
            TriggerCallbacksCache.UnRegisterTrigger(this, col);
            Debug.Log(name + " exit " + _count--);
        }
    }
}
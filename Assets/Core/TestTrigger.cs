using Core.Lib;
using UnityEngine;

namespace Core
{
    public class TestTrigger : MonoBehaviour, ITriggerDispatcherTarget
    {
        private int _count;
        private Collider _coll;

        private void Awake()
        {
            _count = 0;
            _coll = GetComponent<Collider>();
        }

        public void OnTriggerEnter(Collider col)
        {
            TriggerDisableHandler.RegisterTrigger(this, col);
            Debug.Log(name + " ENTER " + _count++);
        }
        
        public void OnTriggerExit(Collider col)
        {
            TriggerDisableHandler.UnRegisterTrigger(this, col);
            Debug.Log(name + " exit " + _count--);
        }
    }
}
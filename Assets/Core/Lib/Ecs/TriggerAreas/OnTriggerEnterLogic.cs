using Core.ExternalEntityLogics;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnterLogic : MonoConstruct, ITriggerDispatcherTarget
    {
        [SerializeField] private AbstractEntityLogic _enterLogic;
        [SerializeField] private AbstractEntityLogic _exitLogic;
        private int _contactCount;
        private ConvertToEntity _entityRef;

        private void Awake() => _entityRef = GetComponentInParent<ConvertToEntity>();

        public void OnTriggerEnter(Collider col)
        {
            TriggerDisableHandler.RegisterTrigger(this, col);
            if (++_contactCount != 1) 
                return;
            
            if (!_enterLogic)
                return;
            
            _enterLogic.Run(_entityRef.RawEntity);
        }

        public void OnTriggerExit(Collider col)
        {
            TriggerDisableHandler.UnRegisterTrigger(this, col);
            if (--_contactCount == 0) 
                return;
            
            if (!_exitLogic)
                return;

            _exitLogic.Run(_entityRef.RawEntity);
        }
    }
}
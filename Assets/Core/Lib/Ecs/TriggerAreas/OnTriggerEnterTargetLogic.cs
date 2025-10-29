using System;
using Core.ExternalEntityLogics;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnterTargetLogic : MonoConstruct, ITriggerDispatcherTarget
    {
        [SerializeField] private AbstractEntityLogic _enterLogic;
        [SerializeField] private AbstractEntityLogic _exitLogic;
        public void OnTriggerEnter(Collider col)
        {
            TriggerDisableHandler.RegisterTrigger(this, col);
            if (!_enterLogic)
                return;

            var entityRef = TriggerCache.ExtractEntity(col);
            _enterLogic.Run(entityRef.RawEntity);
        }

        public void OnTriggerExit(Collider col)
        {
            TriggerDisableHandler.UnRegisterTrigger(this, col);
            if (!_exitLogic)
                return;

            var entityRef = TriggerCache.ExtractEntity(col);
            _exitLogic.Run(entityRef.RawEntity);
        }
    }
}
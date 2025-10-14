using Core.ExternalEntityLogics;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class OnTriggerEnter2DTargetLogic : MonoConstruct, ITriggerDispatcherTarget2D
    {
        [SerializeField] private AbstractEntityLogic _enterLogic;
        [SerializeField] private AbstractEntityLogic _exitLogic;
        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!_enterLogic)
                return;
            
            var entityRef = TriggerCache.ExtractEntity(col);
            _enterLogic.Run(entityRef.RawEntity);
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if (!_exitLogic)
                return;

            var entityRef = TriggerCache.ExtractEntity(col);
            _exitLogic.Run(entityRef.RawEntity);
        }
    }
}
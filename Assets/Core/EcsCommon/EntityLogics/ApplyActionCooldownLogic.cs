using Core.Components;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionCooldownLogic : AbstractEntityLogic, IActionGenericLogic
    {
        private LazyPool _cooldownPool;
        private LazyPool _eventPool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _cooldownPool = new LazyPool(context);
            _eventPool = new LazyPool(context);
        }
        
        public override void Run(int entity) => _action.InvokeActionContext(this, entity);

        public void GenericRun<T>(int entity)
        {
            _cooldownPool.GetPool<CooldownValueComponent<T>>().Get(entity).startTime = Time.time;
            _eventPool.GetPool<EventStartCooldown<CooldownValueComponent<T>>>().AddIfNotExist(entity);
        }
    }
}
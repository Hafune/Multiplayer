using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ApplyResourceRecoveryPerHitSubLogic : MonoConstruct, IActionSubStartLogic, IActionSubCancelLogic, IActionGenericLogic
    {
        private EcsPool<ResourceRecoveryPerHitComponent> _RestoreResourcePerHitPool;
        private LazyPool _pool;
        private AbstractEntityAction _action;
        private float _value;

        private void Awake()
        {
            _RestoreResourcePerHitPool = context.Resolve<ComponentPools>().ResourceRecoveryPerHit;
            _action = GetComponentInParent<AbstractEntityAction>();
            _pool = new LazyPool(context);
        }

        public void SubStart(int entity)
        {
            _action.InvokeActionContext(this, entity);
            _RestoreResourcePerHitPool.Add(entity).value = _value;
        }

        public void GenericRun<T>(int entity) => _value = _pool.GetPool<ResourceRecoveryPerHitValueComponent<T>>().Get(entity).value;

        public void SubCancel(int entity) => _RestoreResourcePerHitPool.Del(entity);
    }
}
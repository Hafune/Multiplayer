using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Unity.Mathematics;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionResourceRecoveryLogic : AbstractEntityLogic, IActionGenericLogic
    {
        private EcsPool<ManaPointValueComponent> _manaPool;
        private EcsPool<ManaPointMaxValueComponent> _manaMaxPool;
        private EcsPool<EventValueUpdated<ManaPointValueComponent>> _eventPool;
        private LazyPool _pool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            var pools = context.Resolve<ComponentPools>(); 
            _manaPool = pools.ManaPointValue;
            _manaMaxPool = pools.ManaPointMaxValue;
            _eventPool = pools.EventUpdatedManaPointValue;
            _pool = new LazyPool(context);
        }

        public override void Run(int entity) => _action.InvokeActionContext(this, entity);

        public void GenericRun<T>(int entity)
        {
            var value = _pool.GetPool<ResourceRecoveryPerUsingValueComponent<T>>().Get(entity).value;
            ref var mana = ref _manaPool.Get(entity);
            var maxManaValue = _manaMaxPool.Get(entity).value;

            mana.value = math.clamp(mana.value + value, 0, maxManaValue);
            _eventPool.AddIfNotExist(entity);
        }
    }
}
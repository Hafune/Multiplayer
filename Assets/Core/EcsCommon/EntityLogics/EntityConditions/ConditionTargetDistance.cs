using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class ConditionTargetDistance : AbstractEntityCondition, IActionGenericLogic
    {
        private bool _isValid;
        private LazyPool _pool;
        private EcsPool<TargetComponent> _targetPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<BodyRadiusComponent> _bodyRadiusPool;
        private AbstractEntityAction _action;

        private void Awake()
        {
            var pools = context.Resolve<ComponentPools>();
            _targetPool = pools.Target;
            _positionPool = pools.Position;
            _bodyRadiusPool = pools.BodyRadius;
            _action = GetComponentInParent<AbstractEntityAction>();
            _pool = new LazyPool(context);
        }

        public override bool Check(int entity)
        {
            if (!_targetPool.Has(entity))
                return false;
            
            _action.InvokeActionContext(this, entity);
            return _isValid;
        }
        
        public void GenericRun<A>(int entity)
        {
            var distance = _pool.GetPool<ActionAttrTargetDistanceComponent<A>>().Get(entity).value;
            int target = _targetPool.Get(entity).entity;
            var radius = _bodyRadiusPool.Get(target).radius + distance;
            var line = _positionPool.Get(target).transform.position - _positionPool.Get(entity).transform.position;
            
            _isValid = line.sqrMagnitude <= radius * radius;
        }
    }
}
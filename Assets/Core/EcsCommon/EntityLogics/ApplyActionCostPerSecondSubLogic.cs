using Core.Components;
using Core.Generated;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionCostPerSecondSubLogic : MonoConstruct, IActionGenericLogic, IActionSubStartLogic
    {
        private LazyPool _pool;
        private AbstractEntityAction _action;
        private ApplyActionSlotSubLogic _applyActionSlotSubLogic;

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _applyActionSlotSubLogic = _action.GetComponentInChildren<ApplyActionSlotSubLogic>();
            _pool = new LazyPool(context);
        }

        public void SubStart(int entity) => _action.InvokeActionContext(this, entity);

        public void GenericRun<T>(int entity)
        {
            var cost = _pool.GetPool<CostValueComponent<T>>().Get(entity).value;
            _applyActionSlotSubLogic.SetValue(ValueEnum.ActionCostPerSecondValue, -cost);
        }
    }
}
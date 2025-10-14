using Core.Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics.ActionAttributes
{
    public class AttrToggle : IActionGenericLogic
    {
        private readonly AbstractEntityAction _action;
        private readonly ConvertToEntity _convertToEntity;
        private readonly IActionAttr _iActionAttr;
        private bool _active;

        public AttrToggle(MonoBehaviour actionAttr)
        {
            _action = actionAttr.GetComponentInParent<AbstractEntityAction>();
            _convertToEntity = actionAttr.GetComponentInParent<ConvertToEntity>();
            _iActionAttr = (IActionAttr)actionAttr;
        }

        public void OnEnable(bool active)
        {
            _active = active;
            _action.TryInvokeActionContext(this, _convertToEntity.RawEntity);
        }

        public void OnDisable(bool active)
        {
            _active = active;
            _action.TryInvokeActionContext(this, _convertToEntity.RawEntity);
        }

        public void GenericRun<A>(int entity)
        {
            if (_active)
                _iActionAttr.Setup<A>(entity);
            else
                _iActionAttr.Remove<A>(entity);
        }
    }
}
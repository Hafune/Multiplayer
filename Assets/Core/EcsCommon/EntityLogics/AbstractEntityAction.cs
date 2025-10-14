using System;
using System.Collections.Generic;
using Core.ExternalEntityLogics.ActionAttributes;
using Lib;

namespace Core.ExternalEntityLogics
{
    public abstract class AbstractEntityAction : MonoConstruct
    {
        private Action<IActionGenericLogic, int> _actionContext;
        private Action<IButtonGenericLogic, int> _buttonContext;
        private readonly List<IActionAttr> _actionAttrs = new();
        private string _key;
        public string Key => _key ??= name;
        
        public void SetActionContext(Action<IActionGenericLogic, int> actionContext) => _actionContext = actionContext;
        
        public void SetButtonContext(Action<IButtonGenericLogic, int> buttonContext) => _buttonContext = buttonContext;

        public void InvokeActionContext(IActionGenericLogic logic, int entity) => _actionContext(logic, entity);
        
        public void InvokeButtonContext(IButtonGenericLogic logic, int entity) => _buttonContext(logic, entity);
        
        public void TryInvokeActionContext(IActionGenericLogic logic, int entity) => _actionContext?.Invoke(logic, entity);
        
        public T GetAttribute<T>() where T : IActionAttr => GetComponentInChildren<T>();
        
        public bool TryGetAttribute<T>(out T attr) where T : IActionAttr
        {
            attr = GetComponentInChildren<T>();
            return attr != null;
        }

        public IReadOnlyList<IActionAttr> GetAttributes()
        {
            GetComponentsInChildren(_actionAttrs);
            return _actionAttrs;
        }

        public abstract bool CheckConditionLogic(int entity);
    }
}
using System.Collections.Generic;
using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Core.Generated;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ApplyActionSlotSubLogic : MonoConstruct, IActionGenericLogic, IActionSubStartLogic, IActionSubCancelLogic
    {
        private AbstractEntityAction _action;
        private SlotFunctions _slotFunctions;
        private int _slotEntity;
        private readonly List<ValueData> _values = new();

        private SlotComponent _actionSlot = new()
        {
            key = nameof(ActionCurrentComponent),
            values = new(),
            tags = new()
        };

#if UNITY_EDITOR
        private bool _debugWasStarted;
#endif

        private void Awake()
        {
            _action = GetComponentInParent<AbstractEntityAction>();
            _slotFunctions = new(context);
        }

        public void SetValue(ValueEnum valueEnum, float value)
        {
#if UNITY_EDITOR
            if (_debugWasStarted)
                Debug.LogError($"{nameof(SetValue)}({value}) вызван уже после запуска слота");
#endif
            var exist = false;
            for (var i = 0; i < _values.Count; i++)
            {
                var data = _values[i];
                if (data.valueEnum == valueEnum)
                {
                    data.value = value;
                    _values[i] = data;
                    exist = true;
                    break;
                }
            }


            if (!exist)
            {
                _values.Add(new()
                {
                    value = value,
                    valueEnum = valueEnum
                });
            }

            _actionSlot.values = _values;
        }

        public void SubStart(int entity)
        {
            _action.InvokeActionContext(this, entity);
#if UNITY_EDITOR
            _debugWasStarted = true;
#endif
        }

        public void GenericRun<T>(int entity) => _slotEntity = _slotFunctions.AddSlot(entity, _actionSlot);

        public void SubCancel(int entity)
        {
            _values.Clear();
            _slotFunctions.RemoveSlot(_slotEntity);
#if UNITY_EDITOR
            _debugWasStarted = false;
#endif
        }
    }
}
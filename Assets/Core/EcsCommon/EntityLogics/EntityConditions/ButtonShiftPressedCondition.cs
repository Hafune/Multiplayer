using Core.Services;
using UnityEngine.InputSystem;

namespace Core.ExternalEntityLogics
{
    public class ButtonShiftPressedCondition : AbstractEntityCondition
    {
        private InputAction _shift;

        private void Start() => _shift = context.Resolve<InputService>().GetInputs(0).Shift;

        public override bool Check(int entity) => _shift.IsPressed();
    }
}
using Core.BehaviorTree;
using Lib;

namespace Core.ExternalEntityLogics
{
    public class ConditionTriggerIsActive : AbstractEntityCondition
    {
        private TriggerCounter2D _triggerCounter2D;

        private void Awake() => _triggerCounter2D = transform.GetSelfChildrenComponent<TriggerCounter2D>();

        public override bool Check(int entity) => _triggerCounter2D.IsTriggerActive();
    }
}
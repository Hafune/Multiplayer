using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class ConditionInvert : AbstractEntityCondition
    {
        [SerializeField] private AbstractEntityCondition _condition;

#if UNITY_EDITOR
        private void Awake() => Assert.IsNotNull(_condition);
#endif

        public override bool Check(int entity) => !_condition.Check(entity);
    }
}
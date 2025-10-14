using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EntityActionInstant : AbstractEntityActionInstant
    {
        [SerializeField] private AbstractEntityCondition _condition;
        [SerializeField] private AbstractEntityLogic _start;
        private IActionSubStartLogic[] _subStart;

#if UNITY_EDITOR
        private void OnValidate() =>
            _condition = _condition ? _condition : transform.GetSelfChildrenComponent<AbstractEntityCondition>();
#endif

        private void Awake()
        {
            _subStart = GetComponentsInChildren<IActionSubStartLogic>();
        }

        public override bool CheckConditionLogic(int entity) => _condition?.Check(entity) ?? true;

        public override void StartLogic(int entity)
        {
            _start?.Run(entity);

            for (int i = 0, iMax = _subStart.Length; i < iMax; i++)
                _subStart[i].SubStart(entity);
        }
    }
}
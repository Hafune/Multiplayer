using Lib;

namespace Core.ExternalEntityLogics
{
    public class ConditionAllNode : AbstractEntityCondition
    {
        private AbstractEntityCondition[] _conditions;

        private void Awake()
        {
            _conditions = new AbstractEntityCondition[transform.childCount];
            int index = 0;
            transform.ForEachSelfChildren<AbstractEntityCondition>(c => _conditions[index++] = c);
        }

        public override bool Check(int entity)
        {
            for (int i = 0, iMax = _conditions.Length; i < iMax; i++)
                if (!_conditions[i].Check(entity))
                    return false;

            return true;
        }
    }
}
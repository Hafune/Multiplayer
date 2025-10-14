using Core.Generated;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ValueSumLogic : AbstractEntityLogic
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private float _byValue;
        [SerializeField] private bool _perSecond;
        private float _totalValue;
        private ComponentPools _pool;

        private void Awake()
        {
            _pool = context.Resolve<ComponentPools>();
            _totalValue = _perSecond ? _byValue * Time.deltaTime : _byValue;
        }

        public override void Run(int entity) => ValuePoolsUtility.Sum(_pool, entity, _value, _totalValue);
    }
}
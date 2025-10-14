using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ValueSetLogic : AbstractEntityLogic, IValueGenericPoolContext
    {
        [SerializeField] private ValueEnum _valueEnum;
        [SerializeField] private float _value;
        private ComponentPools _pools;
        private bool _isInitialize;
        private LazyPool _valuePool;
        private LazyPool _eventValueUpdatedPool;
        private int _entity;
        private Action _genericCall;

        private void Awake()
        {
            _pools = context.Resolve<ComponentPools>();
            _valuePool = new LazyPool(context);
            _eventValueUpdatedPool = new LazyPool(context);
        }

        public override void Run(int entity)
        {
            if (_genericCall is null) 
                ValuePoolsUtility.CallGenericPool(this, _pools, _valueEnum);

            _entity = entity;
            _genericCall!.Invoke();
        }

        public void GenericRun<V>(EcsPool<V> pool) where V : struct, IValue
        {
            _genericCall = () =>
            {
                _valuePool.GetPool<V>().Get(_entity).value = _value;
                _eventValueUpdatedPool.GetPool<EventValueUpdated<V>>().GetOrInitialize(_entity);
            };
        }
    }
}
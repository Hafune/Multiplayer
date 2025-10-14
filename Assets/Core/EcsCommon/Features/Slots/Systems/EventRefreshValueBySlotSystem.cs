using System.Runtime.CompilerServices;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class EventRefreshValueBySlotSystem : IEcsRunSystem, IValueGenericPoolContext
    {
        private readonly EcsFilterInject<Inc<EventStartRecalculateAllValues, SlotNodeComponent>> _filter;
        private readonly EcsPoolInject<SlotNodeComponent> _pool;

        private readonly ComponentPools _pools;
        private float _value;
        private int _entity;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var node = _pool.Value.Get(i);
                foreach (var tagPool in node.tagPools)
                    tagPool.Del(i);

                node.tagPools.Clear();
                foreach (var entity in node.children)
                {
                    var slot = _pools.Slot.Get(entity);
                    foreach (var data in slot.values)
                    {
                        _entity = i;
                        _value = data.value;
                        ValuePoolsUtility.CallGenericPool(this, _pools, data.valueEnum);
                    }

                    foreach (var tag in slot.tags)
                    {
                        SlotValueTypes.GetEventUpdatedSlotTagPool(tag, _pools).AddIfNotExist(entity);
                        var tagPool = SlotValueTypes.GetSlotTagPool(tag, _pools);
                        tagPool.AddDefault(entity);
                        node.tagPools.Add(tagPool);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GenericRun<V>(EcsPool<V> pool) where V : struct, IValue
        {
            if (pool.Has(_entity))
                pool.Get(_entity).value = ValueSumRules.SumValueByRule<V>(pool.Get(_entity).value, _value);
#if UNITY_EDITOR
            else
                Debug.LogError(typeof(V).Name + " отсутствует у персонажа");
#endif
        }
    }
}
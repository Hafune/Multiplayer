using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class CooldownSystem<V> : IEcsRunSystem
        where V : struct, IGenCooldownValue
    {
        private readonly EcsFilterInject<
            Inc<
                CooldownValueComponent<V>,
                EventStartCooldown<CooldownValueComponent<V>>
            >,
            Exc<
                ChargeValueComponent<V>,
                InProgressTag<CooldownValueComponent<V>>
            >> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                CooldownValueComponent<V>,
                InProgressTag<CooldownValueComponent<V>>
            >,
            Exc<
                ChargeValueComponent<V>
            >> _progressFilter;

        private readonly EcsFilterInject<
            Inc<
                CooldownValueComponent<V>,
                ChargeValueComponent<V>,
                ChargeMaxValueComponent<V>
            >> _chargeFilter;

        private readonly EcsFilterInject<
            Inc<
                EventStartCooldown<CooldownValueComponent<V>>
            >> _eventFilter;

        private readonly EcsPoolInject<CooldownValueComponent<V>> _valuePool;
        private readonly EcsPoolInject<EventValueUpdated<CooldownValueComponent<V>>> _valueUpdatedPool;
        private readonly EcsPoolInject<InProgressTag<CooldownValueComponent<V>>> _progressPool;
        private readonly EcsPoolInject<EventStartCooldown<CooldownValueComponent<V>>> _eventPool;
        private readonly EcsPoolInject<ChargeValueComponent<V>> _chargePool;
        private readonly EcsPoolInject<ChargeMaxValueComponent<V>> _chargeMaxPool;
        private readonly EcsPoolInject<EventValueUpdated<ChargeValueComponent<V>>> _chargeUpdatedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
            {
                ref var value = ref _valuePool.Value.Get(i);
                value.startTime = Time.time;
                _progressPool.Value.Add(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                var value = _valuePool.Value.Get(i);
                _valueUpdatedPool.Value.AddIfNotExist(i);

                if (Time.time - value.startTime < value.value)
                    continue;

                _progressPool.Value.Del(i);
            }

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);

            foreach (var i in _chargeFilter.Value)
            {
                ref var charge = ref _chargePool.Value.Get(i);
                var chargeMax = _chargeMaxPool.Value.Get(i).value;

                if (charge.value < 1) 
                    _valueUpdatedPool.Value.AddIfNotExist(i);

                if (charge.value >= chargeMax)
                    continue;

                ref var cooldown = ref _valuePool.Value.Get(i);

                if (cooldown.lastChargeAddTime < cooldown.startTime)
                    cooldown.lastChargeAddTime = cooldown.startTime;

                if (Time.time - cooldown.lastChargeAddTime < cooldown.value)
                    continue;

                charge.value++;
                cooldown.lastChargeAddTime += cooldown.value;

                _chargeUpdatedPool.Value.AddIfNotExist(i);
            }
        }
    }
}
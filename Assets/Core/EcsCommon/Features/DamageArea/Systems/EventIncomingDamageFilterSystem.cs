using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class EventIncomingDamageFilterSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventIncomingDamage,
                ArmorValueComponent
            >> _armorFilter;

        private readonly EcsFilterInject<
            Inc<
                EventIncomingDamage,
                ResistanceAllValueComponent
            >> _resistanceFilter;

        private readonly EcsFilterInject<
            Inc<
                EventIncomingDamage,
                IncomingDamagePercentValueComponent
            >> _incomingDamageFilter;

        private readonly ComponentPools _pools;
        private static int _armorOverValue = 50;
        private static int _resistOverValue = 5;

        public EventIncomingDamageFilterSystem(Context context)
        {
            // var service = context.Resolve<ExperienceService>();
            // service.OnChange += () =>
            // {
            //     _armorOverValue = service.Level * 50;
            //     _resistOverValue = service.Level * 5;
            // };
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _armorFilter.Value)
            {
                var data = _pools.EventIncomingDamage.Get(i).data;
                var defence = _pools.ArmorValue.Get(i).value;
                var reducePercent = CalculateReducePercentByArmor(defence);

                foreach (ref var value in data)
                    value.damage *= reducePercent;
            }

            foreach (var i in _resistanceFilter.Value)
            {
                var data = _pools.EventIncomingDamage.Get(i).data;
                var defence = _pools.ResistanceAllValue.Get(i).value;
                var reducePercent = CalculateReducePercentByResist(defence);

                foreach (ref var value in data)
                    value.damage *= reducePercent;
            }

            foreach (var i in _incomingDamageFilter.Value)
            {
                var incomingPercent = _pools.IncomingDamagePercentValue.Get(i).value;
                if (incomingPercent == 0)
                    continue;
                
                var data = _pools.EventIncomingDamage.Get(i).data;
                foreach (ref var value in data)
                    value.damage *= 1 + incomingPercent;
            }
        }

        public static float CalculateReducePercentByArmor(float value) =>
            1f - value / (value + _armorOverValue);

        public static float CalculateReducePercentByResist(float value) =>
            1f - value / (value + _resistOverValue);
    }
}
using Core.Components;
using Core.Generated;
using DamageNumbersPro;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class DamageReflectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventIncomingDamage,
                DamageReflectionPercentValueComponent
            >> _percentFilter;

        private readonly ComponentPools _pools;
        private readonly DamageNumber _damageTextEffectPrefab;

        public DamageReflectionSystem(Context context) => _damageTextEffectPrefab = context.Resolve<CommonValues>().DamageTextEffectPrefab;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _percentFilter.Value)
            {
                var percent = _pools.DamageReflectionPercentValue.Get(i).value;
                if (percent == 0)
                    continue;
                
                foreach (var targetData in _pools.EventIncomingDamage.Get(i).data)
                {
                    var ownerData = _pools.EventIncomingDamage.GetOrInitialize(targetData.owner).data;
                    ownerData.Add((
                        targetData.damage * percent,
                        targetData.ownerPosition,
                        targetData.triggerPoint,
                        i,
                        _damageTextEffectPrefab
                        ));
                }
            }
        }
    }
}
using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class EventSetupParentComponentSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems(Context context) => new IEcsSystem[]
        {
            new EventSetupParentValueSystem<AttackSpeedValueComponent>(context),
            new EventSetupParentValueSystem<CriticalChanceValueComponent>(context),
            new EventSetupParentValueSystem<CriticalDamageValueComponent>(context),
            new EventSetupParentValueSystem<DamageValueComponent>(context),
            new EventSetupParentValueSystem<DamageMinValueComponent>(context),
            new EventSetupParentValueSystem<DamageMaxValueComponent>(context),
            new EventSetupParentValueSystem<ExplosionScaleValueComponent>(context),
            //
            new EventSetupParentComponentSystem<ThroughProjectileSlotTag>(),
            //
            new DelHere<EventChildAdded>()
        };
    }
}
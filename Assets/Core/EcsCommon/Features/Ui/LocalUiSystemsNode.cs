using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class LocalUiSystemsNode
    {
        //подумать о создании через в кодогенерацию
        public static IEnumerable<IEcsSystem> BuildSystems()
        {
            return new IEcsSystem[]
            {
                new UpdateLocalUiSystem<HitPointValueComponent>(),
                new UpdateLocalUiSystem<HitPointMaxValueComponent>(),
                new UpdateLocalUiSystem<ManaPointValueComponent>(),
                new UpdateLocalUiSystem<ManaPointMaxValueComponent>(),
                new UpdateLocalUiSystem<MoveSpeedValueComponent>(),
                //
                new UpdateLocalUiByEventSystem<EventLevelUpTaken>(),
                new UpdateLocalUiByEventSystem<EventDeath>(),
            };
        }
    }
}
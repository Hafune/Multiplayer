using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public static class UiSystemsNode
    {
        //подумать о создании через в кодогенерацию
        public static IEnumerable<IEcsSystem> BuildSystems(List<IEcsSystem> globalUiSystems)
        {
            var localSystems = new IEcsSystem[]
            {
                new UpdateLocalUiSystem<HitPointValueComponent>(),
                new UpdateLocalUiSystem<HitPointMaxValueComponent>(),
                new UpdateLocalUiSystem<ManaPointValueComponent>(),
                new UpdateLocalUiSystem<ManaPointMaxValueComponent>(),
                new UpdateLocalUiSystem<MoveSpeedValueComponent>(),
                //
                new UpdateLocalUiByEventSystem<EventLevelUpTaken>(),
                new UpdateLocalUiByEventSystem<EventDeath>(),
                
                new DelHere<
                    EventValueUpdated<ActionLinkButton1Component>,
                    EventValueUpdated<ActionLinkButton2Component>,
                    EventValueUpdated<ActionLinkButton3Component>,
                    EventValueUpdated<ActionLinkButton4Component>,
                    EventValueUpdated<ActionLinkMouseLeftComponent>,
                    EventValueUpdated<ActionLinkMouseRightComponent>
                >()
            };

            return globalUiSystems.Concat(localSystems);
        }
    }
}
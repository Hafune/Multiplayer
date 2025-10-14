//Файл генерируется в GenActionSystemsSchema
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class PlaybackAndRecordingActionsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<WriteCommandsTag>> _writeCommandsFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionAttackComponent>>> ActionAttackComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionDashComponent>>> ActionDashComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionDeathComponent>>> ActionDeathComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionDefaultComponent>>> ActionDefaultComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionDefaultAttackComponent>>> ActionDefaultAttackComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionDizzyComponent>>> ActionDizzyComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionHitStunComponent>>> ActionHitStunComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionMoveComponent>>> ActionMoveComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<ActionReviveComponent>>> ActionReviveComponentFilter;
        private readonly EcsFilterInject<Inc<WriteCommandsTag, EventActionStart<NpcActionComponent>>> NpcActionComponentFilter;

        public void Run(IEcsSystems systems)
        {
            if (_writeCommandsFilter.Value.GetEntitiesCount() == 0)
                return;
                
            foreach (var i in ActionAttackComponentFilter.Value) {}
            foreach (var i in ActionDashComponentFilter.Value) {}
            foreach (var i in ActionDeathComponentFilter.Value) {}
            foreach (var i in ActionDefaultComponentFilter.Value) {}
            foreach (var i in ActionDefaultAttackComponentFilter.Value) {}
            foreach (var i in ActionDizzyComponentFilter.Value) {}
            foreach (var i in ActionHitStunComponentFilter.Value) {}
            foreach (var i in ActionMoveComponentFilter.Value) {}
            foreach (var i in ActionReviveComponentFilter.Value) {}
            foreach (var i in NpcActionComponentFilter.Value) {}
        }
    }
    // @formatter:on
}
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Services;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public static class ActionSystemsNode
    {
        public static IEnumerable<IEcsSystem> BuildSystems(Context context)
        {
            //Управление игрока
            var playerSystems = context.Resolve<PlayersService>().BuildControllerSystems();
            var otherSystems = new IEcsSystem[]
            {
                //Вызов верхнеуровнего поведения
                // new PlayerInRangeSystem(),
                // new BehaviorTreeSystem(context),
                // new SimpleEnemyMoveToTargetBehaviorSystem(),
                // new SimpleEnemyAttackBehaviorSystem(context),
                //
                new HitBlinkSystem(),
                //
                new DelHere<EventActionChanged>(),
                //Срабатывание timeline события.
                new EventTimelineActionSystem(),
                //
                // new PlaybackAndRecordingActionsSystem(),
                //Сетап компонентов на старте
                new DeathWithRelationSystem<AimComponent, TargetComponent, DeathWithTargetTag>(context),
                new DeathWithRelationSystem<NodeComponent, ParentComponent, DeathWithParentTag>(context),
                new ActionDeathSystem(),
                new AbilityHealingPotionSystem(),
                new ActionReviveSystem(),
                new ActionDizzySystem(),
                new ActionAttractionSystem(),
                new ActionHitStunSystem(),
                new NpcActionSystem(),
                new ActionAttackInstantSystem(),
                new ActionAttackSystem(),
                new ActionDefaultAttackSystem(),
                new ActionDashSystem(),
                new ActionMoveSystem(),
                new ActionDefaultSystem(),
                //Подготовка данных к апдейту
                //Update-----------
                new CalculateMoveDestinationSystem(),
                // new BattleCryVisualSystem(),
                new DirectionUpdateSystem(),
                new MoveUpdateSystem(),
                new CompleteIfResourceIsOverSystem(),
                //
                new ActionUpdateSystem(),
                //-----------
                //Система BTree проверяющая что нужный экшен не начался
                // new EventBehaviorTreeActionFailCheckSystem(),
                //-----------
                new DelHere<
                    EventActionStart<ActionAttackComponent>,
                    EventActionStart<ActionAttackInstantComponent>,
                    EventActionCompleteStreaming,
                    EventTimelineAction
                >(),
                
                //системы скилов персонажей
                // new BarbRevengeSystem(),
            };

            return playerSystems.Concat(otherSystems);
        }
    }
}
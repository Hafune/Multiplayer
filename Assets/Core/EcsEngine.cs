using System.Collections.Generic;
using System.Reflection;
using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core
{
    public class EcsEngine : MonoConstruct
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private readonly List<IEcsSystem> _globalUiSystems = new();
        private readonly List<IEcsSystem> _removeEntityReactionUiSystems = new();

        public UiEntityFactory UiEntityFactory { get; private set; }

        private void Awake()
        {
            _world = context.Resolve<EcsWorld>();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            UiEntityFactory = new UiEntityFactory(_world, this);
        }

        private void Start()
        {
            _updateSystems
                .Add(new MultiplayerUpdateClientSystem(context))
                .Add(new EventTimeDilationSystem(context));
            // .Add(new EventCameraShakeSystem(Context));

            _fixedUpdateSystems
#if UNITY_EDITOR
                // .Add(new DebugEnemySystem())
#endif
                .Add(new ActionCancelBeforeRemoveEntitySystem())
                //
                //Добавление обязательных компонентов. 
                .Add(new InitSubComponentSystem(context))
                .Add(new EventSetupChildrenSystem(context))
                //Удаление сущностей
                // .AddMany(BeforeRemoveSystemsNode.BuildSystems())
                .AddMany(RelationCleanSystemsNode.BuildRemoveWithRelationSystems(context))
                .Add(new SlotCancelBeforeRemoveSystem(context))
                .AddMany(RelationCleanSystemsNode.BuildCascadeCleanSystems(context))
                //Те UI которым необходимо знать об удалении сущности
                .AddMany(_removeEntityReactionUiSystems)
                .Add(new EventRemoveEntitySystem())
                //Добавление значений из базовых
                .Add(new InitValuesFromBaseValuesSystem())
                //Начало перерасчёта значений
                .AddMany(RecalculateValueSystemsNode.BuildSystems())
                //Суммирование со слотами
                .Add(new EventRefreshValueBySlotSystem())
                .Add(new DelHere<EventStartRecalculateAllValues>())
                //Вставка value если есть maxValue
                .AddMany(InitValueIfExistMaxValueSystemsNode.BuildSystems())
                //Приминение бонусов, процент к урону/хп и т.д.
                .AddMany(MathValueSystemsNode.BuildSystems())
                //Приравнивание значений к макс значениям при инициализации (хп к макс хп и т.д.)
                .AddMany(InitEquateValuesSystemsNode.BuildSystems())
                //Удаление ивента иницыализации
                .Add(new DelHere<EventInit>())
                //=============================================================================================
                //--------
                //Приравнивание значений к макс значениям при изменении одноо из них (хп к макс хп и т.д.)
                .AddMany(ClampValuesSystemsNode.BuildSystems())
                //Приминение значений родителя к детям
                .AddMany(EventSetupParentComponentSystemsNode.BuildSystems(context))
                //-------------------
                //Приминение данных с сервера
                //.Add(new MultiplayerUpdateClientSystem(context))
                //
                .AddMany(RestoreByCheckpointNode.BuildSystems())
                //
                //Системы экшенов
                //-----------------------------------------------------------------------
                .Add(new FindTargetSystem<EnemyComponent>(context))
                .Add(new FindTargetSystem<FriendTag>(context))
                .AddMany(ActionSystemsNode.BuildSystems(context))

                //запуск перезарядки экшена
                .AddMany(CooldownSystemsNode.BuildSystems())
                //
                .Add(new EventMoveCompleteSystem())
                //
                //-----------------------------------------------------------------------
                // .Add(new AuraAreaSystem())
                // .Add(new ReflectionAreaSystem(Context))
                //Накладывание урона
                //Накладывание импактов
                .Add(new DelHere<
                    EventResourceGenerated,
                    EventHitTaken,
                    EventIncomingDamage,
                    EventDamageAreaSelfImpactInfoComponent
                >())
                .Add(new DamageAreaSystem(context))
                .Add(new DamagePerSecondSystem(context))
                //
                .Add(new DamageReflectionSystem(context))
                //
                .Add(new EventResourceGeneratedSystem<ManaPointValueComponent, ManaPointMaxValueComponent>())

                //Удаление нанесенного урона
                .Add(new DelHere<EventCausedDamage>())

                //Удаление события получения урона
                .Add(new DelHere<EventDamageTaken>())

                //Применение защиты
                .Add(new EventIncomingDamageFilterSystem(context))
                .Add(new HealthPerHitSystem())
                .Add(new EventHealingPercentSystem())

                //Нанесение урона
                .Add(new EventIncomingDamageApplySystem())
                //
                .Add(new ResourceRecoveryPerDamageTakenSystem())
                .Add(new ResourceRecoveryPerHitSystem())
                //
                .Add(new EventDeathByDealDamageSystem())

                //применение слот тегов
                .AddMany(SlotTagSystemsNode.BuildSystems(context))

                //запуск регенерации если значение было изменено
                .AddMany(ValuePerSecondSystemsNode.BuildSystems())

                //Срабатывание экшена смерти сущностей с истекшим сроком жизни
                .Add(new LifetimeSystem())
                //EventRemoveEntity сущностей по истечению времени
                .Add(new RemoveByTimeSystem())

                //EventRemoveEntity при столкновении с окружением
                .Add(new DeathOnTouchWallSystem())

                //EventRemoveEntity сущностей которые нанесли урон
                .Add(new DeathOnDealDamageSystem())
                .Add(new RemoveOnDealDamageSystem())

                //Удаление евентов которые должны быть удалены в конце
                .Add(new DelHere<EventTouch>())
                .Add(new DamageAreaReceiversClearSystem())
                // .Add(new DamageAreaAutoResetSystem())

                //=============================================================================================
                //Обновление UI
                .AddMany(_globalUiSystems)
                .AddMany(LocalUiSystemsNode.BuildSystems())
                .Add(new DelHere<
                    EventValueUpdated<ActionLinkButton1Component>,
                    EventValueUpdated<ActionLinkButton2Component>,
                    EventValueUpdated<ActionLinkButton3Component>,
                    EventValueUpdated<ActionLinkButton4Component>,
                    EventValueUpdated<ActionLinkMouseLeftComponent>,
                    EventValueUpdated<ActionLinkMouseRightComponent>
                >())
                //
                .Add(new DeathCallbackSystem())
                .Add(new EventEndFrameCallSystem())
                //
                .Add(new MultiplayerUpdateServerSystem(context))
                // .AddMany(ModuleSystemsNode.ModuleBindSystems(Context))
                //==>
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(bakeComponentsInName: false))
#endif
                ;

            _updateSystems.Inject();
            _fixedUpdateSystems.Inject();

            var componentPools = context.Resolve<ComponentPools>();
            InjectGeneratedPoolsAndFilters(_updateSystems, componentPools);
            InjectGeneratedPoolsAndFilters(_fixedUpdateSystems, componentPools);

            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update() => _updateSystems.Run();

        private void FixedUpdate() => _fixedUpdateSystems.Run();

        public void Tick()
        {
            _updateSystems?.Run();
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
        }

        public void AddUiSystem(IEcsSystem system) => _globalUiSystems.Add(system);
        public void AddRemoveEntityReactionUiSystem(IEcsSystem system) => _removeEntityReactionUiSystems.Add(system);

        private void InjectGeneratedPoolsAndFilters(
            IEcsSystems systems,
            ComponentPools pools)
        {
            foreach (var system in systems.GetAllSystems())
            foreach (var f in system.GetType().GetFields(
                         BindingFlags.Public |
                         BindingFlags.NonPublic |
                         BindingFlags.Instance))
            {
                if (f.IsStatic)
                    continue;

                if (typeof(ComponentPools).IsAssignableFrom(f.FieldType))
                    f.SetValue(system, pools);
            }
        }
    }
}
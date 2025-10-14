using System;
using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class PlayerControllerSystem<T> : IEcsRunSystem
        where T : struct
    {
        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent,
                MoveDesiredPositionComponent
            >,
            Exc<
                TargetComponent
            >> _moveDesiredPositionFilter;

        private readonly EcsFilterInject<
            Inc<
                TargetComponent,
                MoveDesiredPositionComponent
            >> _moveDesiredTargetPositionFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent,
                ActionPressedOnUnitTag
            >,
            Exc<
                TargetComponent
            >> _restartActionPressedFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent,
                ActionCompleteTag,
                InProgressTag<ActionPressedComponent>
            >> _resetActionPressedFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent
            >,
            Exc<
                InProgressTag<ActionPressedComponent>
            >> _setupActionPressedFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionPressedComponent>
            >,
            Exc<
                ActionPressedComponent
            >> _removeActionPressedFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent,
                ActionAttackComponent
            >,
            Exc<
                InProgressTag<ActionPressedOnEnvironmentTag>
            >> _TryAttackFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedComponent,
                T
            >> _moveStartFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionPressedOnEnvironmentTag>,
                T
            >,
            Exc<
                ActionPressedOnEnvironmentTag
            >> _moveToEnvironmentCancelFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedOnUnitTag,
                TargetComponent,
                MoveDesiredPositionComponent,
                T
            >> _moveToUnitStartFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionPressedOnUnitTag>,
                T
            >,
            Exc<
                ActionPressedOnUnitTag
            >> _moveToUnitCancelFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedOnGroundTag,
                TargetComponent,
                T
            >> _moveStreamingRemoveTargetFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionPressedOnGroundTag,
                ActionOnMoveCompleteComponent,
                T
            >> _moveStreamingRemoveActionOnMoveCompleteFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionPressedOnGroundTag>,
                T
            >,
            Exc<
                ActionPressedOnGroundTag
            >> _moveStreamingCancelFilter;

        private readonly EcsFilterInject<
            Inc<
                ButtonUseHealing
            >> _useHealingFilter;

        private readonly EcsFilterInject<
            Inc<
                PlayerInputMemoryComponent,
                ActionCurrentComponent,
                InProgressTag<PlayerInputMemoryComponent>
            >> _inputMemoryFilter;

        private readonly PlayerInputs.PlayerActions _playerActions;
        private const float InputMemoryTime = .3f;

        private readonly ComponentPools _pools;
        private readonly Camera _camera;
        private readonly RelationFunctions<AimComponent, TargetComponent> _relationFunctions;

        private readonly ActionControls<MouseLeftTag, ActionLinkMouseLeftComponent> _mouseLeftHandler;
        private readonly ActionControls<MouseRightTag, ActionLinkMouseRightComponent> _mouseRightHandler;
        private readonly ActionControls<Button1Tag, ActionLinkButton1Component> _button1Handler;
        private readonly ActionControls<Button2Tag, ActionLinkButton2Component> _button2Handler;
        private readonly ActionControls<Button3Tag, ActionLinkButton3Component> _button3Handler;
        private readonly ActionControls<Button4Tag, ActionLinkButton4Component> _button4Handler;
        private readonly SimpleActionControls<ButtonTeleport, ActionLinkTeleportToHubComponent> _buttonTeleportHandler;
        private int a;

        public PlayerControllerSystem(Context context, PlayerInputs.PlayerActions playerActions)
        {
            _playerActions = playerActions;
            _camera = context.Resolve<Camera>();
            _relationFunctions = new (context);
            var world = context.Resolve<EcsWorld>();
            var pools = context.Resolve<ComponentPools>();
            _mouseLeftHandler = new(world, pools, _relationFunctions);
            _mouseRightHandler = new(world, pools, _relationFunctions);
            _button1Handler = new(world, pools, _relationFunctions);
            _button2Handler = new(world, pools, _relationFunctions);
            _button3Handler = new(world, pools, _relationFunctions);
            _button4Handler = new(world, pools, _relationFunctions);
            _buttonTeleportHandler = new(world, pools);
        }

        public void Run(IEcsSystems systems)
        {
            _mouseLeftHandler.Run();
            _mouseRightHandler.Run();
            _button1Handler.Run();
            _button2Handler.Run();
            _button3Handler.Run();
            _button4Handler.Run();

            _buttonTeleportHandler.Run();

            foreach (var i in _restartActionPressedFilter.Value)
                _pools.InProgressActionPressed.Del(i);

            foreach (var i in _resetActionPressedFilter.Value)
                _pools.InProgressActionPressed.Del(i);

            // foreach (var i in _setupActionPressedFilter.Value)
            // {
            //     _pools.InProgressActionPressed.Add(i);
            //     _relationFunctions.DisconnectChild(i);
            //     _pools.ActionPressedOnUnit.DelIfExist(i);
            //     _pools.ActionPressedOnEnvironment.DelIfExist(i);
            //     _pools.ActionPressedOnGround.DelIfExist(i);
            //     
            //     if (!_pools.ControlLockOnTarget.Has(i))
            //     {
            //         _pools.ActionPressedOnGround.AddIfNotExist(i);
            //         _pools.InProgressActionPressedOnGround.AddIfNotExist(i);
            //     }
            //     else if (_unitService.TrySetupPressed())
            //     {
            //         _pools.ActionPressedOnUnit.AddIfNotExist(i);
            //         _pools.InProgressActionPressedOnUnit.AddIfNotExist(i);
            //         _relationFunctions.Connect(_unitService.RawEntity, i);
            //     }
            //     else if (_environmentService.HasHovered())
            //     {
            //         _environmentService.SetupPressed(i);
            //         _pools.ActionPressedOnEnvironment.AddIfNotExist(i);
            //         _pools.InProgressActionPressedOnEnvironment.AddIfNotExist(i);
            //         _relationFunctions.Connect(_environmentService.RawEntity, i);
            //     }
            //     else
            //     {
            //         _pools.ActionPressedOnGround.AddIfNotExist(i);
            //         _pools.InProgressActionPressedOnGround.AddIfNotExist(i);
            //     }
            // }

            foreach (var i in _TryAttackFilter.Value)
            {
                _mouseLeftHandler.SendStartEvent();
                _mouseRightHandler.SendStartEvent();
                _button1Handler.SendStartEvent();
                _button2Handler.SendStartEvent();
                _button3Handler.SendStartEvent();
                _button4Handler.SendStartEvent();
            }

            foreach (var i in _removeActionPressedFilter.Value)
            {
                _pools.InProgressActionPressed.Del(i);
                _pools.ActionPressedOnUnit.DelIfExist(i);
                _pools.ActionPressedOnEnvironment.DelIfExist(i);
                _pools.ActionPressedOnGround.DelIfExist(i);
            }

            //Move To Environment ===================================================
            foreach (var i in _moveStartFilter.Value)
                _pools.EventStartActionMove.Add(i);

            // foreach (var i in _moveToEnvironmentCancelFilter.Value)
            // {
            //     _pools.InProgressActionPressedOnEnvironment.Del(i);
            //     _environmentService.RemovePressed();
            // }

            //=======================================================================
            foreach (var i in _moveToUnitStartFilter.Value)
            {
                _mouseLeftHandler.PrepareOnMoveCompleteAttack();
                _mouseRightHandler.PrepareOnMoveCompleteAttack();
                _button1Handler.PrepareOnMoveCompleteAttack();
                _button2Handler.PrepareOnMoveCompleteAttack();
                _button3Handler.PrepareOnMoveCompleteAttack();
                _button4Handler.PrepareOnMoveCompleteAttack();
            }

            // foreach (var i in _moveToUnitCancelFilter.Value)
            // {
            //     _pools.InProgressActionPressedOnUnit.Del(i);
            //     _unitService.RemovePressed();
            // }

            foreach (var i in _moveStreamingRemoveTargetFilter.Value)
                _relationFunctions.DisconnectChild(i);

            foreach (var i in _moveStreamingRemoveActionOnMoveCompleteFilter.Value)
                _pools.ActionOnMoveComplete.Del(i);

            foreach (var i in _moveStreamingCancelFilter.Value)
                _pools.InProgressActionPressedOnGround.Del(i);

            foreach (var i in _useHealingFilter.Value)
                _pools.EventAbilityStartHealingPotionValue.Add(i);

            //===========================================================================

            // foreach (var i in _jumpFilter.Value)
            //     AddIfReadyElseRemember(i, _pools.EventStartActionJump.AddIfNotExist);
            //
            // foreach (var i in _useHealingFilter.Value)
            //     AddIfReadyElseRemember(i, _pools.EventStartActionUseHealingPotion.AddIfNotExist);
            //
            // foreach (var i in _strongAttackFilter.Value)
            //     AddIfReadyElseRemember(i, _pools.EventStartActionStrongAttack1.AddIfNotExist);
            //
            // foreach (var i in _specialAttackFilter.Value)
            //     AddIfReadyElseRemember(i, _pools.EventStartActionSpecialAttack.AddIfNotExist);
            //
            // foreach (var i in _specialDownAttackFilter.Value)
            //     _pools.EventStartActionSpecialDownAttack.Add(i);
            //
            // foreach (var i in _specialForwardKickAttackFilter.Value)
            //     _pools.EventStartActionSpecialForwardKickAttack.Add(i);

            foreach (var i in _moveDesiredPositionFilter.Value)
            {
                var pos = (Vector3)_playerActions.MousePosition.ReadValue<Vector2>();
                var ray = _camera.ScreenPointToRay(pos);
                float t = -ray.origin.z / ray.direction.z;
                var position = ray.origin + t * ray.direction;
                
                const float distanceToSuccess = .2f;
                ref var dest = ref _pools.MoveDesiredPosition.Get(i);
                dest.position = position;
                dest.distanceToSuccess = distanceToSuccess;
            }

            foreach (var i in _moveDesiredTargetPositionFilter.Value)
            {
                int target = _pools.Target.Get(i).entity;
                ref var dest = ref _pools.MoveDesiredPosition.Get(i);
                dest.position = _pools.Position.Get(target).transform.position;
                dest.distanceToSuccess = _pools.BodyRadius.Get(target).radius;
            }

            foreach (var i in _inputMemoryFilter.Value)
            {
                ref var memory = ref _pools.PlayerInputMemory.Get(i);

                if (memory.inputTime < Time.unscaledTime)
                {
                    _pools.InProgressPlayerInputMemory.Del(i);
                    continue;
                }

                var actionCurrent = _pools.ActionCurrent.Get(i);

                if (actionCurrent.currentAction != memory.lastActionSystem)
                {
                    _pools.InProgressPlayerInputMemory.Del(i);
                    continue;
                }

                if (!_pools.ActionCanBeCanceled.Has(i) && !_pools.ActionComplete.Has(i))
                    continue;

                memory.AddEventStartAction.Invoke(i);
                _pools.InProgressPlayerInputMemory.Del(i);
            }
        }

        private void AddIfReadyElseRemember(int i, Action<int> startAction)
        {
            if (_pools.ActionCanBeCanceled.Has(i))
                startAction(i);
            else
                WriteMemory(i, startAction);
        }

        private void WriteMemory(int entity, Action<int> addActionEvent)
        {
            var actionCurrent = _pools.ActionCurrent.Get(entity);
            ref var memory = ref _pools.PlayerInputMemory.Get(entity);
            memory.AddEventStartAction = addActionEvent;
            memory.inputTime = Time.unscaledTime + InputMemoryTime;
            memory.lastActionSystem = actionCurrent.currentAction;
            _pools.InProgressPlayerInputMemory.AddIfNotExist(entity);
        }
    }
}
using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class PlayerInputsSystem<T> : IEcsRunSystem
        where T : struct
    {
        private readonly EcsFilterInject<
            Inc<
                ActionMoveComponent,
                MoveDirectionComponent,
                ButtonMoveTag,
                T
            >> _moveStreamingFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionMoveComponent>,
                MoveDirectionComponent,
                EventButtonCanceled<ButtonMoveTag>,
                T
            >> _moveCompleteFilter;
        
        private readonly EcsFilterInject<
            Inc<
                ActionAttackComponent,
                MouseLeftTag,
                T
            >> _mouseLeftStreamingFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionAttackComponent>,
                EventButtonCanceled<MouseLeftTag>,
                T
            >> _mouseLeftCompleteFilter;

        private readonly EcsFilterInject<
            Inc<
                PlayerInputMemoryComponent,
                ActionCurrentComponent,
                InProgressTag<PlayerInputMemoryComponent>
            >> _inputMemoryFilter;

        private readonly PlayerInputs.PlayerActions _playerActions;
        private const float InputMemoryTime = .3f;

        private readonly ComponentPools _pools;
        private readonly Transform _cameraTransform;

        private readonly ActionControls<MouseLeftTag, ActionLinkMouseLeftComponent> _mouseLeftHandler;
        private readonly ActionControls<MouseRightTag, ActionLinkMouseRightComponent> _mouseRightHandler;
        private readonly ActionControls<Button1Tag, ActionLinkButton1Component> _button1Handler;
        private readonly ActionControls<Button2Tag, ActionLinkButton2Component> _button2Handler;
        private readonly ActionControls<Button3Tag, ActionLinkButton3Component> _button3Handler;
        private readonly ActionControls<Button4Tag, ActionLinkButton4Component> _button4Handler;
        private readonly SimpleActionControls<ButtonTeleport, ActionLinkTeleportToHubComponent> _buttonTeleportHandler;


        public PlayerInputsSystem(Context context, PlayerInputs.PlayerActions playerActions)
        {
            _playerActions = playerActions;
            _cameraTransform = context.Resolve<Camera>().transform;
            
            _mouseLeftHandler = new(context);
            _mouseRightHandler = new(context);
            _button1Handler = new(context);
            _button2Handler = new(context);
            _button3Handler = new(context);
            _button4Handler = new(context);
            _buttonTeleportHandler = new(context);

        }

        public void Run(IEcsSystems systems)
        {
            _mouseLeftHandler.Run();
            _mouseRightHandler.Run();
            _button1Handler.Run();
            _button2Handler.Run();
            _button3Handler.Run();
            _button4Handler.Run();
            
            foreach (var i in _moveStreamingFilter.Value)
            {
                var cameraForward = _cameraTransform.forward;
                var cameraRight = _cameraTransform.right;
                cameraForward.y = 0f;
                cameraRight.y = 0f;
                cameraForward.Normalize();
                cameraRight.Normalize();

                ref var move = ref _pools.MoveDirection.Get(i);
                var direction = _playerActions.Move.ReadValue<Vector2>();
                var direction3D = (cameraForward * direction.y + cameraRight * direction.x).normalized;
                move.direction = new Vector2(direction3D.x, direction3D.z);

                if (!_pools.InProgressActionMove.Has(i))
                    _pools.EventStartActionMove.Add(i);
            }

            foreach (var i in _moveCompleteFilter.Value)
                _pools.ActionComplete.AddIfNotExist(i);

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
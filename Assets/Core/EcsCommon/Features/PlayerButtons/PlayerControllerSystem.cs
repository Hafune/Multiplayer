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
    public class PlayerControllerSystem<T> : IEcsRunSystem
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
                PlayerInputMemoryComponent,
                ActionCurrentComponent,
                InProgressTag<PlayerInputMemoryComponent>
            >> _inputMemoryFilter;

        private readonly PlayerInputs.PlayerActions _playerActions;
        private const float InputMemoryTime = .3f;

        private readonly ComponentPools _pools;
        private int a;

        public PlayerControllerSystem(Context context, PlayerInputs.PlayerActions playerActions)
        {
            _playerActions = playerActions;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _moveStreamingFilter.Value)
            {
                ref var move = ref _pools.MoveDirection.Get(i);
                move.direction = _playerActions.Move.ReadValue<Vector2>();

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
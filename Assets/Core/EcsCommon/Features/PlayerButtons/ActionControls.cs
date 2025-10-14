using System;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.Systems
{
    public class ActionControls<B, A>
        where B : struct, IButtonComponent
        where A : struct, IEntityActionComponent
    {
        private readonly EcsFilter _actionPressedFilter;
        private readonly EcsFilter _prepareOnMoveCompleteFilter;
        private readonly EcsFilter _eventButtonPerformedFilter;
        private readonly EcsFilter _releasedFilter;
        private readonly EcsFilter _completeStreamingFilter;
        private readonly EcsPool<A> _actionPool;

        private readonly EcsPool<ActionAttrControlLockOnTargetTag<A>> _actionAttrControlLockOnTargetPool;
        private readonly EcsPool<ActionAttrTargetDistanceComponent<A>> _actionAttrTargetDistancePool;

        private readonly ComponentPools _pools;
        private readonly RelationFunctions<AimComponent, TargetComponent> _relationFunctions;
        private readonly Action<int> _prepareActionAndSendStartEvent;

        public ActionControls(EcsWorld world, ComponentPools pools, RelationFunctions<AimComponent, TargetComponent> relationFunctions)
        {
            _actionPressedFilter = world
                .Filter<B>()
                .Inc<A>()
                .Inc<ActionAttackComponent>()
                .End();

            _prepareOnMoveCompleteFilter = world
                .Filter<B>()
                .Inc<A>()
                .Inc<ActionAttackComponent>()
                .Inc<ActionAttrTargetDistanceComponent<A>>()
                .End();

            _eventButtonPerformedFilter = world
                .Filter<EventButtonPerformed<B>>()
                .End();

            _releasedFilter = world
                .Filter<EventButtonCanceled<B>>()
                .End();

            _completeStreamingFilter = world.Filter<EventButtonCanceled<B>>().Inc<WaitStreamingCancel<B>>().End();
            _pools = pools;

            _actionPool = world.GetPool<A>();
            _actionAttrControlLockOnTargetPool = world.GetPool<ActionAttrControlLockOnTargetTag<A>>();
            _actionAttrTargetDistancePool = world.GetPool<ActionAttrTargetDistanceComponent<A>>();
            _relationFunctions = relationFunctions;

            _prepareActionAndSendStartEvent = PrepareActionAndSendStartEvent;
        }

        public void Run()
        {
            foreach (var i in _eventButtonPerformedFilter)
                _pools.ActionPressed.GetOrInitialize(i).pressedCount++;

            foreach (var i in _releasedFilter)
                if (--_pools.ActionPressed.Get(i).pressedCount == 0)
                    _pools.ActionPressed.Del(i);

            foreach (var i in _actionPressedFilter)
            {
                var actionLogic = _actionPool.Get(i).logic;

                if (!actionLogic)
                    continue;

                if (actionLogic is EntityActionInstant)
                    continue;

                if (_actionAttrControlLockOnTargetPool.Has(i))
                {
                    _pools.ControlLockOnTarget.AddIfNotExist(i);
                }
                else
                {
                    _pools.ControlLockOnTarget.DelIfExist(i);
                    _relationFunctions.DisconnectChild(i);
                }
            }

            foreach (var i in _completeStreamingFilter)
                _pools.EventActionCompleteStreaming.AddIfNotExist(i);
        }

        public void SendStartEvent()
        {
            foreach (var i in _actionPressedFilter)
                PrepareActionAndSendStartEvent(i);
        }

        public void PrepareOnMoveCompleteAttack()
        {
            foreach (var i in _prepareOnMoveCompleteFilter)
            {
                 ref var value = ref _pools.ActionOnMoveComplete.GetOrInitialize(i);
                 value.action = _prepareActionAndSendStartEvent;
                 value.actionDistance = _actionAttrTargetDistancePool.Get(i).value;
            }
        }

        private void PrepareActionAndSendStartEvent(int i)
        {
            var actionLogic = _actionPool.Get(i).logic;

            if (!actionLogic)
                return;

            if (actionLogic is EntityActionInstant action)
            {
                _pools.ActionAttackInstant.Get(i).logic = action;
                _pools.EventStartActionAttackInstant.AddIfNotExist(i);
                return;
            }

            if (!_pools.ActionComplete.Has(i) && !_pools.ActionCanBeCanceled.Has(i))
                return;

            _pools.ActionAttack.Get(i).logic = (AbstractEntityActionStateful)actionLogic;
            _pools.EventStartActionAttack.AddIfNotExist(i);
        }
    }
}
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class ActionControls<B, A>
        where B : struct, IButtonComponent
        where A : struct, IEntityActionComponent
    {
        private readonly EcsFilter _eventButtonPerformedFilter;
        private readonly EcsFilter _releasedFilter;
        private readonly EcsFilter _completeStreamingFilter;
        private readonly EcsFilter _actionPressedFilter;
        private readonly EcsPool<A> _actionPool;

        private readonly ComponentPools _pools;

        public ActionControls(Context context)
        {
            var world = context.Resolve<EcsWorld>();
            _pools = context.Resolve<ComponentPools>();
            
            _eventButtonPerformedFilter = world
                .Filter<EventButtonPerformed<B>>()
                .Inc<ActionAttackComponent>()
                .End();
            
            _actionPressedFilter = world
                .Filter<B>()
                .Inc<A>()
                .Inc<ActionAttackComponent>()
                .End();

            _releasedFilter = world
                .Filter<EventButtonCanceled<B>>()
                .Inc<ActionPressedComponent>()
                .End();

            _completeStreamingFilter = world.Filter<EventButtonCanceled<B>>().Inc<WaitStreamingCancel<B>>().End();
            _actionPool = world.GetPool<A>();
        }

        public void Run()
        {
            foreach (var i in _eventButtonPerformedFilter)
                _pools.ActionPressed.GetOrInitialize(i).pressedCount++;
            
            foreach (var i in _actionPressedFilter)
                PrepareActionAndSendStartEvent(i);

            foreach (var i in _releasedFilter)
                if (--_pools.ActionPressed.Get(i).pressedCount == 0)
                    _pools.ActionPressed.Del(i);

            foreach (var i in _completeStreamingFilter)
                _pools.EventActionCompleteStreaming.AddIfNotExist(i);
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
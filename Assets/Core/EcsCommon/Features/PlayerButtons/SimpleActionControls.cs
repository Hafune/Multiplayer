using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite;
using Lib;

namespace Core.Systems
{
    public readonly struct SimpleActionControls<B, A>
        where B : struct, IButtonComponent
        where A : struct, IEntityActionComponent
    {
        private readonly EcsFilter _pressedFilter;

        private readonly EcsPool<A> _actionPool;
        private readonly ComponentPools _pools;

        public SimpleActionControls(EcsWorld world, ComponentPools pools)
        {
            _pressedFilter = world.Filter<B>().Inc<A>().Inc<ActionAttackComponent>().End();
            _pools = pools;
            _actionPool = world.GetPool<A>();
        }

        public void Run()
        {
            foreach (var i in _pressedFilter)
            {
                var actionLogic = _actionPool.Get(i).logic;

                if (!actionLogic)
                    continue;

                ref var action = ref _pools.ActionAttack.Get(i);
                action.logic = (AbstractEntityActionStateful)actionLogic;
                _pools.EventStartActionAttack.AddIfNotExist(i);
            }
        }
    }
}
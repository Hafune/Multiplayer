using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class DeathWithRelationSystem<N,P,T> : IEcsRunSystem
    where N : struct, INodeComponent<P>
    where P : struct, IParentComponent
    where T : struct
    {
        private readonly EcsFilterInject<
            Inc<
                EventActionStart<ActionDeathComponent>,
                N
            >> _filter;

        private readonly RelationFunctions<N, P> _aimRelationFunctions;
        private readonly EcsPool<EventActionStart<ActionDeathComponent>> _eventStartDeathPool;
        private readonly EcsPoolInject<T> _deathWithTarget;

        public DeathWithRelationSystem(Context context)
        {
            _aimRelationFunctions = new(context);
            _eventStartDeathPool = context.Resolve<ComponentPools>().EventStartActionDeath;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            foreach (var e in _aimRelationFunctions.EnumerateSelfChilds(i, _deathWithTarget.Value))
                _eventStartDeathPool.AddIfNotExist(e);
        }
    }
}
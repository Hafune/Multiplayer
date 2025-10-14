using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class RemoveWithRelationSystem<N, P, R> : IEcsRunSystem
        where N : struct, INodeComponent<P>
        where P : struct, IParentComponent
        where R : struct
    {
        private readonly EcsFilterInject<
            Inc<
                N,
                EventRemoveEntity
            >> _filter;

        private readonly RelationFunctions<N, P> _relationFunctions;
        private readonly EcsPool<EventRemoveEntity> _eventRemoveEntityPool;
        private readonly EcsPoolInject<R> _removeWithParentPool;

        public RemoveWithRelationSystem(Context context)
        {
            _relationFunctions = new(context);
            _eventRemoveEntityPool = context.Resolve<ComponentPools>().EventRemoveEntity;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            foreach (var e in _relationFunctions.EnumerateSelfChilds(i, _removeWithParentPool.Value))
                _eventRemoveEntityPool.AddIfNotExist(e);
        }
    }
}
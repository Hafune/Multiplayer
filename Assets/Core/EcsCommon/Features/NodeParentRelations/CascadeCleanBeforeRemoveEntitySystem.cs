using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class CascadeCleanBeforeRemoveEntitySystem<N, P> : IEcsRunSystem
        where N : struct, INodeComponent<P>
        where P : struct, IParentComponent
    {
        private readonly EcsFilterInject<
            Inc<
                N,
                EventRemoveEntity
            >> _nodeFilter;

        private readonly EcsFilterInject<
            Inc<
                P,
                EventRemoveEntity
            >> _parentFilter;

        private readonly RelationFunctions<N, P> _relationFunctions;

        public CascadeCleanBeforeRemoveEntitySystem(Context context) => _relationFunctions =
            new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var nodeEntity in _nodeFilter.Value)
                _relationFunctions.DisconnectNodeChilds(nodeEntity);

            foreach (var i in _parentFilter.Value)
                _relationFunctions.DisconnectChild(i);
        }
    }
}
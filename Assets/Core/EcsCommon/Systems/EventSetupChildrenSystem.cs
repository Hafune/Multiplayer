using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class EventSetupChildrenSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventSetupChildren
            >> _filter;

        private readonly ComponentPools _pools;
        private readonly RelationFunctions<NodeComponent, ParentComponent> _relationFunctions;

        public EventSetupChildrenSystem(Context context) => _relationFunctions =
            new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                foreach (var entityRef in _pools.EventSetupChildren.Get(i).children)
                    _relationFunctions.Connect(i, entityRef.RawEntity);
                
                _pools.EventSetupChildren.Del(i);
            }
        }
    }
}
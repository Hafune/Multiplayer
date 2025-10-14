using Core.Components;
using Core.Generated;
using Core.Lib;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class EventCheckMergesSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventCheckMerges
            >> _filter;

        private readonly ComponentPools _pools;
        private readonly ComponentMerges _componentMerges;

        public EventCheckMergesSystem(Context context) => _componentMerges = context.Resolve<ComponentMerges>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                while (_componentMerges.TryCombine(i))
                {
                }

                _pools.EventCheckMerges.Del(i);
            }
        }
    }
}
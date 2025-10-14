using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class HitBlinkSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                HitBlinkComponent,
                EventHitTaken
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var blink = _pools.HitBlink.Get(i).hitBlink;
                blink.Activate(blink.Data.material, blink.Data.time);
            }
        }
    }
}
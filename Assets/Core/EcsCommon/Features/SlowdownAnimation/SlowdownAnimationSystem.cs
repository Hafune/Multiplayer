using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class SlowdownAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                SlowdownAnimationValueComponent,
                EventValueUpdated<SlowdownAnimationValueComponent>,
                AnimatorComponent
            >> _valueFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueFilter.Value)
            {
                var value = _pools.SlowdownAnimationValue.Get(i).value;
                _pools.Animator.Get(i).animancer.Graph.Speed = 1 - value;
            }
        }
    }
}
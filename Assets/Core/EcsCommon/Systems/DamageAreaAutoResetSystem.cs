using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class DamageAreaAutoResetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DamageAreaAutoResetComponent
            >> _filter;

        private readonly EcsPoolInject<DamageAreaAutoResetComponent> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                ref var component = ref _pool.Value.Get(i);

                if (Time.time - component.lastTime < component.delay)
                    continue;
                
                component.area.ResetTargets();
                component.lastTime = Time.time;
                // _pool.Value.Del(i);
                // _progressPool.Value.Del(i);
            }
        }
    }
}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class DeathCallbackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventDeath,
                DeathCallbackComponent
            >> _filter;

        private readonly EcsPoolInject<DeathCallbackComponent> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var call = _pool.Value.Get(i).call;
                call.Invoke(i);
                call.Clear();
            }
        }
    }
}
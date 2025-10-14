using Core.Components;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class EventStartActionReviveLogic : AbstractEntityLogic
    {
        private EcsPool<EventActionStart<ActionReviveComponent>> _pool;
        private void Awake() => _pool = context.Resolve<EcsWorld>().GetPool<EventActionStart<ActionReviveComponent>>();

        public override void Run(int entity) => _pool.Add(entity);
    }
}
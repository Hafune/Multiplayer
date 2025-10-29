using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ActionDefaultSystem : AbstractActionSystem<ActionDefaultComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDefaultComponent,
                ActionCurrentComponent,
                ActionCompleteTag,
                RigidbodyComponent
            >,
            Exc<
                InProgressTag<ActionDefaultComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActionDefaultComponent,
                EventActionStart<ActionDefaultComponent>,
                ActionCurrentComponent,
                RigidbodyComponent
            >> _activateFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);

            foreach (var i in _activateFilter.Value)
                if (_actionPool.Value.Get(i).logic.CheckConditionLogic(i))
                    UpdateEntity(i);

            CleanEventStart();
        }

        private void UpdateEntity(int entity)
        {
            BeginActionProgress(entity, _actionPool.Value.Get(entity).logic, false);
            _pools.ActionCanBeCanceled.AddIfNotExist(entity);
            _pools.ActionComplete.AddIfNotExist(entity);
            _pools.Rigidbody.Get(entity).rigidbody.linearDamping = 10;
        }
        
        public override void Cancel(int entity)
        {
            base.Cancel(entity);
            var rigidbody = _pools.Rigidbody.Get(entity).rigidbody; 
            rigidbody.linearDamping = 0;
        }
    }
}
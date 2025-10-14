using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionDefaultSystem : AbstractActionSystem<ActionDefaultComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDefaultComponent,
                ActionCurrentComponent,
                ActionCompleteTag
            >,
            Exc<
                InProgressTag<ActionDefaultComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActionDefaultComponent,
                EventActionStart<ActionDefaultComponent>,
                ActionCurrentComponent
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
        }
    }
}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ActionDeathSystem : AbstractActionSystem<ActionDeathComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDeathComponent,
                EventActionStart<ActionDeathComponent>,
                ActionCurrentComponent
            >,
            Exc<
                InProgressTag<ActionDeathComponent>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventActionStart<ActionDeathComponent>
            >,
            Exc<
                ActionDeathComponent
            >> _withoutActionFilter;

        private readonly EcsFilterInject<
            Inc<
                ActionCompleteTag,
                InProgressTag<ActionDeathComponent>
            >> _watchFilter;

        private readonly EcsFilterInject<
            Inc<
                EventDeath
            >> _eventDiedFilter;

        private readonly EcsFilterInject<
            Inc<
                EnemyComponent,
                ConvertToEntityComponent,
                EventDeath
            >> _eventDiedForListenerFilter;

        // private readonly EcsFilterInject<
        //     Inc<
        //         EnemiesDiedListener
        //     >> _enemyDiedListenerFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventDiedFilter.Value)
                _pools.EventDeath.Del(i);

            foreach (var i in _filter.Value)
            {
                BeginActionProgress(i, _actionPool.Value.Get(i).logic);
                _pools.EventDeath.Add(i);
            }
            
            // foreach (var e in _eventDiedForListenerFilter.Value)
            // foreach (var i in _enemyDiedListenerFilter.Value)
            //     _pools.EnemiesDiedListener.Get(i).onDied?.Invoke(_pools.ConvertToEntity.Get(e).convertToEntity.TemplateId);

            foreach (var i in _withoutActionFilter.Value)
            {
                _pools.EventDeath.Add(i);
                _pools.EventRemoveEntity.AddIfNotExist(i);
            }

            foreach (var i in _watchFilter.Value)
                _pools.EventRemoveEntity.AddIfNotExist(i);

            CleanEventStart();
        }
    }
}
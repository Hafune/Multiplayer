using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class EventSetupParentValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventSetupParentValue<T>
            >,
            Exc<
                T
            >> _prepareFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                NodeComponent,
                EventChildAdded
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventSetupParentValue<T>
            >> _eventFilter;

        private readonly EcsFilterInject<
            Inc<
                NodeComponent,
                EventValueUpdated<T>,
                T
            >> _updateValueByParentFilter;

        private readonly EcsPoolInject<EventSetupParentValue<T>> _eventPool;
        private readonly EcsPoolInject<EventStartRecalculateAllValues> _eventStartRecalculateValuePool;
        private readonly EcsPoolInject<ParentComponent> _parentPool;
        private readonly EcsPoolInject<BaseValueComponent<T>> _basePool;
        private readonly EcsPoolInject<ParentValueTag<T>> _parentValuePool;
        private readonly EcsPoolInject<T> _valuePool;

        private readonly RelationFunctions<NodeComponent, ParentComponent> _relationFunctions;
        private int _parentEntity;

        public EventSetupParentValueSystem(Context context) => _relationFunctions = new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _prepareFilter.Value)
                _valuePool.Value.Add(i);

            //Неверная логика один ко многим, родитель не может знать что потребуется всем будущим детям 
            //Детям не могут передаваться компоненты как конструктор, любой набор компонентов должен быть ожидаемым, а не случайным.
            foreach (var i in _filter.Value)
            {
                _parentEntity = i;
                
                foreach (var e in _relationFunctions.EnumerateSelfChilds(i, _eventPool.Value))
                {
                    AddParentValueTag(e);
                    SetupBaseValueByParent(e);
                }
            }

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);

            foreach (var i in _updateValueByParentFilter.Value)
            {
                _parentEntity = i;
                foreach (var e in _relationFunctions.EnumerateSelfChilds(i, _parentValuePool.Value))
                    SetupBaseValueByParent(e);
            }
        }

        private void SetupBaseValueByParent(int entity)
        {
            float value = _valuePool.Value.Get(_parentEntity).value;
            _basePool.Value.GetOrInitialize(entity).baseValue = value;
            _valuePool.Value.GetOrInitialize(entity).value = value;
            _eventStartRecalculateValuePool.Value.AddIfNotExist(entity);
        }

        private void AddParentValueTag(int entity) => _parentValuePool.Value.Add(entity);
    }
}
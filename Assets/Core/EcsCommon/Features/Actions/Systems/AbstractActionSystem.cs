using System.Runtime.CompilerServices;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public interface IAbstractActionSystem
    {
        void Cancel(int entity);
    }

    public abstract class AbstractActionSystem<T> : IAbstractActionSystem where T : struct
    {
        protected readonly EcsFilterInject<
            Inc<
                EventActionStart<T>
            >> _eventStartFilter;

        protected readonly EcsPoolInject<T> _actionPool;
        protected readonly EcsPoolInject<InProgressTag<T>> _inProgressPool;
        protected readonly EcsPoolInject<EventActionStart<T>> _eventStartPool;

        protected readonly ComponentPools _pools;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CleanEventStart()
        {
            foreach (var i in _eventStartFilter.Value)
                _eventStartPool.Value.Del(i);
        }

        protected void BeginActionProgress(int entity, AbstractEntityActionStateful logic, bool removeCompleteTag = true)
        {
            ref var current = ref _pools.ActionCurrent.Get(entity);
            current.currentAction?.Cancel(entity);
            current.currentAction = this;

            if (removeCompleteTag)
                _pools.ActionComplete.DelIfExist(entity);

            _pools.ActionCanBeCanceled.DelIfExist(entity);
            _inProgressPool.Value.Add(entity);

            current.logic = logic;
            logic.StartLogic(entity);
            _pools.EventActionChanged.AddIfNotExist(entity);

            if (current.BTreeDesiredActionLogic != current.logic)
                return;

            current.BTreeOnActionStart?.Invoke();
            // _pools.EventBehaviorTreeActionStartFailedCheck.DelIfExist(entity);
        }

        public virtual void Cancel(int entity)
        {
            _pools.ActionCurrent.Get(entity).logic?.CancelLogic(entity);
            _inProgressPool.Value.Del(entity);
        }
    }
}
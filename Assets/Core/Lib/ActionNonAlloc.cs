using System;
using System.Runtime.CompilerServices;
using Core.Lib.Utils;

namespace Core.Lib
{
    public class ActionNonAlloc<T> : AbstractActionNonAlloc<Action<T>>
    {
        public static ActionNonAlloc<T> operator +(ActionNonAlloc<T> actionNonAlloc, Action<T> action)
        {
            actionNonAlloc.Subscribe(action);
            return actionNonAlloc;
        }

        public static ActionNonAlloc<T> operator -(ActionNonAlloc<T> actionNonAlloc, Action<T> action)
        {
            actionNonAlloc.Unsubscribe(action);
            return actionNonAlloc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(T parameter)
        {
            if (_isExecuting)
                throw new InvalidOperationException("Рекурсивный вызов невозможен");

            PrepareExecuting();

            for (int i = 0, iMax = _subscribersCount; i < iMax; i++)
                _subscribers[i](parameter);

            FinishExecuting();
        }
    }

    public class ActionNonAlloc : AbstractActionNonAlloc<Action>
    {
        public static ActionNonAlloc operator +(ActionNonAlloc actionNonAlloc, Action action)
        {
            actionNonAlloc.Subscribe(action);
            return actionNonAlloc;
        }

        public static ActionNonAlloc operator -(ActionNonAlloc actionNonAlloc, Action action)
        {
            actionNonAlloc.Unsubscribe(action);
            return actionNonAlloc;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke()
        {
            if (_isExecuting)
                throw new InvalidOperationException("Рекурсивный вызов невозможен");

            PrepareExecuting();
            
            for (int i = 0, iMax = _subscribersCount; i < iMax; i++)
                _subscribers[i]();

            FinishExecuting();
        }
    }

    public abstract class AbstractActionNonAlloc<T> where T : Delegate
    {
        protected int _reserveCount;
        protected T[] _reserve;

        protected int _subscribersCount;
        protected T[] _subscribers;

        protected bool _isExecuting;

        protected AbstractActionNonAlloc()
        {
            _subscribers = new T[2];
            _reserve = new T[2];
        }

        protected void Subscribe(T action)
        {
#if UNITY_EDITOR
            if (action == null)
                throw new ArgumentNullException(nameof(action));
#endif

            if (_isExecuting)
                MyArrayUtility.Add(ref _reserve, ref _reserveCount, action);
            else
                MyArrayUtility.Add(ref _subscribers, ref _subscribersCount, action);
        }

        protected void Unsubscribe(T action)
        {
#if UNITY_EDITOR
            if (action == null)
                throw new ArgumentNullException(nameof(action));
#endif

            if (_isExecuting)
                MyArrayUtility.Remove(ref _reserve, ref _reserveCount, action);
            else
                MyArrayUtility.Remove(ref _subscribers, ref _subscribersCount, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void PrepareExecuting()
        {
            _isExecuting = true;
            
            if (_reserve.Length != _subscribers.Length)
                _reserve = new T[_subscribers.Length];

            Array.Copy(_subscribers, _reserve, _subscribersCount);
            _reserveCount = _subscribersCount;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void FinishExecuting()
        {
            if (_subscribers.Length != _reserve.Length)
                _subscribers = new T[_reserve.Length];
            
            Array.Copy(_reserve, _subscribers, _reserveCount);
            _subscribersCount = _reserveCount;

            _isExecuting = false;
        }

        public void Clear()
        {
#if UNITY_EDITOR
            if (_isExecuting)
                throw new ArgumentNullException(nameof(Clear) + " во время выполнения");
#endif
            _subscribersCount = 0;
        }
    }
}
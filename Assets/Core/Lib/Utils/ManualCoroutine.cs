using System.Collections;
using System.Collections.Generic;

namespace Core.Lib
{
    public class ManualCoroutine
    {
        private readonly Stack<IEnumerator> _stack = new();

        /// <summary>
        /// Запускает новую корутину. Предыдущая сбрасывается.
        /// </summary>
        public void StartCoroutine(IEnumerator root)
        {
            _stack.Clear();
            if (root != null)
                _stack.Push(root);
        }

        /// <summary>
        /// Выполняет один шаг. Возвращает true, если выполнение продолжается, иначе false.
        /// </summary>
        public bool MoveNext()
        {
            while (_stack.Count > 0)
            {
                var current = _stack.Peek();

                if (current.MoveNext())
                {
                    if (current.Current is IEnumerator nested)
                    {
                        _stack.Push(nested);
                    }

                    return true;
                }

                _stack.Pop(); // Вложенный завершён — возвращаемся к предыдущему
            }

            return false; // Всё завершено
        }
        
        public void Cancel() => _stack.Clear();

        /// <summary>
        /// Является ли корутина активной (ещё не завершена).
        /// </summary>
        public bool IsRunning => _stack.Count > 0;
    }
}
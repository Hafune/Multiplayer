using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Lib
{
    public class MyList<T> : IEnumerable<T>
    {
        private static readonly MyList<T> _empty = new();

        public static MyList<T> Empty
        {
            get
            {
                _empty._createdAsEmpty = true;
                return _empty;
            }
        }

        private T[] _items;
        private int _count;
        private bool _createdAsEmpty;

        public int Count => _count;

        public MyList() => _items = Array.Empty<T>(); // Initial capacity

        public MyList(int capacity) => _items = new T[NextPowerOfTwo(capacity)];

        public MyList(MyList<T> source)
        {
            _count = source._count;
            _items = new T[source._items.Length];
            Array.Copy(source._items, _items, _count);
        }

        public MyList(IEnumerable<T> source)
        {
            _items = Array.Empty<T>();
            foreach (var value in source)
                Add(value);
        }

        // Вспомогательный метод:
        private static int NextPowerOfTwo(int value)
        {
            if (value < 1)
                return 1;

            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if UNITY_EDITOR
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
#endif
                return ref _items[index];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            if (_count == _items.Length)
                ResizeArray();

            _items[_count++] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(List<T> items)
        {
            if (_count + items.Count > _items.Length)
                ResizeArray(_count + items.Count);

            for (int i = 0; i < items.Count; i++)
                _items[_count++] = items[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(MyList<T> items)
        {
            if (_count + items.Count > _items.Length)
                ResizeArray(_count + items._count);

            for (int i = 0; i < items._count; i++)
                _items[_count++] = items._items[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(T[] items)
        {
            if (_count + items.Length > _items.Length)
                ResizeArray(_count + items.Length);

            for (int i = 0; i < items.Length; i++)
                _items[_count++] = items[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Set(int i, T value) => _items[i] = value;

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public T Get(int i) => Items[i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(int i) => ref _items[i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Last() => _items[_count - 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item) => Array.IndexOf(_items, item, 0, _count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryIndexOf(T item, out int index) => (index = Array.IndexOf(_items, item, 0, _count)) != -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item)
        {
            int index = Array.IndexOf(_items, item, 0, _count);
            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InsertAt(int index, T item)
        {
            if (_count == _items.Length)
                ResizeArray();

            _count++;
            for (int i = _count - 1; i > index; i--)
                _items[i] = _items[i - 1];

            _items[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index)
        {
            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];

            _count--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item) => Array.IndexOf(_items, item, 0, _count) >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Func<T, bool> compare)
        {
            for (int i = 0; i < _count; i++)
                if (compare(_items[i]))
                    return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() => _count = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop()
        {
            int index = _count - 1;
            var item = _items[index];
            RemoveAt(index);
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Dequeue()
        {
            var item = _items[0];
            RemoveAt(0);
            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Random() => _items[UnityEngine.Random.Range(0, _count)];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Random(MyRandom random) => _items[random.NextInt(_count)];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RemoveAll(Predicate<T> match)
        {
            int removedCount = 0;
            int writeIndex = 0;

            for (int readIndex = 0; readIndex < _count; readIndex++)
            {
                if (!match(_items[readIndex]))
                {
                    if (writeIndex != readIndex)
                        _items[writeIndex] = _items[readIndex];
                    writeIndex++;
                }
                else
                {
                    removedCount++;
                }
            }

            _count = writeIndex;
            return removedCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResizeArray(int minCapacity = 0)
        {
            if (_createdAsEmpty)
                Debug.LogError("ресайз Empty массива");

            var newCapacity = math.max(_items.Length * 2, 1);
            while (newCapacity < minCapacity)
                newCapacity *= 2;

            var newItems = new T[newCapacity];
            Array.Copy(_items, newItems, _items.Length);
            _items = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return _items[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        public ref struct Enumerator
        {
            private readonly int _count;
            private readonly T[] _data;
            private int _idx;

            public Enumerator(MyList<T> data)
            {
                _count = data._count;
                _data = data._items;
                _idx = -1;
            }

            public ref T Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => ref _data[_idx];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() => ++_idx < _count;
        }
    }
}
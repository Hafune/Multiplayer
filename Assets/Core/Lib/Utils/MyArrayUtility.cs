using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Core.Lib.Utils
{
    public static class MyArrayUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(ref T[] items, ref int count, T item)
        {
            if (count == items.Length)
                ResizeArray(ref items);

            items[count++] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ResizeArray<T>(ref T[] items)
        {
            var newItems = new T[math.max(items.Length * 2, 2)];
            Array.Copy(items, newItems, items.Length);
            items = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T>(ref T[] items, ref int count, T item)
        {
            int index = Array.IndexOf(items, item, 0, count);
            if (index < 0)
                return false;

            RemoveAt(ref items, ref count, index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAt<T>(ref T[] items, ref int count, int index)
        {
            for (int i = index; i < count - 1; i++)
                items[i] = items[i + 1];

            count--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this T[] items, T item) => Array.IndexOf(items, item) > -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this T[] items, T[] otherItems)
        {
            for (int i = 0; i < otherItems.Length; i++)
                if (Array.IndexOf(items, otherItems[i]) > -1)
                    return true;
            
            return false;
        }
    }
}
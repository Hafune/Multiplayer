using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Lib;
using UnityEngine;
using Random = System.Random;

namespace Lib
{
    public static class MyFunctions
    {
        private static Random random = new();
        private static Assembly[] _assemblies;

        public static void ForEachDimensions(int[] dimensions, Action<int[]> callback)
        {
            var args = new int[dimensions.Length];
            RecursiveCallback(dimensions, args, 0, callback);
        }

        private static void RecursiveCallback(IReadOnlyList<int> dimensions, int[] array, int index,
            Action<int[]> callback)
        {
            for (int i = 0; i < dimensions[index]; i++)
            {
                array[index] = i;

                if (index < array.Length - 1)
                    RecursiveCallback(dimensions, array, index + 1, callback);
                else
                    callback.Invoke(array);
            }
        }

        public static void RepeatTimesIndexed(int t0, int t1, int t2, Action<int, int, int> callback)
        {
            for (int _t0 = 0; _t0 < t0; _t0++)
            for (int _t1 = 0; _t1 < t1; _t1++)
            for (int _t2 = 0; _t2 < t2; _t2++)
                callback.Invoke(_t0, _t1, _t2);
        }

        public static void ClearConsole()
        {
#if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method!.Invoke(new object(), null);
#endif
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static void Shuffle<T>(this IList<T> list, MyRandom r)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.NextInt(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T[] ToShuffledArray<T>(this IList<T> originList)
        {
            var list = originList.ToArray();
            int n = list.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }

        public static int[] ToShuffledIndexesArray<T>(this IList<T> originList)
        {
            int index = -1;
            var array = new int[originList.Count];
            for (int t0 = 0; t0 < array.Length; t0++)
                array[t0] = ++index;

            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            return array;
        }

        public static void WriteShuffledIndexes(IList<int> array)
        {
            int index = -1;
            for (int t0 = 0; t0 < array.Count; t0++)
                array[t0] = ++index;

            int n = array.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        public static int ExtractLayerMask(LayerMask layer)
        {
            int layerMask = 0;

            for (int i = 0; i < 32; i++)
                if (((1 << i) & layer.value) != 0)
                    layerMask |= Physics2D.GetLayerCollisionMask(i);

            return layerMask;
        }

        public static IEnumerable<Type> GetAssignableTypes<T>(bool findGenerics = false)
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsAbstract && type.IsGenericType == findGenerics);
        }
#if UNITY_EDITOR
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            var type = original.GetType();
            var dst = destination.AddComponent(type) as T;

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite ||
                    prop.GetCustomAttribute<ObsoleteAttribute>() is not null ||
                    prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }

            return dst;
        }
#endif
    }
}
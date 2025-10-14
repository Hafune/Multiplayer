namespace Core.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class DynamicUtility
    {
        public static object GetValueByPath(object obj, string[] path)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (path == null || path.Length == 0) throw new ArgumentException("Path must be non-empty", nameof(path));

            var current = obj;

            for (var i = 0; i < path.Length; i++)
            {
                if (current == null)
                    throw new NullReferenceException($"Null encountered before '{path[i]}'");

                var type = current.GetType();
                var member = (MemberInfo)GetAnyProperty(type, path[i]) ?? GetAnyField(type, path[i]);
                if (member == null)
                    throw new MissingMemberException(type.Name, path[i]);

                current = GetMemberValue(current, member);
            }

            return current;
        }

        public static void SetValueByPath(object obj, object value, string[] path)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (path == null || path.Length == 0) throw new ArgumentException("Path must be non-empty", nameof(path));

            var containers = new List<object>(path.Length);
            var members = new List<MemberInfo>(path.Length);

            var current = obj;

            for (var i = 0; i < path.Length; i++)
            {
                if (current == null)
                    throw new NullReferenceException($"Null encountered before '{path[i]}'");

                var type = current.GetType();
                var member = (MemberInfo)GetAnyProperty(type, path[i]) ?? GetAnyField(type, path[i]);
                if (member == null)
                    throw new MissingMemberException(type.Name, path[i]);

                containers.Add(current);
                members.Add(member);

                if (i < path.Length - 1)
                    current = GetMemberValue(current, member);
            }

            var lastIndex = path.Length - 1;

            var boxed = containers[lastIndex];
            SetMemberValue(boxed, members[lastIndex], value);
            var updated = boxed;

            for (var i = lastIndex - 1; i >= 0; i--)
            {
                var parentBoxed = containers[i];
                SetMemberValue(parentBoxed, members[i], updated);
                updated = parentBoxed;
            }
        }

        private static PropertyInfo GetAnyProperty(Type type, string name)
        {
            for (var t = type; t != null; t = t.BaseType)
            {
                var p = t.GetProperty(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (p != null) return p;
            }

            return null;
        }

        private static FieldInfo GetAnyField(Type type, string name)
        {
            for (var t = type; t != null; t = t.BaseType)
            {
                var f = t.GetField(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (f != null) return f;
            }

            return null;
        }

        private static object GetMemberValue(object instance, MemberInfo member)
        {
            if (member is PropertyInfo p)
            {
                var getter = p.GetGetMethod(true);
                if (getter == null) throw new InvalidOperationException($"Property '{p.Name}' has no getter");
                return getter.Invoke(instance, null);
            }

            if (member is FieldInfo f)
                return f.GetValue(instance);

            throw new ArgumentException("Unsupported member type", nameof(member));
        }

        private static void SetMemberValue(object instance, MemberInfo member, object value)
        {
            var targetType = GetMemberType(member);

            if (value == null && targetType.IsValueType && !IsNullableValueType(targetType))
                throw new ArgumentException($"Cannot assign null to non-nullable value type '{targetType.Name}'");

            if (value != null && !targetType.IsAssignableFrom(value.GetType()))
                throw new ArgumentException($"Value of type '{value.GetType().Name}' is not assignable to '{targetType.Name}'");

            if (member is PropertyInfo p)
            {
                var setter = p.GetSetMethod(true);
                if (setter == null) throw new InvalidOperationException($"Property '{p.Name}' has no setter");
                setter.Invoke(instance, new[] { value });
                return;
            }

            if (member is FieldInfo f)
            {
                f.SetValue(instance, value);
                return;
            }

            throw new ArgumentException("Unsupported member type", nameof(member));
        }

        private static Type GetMemberType(MemberInfo member)
        {
            if (member is PropertyInfo p) return p.PropertyType;
            if (member is FieldInfo f) return f.FieldType;
            throw new ArgumentException("Unsupported member type", nameof(member));
        }

        private static bool IsNullableValueType(Type t) =>
            t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}
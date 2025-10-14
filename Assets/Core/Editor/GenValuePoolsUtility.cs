using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Generated;

namespace Core.Editor
{
    public static class GenValuePoolsUtility
    {
        private const string TargetName = nameof(ValuePoolsUtility);

        private static string Template = $@"//Файл генерируется в {nameof(GenValuePoolsUtility)}
using System;
using System.Runtime.CompilerServices;
using Core.Components;
using Lib;

namespace Core.Generated
{{    
    // @formatter:off
    public static class {TargetName}
    {{
        public static void Sum({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, float byValue)
        {{
            switch (@enum)
            {{
                #INCREMENT_BY_FLOAT#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}
        
        public static void Sum({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, {GenValueEnum.TargetName} byValue)
        {{
            switch (@enum)
            {{
                #INCREMENT_BY_VALUE#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}
        
        public static float GetValue({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum) => @enum switch
        {{
            #GET#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};

        public static void SetValue({GenComponentPools.ComponentPoolsName} pools, int entity, {GenValueEnum.TargetName} @enum, float value)
        {{
            switch (@enum)
            {{
                #SET#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}

        public static void CallGenericPool<T>(T poolContext, {GenComponentPools.ComponentPoolsName} pools, {GenValueEnum.TargetName} @enum) where T : IValueGenericPoolContext
        {{
            switch(@enum)
            {{
                #CALL_GENERIC_POOL#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}

        public static void CallGenericValue<T>(ref T valueContext, {GenValueEnum.TargetName} @enum) where T : IValueContext
        {{
            switch(@enum)
            {{
                #CALL_GENERIC_VALUE#
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }}
        }}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static {GenValueEnum.TargetName} GetEnumByType<T>() where T : struct, IValue => 
            ValueEnumCache<T>.Value;
    }}
    
    public static class ValueEnumCache<T> where T : struct, IValue
    {{
        public static readonly {GenValueEnum.TargetName} Value = GetValueEnum();
        
        private static {GenValueEnum.TargetName} GetValueEnum()
        {{
            #VALUE_ENUM_CACHE_LOGIC#
            
            throw new ArgumentException($""Unknown value type: {{typeof(T)}}"");
        }}
    }}
}}";

        internal static bool Gen()
        {
            var names = ComponentsUtility.GetTotalValueComponentNames();
            var eve = typeof(EventValueUpdated<>).Name;
            eve = eve.Remove(eve.IndexOf('`'));

            string R(string value) => ComponentsUtility.ToFieldName(value);

            var byFloat = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value += byValue; pools.{R(eve + name)}.AddIfNotExist(entity); break;");

            var byValue = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value += GetValue(pools, entity, byValue); pools.{R(eve + name)}.AddIfNotExist(entity); break;");

            var get = names.Select(name =>
                $"{GenValueEnum.TargetName}.{R(name)} => pools.{R(name)}.Get(entity).value,");

            var set = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: pools.{R(name)}.Get(entity).value = value; pools.{R(eve + name)}.AddIfNotExist(entity); break;");

            var callGenericPool = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: poolContext.GenericRun(pools.{R(name)}); break;");

            var callGenericValue = names.Select(name =>
                $"case {GenValueEnum.TargetName}.{R(name)}: valueContext.GenericValueRun<{name}>(); break;");

            var valueEnumCacheLogic = names.Select(name =>
                $"if (typeof(T) == typeof({name})) return {GenValueEnum.TargetName}.{R(name)};");

            return Write(
                byFloat, 
                byValue,
                get,  
                set,
                callGenericPool,
                callGenericValue,
                valueEnumCacheLogic
                );
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> byFloat,
            IEnumerable<string> byValue,
            IEnumerable<string> get,
            IEnumerable<string> set,
            IEnumerable<string> callGenericPool,
            IEnumerable<string> callGenericValue,
            IEnumerable<string> valueEnumCacheLogic)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(Template, "#INCREMENT_BY_FLOAT#", byFloat);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#INCREMENT_BY_VALUE#", byValue);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#GET#", get);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#SET#", set);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CALL_GENERIC_POOL#", callGenericPool);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#CALL_GENERIC_VALUE#", callGenericValue);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#VALUE_ENUM_CACHE_LOGIC#", valueEnumCacheLogic);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
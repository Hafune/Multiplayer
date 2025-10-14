using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Lib;

namespace Core.Editor
{
    public static class GenActionValueLinks
    {
        private const string _TargetName = nameof(_ActionValueLinks);

        private static string _Template = $@"//Файл генерируется в {nameof(GenActionValueLinks)}
using Core.Components;

namespace Core.Editor
{{
    public class {_TargetName}
    {{
        private readonly string[] links =
        {{
            #FIELDS#
        }};
    }}
}}";

        internal static bool Gen()
        {
            //Сопоставление интерфейса к генерируемому типу
            var generatedLinks = new[]
            {
                GenGeneric<IGenCooldownValue>(typeof(CooldownValueComponent<>)),
                GenGeneric<IGenCostValue>(typeof(CostValueComponent<>)),
                GenGeneric<IGenValue>(typeof(ChargeValueComponent<>)),
                GenGeneric<IGenValue>(typeof(ChargeMaxValueComponent<>)),
                GenGeneric<IGenValue>(typeof(ChargeCostValueComponent<>)),
                GenGeneric<IGenValue>(typeof(ResourceRecoveryPerUsingValueComponent<>)),
                GenGeneric<IGenValue>(typeof(ResourceRecoveryPerHitValueComponent<>)),
            };
            
            return WriteLinks(generatedLinks.SelectMany(links => links));
        }

#pragma warning disable CS0693
        private static IEnumerable<string> GenGeneric<I>(Type V)
#pragma warning restore CS0693
        {
            var names = MyFunctions.GetAssignableTypes<I>()
                .Select(i => i.Name)
                .OrderBy(i => i);

            var valueName = V.Name.Replace("`1", "");
            var links = names.Select(name => $"nameof({valueName}<{name}>),");

            return links;
        }

        public static void RemoveGen()
        {
            WriteLinks(
                new[] { "//" }
            );
        }

        private static bool WriteLinks(IEnumerable<string> links)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(_Template, "#FIELDS#", links);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakeEditorPath(_TargetName));
        }
        
        private static IEnumerable<string> GenGeneric()
        {
            var valueTypes = MyFunctions.GetAssignableTypes<IGenValue>().OrderBy(t => t.Name);

            var componentTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsValueType && t.IsGenericType)
                .Where(t =>
                {
                    var genericArgs = t.GetGenericArguments();
                    return genericArgs.Length == 1 && 
                           genericArgs[0].GetGenericParameterConstraints()
                               .Any(c => typeof(IGenValue).IsAssignableFrom(c));
                })
                .ToList();

            foreach (var valueType in valueTypes)
            {
                var matchingComponent = componentTypes.FirstOrDefault(ct =>
                    ct.GetGenericArguments()[0].GetGenericParameterConstraints()
                        .Any(constraint => valueType.GetInterfaces().Contains(constraint))); // Проверяем, относится ли constraint к текущему valueType

                if (matchingComponent != null)
                {
                    var valueName = matchingComponent.Name.Replace("`1", "");
                    yield return $"nameof({valueName}<{valueType.Name}>),";
                }
            }
        }
    }
}
//Универсальная версия, определявшая типы исходя из дженерик аргумента
//пришлось от неё отказаться т.к. аграничение дженерик типа мешало получать пул на основе независимого дженерика
// public struct CooldownValueComponent<T> : IValue, ICooldownComponent
//     where T : IGenCooldownValue <---
/*
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Lib;

namespace Core.Editor
{
    public static class GenActionValueLinks
    {
        private const string _TargetName = nameof(_ActionValueLinks);

        private static string _Template = $@"//Файл генерируется в {nameof(GenActionValueLinks)}
using Core.Components;

namespace Core.Editor
{{
    public class {_TargetName}
    {{
        private readonly string[] links =
        {{
            #FIELDS#
        }};
    }}
}}";

        internal static bool Gen() => WriteLinks(GenGeneric());

        private static IEnumerable<string> GenGeneric()
        {
            var valueTypes = MyFunctions.GetAssignableTypes<IGenValue>().OrderBy(t => t.Name);

            var componentTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsValueType && t.IsGenericType)
                .Where(t =>
                {
                    var genericArgs = t.GetGenericArguments();
                    return genericArgs.Length == 1 &&
                           genericArgs[0].GetGenericParameterConstraints()
                               .Any(c => typeof(IGenValue).IsAssignableFrom(c));
                })
                .OrderBy(t => t.Name)
                .ToList();

            foreach (var componentType in componentTypes)
            foreach (var valueType in valueTypes)
            {
                var matchingComponent = componentType.GetGenericArguments()[0].GetGenericParameterConstraints()
                    .Any(constraint =>
                        valueType.GetInterfaces().Contains(constraint)); // Проверяем, относится ли constraint к текущему valueType

                if (!matchingComponent)
                    continue;

                var valueName = componentType.Name.Replace("`1", "");
                yield return $"nameof({valueName}<{valueType.Name}>),";
            }
        }

        public static void RemoveGen()
        {
            WriteLinks(
                new[] { "//" }
            );
        }

        private static bool WriteLinks(IEnumerable<string> links)
        {
            var content = TemplateUtility.ReplaceTagWithIndentedMultiline(_Template, "#FIELDS#", links);
            
            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakeEditorPath(_TargetName));
        }
    }
}
*/
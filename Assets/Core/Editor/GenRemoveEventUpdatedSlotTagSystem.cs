using System.Linq;
using Core.Components;
using Lib;

namespace Core.Editor
{
    public static class GenRemoveEventUpdatedSlotTagSystem
    {
        private const string TargetName = "RemoveEventUpdatedSlotTagSystem";

        private static string Template = $@"//Файл генерируется в {nameof(GenRemoveEventUpdatedSlotTagSystem)}
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{{
    // @formatter:off
    public class {TargetName} : IEcsRunSystem
    {{
        #FILTERS#
                
        #POOLS#

        public void Run(IEcsSystems systems)
        {{
            #ITERATORS#
        }}
    }}
}}";

        internal static bool Gen()
        {
            var totalNames = ComponentsUtility.GetSlotTagNames();

            string R(string value) => ComponentsUtility.ToFieldName(value);
            var eventName = typeof(EventValueUpdated<>).GetNameWithoutGeneric();

            var filters = totalNames.Select(name =>
                $"private readonly EcsFilterInject<Inc<{eventName}<{name}>>> _eventUpdated{R(name)}Filter;"
            );

            var pools = totalNames.Select(name =>
                $"private readonly EcsPoolInject<{eventName}<{name}>> _eventUpdated{R(name)}Pool;"
            );

            var iterators = totalNames.Select(name =>
                $"foreach (var i in _eventUpdated{R(name)}Filter.Value) _eventUpdated{R(name)}Pool.Value.Del(i);"
            );

            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FILTERS#", filters);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", pools);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", iterators);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }

        public static void RemoveGen()
        {
            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#FILTERS#", new[] { "//" });
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#POOLS#", new[] { "//" });
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#ITERATORS#", new[] { "//" });

            TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
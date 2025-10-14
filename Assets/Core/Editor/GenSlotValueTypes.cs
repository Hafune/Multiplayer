using System.Collections.Generic;
using System.Linq;
using Core.Generated;

namespace Core.Editor
{
    public static class GenSlotValueTypes
    {
        private const string TargetName = "SlotValueTypes";

        private static string Template = $@"//Файл генерируется в {nameof(GenSlotValueTypes)}
using System;
using Leopotam.EcsLite;

namespace Core.Generated
{{
    public static class {TargetName}
    {{
        // @formatter:off
        public static IEcsPool GetSlotTagPool({GenSlotTagEnum.TargetName} @enum, ComponentPools pools) => @enum switch
        {{
            #TAGS#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};

        public static IEcsPool GetEventUpdatedSlotTagPool({nameof(SlotTagEnum)} @enum, ComponentPools pools) => @enum switch
        {{
            #UPDATED_TAGS#
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        }};
        // @formatter:on
    }}
}}
";

        internal static bool Gen()
        {
            string R(string value) => ComponentsUtility.ToFieldName(value);

            var tagNames = ComponentsUtility.GetSlotTagNames();

            var tags = tagNames.Select(name => $"{GenSlotTagEnum.TargetName}.{R(name)} => pools.{name},")
                .OrderBy(i => i);

            var updatedTags = tagNames.Select(name => $"{GenSlotTagEnum.TargetName}.{R(name)} => pools.EventUpdated{name},")
                .OrderBy(i => i);

            return Write(
                tags,
                updatedTags
            );
        }

        public static void RemoveGen() => Write(
            new[] { "//" },
            new[] { "//" }
        );

        private static bool Write(
            IEnumerable<string> tags,
            IEnumerable<string> updatedTags
        )
        {
            string content = Template;
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#TAGS#", tags);
            content = TemplateUtility.ReplaceTagWithIndentedMultiline(content, "#UPDATED_TAGS#", updatedTags);

            return TemplateUtility.WriteScriptIfDifferent(content, GenLauncher.MakePath(TargetName));
        }
    }
}
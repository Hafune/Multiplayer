using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Core.Editor
{
    internal static class TemplateUtility
    {
        public static string ReplaceTagWithIndentedMultiline(string templateContent, string tag,
            IEnumerable<string> newLines)
        {
            var tagWithIndentRegex = $"\\ *{tag}";
            var match = Regex.Match(templateContent, tagWithIndentRegex);
            var indent = match.Value.Substring(0, match.Value.Length - tag.Length);
            var replacement = string.Join(Environment.NewLine + indent, newLines);
            return Regex.Replace(templateContent, tag, replacement);
        }

        public static string ReadFile(string path) => File.ReadAllText(Application.dataPath + path);

        public static bool WriteScriptIfDifferent(string content, string assetFilePath)
        {
            var fullPath = Application.dataPath + "/" + assetFilePath;
            var directoryPath = Path.GetDirectoryName(fullPath)!;
            
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            
            if (File.Exists(fullPath) && File.ReadAllText(fullPath) == content)
                return false;

            File.WriteAllText(fullPath, content);
            return true;
        }
    }
}
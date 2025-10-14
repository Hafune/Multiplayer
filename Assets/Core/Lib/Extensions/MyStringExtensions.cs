using UnityEditor;

namespace Lib
{
    public static class MyStringExtensions
    {
        public static string FormatAddCharBeforeCapitalLetters(this string value, char separator = ' ')
        {
#if UNITY_EDITOR
            return System.Text.RegularExpressions.Regex.Replace(ObjectNames.NicifyVariableName(value), " ", $"{separator}");
#endif
            return value;
        }

        public static string FormatReplaceSpaces(this string value) =>
            System.Text.RegularExpressions.Regex.Replace(value, " ", $"_");
    }
}
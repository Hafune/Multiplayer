using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Core.Editor
{
    [InitializeOnLoad]
    public class GenLauncher
    {
        private const string DIR = "Core/_GeneratedFiles";
        private const string EDITOR_DIR = "Core/Editor/GeneratedLinks";
        public static string MakePath(string name) => $"{DIR}/{name}.cs";
        public static string MakeEditorPath(string name) => $"{EDITOR_DIR}/{name}.cs";
        
        static GenLauncher()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
                return;

            var stopWatch = Stopwatch.StartNew();
            int errorRepeatCount = 4;
            int repeatCount = 0;

            while (repeatCount++ < errorRepeatCount &&
                   GenActionValueLinks.Gen() | //Генерация значений безнеслогики, может меняться от игры к игре
                   GenValueEnum.Gen() |
                   // GenEnumValues.Gen() | //слишком долго только ручной запуск
                   GenSlotTagEnum.Gen() |
                   GenComponentPools.Gen() |
                   GenValuePoolsUtility.Gen() |
                   GenSlotValueTypes.Gen() |
                   GenInitValuesFromBaseValuesSystem.Gen() |
                   GenEventStartRecalculateValueSystem.Gen() |
                   GenRemoveEventUpdatedSlotTagSystem.Gen() |
                   GenValueScaleSystemsUtility.Gen())
            {
                AssetDatabase.Refresh();
            }

            stopWatch.Stop();
            Debug.Log(
                $"CodeGenLauncher StopWatch.Elapsed: {FormatFloatToStringUtility.FloatDim2((float)stopWatch.Elapsed.TotalSeconds)} Seconds, repeats {repeatCount}/{errorRepeatCount}");

            if (repeatCount == errorRepeatCount)
                Debug.LogWarning($"Слишком много ({errorRepeatCount}) проходов генерации кода !!!");
        }

        [MenuItem("Auto/Gen/Remove Generated Values Logic")]
        private static void RemoveGeneratedCode()
        {
            GenActionValueLinks.RemoveGen();
            GenValuePoolsUtility.RemoveGen();
            GenSlotValueTypes.RemoveGen();
            GenInitValuesFromBaseValuesSystem.RemoveGen();
            GenEventStartRecalculateValueSystem.RemoveGen();
            GenRemoveEventUpdatedSlotTagSystem.RemoveGen();
            GenValueScaleSystemsUtility.RemoveGen();
        }
    }
}
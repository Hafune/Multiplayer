using System.IO;
using BansheeGz.BGDatabase.Editor;
using Core.Lib;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class BGMigration
    {
        // [MenuItem("Auto/Test/BGMigration")]
        // public static void Migrate()
        // {
        //     var itemValuesPath = "Assets/Resources/ItemValues";
        //     if (!Directory.Exists(itemValuesPath))
        //         Directory.CreateDirectory(itemValuesPath);
        //
        //     RangeTemplates.ForEachEntity(entity =>
        //     {
        //         var assetName = entity.name;
        //         var assetPath = $"{itemValuesPath}/{assetName}.asset";
        //         
        //         var asset = AssetDatabase.LoadAssetAtPath<ItemMinMaxValues>(assetPath);
        //         if (asset == null)
        //         {
        //             asset = ScriptableObject.CreateInstance<ItemMinMaxValues>();
        //             AssetDatabase.CreateAsset(asset, assetPath);
        //         }
        //
        //         var ranges = entity.Ranges;
        //         if (ranges.Count > 0)
        //         {
        //             var minKeys = new Keyframe[ranges.Count];
        //             var maxKeys = new Keyframe[ranges.Count];
        //             
        //             for (int i = 0; i < ranges.Count; i++)
        //             {
        //                 var range = ranges[i];
        //                 var level = (float)range.Level;
        //                 var minValue = range.Range.x;
        //                 var maxValue = range.Range.y;
        //                 
        //                 minKeys[i] = new Keyframe(level, minValue);
        //                 maxKeys[i] = new Keyframe(level, maxValue);
        //             }
        //             
        //             asset.Min.keys = minKeys;
        //             asset.Max.keys = maxKeys;
        //             
        //             for (int i = 0; i < asset.Min.length; i++)
        //             {
        //                 AnimationUtility.SetKeyLeftTangentMode(asset.Min, i, AnimationUtility.TangentMode.Linear);
        //                 AnimationUtility.SetKeyRightTangentMode(asset.Min, i, AnimationUtility.TangentMode.Linear);
        //                 AnimationUtility.SetKeyLeftTangentMode(asset.Max, i, AnimationUtility.TangentMode.Linear);
        //                 AnimationUtility.SetKeyRightTangentMode(asset.Max, i, AnimationUtility.TangentMode.Linear);
        //             }
        //         }
        //
        //         EditorUtility.SetDirty(asset);
        //     });
        //     
        //     AssetDatabase.SaveAssets();
        //     AssetDatabase.Refresh();
        // }
        //
        // [MenuItem("Auto/Test/AssignAssets")]
        // public static void AssignAssets()
        // {
        //     RangeTemplates.ForEachEntity(entity =>
        //     {
        //         var assetName = entity.name;
        //         RangeTemplates._Ranges1.SetAssetPath(entity.Index, $"ItemValues\\{assetName}");
        //     });
        //     
        //     BGRepoSaver.SaveAndMarkAsSaved();
        // }
    }
}
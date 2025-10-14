// using System.IO;
// using System.Linq;
// using System.Text;
// using UnityEditor;
// using UnityEditor.Localization;
// using UnityEngine;
// using UnityEngine.Localization.Settings;
// using UnityEngine.Localization.Tables;
//
// public class LocalizationMigrator
// {
//     [MenuItem("Tools/Migrate Localization To L2")]
//     public static void MigrateToL2()
//     {
//         var stringTableCollections = LocalizationEditorSettings.GetStringTableCollections();
//
//         foreach (var collection in stringTableCollections)
//         {
//             var output = new StringBuilder();
//             var tableName = collection.TableCollectionName;
//             var locales = LocalizationEditorSettings.GetLocales();
//
//             // Заголовок CSV
//             output.Append("Key");
//             foreach (var locale in locales)
//                 output.Append($",{locale.Identifier.Code}");
//             output.AppendLine();
//
//             // Получаем все ключи
//             var allKeys = collection.SharedData.Entries.Select(e => e.Key).Distinct();
//
//             foreach (var key in allKeys)
//             {
//                 output.Append(key);
//                 foreach (var locale in locales)
//                 {
//                     var table = collection.GetTable(locale.Identifier) as StringTable;
//                     var entry = table?.GetEntry(key);
//                     output.Append(",");
//                     output.Append(entry != null ? entry.LocalizedValue.Replace("\n", "\\n").Replace(",", "\\,") : "");
//                 }
//                 output.AppendLine();
//             }
//
//             // Сохраняем CSV
//             var folderPath = "Assets/L2LocalizationExport/";
//             Directory.CreateDirectory(folderPath);
//             var path = Path.Combine(folderPath, $"{tableName}.csv");
//             File.WriteAllText(path, output.ToString(), Encoding.UTF8);
//
//             Debug.Log($"Localization exported to: {path}");
//         }
//
//         AssetDatabase.Refresh();
//     }
// }
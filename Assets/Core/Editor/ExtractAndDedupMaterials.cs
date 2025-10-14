#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.EditorTools
{
    public static class ExtractAndDedupMaterials
    {
        private static Dictionary<string, Material> LoadMaterialsFromFolder(string matFolder)
        {
            var existingByName = new Dictionary<string, Material>();
            var matGuids = AssetDatabase.FindAssets("t:Material", new[] { matFolder });
            foreach (var g in matGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
                if (mat != null)
                {
                    existingByName[mat.name] = mat;
                }
            }
            return existingByName;
        }

        [MenuItem("Auto/Materials/Extract And Deduplicate In Folder...")]
        public static void Run()
        {
            var abs = EditorUtility.OpenFolderPanel("Select folder with FBX models", Application.dataPath, string.Empty);
            if (string.IsNullOrEmpty(abs)) return;
            if (!abs.Replace("\\", "/").StartsWith(Application.dataPath.Replace("\\", "/")))
            {
                EditorUtility.DisplayDialog("Error", "Folder must be inside the project Assets folder.", "Ok");
                return;
            }

            var rel = "Assets" + abs.Replace("\\", "/").Substring(Application.dataPath.Replace("\\", "/").Length);
            var matFolder = rel + "/Materials/";

            if (!AssetDatabase.IsValidFolder(matFolder))
                AssetDatabase.CreateFolder(rel, "Materials");

            var existingByName = LoadMaterialsFromFolder(matFolder);

            // First pass: Create all missing materials
            var modelGuids = AssetDatabase.FindAssets("t:Model", new[] { rel });
            var materialsCreated = false;

            foreach (var g in modelGuids)
            {
                var modelPath = AssetDatabase.GUIDToAssetPath(g);
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(modelPath);

                foreach (var obj in subAssets)
                {
                    var embeddedMat = obj as Material;
                    if (embeddedMat == null) continue;

                    if (!existingByName.ContainsKey(embeddedMat.name))
                    {
                        var destPath = matFolder + embeddedMat.name + ".mat";

                        var newMat = Object.Instantiate(embeddedMat);
                        newMat.name = embeddedMat.name;
                        AssetDatabase.CreateAsset(newMat, destPath);
                        materialsCreated = true;

                        existingByName[embeddedMat.name] = newMat;
                    }
                }
            }

            // Save and refresh if any materials were created
            if (materialsCreated)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            EditorApplication.delayCall += () =>
            {
                var materialsByName = LoadMaterialsFromFolder(matFolder);

                // Second pass: Remap materials and reimport models
                foreach (var g in modelGuids)
                {
                    var modelPath = AssetDatabase.GUIDToAssetPath(g);
                    var importer = AssetImporter.GetAtPath(modelPath) as ModelImporter;
                    if (importer == null) continue;

                    importer.materialImportMode = ModelImporterMaterialImportMode.ImportViaMaterialDescription;

                    var subAssets = AssetDatabase.LoadAllAssetsAtPath(modelPath);
                    foreach (var obj in subAssets)
                    {
                        var embeddedMat = obj as Material;
                        if (embeddedMat == null) continue;

                        if (materialsByName.TryGetValue(embeddedMat.name, out var targetMat) && targetMat != null)
                        {
                            var id = new AssetImporter.SourceAssetIdentifier(typeof(Material), embeddedMat.name);
                            importer.AddRemap(id, targetMat);
                        }
                    }

                    AssetDatabase.ImportAsset(modelPath, ImportAssetOptions.ForceUpdate);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            };
        }
    }
}
#endif
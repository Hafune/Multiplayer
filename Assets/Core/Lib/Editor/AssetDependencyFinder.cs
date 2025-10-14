using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Core.Lib.Editor
{
    public class AssetDependencyFinder : EditorWindow
    {
        // Типы ассетов
        private enum AssetType
        {
            Animation,
            AudioClip,
            AudioMixer,
            ComputeShader,
            Font,
            GUISkin,
            Material,
            Mesh,
            Model,
            PhysicMaterial,
            Prefab,
            Scene,
            Script,
            Shader,
            Sprite,
            Texture,
            VideoClip,
            VisualEffectAsset
        }

        private List<Object> foundObjects = new();
        private Vector2 scrollPosition;
        private AssetType selectedAssetType = AssetType.Texture;
        private bool excludePackageAssets = true;
        private bool excludeBuiltInAssets = true;
        private bool liteSearch;
        private bool[] objectsToSelect;

        [MenuItem("Auto/Finder Asset Dependencies")]
        public static void ShowWindow()
        {
            GetWindow<AssetDependencyFinder>("Поиск зависимостей");
        }

        private void OnGUI()
        {
            GUILayout.Label("Поиск зависимостей в ассетах Unity", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            selectedAssetType = (AssetType)EditorGUILayout.EnumPopup("Тип ассета для поиска:", selectedAssetType);

            excludePackageAssets = EditorGUILayout.Toggle("Исключить ассеты пакетов", excludePackageAssets);
            excludeBuiltInAssets = EditorGUILayout.Toggle("Исключить встроенные ассеты", excludeBuiltInAssets);
            liteSearch = EditorGUILayout.Toggle("Не глубокий поиск", liteSearch);

            EditorGUILayout.Space();

            if (GUILayout.Button("Найти зависимости"))
            {
                FindDependencies();
            }

            EditorGUILayout.Space();

            if (foundObjects != null && foundObjects.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Найдено объектов типа {selectedAssetType}: {foundObjects.Count}");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Выделить все найденные объекты"))
                {
                    Selection.objects = foundObjects.ToArray();
                }

                if (GUILayout.Button("Выбрать только отмеченные"))
                {
                    SelectOnlyCheckedItems();
                }
                EditorGUILayout.EndHorizontal();

                if (objectsToSelect == null || objectsToSelect.Length != foundObjects.Count)
                {
                    objectsToSelect = new bool[foundObjects.Count];
                }

                EditorGUILayout.Space();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                for (int i = 0; i < foundObjects.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.ObjectField(foundObjects[i], typeof(Object), false);
                    objectsToSelect[i] = EditorGUILayout.Toggle(objectsToSelect[i], GUILayout.Width(20));

                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        foundObjects.RemoveAt(i);
                        var newArray = new bool[objectsToSelect.Length - 1];
                        Array.Copy(objectsToSelect, 0, newArray, 0, i);
                        if (i < objectsToSelect.Length - 1)
                            Array.Copy(objectsToSelect, i + 1, newArray, i, objectsToSelect.Length - i - 1);
                        objectsToSelect = newArray;
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
            }
        }

        private void SelectOnlyCheckedItems()
        {
            if (objectsToSelect == null || foundObjects == null || objectsToSelect.Length != foundObjects.Count)
                return;

            var newList = new List<Object>();

            for (int i = 0; i < foundObjects.Count; i++)
            {
                if (objectsToSelect[i])
                {
                    newList.Add(foundObjects[i]);
                }
            }

            Selection.objects = newList.ToArray();
        }

        private void FindDependencies()
        {
            foundObjects.Clear();
            var uniqueObjects = new HashSet<Object>();
            var processedPaths = new HashSet<string>();

            var sourceObjects = Selection.objects.Concat(GetFolderAssets(Selection.objects)).ToArray();

            if (sourceObjects == null || sourceObjects.Length == 0)
            {
                EditorUtility.DisplayDialog("Ошибка", "Выберите хотя бы один исходный объект!", "OK");
                return;
            }

            try
            {
                int processedCount = 0;

                foreach (var sourceObj in sourceObjects)
                {
                    if (sourceObj == null)
                        continue;

                    EditorUtility.DisplayProgressBar("Поиск зависимостей",
                        $"Анализ объекта {sourceObj.name}...",
                        (float)processedCount / sourceObjects.Length);

                    var assetPath = AssetDatabase.GetAssetPath(sourceObj);
                    if (!string.IsNullOrEmpty(assetPath))
                    {
                        ProcessAssetPath(assetPath, uniqueObjects, processedPaths);
                    }

                    processedCount++;
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            if (selectedAssetType is AssetType.Texture)
            {
                foundObjects = uniqueObjects.OrderByDescending(a => ((Texture)a).width).ToList();
            }
            else
            {
                foundObjects = uniqueObjects.ToList();
            }

            objectsToSelect = new bool[foundObjects.Count];

            if (foundObjects.Count == 0)
            {
                EditorUtility.DisplayDialog("Результат", $"Объекты типа {selectedAssetType} не найдены!", "OK");
            }
            else
            {
                Debug.Log($"Найдено объектов типа {selectedAssetType}: {foundObjects.Count}");
            }
        }

        private Object[] GetFolderAssets(Object[] possibleFolders)
        {
            var assets = new List<Object>();

            foreach (var folder in possibleFolders)
            {
                assets.AddRange(GetFolderAssets(folder));
            }

            return assets.ToArray();
        }

        private IEnumerable<Object> GetFolderAssets(Object folder)
        {
            var folderPath = AssetDatabase.GetAssetPath(folder);
            if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
            {
                return Array.Empty<Object>();
            }

            var assetGuids = AssetDatabase.FindAssets("", new[] { folderPath });
            var assets = new List<Object>();

            foreach (var guid in assetGuids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            return assets;
        }

        private void ProcessAssetPath(string assetPath, HashSet<Object> objects, HashSet<string> processedPaths)
        {
            if (processedPaths.Contains(assetPath))
                return;

            processedPaths.Add(assetPath);
            
            if (IsAssetOfSelectedType(assetPath))
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset != null)
                {
                    objects.Add(asset);
                }
            }

            // Проверяем sub-assets для Mesh'ей
            if (selectedAssetType == AssetType.Mesh)
            {
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                foreach (var subAsset in subAssets)
                {
                    if (subAsset is Mesh mesh && mesh != null)
                    {
                        objects.Add(mesh);
                    }
                }
            }

            var dependencies = liteSearch ? Array.Empty<string>(): AssetDatabase.GetDependencies(assetPath, true);

            foreach (var dependencyPath in dependencies)
            {
                if (processedPaths.Contains(dependencyPath))
                    continue;

                processedPaths.Add(dependencyPath);

                if (ShouldExcludeAsset(dependencyPath))
                    continue;

                if (IsAssetOfSelectedType(dependencyPath))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(dependencyPath);
                    if (asset != null)
                    {
                        objects.Add(asset);
                    }
                }

                // Проверяем sub-assets для Mesh'ей в зависимостях
                if (selectedAssetType == AssetType.Mesh)
                {
                    var subAssets = AssetDatabase.LoadAllAssetsAtPath(dependencyPath);
                    foreach (var subAsset in subAssets)
                    {
                        if (subAsset is Mesh mesh && mesh != null)
                        {
                            objects.Add(mesh);
                        }
                    }
                }
            }
        }

        private bool ShouldExcludeAsset(string path)
        {
            if (excludeBuiltInAssets && path.StartsWith("Library/"))
                return true;

            if (excludeBuiltInAssets && path.StartsWith("Resources/unity_builtin"))
                return true;

            if (excludeBuiltInAssets && path.Contains("unity default resources"))
                return true;

            if (excludePackageAssets && path.StartsWith("Packages/"))
                return true;

            if (excludePackageAssets && path.Contains("TextMesh Pro/"))
                return true;

            if (excludeBuiltInAssets && path.Contains("TextMeshPro") && path.Contains("Icon"))
                return true;

            return false;
        }

        private bool IsAssetOfSelectedType(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
            if (assetType == null)
                return false;

            switch (selectedAssetType)
            {
                case AssetType.Animation:
                    return assetType == typeof(AnimationClip);
                case AssetType.AudioClip:
                    return assetType == typeof(AudioClip);
                case AssetType.AudioMixer:
                    return assetType == typeof(UnityEngine.Audio.AudioMixer);
                case AssetType.ComputeShader:
                    return assetType == typeof(ComputeShader);
                case AssetType.Font:
                    return assetType == typeof(Font) || assetType.Name == "TMP_FontAsset";
                case AssetType.GUISkin:
                    return assetType == typeof(GUISkin);
                case AssetType.Material:
                    return assetType == typeof(Material);
                case AssetType.Mesh:
                    return assetType == typeof(Mesh) || 
                           path.EndsWith(".fbx") || path.EndsWith(".obj") || 
                           path.EndsWith(".3ds") || path.EndsWith(".dae") ||
                           path.EndsWith(".blend");
                case AssetType.Model:
                    return path.EndsWith(".fbx") || path.EndsWith(".obj") ||
                           path.EndsWith(".3ds") || path.EndsWith(".dae") ||
                           assetType == typeof(GameObject) && !path.EndsWith(".prefab");
                case AssetType.PhysicMaterial:
                    return assetType == typeof(PhysicsMaterial) || assetType == typeof(PhysicsMaterial2D);
                case AssetType.Prefab:
                    return assetType == typeof(GameObject) && path.EndsWith(".prefab");
                case AssetType.Scene:
                    return assetType == typeof(UnityEngine.SceneManagement.Scene) || path.EndsWith(".unity");
                case AssetType.Script:
                    return assetType == typeof(MonoScript) || path.EndsWith(".cs") ||
                           path.EndsWith(".js") || path.EndsWith(".dll");
                case AssetType.Shader:
                    return assetType == typeof(Shader) || path.EndsWith(".shader");
                case AssetType.Sprite:
                    return assetType == typeof(Sprite);
                case AssetType.Texture:
                    return assetType == typeof(Texture) || assetType == typeof(Texture2D) ||
                           assetType == typeof(Cubemap) || assetType == typeof(RenderTexture) ||
                           assetType == typeof(Texture2DArray) || assetType == typeof(Texture3D) ||
                           assetType == typeof(CubemapArray);
                case AssetType.VideoClip:
                    return assetType == typeof(UnityEngine.Video.VideoClip);
                case AssetType.VisualEffectAsset:
                    return assetType.Name == "VisualEffectAsset";
                default:
                    return false;
            }
        }
    }
}
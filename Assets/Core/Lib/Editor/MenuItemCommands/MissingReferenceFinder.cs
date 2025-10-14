using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class MissingReferenceFinder
    {
        private struct MissingReferenceInfo
        {
            public GameObject gameObject;
            public Component component;
            public string componentType;
            public string propertyPath;
        }

        [MenuItem("Auto/Validate/Find Missing References in Selection")]
        private static void FindMissingReferences()
        {
            if (Selection.gameObjects.Length == 0)
            {
                Debug.Log("No objects selected.");
                return;
            }

            var missingReferences = new List<MissingReferenceInfo>();

            foreach (var obj in Selection.gameObjects) 
                CheckHierarchyForMissingReferences(obj, missingReferences);

            if (missingReferences.Count == 0)
            {
                Debug.Log("No missing references found in selected objects.");
            }
            else
            {
                Debug.LogWarning($"Found {missingReferences.Count} missing references:");
                foreach (var info in missingReferences)
                {
                    Debug.LogWarning($"Missing reference in '{info.gameObject.name}' -> " +
                                   $"{info.componentType} -> " +
                                   $"{info.propertyPath}", info.gameObject);
                }
            }
        }

        private static void CheckHierarchyForMissingReferences(GameObject obj, List<MissingReferenceInfo> resultList)
        {
            FindMissingReferencesInObject(obj, resultList);

            foreach (Transform child in obj.transform)
                CheckHierarchyForMissingReferences(child.gameObject, resultList);
        }

        private static void FindMissingReferencesInObject(GameObject obj, List<MissingReferenceInfo> resultList)
        {
            var components = obj.GetComponents<Component>();
            
            for (int i = 0; i < components.Length; i++)
            {
                var component = components[i];
                
                if (component == null)
                {
                    resultList.Add(new MissingReferenceInfo
                    {
                        gameObject = obj,
                        component = null,
                        componentType = $"Missing Component at index {i}",
                        propertyPath = "Component itself is missing"
                    });
                    continue;
                }

                var so = new SerializedObject(component);
                var prop = so.GetIterator();

                while (prop.NextVisible(true))
                {
                    if (prop.propertyType == SerializedPropertyType.ObjectReference && 
                        prop.objectReferenceValue == null &&
                        prop.objectReferenceInstanceIDValue != 0)
                    {
                        resultList.Add(new MissingReferenceInfo
                        {
                            gameObject = obj,
                            component = component,
                            componentType = component.GetType().Name,
                            propertyPath = prop.propertyPath
                        });
                    }
                }
            }
        }
    }
}
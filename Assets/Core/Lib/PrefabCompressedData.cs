using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
#endif

namespace Core.Lib
{
    [ExecuteAlways]
    public class PrefabCompressedData : MonoBehaviour
    {
        [Serializable]
        public struct Data
        {
            [SerializeField] public float[] values;
            [SerializeField] public GameObject[] prefabs;
            [SerializeField] public int[] prefabUsageCounts;
            [SerializeField] public float[] simpleValues;
            [SerializeField] public GameObject[] simplePrefabs;
            [SerializeField] public int[] simplePrefabUsageCounts;
        }

        [SerializeField] private Data _data;
        [SerializeField] private HideFlags _flags;
        [SerializeField] private bool _instantiateOnAwake;
        [SerializeField] private Transform _root;

        [SerializeField, TypeCheck(typeof(IEnvironmentPostProcess))]
        private MonoBehaviour _environmentPostProcess;

        [SerializeField] private int _positionSnapping;

        public Data GetData() => _data;

#if UNITY_EDITOR
        [MyButton]
        public void SerializeProps()
        {
            var instances = new List<GameObject>();
            var scene = SceneManager.GetActiveScene();
            var roots = _root ? _root.GetComponents<Transform>().Select(t => t.gameObject).ToArray() : scene.GetRootGameObjects();
            var stack = new Stack<Transform>();
            for (int i = 0; i < roots.Length; i++)
            {
                var rootGo = roots[i];
                if (rootGo != gameObject && rootGo.hideFlags == HideFlags.None && PrefabUtility.IsAnyPrefabInstanceRoot(rootGo))
                {
                    instances.Add(rootGo);
                    continue;
                }

                stack.Push(rootGo.transform);
            }

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                foreach (Transform child in node)
                {
                    var go = child.gameObject;
                    if (go.hideFlags == HideFlags.None && PrefabUtility.IsAnyPrefabInstanceRoot(go))
                    {
                        instances.Add(go);
                        continue;
                    }

                    stack.Push(child);
                }
            }

            if (instances.Count == 0)
            {
                _data.values = Array.Empty<float>();
                _data.prefabs = Array.Empty<GameObject>();
                _data.prefabUsageCounts = Array.Empty<int>();
                _data.simpleValues = Array.Empty<float>();
                _data.simplePrefabs = Array.Empty<GameObject>();
                _data.simplePrefabUsageCounts = Array.Empty<int>();
                return;
            }

            var originalToInstances = new Dictionary<GameObject, List<GameObject>>();
            var originalToSimpleInstances = new Dictionary<GameObject, List<GameObject>>();
            for (int i = 0; i < instances.Count; i++)
            {
                var inst = instances[i];
                var original = PrefabUtility.GetCorrespondingObjectFromOriginalSource(inst);
                if (!original) continue;
                var path = AssetDatabase.GetAssetPath(original);
                if (string.IsNullOrEmpty(path)) continue;

                var t = inst.transform;
                var isSimple = t.rotation == Quaternion.identity && t.localScale == Vector3.one;

                if (isSimple)
                {
                    if (!originalToSimpleInstances.TryGetValue(original, out var simpleList))
                    {
                        simpleList = new List<GameObject>();
                        originalToSimpleInstances.Add(original, simpleList);
                    }

                    simpleList.Add(inst);
                }
                else
                {
                    if (!originalToInstances.TryGetValue(original, out var list))
                    {
                        list = new List<GameObject>();
                        originalToInstances.Add(original, list);
                    }

                    list.Add(inst);
                }
            }

            var originals = new List<GameObject>(originalToInstances.Keys);
            originals.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));

            var values = new List<float>(originals.Count * 9);
            var prefabs = new List<GameObject>(originals.Count);
            var prefabUsageCounts = new List<int>(originals.Count);

            var simpleOriginals = new List<GameObject>(originalToSimpleInstances.Keys);
            simpleOriginals.Sort((a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));

            var simpleValues = new List<float>(simpleOriginals.Count * 3);
            var simplePrefabs = new List<GameObject>(simpleOriginals.Count);
            var simplePrefabUsageCounts = new List<int>(simpleOriginals.Count);

            for (int pi = 0; pi < originals.Count; pi++)
            {
                var original = originals[pi];
                var list = originalToInstances[original];

                prefabs.Add(original);
                prefabUsageCounts.Add(list.Count);

                // Стабильный порядок
                list.Sort((x, y) => x.GetInstanceID().CompareTo(y.GetInstanceID()));

                var temp = new GameObject("PrefabCompressedData_Temp");
                temp.hideFlags = HideFlags.DontSave;
                var tt = temp.transform;

                for (int ii = 0; ii < list.Count; ii++)
                {
                    var inst = list[ii];

                    tt.SetParent(inst.transform.parent, false);
                    tt.position = inst.transform.position;
                    tt.rotation = inst.transform.rotation;
                    tt.localScale = inst.transform.localScale;
                    tt.SetParent(transform, true);

                    var pos = tt.localPosition;
                    var euler = tt.localEulerAngles;
                    var scale = tt.localScale;

                    if (_positionSnapping > 1)
                    {
                        pos.x = Mathf.Round(pos.x * _positionSnapping) / _positionSnapping;
                        pos.y = Mathf.Round(pos.y * _positionSnapping) / _positionSnapping;
                        pos.z = Mathf.Round(pos.z * _positionSnapping) / _positionSnapping;
                    }

                    values.Add(pos.x);
                    values.Add(pos.y);
                    values.Add(pos.z);
                    values.Add(euler.x);
                    values.Add(euler.y);
                    values.Add(euler.z);
                    values.Add(scale.x);
                    values.Add(scale.y);
                    values.Add(scale.z);
                }

                DestroyImmediate(temp);
            }

            for (int pi = 0; pi < simpleOriginals.Count; pi++)
            {
                var original = simpleOriginals[pi];
                var list = originalToSimpleInstances[original];

                simplePrefabs.Add(original);
                simplePrefabUsageCounts.Add(list.Count);

                list.Sort((x, y) => x.GetInstanceID().CompareTo(y.GetInstanceID()));

                var temp = new GameObject("PrefabCompressedData_Temp");
                temp.hideFlags = HideFlags.DontSave;
                var tt = temp.transform;

                for (int ii = 0; ii < list.Count; ii++)
                {
                    var inst = list[ii];

                    tt.SetParent(inst.transform.parent, false);
                    tt.position = inst.transform.position;
                    tt.SetParent(transform, true);

                    var pos = tt.localPosition;

                    if (_positionSnapping > 1)
                    {
                        pos.x = Mathf.Round(pos.x * _positionSnapping) / _positionSnapping;
                        pos.y = Mathf.Round(pos.y * _positionSnapping) / _positionSnapping;
                        pos.z = Mathf.Round(pos.z * _positionSnapping) / _positionSnapping;
                    }

                    simpleValues.Add(pos.x);
                    simpleValues.Add(pos.y);
                    simpleValues.Add(pos.z);
                }

                DestroyImmediate(temp);
            }

            _data.values = values.ToArray();
            _data.prefabs = prefabs.ToArray();
            _data.prefabUsageCounts = prefabUsageCounts.ToArray();
            _data.simpleValues = simpleValues.ToArray();
            _data.simplePrefabs = simplePrefabs.ToArray();
            _data.simplePrefabUsageCounts = simplePrefabUsageCounts.ToArray();
            EditorUtility.SetDirty(this);
        }

        [MyButton]
        private void Clear()
        {
            if (_root)
                _root.DestroyChildren(true);
            else
                transform.DestroyChildren(true);
        }

        [MyButton]
        private void SelectPrefabs()
        {
            var prefabs = _data.prefabs ?? Array.Empty<GameObject>();
            var simplePrefabs = _data.simplePrefabs ?? Array.Empty<GameObject>();
            var allPrefabs = new GameObject[prefabs.Length + simplePrefabs.Length];
            prefabs.CopyTo(allPrefabs, 0);
            simplePrefabs.CopyTo(allPrefabs, prefabs.Length);
            Selection.objects = allPrefabs;
        }
#endif

        private void Awake()
        {
#if UNITY_EDITOR
            if (!_instantiateOnAwake)
                return;
#endif
            Spawn();
        }

        private void Start()
        {
            

            if (_environmentPostProcess && Application.isPlaying)
                ((IEnvironmentPostProcess)_environmentPostProcess).PostProcess(_root ? _root.gameObject : gameObject);
        }

        [MyButton]
        private void Spawn()
        {
            int index = 0;
            int prefabIndex = 0;
            var values = _data.values ??= Array.Empty<float>();
            var prefabs = _data.prefabs ??= Array.Empty<GameObject>();
            var prefabUsageCounts = _data.prefabUsageCounts ??= Array.Empty<int>();

            for (int i = 0, iMax = values.Length; i < iMax; i += 9)
            {
                var pos = new Vector3(
                    values[i + 0], values[i + 1], values[i + 2]);
                var euler = new Vector3(
                    values[i + 3], values[i + 4], values[i + 5]);
                var scale = new Vector3(
                    values[i + 6], values[i + 7], values[i + 8]);
                GameObject go;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var inst = PrefabUtility.InstantiatePrefab(prefabs[prefabIndex], _root) as GameObject;
                    go = inst ? inst : Instantiate(prefabs[prefabIndex], _root);
                }
                else
                {
                    go = Instantiate(prefabs[prefabIndex], _root);
                }
#else
                go = Instantiate(prefabs[prefabIndex], _root);
#endif
                go.transform.localPosition = pos;
                go.transform.localEulerAngles = euler;
                go.transform.localScale = scale;
                go.hideFlags = _flags;

                index++;
                if (index >= prefabUsageCounts[prefabIndex])
                {
                    index = 0;
                    prefabIndex++;
                }
            }

            index = 0;
            prefabIndex = 0;
            var simpleValues = _data.simpleValues ??= Array.Empty<float>();
            var simplePrefabs = _data.simplePrefabs ??= Array.Empty<GameObject>();
            var simplePrefabUsageCounts = _data.simplePrefabUsageCounts ??= Array.Empty<int>();

            for (int i = 0, iMax = simpleValues.Length; i < iMax; i += 3)
            {
                var pos = new Vector3(
                    simpleValues[i + 0], simpleValues[i + 1], simpleValues[i + 2]);
                GameObject go;
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var inst = PrefabUtility.InstantiatePrefab(simplePrefabs[prefabIndex], _root) as GameObject;
                    go = inst ? inst : Instantiate(simplePrefabs[prefabIndex], _root);
                }
                else
                {
                    go = Instantiate(simplePrefabs[prefabIndex], _root);
                }
#else
                go = Instantiate(simplePrefabs[prefabIndex], _root);
#endif
                go.transform.localPosition = pos;
                go.hideFlags = _flags;

                index++;
                if (index >= simplePrefabUsageCounts[prefabIndex])
                {
                    index = 0;
                    prefabIndex++;
                }
            }
        }
    }
}
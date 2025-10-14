using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    /// <summary>
    /// ПАМЯТКА: Максимальный размер мира для long-упаковки координат
    /// 
    /// При step = 0.05:
    /// • Рекомендуемый размер: ±10,000 Unity единиц
    /// • Максимальный безопасный: ±25,000 Unity единиц  
    /// • Абсолютный лимит: ±52,428 Unity единиц
    /// 
    /// Технические лимиты:
    /// • 21 бит на ось = ±1,048,575 ячеек максимум
    /// • Формула: max_world_size = max_cells × step
    /// 
    /// Если нужен больший мир:
    /// 1. Увеличить step (снижает детализацию)
    /// 2. Вернуться к Vector3Int (без ограничений)
    /// 3. Разбить на чанки
    /// </summary>
    public class PrefabShadowBaker : MonoBehaviour
    {
        [SerializeField] private HideFlags _flags;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<GameObject> _ignoredPrefabs;
        [SerializeField, Min(0.01f)] private float _step = 1f;
        [SerializeField] private float[] _baked;

        private void OnValidate() => _ignoredPrefabs = _ignoredPrefabs?.Distinct().ToList();

#if UNITY_EDITOR
        [MyButton]
        private void Bake()
        {
            var instances = new List<GameObject>();

            var scene = SceneManager.GetActiveScene();
            var roots = scene.GetRootGameObjects();

            var visited = new HashSet<GameObject>();
            var stack = new Stack<Transform>();
            for (int i = 0; i < roots.Length; i++)
                stack.Push(roots[i].transform);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                var go = node.gameObject;
                if (visited.Contains(go))
                    continue;
                visited.Add(go);

                if (PrefabUtility.IsAnyPrefabInstanceRoot(go))
                {
                    var original = PrefabUtility.GetCorrespondingObjectFromOriginalSource(go);
                    if (!original || !_ignoredPrefabs.Contains(original))
                    {
                        instances.Add(go);
                        continue;
                    }
                    
                    if (_ignoredPrefabs.Contains(original))
                        continue;
                }

                foreach (Transform child in node)
                    stack.Push(child);
            }
            

            var step = Mathf.Max(_step, 0.01f);
            var renderers = new List<Renderer>(64);
            var cells = new List<Vector3Int>(instances.Count * 64);

            int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;

            foreach (var inst in instances)
            {
                renderers.Clear();
                inst.GetComponentsInChildren(true, renderers);
                if (renderers.Count == 0)
                    continue;

                Bounds b = renderers[0].bounds;
                for (int r = 1; r < renderers.Count; r++)
                    b.Encapsulate(renderers[r].bounds);

                int x0 = Mathf.RoundToInt(b.min.x / step);
                int y0 = Mathf.RoundToInt(b.min.y / step);
                int z0 = Mathf.RoundToInt(b.min.z / step);
                int x1 = Mathf.RoundToInt(b.max.x / step) - 1;
                int y1 = Mathf.RoundToInt(b.max.y / step) - 1;
                int z1 = Mathf.RoundToInt(b.max.z / step) - 1;

                for (int z = z0; z <= z1; z++)
                for (int y = y0; y <= y1; y++)
                for (int x = x0; x <= x1; x++)
                {
                    cells.Add(new Vector3Int(x, y, z));
                    if (x < minX) minX = x; if (x > maxX) maxX = x;
                    if (y < minY) minY = y; if (y > maxY) maxY = y;
                    if (z < minZ) minZ = z; if (z > maxZ) maxZ = z;
                }
            }

            if (cells.Count == 0)
            {
                _baked = System.Array.Empty<float>();
                EditorUtility.SetDirty(this);
                return;
            }

            var occ = new HashSet<long>(cells.Count);
            var used = new HashSet<long>(cells.Count);

            // Fill HashSet with packed coords
            foreach (var c in cells)
            {
                var key = PackCoords(c.x, c.y, c.z, minX, minY, minZ);
                occ.Add(key);
            }
            var baked = new List<float>(occ.Count * 6);

            cells.Sort((a, b) =>
            {
                if (a.z != b.z) return a.z.CompareTo(b.z);
                if (a.y != b.y) return a.y.CompareTo(b.y);
                return a.x.CompareTo(b.x);
            });

            foreach (var start in cells)
            {
                var startKey = PackCoords(start.x, start.y, start.z, minX, minY, minZ);
                if (used.Contains(startKey))
                    continue;

                int x = start.x;
                int y = start.y;
                int z = start.z;

                // Grow in X
                int x2 = x;
                while (true)
                {
                    int nx = x2 + 1;
                    if (nx > maxX) break;
                    var key = PackCoords(nx, y, z, minX, minY, minZ);
                    if (!occ.Contains(key) || used.Contains(key)) break;
                    x2++;
                }

                // Grow in Y while full width present
                int y2 = y;
                while (true)
                {
                    int nextY = y2 + 1;
                    if (nextY > maxY) break;
                    bool ok = true;
                    for (int xi = x; xi <= x2; xi++)
                    {
                        var key = PackCoords(xi, nextY, z, minX, minY, minZ);
                        if (!occ.Contains(key) || used.Contains(key)) { ok = false; break; }
                    }
                    if (!ok) break;
                    y2 = nextY;
                }

                // Grow in Z while full rectangle present
                int z2 = z;
                while (true)
                {
                    int nextZ = z2 + 1;
                    if (nextZ > maxZ) break;
                    bool ok = true;
                    for (int yi = y; yi <= y2 && ok; yi++)
                    for (int xi = x; xi <= x2; xi++)
                    {
                        var key = PackCoords(xi, yi, nextZ, minX, minY, minZ);
                        if (!occ.Contains(key) || used.Contains(key)) { ok = false; break; }
                    }
                    if (!ok) break;
                    z2 = nextZ;
                }

                for (int zz = z; zz <= z2; zz++)
                for (int yy = y; yy <= y2; yy++)
                for (int xx = x; xx <= x2; xx++)
                {
                    var key = PackCoords(xx, yy, zz, minX, minY, minZ);
                    used.Add(key);
                }

                float sX = (x2 - x + 1) * step;
                float sY = (y2 - y + 1) * step;
                float sZ = (z2 - z + 1) * step;
                var center = new Vector3((x + x2 + 1) * 0.5f * step,
                    (y + y2 + 1) * 0.5f * step,
                    (z + z2 + 1) * 0.5f * step);

                baked.Add(center.x);
                baked.Add(center.y);
                baked.Add(center.z);
                baked.Add(sX);
                baked.Add(sY);
                baked.Add(sZ);
            }

            _baked = baked.ToArray();
            EditorUtility.SetDirty(this);
        }

        private static long PackCoords(int x, int y, int z, int minX, int minY, int minZ)
        {
            // Pack into 64-bit long: 21 bits per axis (supports ±1M range per axis)
            var dx = (long)(x - minX);
            var dy = (long)(y - minY);
            var dz = (long)(z - minZ);
            return (dx << 42) | (dy << 21) | dz;
        }

        [MyButton]
        private void Spawn()
        {
            if (_prefab == null) return;
            Clear();
            SpawnFromBaked();
            EditorUtility.SetDirty(this);
        }

        [MyButton]
        private void Clear() => transform.DestroyChildren();
#endif

        private void Awake()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            if (_prefab) SpawnFromBaked();
        }

        private void SpawnFromBaked()
        {
            var container = new GameObject("Shadow Casters").transform;
            container.parent = transform;
            container.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            container.localScale = Vector3.one;
            if (_baked == null || _baked.Length == 0) return;
            for (int i = 0; i < _baked.Length; i += 6)
            {
                var pos = new Vector3(_baked[i + 0], _baked[i + 1], _baked[i + 2]);
                var scale = new Vector3(_baked[i + 3], _baked[i + 4], _baked[i + 5]);
                var go = Instantiate(_prefab, container);
                go.transform.localPosition = pos;
                go.transform.localScale = scale;
                go.hideFlags = Application.isPlaying ? HideFlags.None : _flags;
            }
        }
    }
}
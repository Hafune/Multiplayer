using System.Collections.Generic;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    public class PrefabBoundsMeshBaker : MonoBehaviour
    {
        [SerializeField] private HideFlags _flags;
        [SerializeField] private Material _material;
        [SerializeField] private List<GameObject> _ignoredPrefabs;
        [SerializeField, Min(0.01f)] private float _step = 1f;
        [SerializeField] private float[] _bakedVertices;
        [SerializeField] private int[] _bakedTriangles;

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
                    if (!_ignoredPrefabs?.Contains(original) ?? true)
                    {
                        instances.Add(go);
                        continue;
                    }
                    if (_ignoredPrefabs != null && _ignoredPrefabs.Contains(original))
                        continue;
                }

                foreach (Transform child in node)
                    stack.Push(child);
            }

            if (instances.Count == 0)
                return;

            var step = Mathf.Max(_step, 0.01f);
            var renderers = new List<Renderer>(64);

            int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
            var occ = new HashSet<long>(instances.Count * 256);

            foreach (var inst in instances)
            {
                renderers.Clear();
                inst.GetComponentsInChildren(true, renderers);
                if (renderers.Count == 0) continue;

                Bounds b = renderers[0].bounds;
                for (int r = 1; r < renderers.Count; r++) b.Encapsulate(renderers[r].bounds);

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
                    occ.Add(Pack(x, y, z));
                    if (x < minX) minX = x; if (x > maxX) maxX = x;
                    if (y < minY) minY = y; if (y > maxY) maxY = y;
                    if (z < minZ) minZ = z; if (z > maxZ) maxZ = z;
                }
            }

            if (occ.Count == 0) return;

            var vertices = new List<Vector3>(occ.Count * 8);
            var triangles = new List<int>(occ.Count * 12);
            var normals = new List<Vector3>(occ.Count * 8);

            // Greedy meshing along each axis using 2D masks
            GreedyAxisX(occ, minX, minY, minZ, maxX, maxY, maxZ, step, vertices, triangles, normals);
            GreedyAxisY(occ, minX, minY, minZ, maxX, maxY, maxZ, step, vertices, triangles, normals);
            GreedyAxisZ(occ, minX, minY, minZ, maxX, maxY, maxZ, step, vertices, triangles, normals);

            var meshGo = new GameObject("BakedBoundsMesh");
            meshGo.transform.SetParent(transform, false);
            meshGo.hideFlags = _flags;

            var mf = meshGo.AddComponent<MeshFilter>();
            var mr = meshGo.AddComponent<MeshRenderer>();
            mr.sharedMaterial = _material;

            var mesh = new Mesh();
            mesh.indexFormat = vertices.Count > 65000 ? UnityEngine.Rendering.IndexFormat.UInt32 : UnityEngine.Rendering.IndexFormat.UInt16;
            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();
#if UNITY_EDITOR
            MeshUtility.Optimize(mesh);
#endif
            mf.sharedMesh = mesh;
            // Save baked data for later spawn
            _bakedVertices = new float[vertices.Count * 3];
            for (int i = 0, j = 0; i < vertices.Count; i++, j += 3)
            {
                var v = vertices[i];
                _bakedVertices[j + 0] = v.x;
                _bakedVertices[j + 1] = v.y;
                _bakedVertices[j + 2] = v.z;
            }
            _bakedTriangles = triangles.ToArray();
            EditorUtility.SetDirty(this);
        }

        [MyButton]
        private void Clear() => transform.DestroyChildren();
#endif

        private static long Pack(int x, int y, int z)
        {
            // 21 bits per axis
            return (((long)x & ((1L << 21) - 1)) << 42) | (((long)y & ((1L << 21) - 1)) << 21) | ((long)z & ((1L << 21) - 1));
        }

        private static bool Has(HashSet<long> occ, int x, int y, int z) => occ.Contains(Pack(x, y, z));

#if UNITY_EDITOR
        [MyButton]
        private void Spawn()
        {
            if (_material == null) return;
            if (_bakedVertices == null || _bakedVertices.Length == 0 || _bakedTriangles == null || _bakedTriangles.Length == 0)
                return;

            var meshGo = new GameObject("BakedBoundsMesh");
            meshGo.transform.SetParent(transform, false);
            meshGo.hideFlags = _flags;

            var mf = meshGo.AddComponent<MeshFilter>();
            var mr = meshGo.AddComponent<MeshRenderer>();
            mr.sharedMaterial = _material;

            int vCount = _bakedVertices.Length / 3;
            var verts = new List<Vector3>(vCount);
            for (int i = 0; i < _bakedVertices.Length; i += 3)
                verts.Add(new Vector3(_bakedVertices[i + 0], _bakedVertices[i + 1], _bakedVertices[i + 2]));

            var mesh = new Mesh();
            mesh.indexFormat = (_bakedTriangles.Length > 65000) ? UnityEngine.Rendering.IndexFormat.UInt32 : UnityEngine.Rendering.IndexFormat.UInt16;
            mesh.SetVertices(verts);
            mesh.SetTriangles(_bakedTriangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mf.sharedMesh = mesh;
            EditorUtility.SetDirty(this);
        }

        [MyButton]
        private void ExportAsset()
        {
            if (_bakedVertices == null || _bakedVertices.Length == 0 || _bakedTriangles == null || _bakedTriangles.Length == 0)
            {
                Debug.LogError("PrefabBoundsMeshBaker: No baked data to export. Run Bake first.");
                return;
            }

            var mesh = new Mesh();
            var verts = new Vector3[_bakedVertices.Length / 3];
            for (int i = 0, j = 0; i < verts.Length; i++, j += 3)
                verts[i] = new Vector3(_bakedVertices[j + 0], _bakedVertices[j + 1], _bakedVertices[j + 2]);
            mesh.vertices = verts;
            mesh.triangles = _bakedTriangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            var path = EditorUtility.SaveFilePanelInProject(
                "Save Baked Mesh",
                "BakedBoundsMesh",
                "asset",
                "Choose a location to save the baked mesh asset");
            if (string.IsNullOrEmpty(path)) return;

            if (AssetDatabase.LoadAssetAtPath<Object>(path) != null)
                AssetDatabase.DeleteAsset(path);

            AssetDatabase.CreateAsset(mesh, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"PrefabBoundsMeshBaker: Exported mesh asset to {path}");
        }
#endif

        private static void GreedyAxisX(HashSet<long> occ, int minX, int minY, int minZ, int maxX, int maxY, int maxZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int H = maxY - minY + 1;
            int W = maxZ - minZ + 1;
            var mask = new bool[H, W];

            // +X faces between x and x+1
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                for (int z = minZ; z <= maxZ; z++)
                    mask[y - minY, z - minZ] = Has(occ, x, y, z) && !Has(occ, x + 1, y, z);
                EmitQuadsX(mask, x + 1, true, minY, minZ, step, vertices, triangles, normals);
            }

            // -X faces between x and x-1
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                for (int z = minZ; z <= maxZ; z++)
                    mask[y - minY, z - minZ] = Has(occ, x, y, z) && !Has(occ, x - 1, y, z);
                EmitQuadsX(mask, x, false, minY, minZ, step, vertices, triangles, normals);
            }
        }

        private static void GreedyAxisY(HashSet<long> occ, int minX, int minY, int minZ, int maxX, int maxY, int maxZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int H = maxZ - minZ + 1;
            int W = maxX - minX + 1;
            var mask = new bool[H, W];

            // +Y
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                for (int x = minX; x <= maxX; x++)
                    mask[z - minZ, x - minX] = Has(occ, x, y, z) && !Has(occ, x, y + 1, z);
                EmitQuadsY(mask, y + 1, true, minX, minZ, step, vertices, triangles, normals);
            }

            // -Y
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                for (int x = minX; x <= maxX; x++)
                    mask[z - minZ, x - minX] = Has(occ, x, y, z) && !Has(occ, x, y - 1, z);
                EmitQuadsY(mask, y, false, minX, minZ, step, vertices, triangles, normals);
            }
        }

        private static void GreedyAxisZ(HashSet<long> occ, int minX, int minY, int minZ, int maxX, int maxY, int maxZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int H = maxY - minY + 1;
            int W = maxX - minX + 1;
            var mask = new bool[H, W];

            // +Z
            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                for (int x = minX; x <= maxX; x++)
                    mask[y - minY, x - minX] = Has(occ, x, y, z) && !Has(occ, x, y, z + 1);
                EmitQuadsZ(mask, z + 1, true, minX, minY, step, vertices, triangles, normals);
            }

            // -Z
            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                for (int x = minX; x <= maxX; x++)
                    mask[y - minY, x - minX] = Has(occ, x, y, z) && !Has(occ, x, y, z - 1);
                EmitQuadsZ(mask, z, false, minX, minY, step, vertices, triangles, normals);
            }
        }

        private static void EmitQuadsX(bool[,] mask, int xPlane, bool positive, int minY, int minZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int rows = mask.GetLength(0);
            int cols = mask.GetLength(1);
            var active = new Dictionary<(int, int), (int yMin, int yMax)>();
            for (int y = 0; y < rows; y++)
            {
                var runs = new List<(int start, int end)>();
                int start = -1;
                for (int z = 0; z < cols; z++)
                {
                    bool on = mask[y, z];
                    if (on && start < 0) start = z;
                    if ((!on || z == cols - 1) && start >= 0)
                    {
                        int end = on && z == cols - 1 ? z : z - 1;
                        runs.Add((start, end));
                        start = -1;
                    }
                }
                var cur = new Dictionary<(int, int), (int yMin, int yMax)>();
                foreach (var r in runs)
                {
                    var key = (r.start, r.end);
                    if (active.TryGetValue(key, out var seg)) cur[key] = (seg.yMin, y);
                    else cur[key] = (y, y);
                }
                foreach (var kv in active)
                    if (!cur.ContainsKey(kv.Key))
                        EmitQuadX(xPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.yMin, kv.Value.yMax, positive, minY, minZ, step, vertices, triangles, normals);
                active = cur;
            }
            foreach (var kv in active)
                EmitQuadX(xPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.yMin, kv.Value.yMax, positive, minY, minZ, step, vertices, triangles, normals);
        }

        private static void EmitQuadsY(bool[,] mask, int yPlane, bool positive, int minX, int minZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int rows = mask.GetLength(0);
            int cols = mask.GetLength(1);
            var active = new Dictionary<(int, int), (int rMin, int rMax)>();
            for (int r = 0; r < rows; r++)
            {
                var runs = new List<(int start, int end)>();
                int start = -1;
                for (int c = 0; c < cols; c++)
                {
                    bool on = mask[r, c];
                    if (on && start < 0) start = c;
                    if ((!on || c == cols - 1) && start >= 0)
                    {
                        int end = on && c == cols - 1 ? c : c - 1;
                        runs.Add((start, end));
                        start = -1;
                    }
                }
                var cur = new Dictionary<(int, int), (int rMin, int rMax)>();
                foreach (var run in runs)
                {
                    var key = (run.start, run.end);
                    if (active.TryGetValue(key, out var seg)) cur[key] = (seg.rMin, r);
                    else cur[key] = (r, r);
                }
                foreach (var kv in active)
                    if (!cur.ContainsKey(kv.Key))
                        EmitQuadY(yPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.rMin, kv.Value.rMax, positive, minX, minZ, step, vertices, triangles, normals);
                active = cur;
            }
            foreach (var kv in active)
                EmitQuadY(yPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.rMin, kv.Value.rMax, positive, minX, minZ, step, vertices, triangles, normals);
        }

        private static void EmitQuadsZ(bool[,] mask, int zPlane, bool positive, int minX, int minY, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            int rows = mask.GetLength(0);
            int cols = mask.GetLength(1);
            var active = new Dictionary<(int, int), (int rMin, int rMax)>();
            for (int r = 0; r < rows; r++)
            {
                var runs = new List<(int start, int end)>();
                int start = -1;
                for (int c = 0; c < cols; c++)
                {
                    bool on = mask[r, c];
                    if (on && start < 0) start = c;
                    if ((!on || c == cols - 1) && start >= 0)
                    {
                        int end = on && c == cols - 1 ? c : c - 1;
                        runs.Add((start, end));
                        start = -1;
                    }
                }
                var cur = new Dictionary<(int, int), (int rMin, int rMax)>();
                foreach (var run in runs)
                {
                    var key = (run.start, run.end);
                    if (active.TryGetValue(key, out var seg)) cur[key] = (seg.rMin, r);
                    else cur[key] = (r, r);
                }
                foreach (var kv in active)
                    if (!cur.ContainsKey(kv.Key))
                        EmitQuadZ(zPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.rMin, kv.Value.rMax, positive, minX, minY, step, vertices, triangles, normals);
                active = cur;
            }
            foreach (var kv in active)
                EmitQuadZ(zPlane, kv.Key.Item1, kv.Key.Item2, kv.Value.rMin, kv.Value.rMax, positive, minX, minY, step, vertices, triangles, normals);
        }

        private static void EmitQuadX(int xPlane, int zStart, int zEnd, int yStart, int yEnd, bool positive, int minY, int minZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            float x = xPlane * step;
            float y0 = (yStart + minY) * step;
            float y1 = (yEnd + minY + 1) * step;
            float z0 = (zStart + minZ) * step;
            float z1 = (zEnd + minZ + 1) * step;

            int vi = vertices.Count;
            if (positive)
            {
                vertices.Add(new Vector3(x, y0, z0));
                vertices.Add(new Vector3(x, y1, z0));
                vertices.Add(new Vector3(x, y1, z1));
                vertices.Add(new Vector3(x, y0, z1));
                normals.Add(Vector3.right); normals.Add(Vector3.right); normals.Add(Vector3.right); normals.Add(Vector3.right);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
            else
            {
                vertices.Add(new Vector3(x, y0, z1));
                vertices.Add(new Vector3(x, y1, z1));
                vertices.Add(new Vector3(x, y1, z0));
                vertices.Add(new Vector3(x, y0, z0));
                normals.Add(Vector3.left); normals.Add(Vector3.left); normals.Add(Vector3.left); normals.Add(Vector3.left);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
        }

        private static void EmitQuadY(int yPlane, int xStart, int xEnd, int zStart, int zEnd, bool positive, int minX, int minZ, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            float y = yPlane * step;
            float x0 = (xStart + minX) * step;
            float x1 = (xEnd + minX + 1) * step;
            float z0 = (zStart + minZ) * step;
            float z1 = (zEnd + minZ + 1) * step;

            int vi = vertices.Count;
            if (positive)
            {
                vertices.Add(new Vector3(x0, y, z1));
                vertices.Add(new Vector3(x1, y, z1));
                vertices.Add(new Vector3(x1, y, z0));
                vertices.Add(new Vector3(x0, y, z0));
                normals.Add(Vector3.up); normals.Add(Vector3.up); normals.Add(Vector3.up); normals.Add(Vector3.up);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
            else
            {
                vertices.Add(new Vector3(x0, y, z0));
                vertices.Add(new Vector3(x1, y, z0));
                vertices.Add(new Vector3(x1, y, z1));
                vertices.Add(new Vector3(x0, y, z1));
                normals.Add(Vector3.down); normals.Add(Vector3.down); normals.Add(Vector3.down); normals.Add(Vector3.down);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
        }

        private static void EmitQuadZ(int zPlane, int xStart, int xEnd, int yStart, int yEnd, bool positive, int minX, int minY, float step,
            List<Vector3> vertices, List<int> triangles, List<Vector3> normals)
        {
            float z = zPlane * step;
            float x0 = (xStart + minX) * step;
            float x1 = (xEnd + minX + 1) * step;
            float y0 = (yStart + minY) * step;
            float y1 = (yEnd + minY + 1) * step;

            int vi = vertices.Count;
            if (positive)
            {
                vertices.Add(new Vector3(x0, y0, z));
                vertices.Add(new Vector3(x1, y0, z));
                vertices.Add(new Vector3(x1, y1, z));
                vertices.Add(new Vector3(x0, y1, z));
                normals.Add(Vector3.forward); normals.Add(Vector3.forward); normals.Add(Vector3.forward); normals.Add(Vector3.forward);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
            else
            {
                vertices.Add(new Vector3(x0, y1, z));
                vertices.Add(new Vector3(x1, y1, z));
                vertices.Add(new Vector3(x1, y0, z));
                vertices.Add(new Vector3(x0, y0, z));
                normals.Add(Vector3.back); normals.Add(Vector3.back); normals.Add(Vector3.back); normals.Add(Vector3.back);
                triangles.Add(vi + 0); triangles.Add(vi + 1); triangles.Add(vi + 2);
                triangles.Add(vi + 0); triangles.Add(vi + 2); triangles.Add(vi + 3);
            }
        }
    }
}



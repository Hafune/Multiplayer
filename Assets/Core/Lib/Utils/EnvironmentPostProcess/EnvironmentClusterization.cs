using System.Collections.Generic;
using UnityEngine;
#if true
using UnityEditor;
#endif

namespace Core.Lib
{
    public class EnvironmentClusterization : MonoBehaviour, IEnvironmentPostProcess
    {
        private static readonly List<Renderer> _renderers = new();
        private static readonly Dictionary<Vector2Int, GameObject> _cellToCluster = new();
        private static readonly Dictionary<Vector2Int, Bounds> _cellToBounds = new();

        [SerializeField] private float _packSize = 1;
        [SerializeField] private Vector2 _scipOverSize = new(10f, 10f);
        [SerializeField, Layer] private int _mask;

        private readonly List<OnTrigger2DListener> _clusters = new();

#if UNITY_EDITOR
        private void Awake() => enabled = false;

        private void OnEnable()
        {
            foreach (var listener in _clusters)
                listener.OnEnter?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var listener in _clusters)
                listener.OnExit?.Invoke();
        }
#endif

        public void PostProcess(GameObject root)
        {
            _clusters.Clear();

            _cellToCluster.Clear();
            _cellToBounds.Clear();

            var cellToRenderers = new Dictionary<Vector2Int, List<Renderer>>();

            _renderers.Clear();
            root.GetComponentsInChildren(true, _renderers);

            for (int r = 0; r < _renderers.Count; r++)
            {
                var renderer = _renderers[r];
                var b = renderer.bounds;

                var size = b.size;
                if (size.x > _scipOverSize.x || size.y > _scipOverSize.y)
                    continue;

                var center = b.center;
                var cell = new Vector2Int(
                    Mathf.FloorToInt(center.x / _packSize),
                    Mathf.FloorToInt(center.y / _packSize)
                );

                if (!cellToRenderers.TryGetValue(cell, out var list))
                {
                    list = new List<Renderer>(8);
                    cellToRenderers[cell] = list;

                    var listener = CreateCluster(cell, list);
                    _cellToCluster[cell] = listener.gameObject;
                    _clusters.Add(listener);
                    _cellToBounds[cell] = b;
                }
                else
                {
                    var cb = _cellToBounds[cell];
                    cb.Encapsulate(b);
                    _cellToBounds[cell] = cb;
                }

                list.Add(renderer);
            }

            foreach (var (key, value) in _cellToBounds)
            {
                var cluster = _cellToCluster[key];
                SetupClusterCollider(cluster, value);
            }

            foreach (var kv in cellToRenderers)
                ToggleRenderers(kv.Value, false);
        }

        private OnTrigger2DListener CreateCluster(Vector2Int cell, List<Renderer> renderers)
        {
            var go = new GameObject($"Cluster_{cell.x}_{cell.y}");
            go.hideFlags = HideFlags.DontSave;
            go.transform.SetParent(transform, false);
            go.layer = _mask;
            go.transform.rotation = Quaternion.identity;

            var listener = go.AddComponent<OnTrigger2DListener>();
            listener.OnEnter = () => ToggleRenderers(renderers, true);
            listener.OnExit = () => ToggleRenderers(renderers, false);

            return listener;
        }

        private void ToggleRenderers(List<Renderer> renderers, bool active)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            foreach (var t in renderers)
                t.enabled = active;
        }

        private void SetupClusterCollider(GameObject cluster, Bounds worldBounds)
        {
            var localMin = new Vector3(worldBounds.min.x, worldBounds.min.y - worldBounds.max.z * 1.8f, 0f);
            var localMax = new Vector3(worldBounds.max.x, worldBounds.max.y - worldBounds.min.z * .3f, 0f);
            var localCenter = (localMin + localMax) * 0.5f;
            var localSizeV3 = localMax - localMin;

            var size = new Vector2(Mathf.Abs(localSizeV3.x), Mathf.Abs(localSizeV3.y)) / transform.lossyScale.x;
            bool canUseCircle = Mathf.Max(size.x, size.y) / Mathf.Min(size.x, size.y) < 1.3;

            if (canUseCircle)
            {
                var col = cluster.AddComponent<CircleCollider2D>();

                col.isTrigger = true;
                col.offset = cluster.transform.InverseTransformPoint(new Vector2(localCenter.x, localCenter.y));
                col.radius = Mathf.Max(size.x, size.y) / 2 / transform.lossyScale.x * (1 / .71f);
            }
            else
            {
                var col = cluster.AddComponent<BoxCollider2D>();

                col.isTrigger = true;
                col.offset = cluster.transform.InverseTransformPoint(new Vector2(localCenter.x, localCenter.y));
                col.size = new Vector2(Mathf.Abs(localSizeV3.x), Mathf.Abs(localSizeV3.y)) / transform.lossyScale.x;
            }
        }
    }
}
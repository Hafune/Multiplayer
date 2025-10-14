using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Lib.Editor
{
    public sealed class GroupBoundsWindow : EditorWindow
    {
        private const float MinSize = 0.01f;
        private readonly List<Transform> _transforms = new();
        private readonly List<Vector3> _normalizedPoints = new();
        private Vector3 _boundsMin;
        private Vector3 _boundsMax;
        private bool _hasSelection;
        private bool _isDragging;
        private int _dragAxis = -1;
        private bool _dragPositive;
        private Vector3 _dragStartBoundsMin;
        private Vector3 _dragStartBoundsMax;
        private readonly List<Vector3> _dragStartNormalizedPoints = new();
        private int _undoGroup = -1;
        private Vector3 _gridSize = Vector3.one;
        private bool _autoSnap;
        private bool _pendingHandleRelease;

        [MenuItem("Auto/Selections/Group Bounds Window")]
        private static void Open()
        {
            var window = GetWindow<GroupBoundsWindow>();
            window.titleContent = new GUIContent("Group Bounds");
            window.minSize = new Vector2(260f, 150f);
            window.RebuildSelection();
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            SceneView.duringSceneGui += OnSceneGUI;
            RebuildSelection();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSelectionChanged()
        {
            EndDrag();
            RebuildSelection();
            Repaint();
            SceneView.RepaintAll();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(4f);
            EditorGUILayout.LabelField("Group Bounds", EditorStyles.boldLabel);
            EditorGUILayout.Space(4f);

            if (!_hasSelection)
            {
                EditorGUILayout.HelpBox("Select at least one transform to display bounds.", MessageType.Info);
                return;
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Objects", _transforms.Count.ToString());
                GridSettingsGUI();
                EditorGUILayout.Space(6f);
                if (GUILayout.Button("Merge By Distance", GUILayout.Height(24f)))
                {
                    SelectionTools.MergeByDistance();
                    RebuildSelection();
                    SceneView.RepaintAll();
                }
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (!_hasSelection)
                return;

            HandleSceneHandleDrag();

            if (!_isDragging)
            {
                var prevMin = _boundsMin;
                var prevMax = _boundsMax;
                ComputeBounds();
                if (prevMin != _boundsMin || prevMax != _boundsMax)
                    Repaint();
            }

            var boundsCenter = (_boundsMin + _boundsMax) * 0.5f;
            var boundsSize = _boundsMax - _boundsMin;
            Handles.color = new Color(0.6f, 0.9f, 1f);
            Handles.DrawWireCube(boundsCenter, boundsSize);

            if (_transforms.Count >= 2)
            {
                Handles.color = Color.cyan;
                DrawFaceHandle(0, true);
                DrawFaceHandle(0, false);
                DrawFaceHandle(1, true);
                DrawFaceHandle(1, false);
                DrawFaceHandle(2, true);
                DrawFaceHandle(2, false);
            }
            else if (_isDragging)
            {
                EndDrag();
            }

            if (_pendingHandleRelease)
            {
                _pendingHandleRelease = false;
                if (_autoSnap)
                    SnapByGrid();
            }
        }

        private void DrawFaceHandle(int axis, bool positive)
        {
            var min = _boundsMin;
            var max = _boundsMax;
            var size = max - min;
            var center = (min + max) * 0.5f;
            var direction = AxisDirection(axis);
            var offset = size[axis] * 0.5f;
            if (!positive)
                offset = -offset;
            var faceCenter = center + direction * offset;
            var normal = positive ? direction : -direction;
            var handleSize = HandleUtility.GetHandleSize(faceCenter) * 0.1f;

            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.Slider(faceCenter, normal, handleSize, Handles.CubeHandleCap, 0f);
            var changed = EditorGUI.EndChangeCheck();
            var isCurrentHandle = _isDragging && _dragAxis == axis && _dragPositive == positive;

            if (!changed)
            {
                if (isCurrentHandle && Event.current.type == EventType.MouseUp)
                {
                    EndDrag();
                    _pendingHandleRelease = true;
                }

                return;
            }

            if (!_isDragging || !isCurrentHandle)
                BeginDrag(axis, positive);

            var axisDirection = AxisDirection(_dragAxis);
            var coordinate = Vector3.Dot(newPosition, axisDirection);
            var newMin = _dragStartBoundsMin;
            var newMax = _dragStartBoundsMax;
            if (_dragPositive)
                newMax[_dragAxis] = coordinate;
            else
                newMin[_dragAxis] = coordinate;

            if (newMax[_dragAxis] - newMin[_dragAxis] < MinSize)
            {
                if (_dragPositive)
                    newMax[_dragAxis] = newMin[_dragAxis] + MinSize;
                else
                    newMin[_dragAxis] = newMax[_dragAxis] - MinSize;
            }

            ApplyResize(newMin, newMax);
        }

        private void ApplyResize(Vector3 newMin, Vector3 newMax)
        {
            var size = newMax - newMin;
            if (size.x <= 0f || size.y <= 0f || size.z <= 0f)
                return;
            if (_dragAxis < 0)
                return;
            if (_dragStartNormalizedPoints.Count != _transforms.Count)
                return;

            var axis = _dragAxis;
            var originalMin = _dragStartBoundsMin[axis];
            var originalMax = _dragStartBoundsMax[axis];
            var newAxisMin = newMin[axis];
            var newAxisMax = newMax[axis];
            var originalSize = originalMax - originalMin;
            var newSize = newAxisMax - newAxisMin;
            if (Mathf.Approximately(originalSize, 0f))
                originalSize = 0.0001f;

            for (var i = 0; i < _transforms.Count; i++)
            {
                var norm = _dragStartNormalizedPoints[i][axis];
                var startValue = originalMin + norm * originalSize;
                var targetValue = newAxisMin + norm * newSize;

                if (_dragPositive)
                {
                    var delta = newAxisMax - originalMax;
                    var ratio = Mathf.Clamp01(norm);
                    targetValue = startValue + delta * ratio;
                }
                else
                {
                    var delta = newAxisMin - originalMin;
                    var ratio = Mathf.Clamp01(1f - norm);
                    targetValue = startValue + delta * ratio;
                }

                var position = _transforms[i].position;
                position[axis] = targetValue;
                _transforms[i].position = position;
            }

            ComputeBounds();
            SceneView.RepaintAll();
            Repaint();
        }

        private void RebuildSelection()
        {
            EndDrag();
            _transforms.Clear();
            _normalizedPoints.Clear();

            var selected = Selection.transforms;
            if (selected == null || selected.Length == 0)
            {
                _hasSelection = false;
                return;
            }

            foreach (var transform in selected)
            {
                if (transform == null)
                    continue;
                _transforms.Add(transform);
            }

            _hasSelection = _transforms.Count > 0;
            ComputeBounds();
        }

        private void ComputeBounds()
        {
            if (!_hasSelection)
                return;

            var firstPos = _transforms[0].position;
            _boundsMin = firstPos;
            _boundsMax = firstPos;

            for (var i = 1; i < _transforms.Count; i++)
            {
                var p = _transforms[i].position;
                _boundsMin = Vector3.Min(_boundsMin, p);
                _boundsMax = Vector3.Max(_boundsMax, p);
            }

            var size = _boundsMax - _boundsMin;
            _normalizedPoints.Clear();

            for (var i = 0; i < _transforms.Count; i++)
            {
                var p = _transforms[i].position;
                var normMin = new Vector3(
                    (size.x <= 0f ? 0f : (p.x - _boundsMin.x) / size.x),
                    (size.y <= 0f ? 0f : (p.y - _boundsMin.y) / size.y),
                    (size.z <= 0f ? 0f : (p.z - _boundsMin.z) / size.z)
                );

                _normalizedPoints.Add(normMin);
            }
        }

        private static Vector3 AxisDirection(int axis)
        {
            return axis switch
            {
                0 => Vector3.right,
                1 => Vector3.up,
                _ => Vector3.forward
            };
        }

        private void BeginDrag(int axis, bool positive)
        {
            _isDragging = true;
            _dragAxis = axis;
            _dragPositive = positive;
            _dragStartBoundsMin = _boundsMin;
            _dragStartBoundsMax = _boundsMax;
            _dragStartNormalizedPoints.Clear();
            _undoGroup = Undo.GetCurrentGroup();
            Undo.IncrementCurrentGroup();
            var targets = new Object[_transforms.Count];
            for (var i = 0; i < _transforms.Count; i++)
                targets[i] = _transforms[i];
            Undo.RegisterCompleteObjectUndo(targets, "Resize Group Bounds");

            for (var i = 0; i < _normalizedPoints.Count; i++)
                _dragStartNormalizedPoints.Add(_normalizedPoints[i]);
        }

        private void EndDrag()
        {
            if (!_isDragging)
                return;
            _isDragging = false;
            _dragAxis = -1;
            if (_undoGroup >= 0)
            {
                Undo.CollapseUndoOperations(_undoGroup);
                _undoGroup = -1;
            }

            _dragStartNormalizedPoints.Clear();
        }

        private void GridSettingsGUI()
        {
            EditorGUI.BeginChangeCheck();
            _gridSize = EditorGUILayout.Vector3Field("Grid Size", _gridSize);
            if (EditorGUI.EndChangeCheck())
            {
                _gridSize.x = Mathf.Max(MinSize, _gridSize.x);
                _gridSize.y = Mathf.Max(MinSize, _gridSize.y);
                _gridSize.z = Mathf.Max(MinSize, _gridSize.z);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                _autoSnap = GUILayout.Toggle(_autoSnap, "Auto Snapping", "Button", GUILayout.Height(22f));
                if (GUILayout.Button("Snap By Grid", GUILayout.Height(22f)) || _autoSnap)
                    SnapByGrid();
            }
        }

        private void SnapByGrid()
        {
            if (_transforms.Count == 0)
                return;

            var targets = new Object[_transforms.Count];
            for (var i = 0; i < _transforms.Count; i++)
                targets[i] = _transforms[i];
            Undo.RecordObjects(targets, "Snap By Grid");

            for (var i = 0; i < _transforms.Count; i++)
            {
                var t = _transforms[i];
                var local = t.localPosition;
                local.x = Mathf.Round(local.x / _gridSize.x) * _gridSize.x;
                local.y = Mathf.Round(local.y / _gridSize.y) * _gridSize.y;
                local.z = Mathf.Round(local.z / _gridSize.z) * _gridSize.z;
                t.localPosition = local;
            }

            ComputeBounds();
            Repaint();
            SceneView.RepaintAll();
        }
        
        private void HandleSceneHandleDrag()
        {
            if (!_isDragging)
                return;

            var evt = Event.current;
            if (evt == null)
                return;

            if (evt.type == EventType.MouseUp || evt.rawType == EventType.MouseUp)
            {
                EndDrag();
                _pendingHandleRelease = true;
            }
        }
    }
}
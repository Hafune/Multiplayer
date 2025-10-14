using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Lib.Editor
{
	public sealed class GridSelectionWindow : EditorWindow
	{
		private const string KeyGridX = "Core.Lib.GridSelectionWindow.GridX";
		private const string KeyGridY = "Core.Lib.GridSelectionWindow.GridY";
		private const string KeyGridZ = "Core.Lib.GridSelectionWindow.GridZ";
		private const string KeyGizmoOffsetX = "Core.Lib.GridSelectionWindow.GizmoOffsetX";
		private const string KeyGizmoOffsetY = "Core.Lib.GridSelectionWindow.GizmoOffsetY";
		private const string KeyGizmoOffsetZ = "Core.Lib.GridSelectionWindow.GizmoOffsetZ";
		private const string KeyOverlap = "Core.Lib.GridSelectionWindow.Overlap";
		private const string KeyGizmoFilled = "Core.Lib.GridSelectionWindow.GizmoFilled";
		private const string KeyGizmoExpand = "Core.Lib.GridSelectionWindow.GizmoExpand";
		private const string KeyXPos = "Core.Lib.GridSelectionWindow.XPos";
		private const string KeyXNeg = "Core.Lib.GridSelectionWindow.XNeg";
		private const string KeyYPos = "Core.Lib.GridSelectionWindow.YPos";
		private const string KeyYNeg = "Core.Lib.GridSelectionWindow.YNeg";
		private const string KeyZPos = "Core.Lib.GridSelectionWindow.ZPos";
		private const string KeyZNeg = "Core.Lib.GridSelectionWindow.ZNeg";

		[SerializeField]
		private Vector3 _grid = Vector3.one;

		[SerializeField]
		private Vector3 _gizmoOffset = Vector3.zero;

		[SerializeField]
		private bool _overlap = true;

		[SerializeField]
		private bool _gizmoFilled;

		[SerializeField]
		private float _gizmoExpand;

		[SerializeField] private bool _xPos;
		[SerializeField] private bool _xNeg;
		[SerializeField] private bool _yPos;
		[SerializeField] private bool _yNeg;
		[SerializeField] private bool _zPos;
		[SerializeField] private bool _zNeg;

		private readonly Dictionary<Vector3, List<GameObject>> _gridMap = new();
		private readonly HashSet<GameObject> _visible = new();
		private readonly HashSet<Vector3> _visibleCells = new();
		private readonly Dictionary<Vector2, Candidate> _best2D = new();
		private readonly List<Object> _tmpObjectList = new();
		private Object[] _tmpObjectsArray;
		private readonly Vector3[] _quadVerts = new Vector3[4];
		private Vector2 _scroll;

		private Transform _origin;
		private Quaternion _originRotation;
		private Vector3 _originPosition;

		[MenuItem("Auto/Selections/Grid Selection Window")] 
		private static void Open()
		{
			var wnd = GetWindow<GridSelectionWindow>(false, "Selection Window", true);
			wnd.minSize = new Vector2(280f, 350f);
			wnd.Rebuild();
		}

		private void OnEnable()
		{
			Selection.selectionChanged += OnSelectionChanged;
			SceneView.duringSceneGui += OnSceneGUI;
			LoadPrefs();
			Rebuild();
		}

		private void OnDisable()
		{
			Selection.selectionChanged -= OnSelectionChanged;
			SceneView.duringSceneGui -= OnSceneGUI;
			SavePrefs();
		}

		private void OnSelectionChanged()
		{
			Rebuild();
			Repaint();
			SceneView.RepaintAll();
		}

		private void OnGUI()
		{
			using (var scroll = new EditorGUILayout.ScrollViewScope(_scroll))
			{
				DrawHeader();
				DrawGridSettings();
				DrawVisibilityAxes();
				DrawGizmoSettings();
				DrawActions();
				DrawStats();
				_scroll = scroll.scrollPosition;
			}
		}

		private void DrawHeader()
		{
			EditorGUILayout.Space(5);
			var titleStyle = new GUIStyle(EditorStyles.boldLabel)
			{
				fontSize = 14,
				alignment = TextAnchor.MiddleCenter
			};
			EditorGUILayout.LabelField("Selection Grid & Visibility", titleStyle);
			EditorGUILayout.Space(10);
		}

		private void DrawGridSettings()
		{
			using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("🔲 Grid Settings", EditorStyles.boldLabel);
				EditorGUILayout.Space(3);

				EditorGUI.BeginChangeCheck();
				_grid = EditorGUILayout.Vector3Field("Grid Size", _grid);
				var gridChanged = EditorGUI.EndChangeCheck();

				if (_grid.x <= 0f || _grid.y <= 0f || _grid.z <= 0f)
				{
					EditorGUILayout.Space(5);
					EditorGUILayout.HelpBox("All grid components must be greater than 0", MessageType.Error);
					return;
				}

				if (gridChanged)
				{
					Rebuild();
					SceneView.RepaintAll();
					SavePrefs();
				}
			}
			EditorGUILayout.Space(5);
		}

		private void DrawVisibilityAxes()
		{
			using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("🎯 Visibility Axes", EditorStyles.boldLabel);
				EditorGUILayout.Space(3);

				EditorGUI.BeginChangeCheck();

				using (new EditorGUILayout.HorizontalScope())
				{
					_xPos = GUILayout.Toggle(_xPos, "X+", "Button", GUILayout.Height(22));
					_xNeg = GUILayout.Toggle(_xNeg, "X-", "Button", GUILayout.Height(22));
					_yPos = GUILayout.Toggle(_yPos, "Y+", "Button", GUILayout.Height(22));
					_yNeg = GUILayout.Toggle(_yNeg, "Y-", "Button", GUILayout.Height(22));
					_zPos = GUILayout.Toggle(_zPos, "Z+", "Button", GUILayout.Height(22));
					_zNeg = GUILayout.Toggle(_zNeg, "Z-", "Button", GUILayout.Height(22));
				}

				var togglesChanged = EditorGUI.EndChangeCheck();

				if (togglesChanged)
				{
					RecomputeVisible();
					Repaint();
					SceneView.RepaintAll();
					SavePrefs();
				}
			}
			EditorGUILayout.Space(5);
		}

		private void DrawGizmoSettings()
		{
			using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("🎨 Gizmo Settings", EditorStyles.boldLabel);
				EditorGUILayout.Space(3);

				EditorGUI.BeginChangeCheck();

				_gizmoOffset = EditorGUILayout.Vector3Field("Offset", _gizmoOffset);
				_gizmoExpand = EditorGUILayout.FloatField("Expand", _gizmoExpand);

				EditorGUILayout.Space(5);
				using (new EditorGUILayout.HorizontalScope())
				{
					_overlap = GUILayout.Toggle(_overlap, "On Top", "Button", GUILayout.Height(22));
					_gizmoFilled = GUILayout.Toggle(_gizmoFilled, "Filled", "Button", GUILayout.Height(22));
				}

				var gizmoChanged = EditorGUI.EndChangeCheck();

				if (gizmoChanged)
				{
					Repaint();
					SceneView.RepaintAll();
					SavePrefs();
				}
			}
			EditorGUILayout.Space(5);
		}

		private void DrawActions()
		{
			using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("⚡ Actions", EditorStyles.boldLabel);
				EditorGUILayout.Space(3);

				// Refresh button
				if (GUILayout.Button("🔄 Refresh", GUILayout.Height(28)))
				{
					Rebuild();
					SceneView.RepaintAll();
				}

				EditorGUILayout.Space(3);

				// Selection buttons
				using (new EditorGUILayout.HorizontalScope())
				{
					using (new EditorGUI.DisabledScope(_visible.Count == 0))
					{
						if (GUILayout.Button("👁️ Select Visible", GUILayout.Height(26)))
						{
							SelectVisible();
						}
					}

					using (new EditorGUI.DisabledScope(Selection.gameObjects?.Length == 0))
					{
						if (GUILayout.Button("🚫 Select Invisible", GUILayout.Height(26)))
						{
							SelectInvisible();
						}
					}
				}
			}
			EditorGUILayout.Space(5);
		}

		private void DrawStats()
		{
			using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
			{
				EditorGUILayout.LabelField("📊 Statistics", EditorStyles.boldLabel);
				EditorGUILayout.Space(3);

				var statsStyle = new GUIStyle(EditorStyles.label)
				{
					fontSize = 11
				};

				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUILayout.LabelField($"Selected: {Selection.gameObjects?.Length ?? 0}", statsStyle);
					EditorGUILayout.LabelField($"Grid Cells: {_gridMap.Count}", statsStyle);
				}

				using (new EditorGUILayout.HorizontalScope()) EditorGUILayout.LabelField($"Visible: {_visible.Count}", statsStyle);
			}
		}

		private void LoadPrefs()
		{
			_grid.x = EditorPrefs.GetFloat(KeyGridX, _grid.x);
			_grid.y = EditorPrefs.GetFloat(KeyGridY, _grid.y);
			_grid.z = EditorPrefs.GetFloat(KeyGridZ, _grid.z);
			_gizmoOffset.x = EditorPrefs.GetFloat(KeyGizmoOffsetX, _gizmoOffset.x);
			_gizmoOffset.y = EditorPrefs.GetFloat(KeyGizmoOffsetY, _gizmoOffset.y);
			_gizmoOffset.z = EditorPrefs.GetFloat(KeyGizmoOffsetZ, _gizmoOffset.z);
			_overlap = EditorPrefs.GetBool(KeyOverlap, _overlap);
			_gizmoFilled = EditorPrefs.GetBool(KeyGizmoFilled, _gizmoFilled);
			_gizmoExpand = EditorPrefs.GetFloat(KeyGizmoExpand, _gizmoExpand);
			_xPos = EditorPrefs.GetBool(KeyXPos, _xPos);
			_xNeg = EditorPrefs.GetBool(KeyXNeg, _xNeg);
			_yPos = EditorPrefs.GetBool(KeyYPos, _yPos);
			_yNeg = EditorPrefs.GetBool(KeyYNeg, _yNeg);
			_zPos = EditorPrefs.GetBool(KeyZPos, _zPos);
			_zNeg = EditorPrefs.GetBool(KeyZNeg, _zNeg);
		}

		private void SavePrefs()
		{
			EditorPrefs.SetFloat(KeyGridX, _grid.x);
			EditorPrefs.SetFloat(KeyGridY, _grid.y);
			EditorPrefs.SetFloat(KeyGridZ, _grid.z);
			EditorPrefs.SetFloat(KeyGizmoOffsetX, _gizmoOffset.x);
			EditorPrefs.SetFloat(KeyGizmoOffsetY, _gizmoOffset.y);
			EditorPrefs.SetFloat(KeyGizmoOffsetZ, _gizmoOffset.z);
			EditorPrefs.SetBool(KeyOverlap, _overlap);
			EditorPrefs.SetBool(KeyGizmoFilled, _gizmoFilled);
			EditorPrefs.SetFloat(KeyGizmoExpand, _gizmoExpand);
			EditorPrefs.SetBool(KeyXPos, _xPos);
			EditorPrefs.SetBool(KeyXNeg, _xNeg);
			EditorPrefs.SetBool(KeyYPos, _yPos);
			EditorPrefs.SetBool(KeyYNeg, _yNeg);
			EditorPrefs.SetBool(KeyZPos, _zPos);
			EditorPrefs.SetBool(KeyZNeg, _zNeg);
		}



		private void Rebuild()
		{
			_gridMap.Clear();
			_visible.Clear();

			var sel = Selection.gameObjects;
			if (sel == null || sel.Length == 0)
			{
				_origin = null;
				return;
			}

			_origin = sel[0].transform;
			_originRotation = _origin.rotation;
			_originPosition = _origin.position;

			for (var i = 0; i < sel.Length; i++)
			{
				var go = sel[i];
				var cell = WorldToCell(go.transform.position);
				if (!_gridMap.TryGetValue(cell, out var list))
				{
					list = new List<GameObject>();
					_gridMap[cell] = list;
				}
				list.Add(go);
			}

			RecomputeVisible();
		}

		private void RecomputeVisible()
		{
			_visible.Clear();
			if (_gridMap.Count == 0)
				return;

			if (_xPos) SweepX(true);
			if (_xNeg) SweepX(false);
			if (_yPos) SweepY(true);
			if (_yNeg) SweepY(false);
			if (_zPos) SweepZ(true);
			if (_zNeg) SweepZ(false);
		}

		private void OnSceneGUI(SceneView sceneView)
		{
			if (_origin == null)
				return;
			if (_grid.x <= 0f || _grid.y <= 0f || _grid.z <= 0f)
				return;

			_visibleCells.Clear();
			foreach (var go in _visible)
			{
				if (go == null)
					continue;
				var cell = WorldToCell(go.transform.position);
				_visibleCells.Add(cell);
			}

			if (_visibleCells.Count == 0)
				return;

			var matrix = Matrix4x4.TRS(_originPosition, _originRotation, Vector3.one);
			using (new Handles.DrawingScope(matrix))
			{
				var prev = Handles.color;
				var prevZ = Handles.zTest;
				Handles.color = Color.green;
				Handles.zTest = _overlap ? UnityEngine.Rendering.CompareFunction.Always : UnityEngine.Rendering.CompareFunction.LessEqual;
				foreach (var cell in _visibleCells)
				{
					var center = cell + _gizmoOffset;
					var size = new Vector3(
						Mathf.Max(0f, _grid.x + _gizmoExpand * 2f),
						Mathf.Max(0f, _grid.y + _gizmoExpand * 2f),
						Mathf.Max(0f, _grid.z + _gizmoExpand * 2f)
					);
					if (_gizmoFilled)
					{
						DrawSolidCubeLocal(center, size, Color.green);
					}
					else
					{
						Handles.DrawWireCube(center, size);
					}
				}
				Handles.color = prev;
				Handles.zTest = prevZ;
			}
		}

		private void DrawSolidCubeLocal(Vector3 center, Vector3 size, Color color)
		{
			var hx = new Vector3(size.x * 0.5f, 0f, 0f);
			var hy = new Vector3(0f, size.y * 0.5f, 0f);
			var hz = new Vector3(0f, 0f, size.z * 0.5f);

			var c000 = center - hx - hy - hz;
			var c001 = center - hx - hy + hz;
			var c010 = center - hx + hy - hz;
			var c011 = center - hx + hy + hz;
			var c100 = center + hx - hy - hz;
			var c101 = center + hx - hy + hz;
			var c110 = center + hx + hy - hz;
			var c111 = center + hx + hy + hz;

			var fill = color;
			var outline = Color.clear;

			_quadVerts[0] = c001; _quadVerts[1] = c101; _quadVerts[2] = c111; _quadVerts[3] = c011; // z+
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
			_quadVerts[0] = c000; _quadVerts[1] = c010; _quadVerts[2] = c110; _quadVerts[3] = c100; // z-
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
			_quadVerts[0] = c000; _quadVerts[1] = c001; _quadVerts[2] = c011; _quadVerts[3] = c010; // x-
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
			_quadVerts[0] = c100; _quadVerts[1] = c110; _quadVerts[2] = c111; _quadVerts[3] = c101; // x+
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
			_quadVerts[0] = c010; _quadVerts[1] = c011; _quadVerts[2] = c111; _quadVerts[3] = c110; // y+
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
			_quadVerts[0] = c000; _quadVerts[1] = c100; _quadVerts[2] = c101; _quadVerts[3] = c001; // y-
			Handles.DrawSolidRectangleWithOutline(_quadVerts, fill, outline);
		}

		private Vector3 WorldToCell(Vector3 world)
		{
			var local = _origin.InverseTransformPoint(world);
			if (_grid.x > 0f) local.x = Mathf.Round(local.x / _grid.x) * _grid.x;
			if (_grid.y > 0f) local.y = Mathf.Round(local.y / _grid.y) * _grid.y;
			if (_grid.z > 0f) local.z = Mathf.Round(local.z / _grid.z) * _grid.z;

			local.x = Round2(local.x);
			local.y = Round2(local.y);
			local.z = Round2(local.z);

			return local;
		}

		private static float Round2(float v)
		{
			return Mathf.Round(v * 100f) / 100f;
		}

		private struct Candidate
		{
			public float Scalar;
			public GameObject Go;
		}

		private void SweepX(bool positive)
		{
			_best2D.Clear();
			foreach (var kv in _gridMap)
			{
				var p = kv.Key;
				var yz = new Vector2(p.y, p.z);
				if (_best2D.TryGetValue(yz, out var c))
				{
					if (positive)
					{
						if (p.x < c.Scalar)
						{
							c.Scalar = p.x;
							c.Go = kv.Value[0];
							_best2D[yz] = c;
						}
					}
					else
					{
						if (p.x > c.Scalar)
						{
							c.Scalar = p.x;
							c.Go = kv.Value[0];
							_best2D[yz] = c;
						}
					}
				}
				else
				{
					_best2D[yz] = new Candidate { Scalar = p.x, Go = kv.Value[0] };
				}
			}
			foreach (var kv in _best2D)
			{
				_visible.Add(kv.Value.Go);
			}
		}

		private void SweepY(bool positive)
		{
			_best2D.Clear();
			foreach (var kv in _gridMap)
			{
				var p = kv.Key;
				var xz = new Vector2(p.x, p.z);
				if (_best2D.TryGetValue(xz, out var c))
				{
					if (positive)
					{
						if (p.y < c.Scalar)
						{
							c.Scalar = p.y;
							c.Go = kv.Value[0];
							_best2D[xz] = c;
						}
					}
					else
					{
						if (p.y > c.Scalar)
						{
							c.Scalar = p.y;
							c.Go = kv.Value[0];
							_best2D[xz] = c;
						}
					}
				}
				else
				{
					_best2D[xz] = new Candidate { Scalar = p.y, Go = kv.Value[0] };
				}
			}
			foreach (var kv in _best2D)
			{
				_visible.Add(kv.Value.Go);
			}
		}

		private void SweepZ(bool positive)
		{
			_best2D.Clear();
			foreach (var kv in _gridMap)
			{
				var p = kv.Key;
				var xy = new Vector2(p.x, p.y);
				if (_best2D.TryGetValue(xy, out var c))
				{
					if (positive)
					{
						if (p.z < c.Scalar)
						{
							c.Scalar = p.z;
							c.Go = kv.Value[0];
							_best2D[xy] = c;
						}
					}
					else
					{
						if (p.z > c.Scalar)
						{
							c.Scalar = p.z;
							c.Go = kv.Value[0];
							_best2D[xy] = c;
						}
					}
				}
				else
				{
					_best2D[xy] = new Candidate { Scalar = p.z, Go = kv.Value[0] };
				}
			}
			foreach (var kv in _best2D)
			{
				_visible.Add(kv.Value.Go);
			}
		}

		private void SelectVisible()
		{
			_tmpObjectList.Clear();
			foreach (var go in _visible)
				_tmpObjectList.Add(go);
			EnsureTmpArraySize(_tmpObjectList.Count);
			for (var i = 0; i < _tmpObjectList.Count; i++)
				_tmpObjectsArray[i] = _tmpObjectList[i];
			Selection.objects = _tmpObjectsArray;
		}

		private void SelectInvisible()
		{
			var current = Selection.gameObjects;
			_tmpObjectList.Clear();
			for (var i = 0; i < current.Length; i++)
			{
				var go = current[i];
				if (!_visible.Contains(go))
					_tmpObjectList.Add(go);
			}
			EnsureTmpArraySize(_tmpObjectList.Count);
			for (var i = 0; i < _tmpObjectList.Count; i++)
				_tmpObjectsArray[i] = _tmpObjectList[i];
			Selection.objects = _tmpObjectsArray;
		}

		private void EnsureTmpArraySize(int size)
		{
			if (_tmpObjectsArray == null || _tmpObjectsArray.Length < size)
				_tmpObjectsArray = new Object[Mathf.NextPowerOfTwo(Mathf.Max(size, 4))];
		}
	}
}



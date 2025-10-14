using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Core.Lib.Editor
{
	public class RenameWindow : EditorWindow
	{
		private enum CaseMode
		{
			None,
			Upper,
			Lower,
			Title
		}

		private readonly List<UnityEngine.Object> _selected = new();
		private readonly List<string> _preview = new();
		private Vector2 _scroll;

		private string _pattern = "{orig}_{i}"; // tokens: {orig}, {i}
		private int _startIndex = 1;
		private int _step = 1;
		private int _pad = 2;
		private string _find = "";
		private string _replace = "";
		private bool _useRegex;
		private bool _trim;
		private CaseMode _caseMode = CaseMode.None;
		private bool _sortByName = true;
		private bool _descending;
		private bool _showHelp;

		[MenuItem("Auto/Rename Window")]
		private static void Open() 
		{
			var window = GetWindow<RenameWindow>(true, "Bulk Rename", true);
			window.minSize = new Vector2(400, 300);
		}

		private void OnEnable()
		{
			Selection.selectionChanged += RebuildSelection;
			RebuildSelection();
		}

		private void OnDisable() => Selection.selectionChanged -= RebuildSelection;

		private void OnGUI()
		{
			EditorGUILayout.BeginVertical();
			
			EditorGUILayout.BeginVertical("box");
			DrawHelp();
			DrawSelection();
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(5);

			EditorGUILayout.BeginVertical("box");
			DrawPatternControls();
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(5);

			EditorGUILayout.BeginVertical("box");
			DrawAdditionalControls();
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space(5);

			DrawPreview();
			
			GUILayout.FlexibleSpace();
			DrawApply();
			
			EditorGUILayout.EndVertical();
		}

		private void DrawHelp()
		{
			_showHelp = EditorGUILayout.Foldout(_showHelp, "Pattern Help", true);
			if (_showHelp)
			{
				EditorGUILayout.BeginVertical(EditorStyles.helpBox);
				EditorGUILayout.LabelField("Available Tokens:", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("• {orig} - Original object name");
				EditorGUILayout.LabelField("• {i} - Counter with padding");
				EditorGUILayout.Space(3);
				EditorGUILayout.LabelField("Examples:", EditorStyles.boldLabel);
				EditorGUILayout.LabelField("• {orig}_{i} → Cube_01, Cube_02");
				EditorGUILayout.LabelField("• Enemy_{i} → Enemy_01, Enemy_02");
				EditorGUILayout.LabelField("• {orig}_Copy → Cube_Copy, Sphere_Copy");
				EditorGUILayout.EndVertical();
			}
		}

		private void DrawSelection()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField($"Selected Objects: {_selected.Count}", EditorStyles.boldLabel);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Refresh", GUILayout.Width(80))) 
				RebuildSelection();
			EditorGUILayout.EndHorizontal();
		}

		private void DrawPatternControls()
		{
			EditorGUILayout.LabelField("Pattern Settings", EditorStyles.boldLabel);
			_pattern = EditorGUILayout.TextField("Pattern", _pattern);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Start Index", GUILayout.Width(80));
			_startIndex = EditorGUILayout.IntField(_startIndex, GUILayout.Width(60));
			GUILayout.Space(10);
			EditorGUILayout.LabelField("Step", GUILayout.Width(40));
			_step = EditorGUILayout.IntField(_step, GUILayout.Width(40));
			GUILayout.Space(10);
			EditorGUILayout.LabelField("Padding", GUILayout.Width(60));
			_pad = Mathf.Clamp(EditorGUILayout.IntField(_pad, GUILayout.Width(40)), 0, 8);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		private void DrawAdditionalControls()
		{
			EditorGUILayout.LabelField("Additional Options", EditorStyles.boldLabel);
			
			EditorGUILayout.BeginHorizontal();
			_find = EditorGUILayout.TextField("Find", _find);
			GUILayout.Space(5);
			_replace = EditorGUILayout.TextField("Replace", _replace);
			GUILayout.Space(5);
			_useRegex = EditorGUILayout.ToggleLeft("Regex", _useRegex, GUILayout.Width(60));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			_trim = EditorGUILayout.ToggleLeft("Trim whitespace", _trim, GUILayout.Width(120));
			GUILayout.Space(10);
			EditorGUILayout.LabelField("Case:", GUILayout.Width(35));
			_caseMode = (CaseMode)EditorGUILayout.EnumPopup(_caseMode, GUILayout.Width(80));
			GUILayout.FlexibleSpace();
			_sortByName = EditorGUILayout.ToggleLeft("Sort by name", _sortByName, GUILayout.Width(100));
			_descending = EditorGUILayout.ToggleLeft("Desc", _descending, GUILayout.Width(50));
			EditorGUILayout.EndHorizontal();
		}

		private void DrawPreview()
		{
			EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
			EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);
			
			if (_selected.Count == 0)
			{
				EditorGUILayout.HelpBox("No objects selected", MessageType.Info);
				EditorGUILayout.EndVertical();
				return;
			}

			RebuildPreview();

			_scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.ExpandHeight(true));

			for (var i = 0; i < _selected.Count; i++)
			{
				var obj = _selected[i];
				var newName = _preview[i];
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.ObjectField(obj, typeof(UnityEngine.Object), false);
				EditorGUILayout.LabelField("→", GUILayout.Width(20));
				EditorGUILayout.LabelField(newName, GUILayout.ExpandWidth(true));
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndVertical();
		}

		private void DrawApply()
		{
			GUI.enabled = _selected.Count > 0;
			if (GUILayout.Button("Apply Rename", GUILayout.Height(35)))
			{
				ApplyRename();
				RebuildSelection();
			}
			GUI.enabled = true;
		}

		private void RebuildSelection()
		{
			_selected.Clear();
			_selected.AddRange(Selection.objects.Where(o => o));

			if (_sortByName)
			{
				_selected.Sort((a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
				if (_descending)
					_selected.Reverse();
			}
			RebuildPreview();
			Repaint();
		}

		private void RebuildPreview()
		{
			_preview.Clear();

			var idx = _startIndex;
			foreach (var obj in _selected)
			{
				var baseName = obj.name;
				if (!string.IsNullOrEmpty(_find))
					baseName = _useRegex 
						? Regex.Replace(baseName, _find, _replace) 
						: baseName.Replace(_find, _replace);

				var formattedIndex = _pad > 0 ? idx.ToString(new string('0', _pad)) : idx.ToString();
				var candidate = _pattern
					.Replace("{orig}", baseName)
					.Replace("{i}", formattedIndex);

				candidate = ApplyCase(candidate);
				candidate = _trim ? candidate.Trim() : candidate;

				_preview.Add(candidate);
				idx += _step;
			}
		}

		private string ApplyCase(string value)
		{
			switch (_caseMode)
			{
				case CaseMode.Upper: return value.ToUpperInvariant();
				case CaseMode.Lower: return value.ToLowerInvariant();
				case CaseMode.Title:
					var textInfo = CultureInfo.InvariantCulture.TextInfo;
					return textInfo.ToTitleCase(value.ToLowerInvariant());
				default: return value;
			}
		}

		private void ApplyRename()
		{
			if (_selected.Count == 0)
				return;

			Undo.IncrementCurrentGroup();
			Undo.SetCurrentGroupName("Bulk Rename");

			for (var i = 0; i < _selected.Count; i++)
			{
				var obj = _selected[i];
				var newName = _preview[i];

				var path = AssetDatabase.GetAssetPath(obj);
				var isAsset = !string.IsNullOrEmpty(path);

				if (isAsset && AssetDatabase.IsMainAsset(obj))
				{
					var error = AssetDatabase.RenameAsset(path, newName);
					if (!string.IsNullOrEmpty(error))
						Debug.LogError(error);
				}
				else
				{
					Undo.RecordObject(obj, "Bulk Rename");
					obj.name = newName;
					EditorUtility.SetDirty(obj);
				}
			}

			AssetDatabase.SaveAssets();
		}
	}
}


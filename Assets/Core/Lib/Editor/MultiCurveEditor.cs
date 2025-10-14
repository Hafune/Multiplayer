using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using Lib;

[CustomEditor(typeof(AbstractMultiCurve), true)]
public class MultiCurveEditor : Editor
{
    private static bool _showValueAlways;
    private static bool _showTimes;
    private static bool _showValues = true;
    private static float _timeStep = 1f;
    private static float _valueStep = 0.1f;

    private List<(string name, AnimationCurve curve)> _curves;
    private Vector2 _mouseHandleDragStart;
    private Vector2 _mouseSelectionRectStart;
    private bool _isDraggingSelection;
    private bool _isDrawingSelectionRect;
    private const int _labelPadding = 40;
    private int _draggingCurveIndex = -1;
    private int _draggingKeyIndex = -1;
    private readonly Color[] _curveColors = { Color.green, Color.red, Color.blue, Color.yellow, Color.magenta };
    private readonly Dictionary<(int curveIndex, int keyIndex), Vector2> _initialKeyPositions = new();
    private readonly List<(int, int)> _selectedHandles = new();
    private readonly List<(Vector2 position, Keyframe key)> _labelsToRender = new();
    private bool _isShiftInterpolation;

    private const string _settingsTooltip = "Горячие клавиши:\n" +
                                            "• Ctrl + Клик - Добавить новую точку на кривую\n" +
                                            "• ПКМ на точке - Удалить точку\n" +
                                            "• Alt + Перетаскивание - Перемещение точки по оси времени\n" +
                                            "• Перетаскивание - Перемещение точки только по оси значений\n" +
                                            "• Клик + Перетаскивание - Выделение нескольких точек\n" +
                                            "• Shift + Перетаскивание выделенных - Интерполяция между крайними точками";

    // используем разные имена для левого и правого индекса
    private readonly Dictionary<int, (int leftKeyIndex, int rightKeyIndex)> _edgeHandlesPerCurve = new();

    public override void OnInspectorGUI()
    {
        var curveSet = (AbstractMultiCurve)target;
        _curves = new List<(string name, AnimationCurve curve)>(curveSet.EditorGetCurves());

        if (_curves.Count == 0)
        {
            EditorGUILayout.HelpBox("No curves to display.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField(new GUIContent("Settings", _settingsTooltip), EditorStyles.boldLabel);
        _timeStep = Math.Max(curveSet.EditorSteps.x, .01f);
        _valueStep = curveSet.EditorSteps.y;
        _showValueAlways = EditorGUILayout.Toggle("Show Always", _showValueAlways);
        EditorGUILayout.BeginHorizontal();
        _showTimes = EditorGUILayout.Toggle("Time", _showTimes);
        _showValues = EditorGUILayout.Toggle("Value", _showValues);
        EditorGUILayout.EndHorizontal();

        // Кнопка для установки размеров графика из данных кривых
        if (GUILayout.Button("Set Graph Size from Curves"))
        {
            Undo.RecordObject(curveSet, "Set Graph Size from Curves");
            curveSet.SetGraphLengthFromCurves();
            EditorUtility.SetDirty(curveSet);
        }

        // --- Вычисление общих min/max по времени и значению
        var minTime = curveSet.GraphLength.x;
        var minValue = curveSet.GraphLength.y;
        var maxTime = curveSet.GraphLength.width;
        var maxValue = curveSet.GraphLength.height;

        if (Mathf.Approximately(minTime, maxTime)) maxTime += 1f;
        if (Mathf.Approximately(minValue, maxValue)) maxValue += 1f;

        // --- Прямоугольник с отступами под подписи
        var previewHeight = curveSet.PreviewHeight;
        var previewRect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth - 20, previewHeight);
        var graphRect = new Rect(previewRect.x + _labelPadding, previewRect.y + 10, previewRect.width - _labelPadding - 10,
            previewRect.height - 20);

        DrawBackground(graphRect);
        DrawAxes(graphRect, minTime, maxTime, minValue, maxValue);

        // --- Рисуем кривые
        for (var i = 0; i < _curves.Count; i++)
        {
            DrawCurve(graphRect, _curves[i].curve, _curveColors[i % _curveColors.Length], minTime, maxTime, minValue, maxValue, i);
        }

        foreach (var (position, key) in _labelsToRender)
        {
            DrawKeyLabel(position, key);
        }

        _labelsToRender.Clear();

        HandleCurvePointAddition(graphRect, _curves, minTime, maxTime, minValue, maxValue);
        HandleCurvePointsSelection(graphRect, _curves, minTime, maxTime, minValue, maxValue);

        GUILayout.Space(5);

        var legendRect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 25);
        DrawLegend(legendRect, _curves);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Curves (Editable)", EditorStyles.boldLabel);

        // --- Стандартная отрисовка редактируемых полей
        EditorGUI.BeginChangeCheck();
        serializedObject.Update();

        base.OnInspectorGUI();

        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(curveSet);
        }
    }

    private static void DrawBackground(Rect rect)
    {
        EditorGUI.DrawRect(rect, new Color(0.15f, 0.15f, 0.15f));
        Handles.color = new Color(1f, 1f, 1f, 0.1f);

        for (var i = 1; i < 10; i++)
        {
            var t = i / 10f;
            var x = Mathf.Lerp(rect.x, rect.xMax, t);
            var y = Mathf.Lerp(rect.y, rect.yMax, t);

            Handles.DrawLine(new Vector2(x, rect.y), new Vector2(x, rect.yMax)); // вертикальные
            Handles.DrawLine(new Vector2(rect.x, y), new Vector2(rect.xMax, y)); // горизонтальные
        }
    }

    private void DrawAxes(Rect rect, float minTime, float maxTime, float minValue, float maxValue)
    {
        var divisionsX = 10;
        var divisionsY = 5;
        var labelStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 10,
            normal = { textColor = Color.gray },
            alignment = TextAnchor.MiddleRight
        };

        for (var i = 0; i <= divisionsY; i++)
        {
            var t = i / (float)divisionsY;

            // Y axis labels (значения)
            var val = Mathf.Lerp(minValue, maxValue, 1 - t);
            var y = Mathf.Lerp(rect.y, rect.yMax, t);
            var labelRect = new Rect(rect.x - 35, y - 10, 30, 20);
            GUI.Label(labelRect, val.ToString("0.##"), labelStyle);
        }

        labelStyle.alignment = TextAnchor.UpperCenter;

        for (var i = 0; i <= divisionsX; i++)
        {
            var t = i / (float)divisionsX;

            // X axis labels (время)
            var time = Mathf.Lerp(minTime, maxTime, t);
            var x = Mathf.Lerp(rect.x, rect.xMax, t);
            const float labelWidth = 40;
            const float labelHeight = 20;
            var labelRect = new Rect(x - labelWidth / 2, rect.yMax + 2, labelWidth, labelHeight);
            GUI.Label(labelRect, time.ToString("0.##"), labelStyle);
        }
    }

    private void DrawCurve(Rect rect, AnimationCurve curve, Color color, float minTime, float maxTime, float minValue, float maxValue,
        int curveIndex)
    {
        if (curve == null || curve.length == 0)
            return;

        Handles.color = color;
        var points = new Vector3[100];

        for (var i = 0; i < points.Length; i++)
        {
            var t = i / (float)(points.Length - 1);
            var time = Mathf.Lerp(minTime, maxTime, t);
            var value = curve.Evaluate(time);

            var x = Mathf.Lerp(rect.x, rect.xMax, (time - minTime) / (maxTime - minTime));
            var y = Mathf.Lerp(rect.yMax, rect.y, (value - minValue) / (maxValue - minValue));
            points[i] = new Vector3(x, y, 0);
        }

        Handles.DrawAAPolyLine(2f, points);

        var keys = curve.keys;
        var changed = false;

        for (var i = 0; i < keys.Length; i++)
        {
            var key = keys[i];

            var keyPosition = GetScreenPosition(rect, key.time, key.value, minTime, maxTime, minValue, maxValue);
            var pointRect = new Rect(keyPosition.x - 5, keyPosition.y - 5, 10, 10);

            // Подсветка выделенных ключей
            var isSelected = _selectedHandles.Contains((curveIndex, i));
            EditorGUI.DrawRect(pointRect, isSelected ? Color.white : color);
            if (isSelected)
            {
                EditorGUI.DrawRect(new Rect(keyPosition.x - 3, keyPosition.y - 3, 6, 6), color);
            }

            EditorGUIUtility.AddCursorRect(pointRect, MouseCursor.MoveArrow);

            // определяем, наведена ли мышь
            var isHover = pointRect.Contains(Event.current.mousePosition);
            var isDragging = _draggingCurveIndex == curveIndex && _draggingKeyIndex == i;

            // рисуем текст при наведении или перетаскивании
            if (isHover || isDragging || _showValueAlways)
            {
                _labelsToRender.Add((keyPosition, key));
            }

            // Удаление по ПКМ
            if (HandleKeyDeletion(pointRect, curve, keys, i))
                return;

            // Обработка начала перетаскивания
            if (HandleDragStart(pointRect, rect, curveIndex, i, minTime, maxTime, minValue, maxValue))
                Event.current.Use();

            // Обработка перетаскивания
            if (HandleDragMove(rect, curveIndex, i, curve, minTime, maxTime, minValue, maxValue))
            {
                changed = true;
                Event.current.Use();
            }

            if (HandleDragEnd(curveIndex, i))
                Event.current.Use();

            // Принудительно задаем линейную интерполяцию для ключа
            ApplyLinear(curve, i);
        }

        if (changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private Vector2 GetScreenPosition(Rect rect, float time, float value, float minTime, float maxTime, float minValue, float maxValue)
    {
        var x = Mathf.Lerp(rect.x, rect.xMax, (time - minTime) / (maxTime - minTime));
        var y = Mathf.Lerp(rect.yMax, rect.y, (value - minValue) / (maxValue - minValue));
        return new Vector2(x, y);
    }

    private Vector2 GetCurveValue(Rect rect, Vector2 screenPos, float minTime, float maxTime, float minValue, float maxValue)
    {
        var time = Mathf.Lerp(minTime, maxTime, Mathf.InverseLerp(rect.x, rect.xMax, screenPos.x));
        var value = Mathf.Lerp(minValue, maxValue, Mathf.InverseLerp(rect.yMax, rect.y, screenPos.y));
        return new Vector2(time, value);
    }

    private void DrawKeyLabel(Vector2 position, Keyframe key)
    {
        var label = string.Empty;
        if (_showTimes)
        {
            label += $"{key.time:0.###}";

            if (_showValues)
                label += " x ";
        }

        if (_showValues)
            label += $"{key.value:0.###}";

        var size = GUI.skin.label.CalcSize(new GUIContent(label));
        var labelRect = new Rect(position.x - size.x / 2, position.y - size.y - 12, size.x, size.y);
        GUI.Label(labelRect, label, EditorStyles.whiteLabel);
    }

    private bool HandleKeyDeletion(Rect pointRect, AnimationCurve curve, Keyframe[] keys, int keyIndex)
    {
        if (Event.current.type != EventType.MouseDown ||
            Event.current.button != 1 ||
            !pointRect.Contains(Event.current.mousePosition))
            return false;

        Undo.RecordObject(target, "Delete Curve Key");

        var newKeys = new List<Keyframe>(keys);
        newKeys.RemoveAt(keyIndex);
        curve.keys = newKeys.ToArray();

        // Применяем линейные тангенты
        for (var k = 0; k < curve.length; k++)
            ApplyLinear(curve, k);

        EditorUtility.SetDirty(target);
        Event.current.Use();
        return true;
    }

    private bool HandleDragStart(Rect pointRect, Rect graphRect, int curveIndex, int keyIndex,
        float minTime, float maxTime, float minValue, float maxValue)
    {
        if (_draggingKeyIndex != -1 || Event.current.type != EventType.MouseDown || !pointRect.Contains(Event.current.mousePosition))
            return false;

        Undo.RecordObject(target, "Drag Curve Key");
        GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
        _draggingCurveIndex = curveIndex;
        _draggingKeyIndex = keyIndex;

        // Проверяем, входит ли текущий ключ в выделение
        _isDraggingSelection = _selectedHandles.Contains((curveIndex, keyIndex));

        // Определяем режим Shift-интерполяции
        _isShiftInterpolation = _isDraggingSelection && Event.current.shift;

        // Если Shift-интерполяция, находим крайние точки
        if (_isShiftInterpolation)
        {
            FindEdgeHandles();
        }
        // Если мы перетаскиваем выделенные ключи, сохраняем начальные позиции всех ключей
        else if (_isDraggingSelection)
        {
            SaveSelectedKeysInitialPositions();
        }

        var curveValue = GetCurveValue(graphRect, Event.current.mousePosition, minTime, maxTime, minValue, maxValue);
        _mouseHandleDragStart = curveValue;

        return true;
    }

    private void SaveSelectedKeysInitialPositions()
    {
        _initialKeyPositions.Clear();
        foreach (var (selCIndex, selKIndex) in _selectedHandles)
        {
            var selCurve = _curves[selCIndex].curve;
            var selKey = selCurve.keys[selKIndex];
            _initialKeyPositions.Add((selCIndex, selKIndex), new Vector2(selKey.time, selKey.value));
        }
    }

    private bool HandleDragMove(Rect rect, int curveIndex, int keyIndex, AnimationCurve curve,
        float minTime, float maxTime, float minValue, float maxValue)
    {
        if (_draggingCurveIndex != curveIndex || _draggingKeyIndex != keyIndex || Event.current.type != EventType.MouseDrag)
            return false;

        Undo.RecordObject(target, "Drag Curve Key");
        var curveValue = GetCurveValue(rect, Event.current.mousePosition, minTime, maxTime, minValue, maxValue);
        var newTime = curveValue.x;
        var newValue = curveValue.y;

        // Округление по шагу
        RoundTimeAndValue(ref newTime, ref newValue);

        // Если перетаскиваем выделение
        if (_isDraggingSelection)
        {
            if (_isShiftInterpolation)
            {
                InterpolateSelectedKeys(newTime, newValue);
            }
            else
            {
                MoveSelectedKeys(newTime, newValue);
            }

            return true;
        }

        // Границы для перемещения по времени
        ChangeKey(curve, keyIndex, newTime, newValue);
        return true;
    }

    private void MoveSelectedKeys(float newTime, float newValue)
    {
        // Вычисляем смещение относительно начальной позиции
        var deltaTime = newTime - _mouseHandleDragStart.x;
        var deltaValue = newValue - _mouseHandleDragStart.y;

        // Двигаем только выделенные ключи текущей кривой
        foreach (var (selCIndex, selKIndex) in _isShiftInterpolation
                     ? _selectedHandles.Where(handle => handle.Item1 == _draggingCurveIndex)
                     : _selectedHandles)
        {
            // Получаем начальную позицию и добавляем смещение
            var initialPos = _initialKeyPositions[(selCIndex, selKIndex)];
            var adjustedTime = initialPos.x + deltaTime;
            var adjustedValue = initialPos.y + deltaValue;

            ChangeKey(_curves[selCIndex].curve, selKIndex, adjustedTime, adjustedValue);
        }
    }

    private static void ChangeKey(AnimationCurve curve, int index, float adjustedTime, float adjustedValue)
    {
        var keys = curve.keys;
        // Границы для перемещения по времени
        float minTime = index > 0 ? keys[index - 1].time + _timeStep : 0;
        float maxTime = index < keys.Length - 1 ? keys[index + 1].time - _timeStep : float.PositiveInfinity;

        // Ограничение времени между соседними ключами
        adjustedTime = Mathf.Clamp(adjustedTime, minTime, maxTime);

        RoundTimeAndValue(ref adjustedTime, ref adjustedValue);

        if (!Event.current.alt)
            adjustedTime = keys[index].time;

        // Применяем новые значения
        var newKey = new Keyframe(adjustedTime, adjustedValue);
        keys[index] = newKey;
        curve.keys = keys;

        // Задаем линейную интерполяцию
        ApplyLinear(curve, index);
    }

    private void InterpolateSelectedKeys(float newTime, float newValue)
    {
        // Если нет определенных крайних точек, выходим
        if (_edgeHandlesPerCurve.Count == 0)
            return;

        // Получаем исходную позицию перетаскиваемой точки
        var draggedInitialPos = _initialKeyPositions[(_draggingCurveIndex, _draggingKeyIndex)];
        
        // Перебираем только выделенные ключи текущей кривой для интерполяции
        foreach (var (selCIndex, selKIndex) in _selectedHandles.Where(handle => handle.Item1 == _draggingCurveIndex))
        {
            var selCurve = _curves[selCIndex].curve;

            // Проверяем, есть ли кривая в словаре с крайними точками
            if (!_edgeHandlesPerCurve.TryGetValue(selCIndex, out var edgeIndices))
            {
                // Для кривых без определенных крайних точек просто двигаем точку как обычно
                if (selCIndex == _draggingCurveIndex && selKIndex == _draggingKeyIndex)
                    ChangeKey(selCurve, selKIndex, newTime, newValue);
                continue;
            }

            var (leftKeyIndex, rightKeyIndex) = edgeIndices;

            // Если это перетаскиваемая точка, устанавливаем новое значение напрямую
            if (selCIndex == _draggingCurveIndex && selKIndex == _draggingKeyIndex)
            {
                ChangeKey(selCurve, selKIndex, newTime, newValue);
                continue;
            }

            // Для остальных точек применяем пропорциональное изменение
            var initialPos = _initialKeyPositions[(selCIndex, selKIndex)];
            var leftInitialPos = _initialKeyPositions[(selCIndex, leftKeyIndex)];
            var rightInitialPos = _initialKeyPositions[(selCIndex, rightKeyIndex)];
            
            // Вычисляем полный диапазон от левой до правой крайней точки
            var fullRangeStart = leftInitialPos.y;
            var fullRangeEnd = rightInitialPos.y;
            var fullRangeSize = fullRangeEnd - fullRangeStart;
            
            // Если полный диапазон равен нулю, не делаем интерполяцию
            if (Mathf.Abs(fullRangeSize) < 0.001f)
            {
                ChangeKey(selCurve, selKIndex, initialPos.x, initialPos.y);
                continue;
            }
            
            // Находим позицию текущей точки в полном диапазоне (от 0 до 1)
            var t = (initialPos.y - fullRangeStart) / fullRangeSize;
            
            // Вычисляем новый диапазон после перетаскивания
            var newRangeStart = leftInitialPos.y;
            var newRangeEnd = rightInitialPos.y;
            
            // Если перетаскиваемая точка не является крайней, обновляем соответствующую границу
            if (_draggingKeyIndex == leftKeyIndex)
            {
                newRangeStart = newValue;
            }
            else if (_draggingKeyIndex == rightKeyIndex)
            {
                newRangeEnd = newValue;
            }
            else
            {
                // Если перетаскиваемая точка в середине, нужно интерполировать границы
                var draggedT = (draggedInitialPos.y - fullRangeStart) / fullRangeSize;
                var currentT = (initialPos.y - fullRangeStart) / fullRangeSize;
                
                if (currentT <= draggedT)
                {
                    // Точка слева от перетаскиваемой - интерполируем от левой границы до новой позиции
                    var leftToMidT = draggedT > 0 ? currentT / draggedT : 0;
                    newRangeStart = fullRangeStart;
                    newRangeEnd = newValue;
                    t = leftToMidT;
                }
                else
                {
                    // Точка справа от перетаскиваемой - интерполируем от новой позиции до правой границы
                    var midToRightT = draggedT < 1 ? (currentT - draggedT) / (1 - draggedT) : 0;
                    newRangeStart = newValue;
                    newRangeEnd = fullRangeEnd;
                    t = midToRightT;
                }
            }
            
            // Применяем интерполяцию в новом диапазоне
            var interpolateTime = initialPos.x;
            var interpolateValue = Mathf.Lerp(newRangeStart, newRangeEnd, t);

            ChangeKey(selCurve, selKIndex, interpolateTime, interpolateValue);
        }
    }

    private bool HandleDragEnd(int curveIndex, int keyIndex)
    {
        if (_draggingCurveIndex != curveIndex || _draggingKeyIndex != keyIndex || Event.current.type != EventType.MouseUp)
            return false;

        Undo.RecordObject(target, "Drag Curve Key");
        _draggingCurveIndex = -1;
        _draggingKeyIndex = -1;
        _isDraggingSelection = false;
        _isShiftInterpolation = false;
        _edgeHandlesPerCurve.Clear(); // Очищаем словарь крайних точек
        _initialKeyPositions.Clear();
        GUIUtility.hotControl = 0;
        return true;
    }

    private void HandleCurvePointAddition(Rect graphRect, List<(string name, AnimationCurve curve)> curves,
        float minTime, float maxTime, float minValue, float maxValue)
    {
        if (Event.current.type != EventType.MouseDown ||
            Event.current.button != 0 ||
            !Event.current.control ||
            !graphRect.Contains(Event.current.mousePosition))
            return;

        var curveValue = GetCurveValue(graphRect, Event.current.mousePosition, minTime, maxTime, minValue, maxValue);
        var time = curveValue.x;
        var value = curveValue.y;

        RoundTimeAndValue(ref time, ref value);

        var closestCurveIndex = FindClosestCurve(curves, time, value);

        if (closestCurveIndex < 0)
            return;

        AddKeyToCurve(curves[closestCurveIndex].curve, time);
        Event.current.Use();
    }

    private static int FindClosestCurve(List<(string name, AnimationCurve curve)> curves, float time, float value)
    {
        var minDistSqr = float.MaxValue;
        var closestCurveIndex = -1;

        for (var i = 0; i < curves.Count; i++)
        {
            var curve = curves[i].curve;
            var keys = curve.keys;
            for (var j = 0; j < keys.Length - 1; j++)
            {
                var key0 = keys[j];
                var key1 = keys[j + 1];
                var p0 = new Vector2(key0.time, key0.value);
                var p1 = new Vector2(key1.time, key1.value);

                var distSqr = MyHandleUtility.DistancePointLine(new Vector3(time, value), p0, p1);

                if (distSqr >= minDistSqr)
                    continue;

                minDistSqr = distSqr;
                closestCurveIndex = i;
            }
        }

        return closestCurveIndex;
    }

    private void AddKeyToCurve(AnimationCurve curve, float time)
    {
        Undo.RecordObject(target, "Added Curve Key");

        var value = curve.Evaluate(time);
        RoundValue(ref value);
        var newKey = new Keyframe(time, value);
        var newKeys = new List<Keyframe>(curve.keys) { newKey };
        newKeys.Sort((a, b) => a.time.CompareTo(b.time));
        curve.keys = newKeys.ToArray();

        for (var k = 0; k < curve.length; k++)
            ApplyLinear(curve, k);

        EditorUtility.SetDirty(target);
    }

    private void HandleCurvePointsSelection(Rect graphRect, List<(string name, AnimationCurve curve)> curves,
        float minTime, float maxTime, float minValue, float maxValue)
    {
        if (Event.current.type == EventType.MouseDown &&
            Event.current.button == 0 &&
            _draggingKeyIndex == -1 &&
            graphRect.Contains(Event.current.mousePosition))
        {
            _mouseSelectionRectStart = Event.current.mousePosition;
            _isDrawingSelectionRect = true;
            _selectedHandles.Clear();
        }

        if (_isDrawingSelectionRect && graphRect.Contains(Event.current.mousePosition))
        {
            var mouse = Event.current.mousePosition;

            var mx0 = Mathf.Min(_mouseSelectionRectStart.x, mouse.x);
            var my0 = Mathf.Min(_mouseSelectionRectStart.y, mouse.y);
            var mx1 = Mathf.Max(_mouseSelectionRectStart.x, mouse.x);
            var my1 = Mathf.Max(_mouseSelectionRectStart.y, mouse.y);

            var selectionRect = Rect.MinMaxRect(mx0, my0, mx1, my1);
            var col = new Color(0.3f, 0.5f, 1f, 0.25f);
            EditorGUI.DrawRect(selectionRect, col);

            Rect curveValueRect = CalculateCurveValueRect(graphRect, mx0, my0, mx1, my1, minTime, maxTime, minValue, maxValue);
            UpdateSelectedHandles(curves, curveValueRect);
        }

        if (Event.current.type == EventType.MouseUp && _isDrawingSelectionRect)
        {
            _isDrawingSelectionRect = false;
        }
    }

    private static Rect CalculateCurveValueRect(Rect graphRect, float mx0, float my0, float mx1, float my1,
        float minTime, float maxTime, float minValue, float maxValue)
    {
        var startTime = Mathf.Lerp(minTime, maxTime, Mathf.InverseLerp(graphRect.x, graphRect.xMax, mx0));
        var startValue = Mathf.Lerp(minValue, maxValue, Mathf.InverseLerp(graphRect.yMax, graphRect.y, my0));

        var endTime = Mathf.Lerp(minTime, maxTime, Mathf.InverseLerp(graphRect.x, graphRect.xMax, mx1));
        var endValue = Mathf.Lerp(minValue, maxValue, Mathf.InverseLerp(graphRect.yMax, graphRect.y, my1));

        var rx0 = Mathf.Min(startTime, endTime);
        var ry0 = Mathf.Min(startValue, endValue);
        var rx1 = Mathf.Max(startTime, endTime);
        var ry1 = Mathf.Max(startValue, endValue);

        return new Rect(rx0, ry0, rx1 - rx0, ry1 - ry0);
    }

    private void UpdateSelectedHandles(List<(string name, AnimationCurve curve)> curves, Rect curveValueRect)
    {
        _selectedHandles.Clear();

        for (var i = 0; i < curves.Count; i++)
        {
            var curve = curves[i].curve;
            var keys = curve.keys;
            for (var j = 0; j < keys.Length; j++)
            {
                var key0 = keys[j];
                var p0 = new Vector2(key0.time, key0.value);

                if (curveValueRect.Contains(p0))
                    _selectedHandles.Add((i, j));
            }
        }
    }

    private void DrawLegend(Rect rect, List<(string name, AnimationCurve curve)> curves)
    {
        var padding = 10f;
        var x = rect.x + padding;
        var y = rect.y + 5f;
        var colorBoxSize = 10f;
        var spacing = 8f;

        var labelStyle = new GUIStyle(EditorStyles.label)
        {
            fontSize = 11,
            normal = { textColor = Color.white }
        };

        for (var i = 0; i < curves.Count; i++)
        {
            var (name, _) = curves[i];
            var color = _curveColors[i % _curveColors.Length];

            // Цветной квадратик
            EditorGUI.DrawRect(new Rect(x, y + 2, colorBoxSize, colorBoxSize), color);

            // Текст
            var labelWidth = labelStyle.CalcSize(new GUIContent(name)).x;
            GUI.Label(new Rect(x + colorBoxSize + 4, y, labelWidth, 20), name, labelStyle);

            x += colorBoxSize + labelWidth + spacing + 4;
        }
    }

    private static void RoundTimeAndValue(ref float time, ref float value)
    {
        if (_timeStep > 0)
            time = Mathf.Round(time / _timeStep) * _timeStep;

        if (_valueStep > 0)
            RoundValue(ref value);
    }

    private static void RoundValue(ref float value) => value = Mathf.Round(value / _valueStep) * _valueStep;

    private static void ApplyLinear(AnimationCurve curve, int k)
    {
        AnimationUtility.SetKeyLeftTangentMode(curve, k, AnimationUtility.TangentMode.Linear);
        AnimationUtility.SetKeyRightTangentMode(curve, k, AnimationUtility.TangentMode.Linear);
    }

    private void FindEdgeHandles()
    {
        _edgeHandlesPerCurve.Clear();

        // Группируем выделенные ключи по индексу кривой
        var keysByCurve = new Dictionary<int, List<int>>();

        foreach (var (selCIndex, selKIndex) in _selectedHandles)
        {
            if (!keysByCurve.ContainsKey(selCIndex))
                keysByCurve[selCIndex] = new List<int>();

            keysByCurve[selCIndex].Add(selKIndex);
        }

        // Для каждой кривой находим крайние точки
        foreach (var (curveIndex, keyIndices) in keysByCurve)
        {
            if (keyIndices.Count < 2)
                continue; // Минимум 2 точки нужны для интерполяции

            // Сортируем индексы по времени
            keyIndices.Sort((a, b) =>
                _curves[curveIndex].curve.keys[a].time.CompareTo(_curves[curveIndex].curve.keys[b].time));

            // Сохраняем крайние индексы
            var leftKeyIndex = keyIndices[0];
            var rightKeyIndex = keyIndices[^1];

            _edgeHandlesPerCurve[curveIndex] = (leftKeyIndex, rightKeyIndex);
        }

        // Сохраняем исходные позиции всех ключей для интерполяции
        SaveSelectedKeysInitialPositions();
    }
}
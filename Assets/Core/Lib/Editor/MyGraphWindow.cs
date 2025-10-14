using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Lib.Utils.MyGraph;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Lib
{
    public class MyGraphWindow : EditorWindow
    {
        internal List<Type> possibleTypes = new();
        internal Type selectedType;
        private MyGraphView _graphView;
        private ObjectField _graphField;
        private MyAbstractGraph _graph;

        public static void Open()
        {
            var wnd = GetWindow<MyGraphWindow>();
            wnd.titleContent = new GUIContent("My Graph");
        }

        public static void OpenWithGraph(MyAbstractGraph graph)
        {
            var window = GetWindow<MyGraphWindow>();
            window.titleContent = new GUIContent("My Graph");
            window.SetGraph(graph);
            window.Focus();
        }

        private void OnEnable()
        {
            ConstructUI();
            SetGraph(Selection.activeObject as MyAbstractGraph);
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }

        private void OnFocus()
        {
            TryAutoAssignGraphFromSelection();
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
        }

        private void OnUndoRedoPerformed()
        {
            if (_graphView != null && _graph != null)
                _graphView.SetGraph(_graph);
        }

        private void ConstructUI()
        {
            var toolbar = new Toolbar();
            _graphField = new ObjectField("Graph") { objectType = typeof(MyAbstractGraph) };
            _graphField.RegisterValueChangedCallback(evt => SetGraph(evt.newValue as MyAbstractGraph));
            toolbar.Add(_graphField);

            var addBtn = new ToolbarButton(() =>
            {
                if (_graph == null)
                    return;
                var pos = _graphView.GetLastMouseContentPositionOrCenter();
                var node = _graph.CreateNode("Node", pos, selectedType);
                EditorUtility.SetDirty(_graph);
                _graphView.AddNodeView(node);
            }) { text = "+ Node" };
            toolbar.Add(addBtn);

            var saveBtn = new ToolbarButton(() =>
            {
                if (_graph == null)
                    return;
                EditorUtility.SetDirty(_graph);
            }) { text = "Save" };
            toolbar.Add(saveBtn);

            var frameBtn = new ToolbarButton(() => _graphView.FrameAll()) { text = "Frame" };
            toolbar.Add(frameBtn);

            rootVisualElement.Add(toolbar);

            _graphView = new MyGraphView(this);
            rootVisualElement.Add(_graphView);
        }

        private void TryAutoAssignGraphFromSelection()
        {
            var selected = Selection.activeObject as MyAbstractGraph;
            if (selected != null && selected != _graph)
                SetGraph(selected);
        }

        public void SetGraph(MyAbstractGraph graph)
        {
            _graph = graph;
            _graphField?.SetValueWithoutNotify(_graph);
            _graphView?.SetGraph(_graph);
            possibleTypes = new List<Type>();

            Type dataBaseType = null;

            var graphType = _graph.GetType().BaseType!;
            dataBaseType = graphType.GetGenericArguments()[0];

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (var a = 0; a < assemblies.Length; a++)
            {
                var asm = assemblies[a];
                var types = asm.GetTypes();
                for (var i = 0; i < types.Length; i++)
                {
                    var t = types[i];
                    if (t.IsAbstract)
                        continue;
                    if (t.IsGenericTypeDefinition)
                        continue;
                    if (!dataBaseType.IsAssignableFrom(t))
                        continue;
                    if (!t.IsValueType && t.GetConstructor(Type.EmptyTypes) == null)
                        continue;
                    var exists = false;
                    for (var k = 0; k < possibleTypes.Count; k++)
                    {
                        if (possibleTypes[k] == t)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                        possibleTypes.Add(t);
                }
            }

            possibleTypes.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
        }
    }

    internal static class MyGraphAssetOpener
    {
        [OnOpenAsset]
        public static bool OnOpen(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId) as MyAbstractGraph;
            if (obj == null)
                return false;

            MyGraphWindow.OpenWithGraph(obj);
            return true;
        }
    }

    public class MyGraphView : GraphView
    {
        [Serializable]
        private class DataWrapper : ScriptableObject
        {
            [SerializeReference] public object data;
        }

        private readonly MyGraphWindow _window;
        private MyAbstractGraph _graph;
        private readonly Dictionary<string, MyNodeView> _idToNodeView = new();
        private Vector2 _lastMouseInContent;
        private bool _suppressGraphViewChanged;
        internal MyAbstractGraph Graph => _graph;

        public MyGraphView(MyGraphWindow window)
        {
            _window = window;

            this.StretchToParentSize();
            focusable = true;
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            graphViewChanged = OnGraphViewChanged;

            nodeCreationRequest = _ =>
            {
                if (_graph == null)
                    return;
                var pos = GetLastMouseContentPositionOrCenter();
                RecordUndo("Create Node");
                var node = _graph.CreateNode("Node", pos, window.selectedType);
                EditorUtility.SetDirty(_graph);
                AddNodeView(node);
            };

            RegisterCallback<MouseMoveEvent>(OnMouseMove);
            RegisterCallback<MouseDownEvent>(OnMouseDown);
            RegisterCallback<KeyDownEvent>(OnKeyDown);

            serializeGraphElements = SerializeGraphElements;
            canPasteSerializedData = CanPasteSerializedData;
            unserializeAndPaste = UnserializeAndPaste;

            this.AddManipulator(new ContextualMenuManipulator(ctx =>
            {
                // Меню на пустом месте
                if (ReferenceEquals(ctx.target, this) || ReferenceEquals(ctx.target, contentViewContainer))
                {
                    var items = ctx.menu.MenuItems();
                    items.Clear();

                    // Секция создания нод по типам
                    if (_graph != null && _window.possibleTypes != null && _window.possibleTypes.Count > 0)
                    {
                        for (var i = 0; i < _window.possibleTypes.Count; i++)
                        {
                            var t = _window.possibleTypes[i];
                            ctx.menu.AppendAction($"Create {t.Name}", _ =>
                            {
                                var prevSelected = _window.selectedType;
                                _window.selectedType = t;
                                var pos = GetLastMouseContentPositionOrCenter();
                                RecordUndo("Create Node");
                                var node = _graph.CreateNode("Node", pos, t);
                                EditorUtility.SetDirty(_graph);
                                AddNodeView(node);
                                _window.selectedType = prevSelected;
                            }, _ => DropdownMenuAction.Status.Normal);
                        }
                    }

                    // Общие действия
                    ctx.menu.AppendSeparator();
                    ctx.menu.AppendAction("Paste", _ => PasteFromClipboard(),
                        CanPasteSerializedData(EditorGUIUtility.systemCopyBuffer)
                            ? DropdownMenuAction.Status.Normal
                            : DropdownMenuAction.Status.Disabled);
                    return;
                }

                // Меню для выделенных элементов
                ctx.menu.AppendAction("Copy", _ => CopySelectionToClipboard(),
                    selection != null && selection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
                ctx.menu.AppendAction("Paste", _ => PasteFromClipboard(),
                    CanPasteSerializedData(EditorGUIUtility.systemCopyBuffer)
                        ? DropdownMenuAction.Status.Normal
                        : DropdownMenuAction.Status.Disabled);
                ctx.menu.AppendAction("Duplicate", _ => DuplicateSelection(),
                    selection != null && selection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
            }));
        }

        public void SetGraph(MyAbstractGraph graph)
        {
            _graph = graph;

            _suppressGraphViewChanged = true;
            try
            {
                var all = new List<GraphElement>();
                foreach (var el in graphElements)
                    all.Add(el);
                DeleteElements(all);
                ClearNodesCache();

                if (_graph == null)
                    return;

                var nodes = _graph.GetNodes();
                if (nodes == null)
                    return;

                for (var i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    AddNodeView(node);
                }

                // edges
                for (var i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var fromView = _idToNodeView[node.id];
                    var choices = node.outputs;
                    for (var c = 0; c < choices.Count; c++)
                    {
                        var toId = choices[c];
                        if (string.IsNullOrEmpty(toId))
                            continue;
                        if (!_idToNodeView.TryGetValue(toId, out var toView))
                            continue;

                        var fromPort = fromView.GetOutputPortAtIndex(c);
                        var e = fromPort.ConnectTo(toView.input);
                        AddElement(e);
                    }
                }
            }
            finally
            {
                _suppressGraphViewChanged = false;
            }
        }

        public void AddNodeView(MyAbstractGraph.Node node)
        {
            var nodeView = new MyNodeView(_graph, node);
            AddElement(nodeView);
            _idToNodeView[node.id] = nodeView;
        }

        private void ClearNodesCache()
        {
            _idToNodeView.Clear();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange change)
        {
            if (_suppressGraphViewChanged || _graph == null)
                return change;
            var shouldRebuild = false;

            if (change.elementsToRemove != null)
            {
                var kept = new List<GraphElement>();
                for (var i = 0; i < change.elementsToRemove.Count; i++)
                {
                    var el = change.elementsToRemove[i];
                    if (el is MyNodeView nv)
                    {
                        RecordUndo("Remove Node");
                        _graph.RemoveNode(nv.node.id);
                        _idToNodeView.Remove(nv.node.id);
                        EditorUtility.SetDirty(_graph);
                        shouldRebuild = true;
                    }
                    else if (el is Edge edge)
                    {
                        var from = (MyNodeView)edge.output.node;
                        var idx = edge.output.userData is int iv ? iv : -1;
                        if (idx >= 0)
                        {
                            // Remove choice fully to keep ordering consistent
                            RecordUndo("Remove Choice");
                            _graph.RemoveOutput(from.node.id, idx);
                            EditorUtility.SetDirty(_graph);
                            shouldRebuild = true;
                        }
                    }
                }

                if (kept.Count > 0)
                {
                    var newList = new List<GraphElement>();
                    for (var i = 0; i < change.elementsToRemove.Count; i++)
                    {
                        var el = change.elementsToRemove[i];
                        var isKept = false;
                        for (var k = 0; k < kept.Count; k++)
                        {
                            if (ReferenceEquals(el, kept[k]))
                            {
                                isKept = true;
                                break;
                            }
                        }

                        if (!isKept) newList.Add(el);
                    }

                    change.elementsToRemove = newList;
                }

                EditorUtility.SetDirty(_graph);
            }

            if (change.edgesToCreate != null)
            {
                var filtered = new List<Edge>();
                for (var i = 0; i < change.edgesToCreate.Count; i++)
                {
                    var e = change.edgesToCreate[i];
                    var from = (MyNodeView)e.output.node;
                    var to = (MyNodeView)e.input.node;

                    var idx = e.output.userData is int iv ? iv : -2;
                    if (idx >= 0)
                    {
                        RecordUndo("Connect Choice");
                        _graph.ConnectOutput(from.node.id, idx, to.node.id);
                        EditorUtility.SetDirty(_graph);
                        filtered.Add(e);
                    }
                    else
                    {
                        RecordUndo("Connect Choice");
                        var assigned = _graph.ConnectOutput(from.node.id, -1, to.node.id);
                        EditorUtility.SetDirty(_graph);
                        // Assign correct index to the port to ensure GraphView's edge points to the right slot after rebuild
                        e.output.userData = assigned;
                        filtered.Add(e);
                    }
                }

                change.edgesToCreate = filtered;
                EditorUtility.SetDirty(_graph);
                ScheduleRebuild();
            }

            if (shouldRebuild)
                ScheduleRebuild();

            if (change.movedElements != null)
            {
                for (var i = 0; i < change.movedElements.Count; i++)
                {
                    var movedElement = change.movedElements[i];
                    var movedNodeView = movedElement as MyNodeView;
                    if (movedNodeView == null)
                        continue;
                    var rect = movedNodeView.GetPosition();
                    movedNodeView.node.position = rect.position;
                }

                EditorUtility.SetDirty(_graph);
            }

            return change;
        }

        private bool _rebuildScheduled;
        private List<string> _pendingSelectIds;

        private void RecordUndo(string action) => Undo.RegisterCompleteObjectUndo(_graph, action);

        private static object CloneData(object source)
        {
            if (source == null)
                return null;

            var sourceWrapper = ScriptableObject.CreateInstance<DataWrapper>();
            sourceWrapper.hideFlags = HideFlags.DontSave;
            sourceWrapper.data = source;

            var json = EditorJsonUtility.ToJson(sourceWrapper);

            var cloneWrapper = ScriptableObject.CreateInstance<DataWrapper>();
            cloneWrapper.hideFlags = HideFlags.DontSave;
            EditorJsonUtility.FromJsonOverwrite(json, cloneWrapper);

            var clone = cloneWrapper.data;

            ScriptableObject.DestroyImmediate(sourceWrapper);
            ScriptableObject.DestroyImmediate(cloneWrapper);

            return clone;
        }

        public static object GetNodeData(MyAbstractGraph.Node node)
        {
            if (node == null)
                return null;
            var field = node.GetType().GetField("data",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
                return null;
            return field.GetValue(node);
        }

        private static void SetNodeData(MyAbstractGraph.Node node, object data)
        {
            if (node == null)
                return;
            var field = node.GetType().GetField("data",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
                return;
            field.SetValue(node, data);
        }

        private void ScheduleRebuild()
        {
            if (_rebuildScheduled)
                return;
            _rebuildScheduled = true;
            EditorApplication.delayCall += () =>
            {
                _rebuildScheduled = false;
                if (_graph == null)
                    return;
                SetGraph(_graph);
                if (_pendingSelectIds != null && _pendingSelectIds.Count > 0)
                {
                    ClearSelection();
                    foreach (var id in _pendingSelectIds)
                    {
                        if (_idToNodeView.TryGetValue(id, out var nv))
                            AddToSelection(nv);
                    }

                    _pendingSelectIds = null;
                }
            };
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatible = new List<Port>();
            foreach (var portElement in ports)
            {
                var port = portElement;
                if (port == startPort)
                    continue;
                if (port.node == startPort.node)
                    continue;
                if (port.direction == startPort.direction)
                    continue;
                if (startPort.portType != null && port.portType != null && startPort.portType != port.portType)
                    continue;
                compatible.Add(port);
            }

            return compatible;
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            _lastMouseInContent = contentViewContainer.WorldToLocal(evt.mousePosition);
        }

        private void OnMouseDown(MouseDownEvent evt)
        {
            _lastMouseInContent = contentViewContainer.WorldToLocal(evt.mousePosition);
        }

        public Vector2 GetLastMouseContentPositionOrCenter()
        {
            if (_lastMouseInContent != default)
                return _lastMouseInContent;

            var centerWorld = new Vector2(_window.position.width * 0.5f, _window.position.height * 0.5f);
            return contentViewContainer.WorldToLocal(centerWorld);
        }

        private void OnKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode != KeyCode.F2)
            {
                // Shortcuts
                if (evt.actionKey && evt.keyCode == KeyCode.C)
                {
                    CopySelectionToClipboard();
                    evt.StopPropagation();
                    return;
                }

                if (evt.actionKey && evt.keyCode == KeyCode.V)
                {
                    PasteFromClipboard();
                    evt.StopPropagation();
                    return;
                }

                if (evt.actionKey && evt.keyCode == KeyCode.D)
                {
                    DuplicateSelection();
                    evt.StopPropagation();
                    return;
                }

                return;
            }

            if (selection == null || selection.Count != 1)
                return;
            var nv = selection[0] as MyNodeView;
            if (nv == null)
                return;

            nv.BeginRename();
            evt.StopPropagation();
        }

        internal void RebuildAndReselect(string nodeId)
        {
            var g = _graph;
            SetGraph(g);
            ClearSelection();
            if (!string.IsNullOrEmpty(nodeId) && _idToNodeView.TryGetValue(nodeId, out var nv))
                AddToSelection(nv);
            Focus();
        }

        // Copy/Paste/Duplicate support
        private string SerializeGraphElements(IEnumerable<GraphElement> elements)
        {
            var nodeViews = elements.OfType<MyNodeView>().ToList();
            if (nodeViews.Count == 0)
                return string.Empty;

            var selectedIds = new HashSet<string>(nodeViews.Select(n => n.node.id));
            var payload = new CopyPayload { nodes = new List<NodeCopy>() };
            foreach (var nv in nodeViews)
            {
                var node = nv.node;
                var copy = new NodeCopy
                {
                    id = node.id,
                    title = node.title,
                    position = node.position,
                    choices = new List<string>(),
                    uiExpanded = node.uiExpanded
                };
                for (var i = 0; i < node.outputs.Count; i++)
                {
                    var choice = node.outputs[i];
                    copy.choices.Add(selectedIds.Contains(choice) ? choice : null);
                }

                payload.nodes.Add(copy);
            }

            return JsonUtility.ToJson(payload);
        }

        private bool CanPasteSerializedData(string data)
        {
            if (string.IsNullOrEmpty(data)) return false;
            try
            {
                JsonUtility.FromJson<CopyPayload>(data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void UnserializeAndPaste(string operationName, string data)
        {
            if (_graph == null)
                return;
            var payload = JsonUtility.FromJson<CopyPayload>(data);
            if (payload == null || payload.nodes == null || payload.nodes.Count == 0)
                return;

            // compute offset to mouse position
            var avg = Vector2.zero;
            for (var i = 0; i < payload.nodes.Count; i++) avg += payload.nodes[i].position;
            avg /= payload.nodes.Count;
            var target = GetLastMouseContentPositionOrCenter();
            var offset = target - avg;

            var idMap = new Dictionary<string, string>();
            var createdIds = new List<string>();
            // create nodes
            RecordUndo("Paste Nodes");
            foreach (var n in payload.nodes)
            {
                var original = _graph.GetNodes().FirstOrDefault(x => x.id == n.id);
                var originalData = GetNodeData(original);
                var createType = originalData?.GetType() ?? _window.selectedType;

                var newNode = _graph.CreateNode(n.title, n.position + offset, createType);
                newNode.uiExpanded = n.uiExpanded;

                if (originalData != null)
                {
                    var clonedData = CloneData(originalData);
                    SetNodeData(newNode, clonedData);
                }

                // replicate choices count
                newNode.outputs.Clear();
                for (var i = 0; i < n.choices.Count; i++)
                {
                    newNode.outputs.Add(null);
                }

                idMap[n.id] = newNode.id;
                createdIds.Add(newNode.id);
            }

            // connect internal choices
            foreach (var n in payload.nodes)
            {
                var fromId = idMap[n.id];
                for (var i = 0; i < n.choices.Count; i++)
                {
                    var toOld = n.choices[i];
                    if (string.IsNullOrEmpty(toOld)) continue;
                    if (!idMap.TryGetValue(toOld, out var toNew)) continue;
                    _graph.ConnectOutput(fromId, i, toNew);
                }
            }

            EditorUtility.SetDirty(_graph);
            _pendingSelectIds = createdIds;
            ScheduleRebuild();
        }

        private void CopySelectionToClipboard()
        {
            if (selection == null || selection.Count == 0) return;
            var json = SerializeGraphElements(selection.Cast<GraphElement>());
            EditorGUIUtility.systemCopyBuffer = json;
        }

        private void PasteFromClipboard()
        {
            var data = EditorGUIUtility.systemCopyBuffer;
            if (!CanPasteSerializedData(data)) return;
            UnserializeAndPaste("Paste", data);
        }

        private void DuplicateSelection()
        {
            if (selection == null || selection.Count == 0) return;
            var json = SerializeGraphElements(selection.Cast<GraphElement>());
            UnserializeAndPaste("Duplicate", json);
        }

        [Serializable]
        private class CopyPayload
        {
            public List<NodeCopy> nodes;
        }

        [Serializable]
        private class NodeCopy
        {
            public string id;
            public string title;
            public Vector2 position;
            public List<string> choices;
            public bool uiExpanded;
        }
    }

    public class MyNodeView : Node
    {
        private class NodeDataWrapper : ScriptableObject
        {
            [SerializeReference] public object data;
        }

        public readonly MyAbstractGraph.Node node;
        public readonly Port input;
        private readonly List<Port> _outputs = new();
        private TextField _renameField;
        private readonly MyAbstractGraph _graph;
        private NodeDataWrapper _wrapper;
        private SerializedObject _wrapperSO;
        private Type _dataType;
        private VisualElement _dataContainer;

        public MyNodeView(MyAbstractGraph graph, MyAbstractGraph.Node node)
        {
            _graph = graph;
            this.node = node;
            title = string.IsNullOrEmpty(node.title) ? "Node" : node.title;

            input = InstantiatePort(Orientation.Horizontal,
                Direction.Input, Port.Capacity.Multi, typeof(int));
            input.portName = "In";
            inputContainer.Add(input);

            EnsureOutputPorts();
            BuildDataInspector();

            var pos = new Rect(node.position, new Vector2(150, 100));
            SetPosition(pos);
        }

        private void BuildDataInspector()
        {
            _dataContainer?.RemoveFromHierarchy();

            _dataType = ResolveGraphDataType(_graph);
            if (_dataType == null)
                return;

            if (_wrapper == null)
            {
                _wrapper = ScriptableObject.CreateInstance<NodeDataWrapper>();
                _wrapper.hideFlags = HideFlags.DontSave;
            }

            _wrapper.data = MyGraphView.GetNodeData(node);
            _wrapperSO = new SerializedObject(_wrapper);

            var typeName = _dataType.Name;
            var dataFoldout = new Foldout { text = typeName, value = node.uiExpanded };
            var prop = _wrapperSO.FindProperty("data");
            var iterator = prop.Copy();
            var endProp = prop.GetEndProperty();
            var enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                if (SerializedProperty.EqualContents(iterator, endProp))
                    break;
                if (iterator.depth <= prop.depth)
                    break;
                var childCopy = iterator.Copy();
                var childField = new PropertyField(childCopy, childCopy.displayName);
                childField.Bind(_wrapperSO);
                // Save on any serialized property change bubbling from the field
                childField.RegisterCallback<SerializedPropertyChangeEvent>(_ => SaveNodeData());
                // Prevent Delete/Backspace from bubbling up to GraphView when editing data
                childField.RegisterCallback<KeyDownEvent>(e =>
                {
                    if (e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.Backspace)
                        e.StopPropagation();
                });
                dataFoldout.Add(childField);
                enterChildren = false;
            }

            dataFoldout.RegisterValueChangedCallback(evt =>
            {
                node.uiExpanded = evt.newValue;
                EditorUtility.SetDirty(_graph);
            });
            // Also listen at the foldout scope to catch nested changes (arrays add/remove, pickers, etc.)
            dataFoldout.RegisterCallback<SerializedPropertyChangeEvent>(_ => SaveNodeData());
            dataFoldout.RegisterCallback<KeyDownEvent>(e =>
            {
                if (e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.Backspace)
                    e.StopPropagation();
            });
            _dataContainer = dataFoldout;
            mainContainer.Add(_dataContainer);
        }

        private void SaveNodeData()
        {
            _wrapperSO.ApplyModifiedProperties();
            _graph.SetDataObject(node.id, _wrapper.data);
            EditorUtility.SetDirty(_graph);
        }

        private static Type ResolveGraphDataType(MyAbstractGraph graph)
        {
            var current = graph.GetType();
            while (current != null)
            {
                if (current.IsGenericType)
                {
                    var definition = current.GetGenericTypeDefinition();
                    if (definition == typeof(MyAbstractGraph<>) || definition == typeof(MyAbstractSimpleGraph<>))
                        return current.GetGenericArguments()[0];
                }

                current = current.BaseType;
            }

            var nodes = graph.GetNodes();
            if (nodes == null || nodes.Count == 0)
                return null;

            var nodeType = nodes[0].GetType();
            var field = nodeType.GetField("data", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field == null)
                return null;

            return field.FieldType;
        }

        private Port CreateOutputPort()
        {
            var p = InstantiatePort(Orientation.Horizontal,
                Direction.Output, Port.Capacity.Single, typeof(int));
            outputContainer.Add(p);
            return p;
        }

        private void EnsureOutputPorts()
        {
            // Placeholder is present only if there is no empty last slot
            var hasTrailingEmpty = node.outputs.Count > 0 && string.IsNullOrEmpty(node.outputs[node.outputs.Count - 1]);
            var needed = node.outputs.Count + (hasTrailingEmpty ? 0 : 1);

            while (_outputs.Count < needed)
            {
                var p = CreateOutputPort();
                _outputs.Add(p);
            }

            while (_outputs.Count > needed)
            {
                var last = _outputs[_outputs.Count - 1];
                last.DisconnectAll();
                last.RemoveFromHierarchy();
                _outputs.RemoveAt(_outputs.Count - 1);
            }

            for (var i = 0; i < node.outputs.Count; i++)
            {
                var p = _outputs[i];
                p.userData = i;
                var toId = node.outputs[i];
                var toName = string.IsNullOrEmpty(toId) ? null : FindNodeTitle(toId);
                p.portName = toName ?? "Choice";
            }

            // Add placeholder if needed
            if (!hasTrailingEmpty)
            {
                var placeholder = _outputs[_outputs.Count - 1];
                placeholder.userData = -1;
                placeholder.portName = "+";
            }

            RefreshExpandedState();
            RefreshPorts();
        }

        public Port GetOutputPortAtIndex(int index)
        {
            if (index < 0 || index >= node.outputs.Count) return null;
            return _outputs[index];
        }

        public void OnChoiceCreatedOnPlaceholder(int assignedIndex, Port placeholderPort)
        {
            placeholderPort.userData = assignedIndex;
            var toId = node.outputs[assignedIndex];
            var toName = string.IsNullOrEmpty(toId) ? null : FindNodeTitle(toId);
            placeholderPort.portName = toName ?? "Choice";
            EnsureOutputPorts();
        }

        public void UpdateChoicePortLabel(int index)
        {
            if (index < 0 || index >= _outputs.Count - 1)
                return;
            var p = _outputs[index];
            var toId = node.outputs[index];
            var toName = string.IsNullOrEmpty(toId) ? null : FindNodeTitle(toId);
            p.portName = toName ?? "Choice";
        }

        public void OnChoiceRemoved()
        {
            EnsureOutputPorts();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            // evt.menu.AppendAction("Rename", _ => BeginRename());
            //
            // if (node.outputs.Count > 0)
            // {
            //     evt.menu.AppendSeparator();
            //     for (var i = 0; i < node.outputs.Count; i++)
            //     {
            //         var index = i;
            //         var toName = FindNodeTitle(node.outputs[i]) ?? "Choice";
            //         evt.menu.AppendAction($"Move '{toName}' Up", _ => MoveChoice(index, -1),
            //             index > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
            //         evt.menu.AppendAction($"Move '{toName}' Down", _ => MoveChoice(index, +1),
            //             index < node.outputs.Count - 1 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
            //     }
            // }
        }

        private void MoveChoice(int index, int delta)
        {
            var gv = GetFirstAncestorOfType<MyGraphView>();
            if (gv == null)
                return;
            gv.Graph.MoveOutput(node.id, index, delta);
            EditorUtility.SetDirty(gv.Graph);
            EnsureOutputPorts();
            // Rebuild edges visually by resetting graph to refresh connections order
            gv.SetGraph(gv.Graph);
        }

        private string FindNodeTitle(string id)
        {
            if (_graph == null) return null;
            var nodes = _graph.GetNodes();
            for (var i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                if (n.id == id) return string.IsNullOrEmpty(n.title) ? "Node" : n.title;
            }

            return null;
        }

        public void BeginRename()
        {
            if (_renameField != null)
                return;

            _renameField = new TextField { value = string.IsNullOrEmpty(node.title) ? "Node" : node.title };
            _renameField.style.position = Position.Absolute;
            _renameField.style.left = 0;
            _renameField.style.right = 0;
            _renameField.style.top = 0;
            _renameField.style.bottom = 0;
            titleContainer.Add(_renameField);

            _renameField.Q("unity-text-input").Focus();
            _renameField.SelectAll();

            _renameField.RegisterCallback<FocusOutEvent>(_ => CommitRename());
            _renameField.RegisterCallback<KeyDownEvent>(OnRenameKeyDown);
        }

        private void OnRenameKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                CommitRename();
                evt.StopPropagation();
            }
            else if (evt.keyCode == KeyCode.Escape)
            {
                CancelRename();
                evt.StopPropagation();
            }
        }

        private void CommitRename()
        {
            if (_renameField == null)
                return;
            var newTitle = _renameField.value;
            node.title = newTitle;
            title = string.IsNullOrEmpty(node.title) ? "Node" : node.title;
            _renameField.RemoveFromHierarchy();
            _renameField = null;

            var gv = GetFirstAncestorOfType<MyGraphView>();
            if (gv != null)
            {
                EditorUtility.SetDirty(gv.Graph);
                gv.RebuildAndReselect(node.id);
            }
        }

        private void CancelRename()
        {
            if (_renameField == null)
                return;
            _renameField.RemoveFromHierarchy();
            _renameField = null;

            var gv = GetFirstAncestorOfType<MyGraphView>();
            if (gv != null)
            {
                gv.Focus();
                gv.ClearSelection();
                gv.AddToSelection(this);
            }
        }
    }
}
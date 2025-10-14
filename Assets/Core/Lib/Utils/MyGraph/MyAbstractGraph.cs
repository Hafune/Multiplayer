using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Lib.Utils.MyGraph
{
    public abstract class MyAbstractGraph : ScriptableObject
    {
        [Serializable]
        public class Node
        {
            public string id;
            public string title;
            public Vector2 position;
            public List<string> outputs = new();
            public bool uiExpanded;
        }

        public abstract IReadOnlyList<Node> GetNodes();
#if UNITY_EDITOR
        public Node CreateNode(string title, Vector2 position, Type dataType)
        {
            var list = GetNodes();
            if (list == null)
                throw new Exception(GetType().Name + " must return a node list instance from GetNodes().");
            var listType = list.GetType();
            var elementType = listType.GetGenericArguments()[0];
            var instance = Activator.CreateInstance(elementType);
            var baseNode = (Node)instance;

            baseNode.id = Guid.NewGuid().ToString("N");
            baseNode.title = string.IsNullOrEmpty(title) ? "Node" : title;
            baseNode.position = position;
            baseNode.outputs = new List<string>();

            if (dataType != null)
            {
                var dataField = elementType.GetField("data");
                dataField.SetValue(instance, Activator.CreateInstance(dataType));
            }

            var add = listType.GetMethod("Add");
            add!.Invoke(list, new[] { instance });
            return baseNode;
        }

        public void RemoveNode(string id)
        {
            var nodes = GetNodes();
            var index = GetNodeIndex(id);
            if (index < 0)
                throw new Exception("Node not found: " + id);

            nodes.GetType().GetMethod("RemoveAt")!.Invoke(nodes, new object[] { index });

            for (var i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                for (var c = n.outputs.Count - 1; c >= 0; c--)
                {
                    if (n.outputs[c] == id)
                        n.outputs.RemoveAt(c);
                }
            }
        }

        public void SetDataObject(string nodeId, object data)
        {
            var node = GetNode(nodeId);
            node.GetType().GetField("data")!.SetValue(node, data);
        }

        public int ConnectOutput(string fromId, int outputIndex, string toId)
        {
            if (fromId == toId)
                throw new Exception("Cannot connect a node to itself");

            var from = GetNode(fromId);
            GetNode(toId);

            if (outputIndex >= 0 && outputIndex < from.outputs.Count)
            {
                from.outputs[outputIndex] = toId;
                return outputIndex;
            }

            from.outputs.Add(toId);
            return from.outputs.Count - 1;
        }

        public void RemoveOutput(string fromId, int outputIndex)
        {
            var from = GetNode(fromId);
            if (outputIndex < 0 || outputIndex >= from.outputs.Count)
                return;
            from.outputs.RemoveAt(outputIndex);
        }

        public void MoveOutput(string fromId, int outputIndex, int delta)
        {
            var from = GetNode(fromId);
            var newIndex = outputIndex + delta;
            if (outputIndex < 0 || outputIndex >= from.outputs.Count)
                return;
            if (newIndex < 0 || newIndex >= from.outputs.Count)
                return;
            var item = from.outputs[outputIndex];
            from.outputs.RemoveAt(outputIndex);
            from.outputs.Insert(newIndex, item);
        }
#endif

        private int GetNodeIndex(string id)
        {
            var nodes = GetNodes();
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].id == id)
                    return i;
            }

            return -1;
        }

        internal Node GetNode(string id)
        {
            var nodes = GetNodes();
            for (var i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                if (n.id == id)
                    return n;
            }

            throw new Exception("Node not found: " + id);
        }

        internal string FindParentUuid(string nodeId)
        {
            var nodes = GetNodes();
            for (var i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                for (var c = 0; c < n.outputs.Count; c++)
                {
                    if (n.outputs[c] == nodeId)
                        return n.id;
                }
            }

            return null;
        }
    }

    public abstract class MyAbstractGraph<TNodeData> : MyAbstractGraph
        where TNodeData : class
    {
        [Serializable]
        public class GenericNode : Node
        {
            [SerializeReference] public TNodeData data;
        }

        public struct NodeData
        {
            public string parentUuid;
            public string uuid;
            public TNodeData data;
        }

        [SerializeField] public List<GenericNode> nodes = new();

        public override IReadOnlyList<Node> GetNodes() => nodes;

        public NodeData GetFirstNodeData(List<NodeData> children = null)
        {
            var node = nodes[0];
            var result = new NodeData
            {
                parentUuid = null,
                uuid = node.id,
                data = GetData(node.id),
            };
            FillChildren(node.id, children);
            return result;
        }

        public NodeData GetNodeData(string nodeId, List<NodeData> children = null)
        {
            var node = GetNode(nodeId);
            var result = new NodeData
            {
                parentUuid = FindParentUuid(nodeId),
                uuid = node.id,
                data = GetData(node.id),
            };
            FillChildren(nodeId, children);
            return result;
        }

        private TNodeData GetData(string nodeId) => ((GenericNode)GetNode(nodeId)).data;

        private void FillChildren(string nodeId, List<NodeData> children)
        {
            if (children == null)
                return;

            var node = GetNode(nodeId);
            children.Clear();
            for (var i = 0; i < node.outputs.Count; i++)
            {
                var toId = node.outputs[i];
                if (string.IsNullOrEmpty(toId))
                    continue;

                var child = new NodeData
                {
                    parentUuid = node.id,
                    uuid = toId,
                    data = GetData(toId),
                };
                children.Add(child);
            }
        }
    }

    public abstract class MyAbstractSimpleGraph<TNodeData> : MyAbstractGraph
        where TNodeData : new()
    {
        [Serializable]
        public class GenericNode : Node
        {
            [SerializeField] public TNodeData data;
        }

        public struct NodeData
        {
            public string parentUuid;
            public string uuid;
            public TNodeData data;
        }

        [SerializeField] public List<GenericNode> nodes = new();

        public override IReadOnlyList<Node> GetNodes() => nodes;

        public NodeData GetFirstNodeData(List<NodeData> children)
        {
            var node = nodes[0];
            var result = new NodeData
            {
                parentUuid = null,
                uuid = node.id,
                data = GetData(node.id),
            };
            FillChildren(node.id, children);
            return result;
        }

        public NodeData GetNodeData(string nodeId, List<NodeData> children)
        {
            var node = GetNode(nodeId);
            var result = new NodeData
            {
                parentUuid = FindParentUuid(nodeId),
                uuid = node.id,
                data = GetData(node.id),
            };
            FillChildren(nodeId, children);
            return result;
        }

        private TNodeData GetData(string nodeId) => ((GenericNode)GetNode(nodeId)).data;

        private void FillChildren(string nodeId, List<NodeData> children)
        {
            var node = GetNode(nodeId);
            children.Clear();
            for (var i = 0; i < node.outputs.Count; i++)
            {
                var toId = node.outputs[i];
                if (string.IsNullOrEmpty(toId))
                    continue;

                var child = new NodeData
                {
                    parentUuid = node.id,
                    uuid = toId,
                    data = GetData(toId),
                };
                children.Add(child);
            }
        }
    }
}
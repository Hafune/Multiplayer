using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Lib.Utils.MyGraph
{
    public class DialogGraphTester : MonoBehaviour
    {
        [SerializeField] private DialogGraph graph;

        [SerializeField, Tooltip("Current node id")]
        private string currentNodeId;

        private MyAbstractGraph<DialogNodeData>.NodeData currentNode;

        [SerializeField, Tooltip("Current key from DialogNodeData")]
        private string key;

        [SerializeField, Tooltip("Current children titles")]
        private string[] children = Array.Empty<string>();

        private readonly List<MyAbstractGraph<DialogNodeData>.NodeData> tmpChildren = new();

        private void OnValidate()
        {
            RefreshFromCurrentId();
        }

        [MyButton]
        private void Right()
        {
            if (graph == null)
                return;
            if (string.IsNullOrEmpty(currentNodeId))
            {
                if (graph.nodes.Count == 0)
                    return;
                currentNode = graph.GetFirstNodeData(tmpChildren);
                currentNodeId = currentNode.uuid;
                UpdateView();
                return;
            }

            graph.GetNodeData(currentNodeId, tmpChildren);
            if (tmpChildren.Count == 0)
                return;
            SetCurrent(tmpChildren[0].uuid);
        }

        [MyButton]
        private void Left()
        {
            if (graph == null || string.IsNullOrEmpty(currentNodeId))
                return;
            if (string.IsNullOrEmpty(currentNode.parentUuid))
                return;
            SetCurrent(currentNode.parentUuid);
        }

        [MyButton]
        private void Down()
        {
            if (graph == null || string.IsNullOrEmpty(currentNodeId))
                return;
            if (string.IsNullOrEmpty(currentNode.parentUuid))
                return;
            graph.GetNodeData(currentNode.parentUuid, tmpChildren);
            if (tmpChildren.Count == 0)
                return;
            var idx = tmpChildren.FindIndex(c => c.uuid == currentNodeId);
            if (idx < 0 || idx + 1 >= tmpChildren.Count)
                return;
            SetCurrent(tmpChildren[idx + 1].uuid);
        }

        [MyButton]
        private void Up()
        {
            if (graph == null || string.IsNullOrEmpty(currentNodeId))
                return;
            if (string.IsNullOrEmpty(currentNode.parentUuid))
                return;
            graph.GetNodeData(currentNode.parentUuid, tmpChildren);
            if (tmpChildren.Count == 0)
                return;
            var idx = tmpChildren.FindIndex(c => c.uuid == currentNodeId);
            if (idx <= 0)
                return;
            SetCurrent(tmpChildren[idx - 1].uuid);
        }

        private void RefreshFromCurrentId()
        {
            if (graph == null || string.IsNullOrEmpty(currentNodeId))
            {
                key = null;
                children = Array.Empty<string>();
                return;
            }

            SetCurrent(currentNodeId);
        }

        private void SetCurrent(string id)
        {
            currentNode = graph.GetNodeData(id, tmpChildren);
            currentNodeId = currentNode.uuid;
            UpdateView();
        }

        private void UpdateView()
        {
            key = currentNode.data.key;
            children = tmpChildren.Select(i => i.data.key).ToArray();
        }
    }
}
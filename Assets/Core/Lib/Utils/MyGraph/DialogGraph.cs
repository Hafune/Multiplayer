using System;
using UnityEngine;

namespace Core.Lib.Utils.MyGraph
{
    [CreateAssetMenu(menuName = "Game Config/Graphs/Dialogs/" + nameof(DialogGraph))]
    public class DialogGraph : MyAbstractGraph<DialogNodeData> { }

    [Serializable]
    public class DialogNodeData
    {
        public string key;
        public ScriptableObject testSO;
    }
    
    [Serializable]
    public class DialogNodeData2 : DialogNodeData
    {
        public string key2;
    }
}
using System;
using System.Linq;
using Core.Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using VInspector.Libs;
#endif

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/Content/" + nameof(AssetsTable))]
    public class AssetsTable : ScriptableObject
    {
        [SerializeField, ReadOnly] private int[] IDs;
        
#if UNITY_EDITOR
        [SerializeField] private AssetsTable ExcludeTable;
        [SerializeField] private GameObject[] Assets = Array.Empty<GameObject>();

        private void OnValidate()
        {
            Assets = Assets
                .Distinct()
                .Except(ExcludeTable?.Assets ?? Array.Empty<GameObject>())
                .OrderBy(i => i.name)
                .ToArray();
            
            IDs = Assets
                .Select(i => Animator.StringToHash(i.GetGuid()))
                .ToArray();
            
            ExcludeTable = null;
        }

        [MyButton]
        private void SelectAssets() => Selection.objects = Assets;
#endif

        public bool Contains(int id) => IDs.Contains(id);
    }
}
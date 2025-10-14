using System.Collections.Generic;
using UnityEngine;

namespace Core.Lib.Utils
{
    public static class MyStaticBatchingUtility
    {
        private static readonly List<MeshRenderer> _renderers = new(16384);
        //Отделять те что надо комбайнить от прочих
        public static void Combine(GameObject root)
        {
            root.GetComponentsInChildren(_renderers);
            _renderers.RemoveAll(r => r.gameObject.isStatic || !r.enabled);
            foreach (var renderer in _renderers)
                renderer.enabled = false;
            
            StaticBatchingUtility.Combine(root);
            
            foreach (var renderer in _renderers)
                renderer.enabled = true;
        }
    }
}
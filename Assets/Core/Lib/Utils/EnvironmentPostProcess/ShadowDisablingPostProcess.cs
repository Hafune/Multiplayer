using System.Collections.Generic;
using Core.Lib;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Services
{
    public class ShadowDisablingPostProcess : MonoBehaviour, IEnvironmentPostProcess
    {
        private static readonly List<MeshRenderer> _renderers = new(16384);
        public void PostProcess(GameObject root)
        {
            root.GetComponentsInChildren(_renderers);
            foreach (var renderer in _renderers)
                if (renderer.transform.position.z > 0)
                    renderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }
}
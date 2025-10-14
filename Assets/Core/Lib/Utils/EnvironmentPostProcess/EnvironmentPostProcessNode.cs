using Core.Lib;
using Lib;
using UnityEngine;

namespace Core.Services
{
    public class EnvironmentPostProcessNode : MonoBehaviour, IEnvironmentPostProcess
    {
        private IEnvironmentPostProcess[] _postProcesses;

        public void PostProcess(GameObject root)
        {
            _postProcesses ??= transform.GetSelfChildrenComponents<IEnvironmentPostProcess>();
            foreach (var postProcess in _postProcesses)
                postProcess.PostProcess(root);
        }
    }
}
using Core.Lib;
using Core.Lib.Utils;
using UnityEngine;

namespace Core.Services
{
    public class MyStaticBatchingPostProcess : MonoBehaviour, IEnvironmentPostProcess
    {
        public void PostProcess(GameObject root) => MyStaticBatchingUtility.Combine(root);
    }
}
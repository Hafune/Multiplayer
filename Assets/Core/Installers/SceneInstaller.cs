using Reflex;
using Reflex.Scripts;
using Reflex.Scripts.Core;
using UnityEngine;

namespace Core
{
    public class SceneInstaller : Installer
    {
        [SerializeField] private Dependencies _dependencies;

        public override void InstallBindings(Context context) => _dependencies.BindInstances(context);
    }
}
using UnityEngine;
using Reflex.Scripts.Core;
using Reflex.Scripts.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly] // https://docs.unity3d.com/ScriptReference/Scripting.AlwaysLinkAssemblyAttribute.html

namespace Reflex.Injectors
{
#if UNITY_EDITOR
    public static class EditorRuntimeContextAccess
    {
        public static Context Context { get; internal set; }
    }
    
    public static class EditorNonRuntimeContextAccess
    {
        public static Context Context { get; } = new("EditorNonRuntimeContextAccess");
    }
#endif

    internal static class UnityInjector
    {
        private static bool _alreadyExist;
        private static Context _projectContainer;

        internal static void BeforeAwakeOfFirstSceneOnly(AContext projectContext)
        {
            if (_alreadyExist)
                return;

            _alreadyExist = true;
            _projectContainer = CreateProjectContainer(projectContext);
            UnityStaticEvents.OnSceneEarlyAwake += scene =>
            {
                var sceneContainer = CreateSceneContainer(scene, _projectContainer);
                SceneInjector.Inject(scene, sceneContainer);
#if UNITY_EDITOR
                EditorRuntimeContextAccess.Context = sceneContainer;
#endif
            };
#if UNITY_EDITOR
            EditorRuntimeContextAccess.Context = _projectContainer;
#endif
        }

        private static Context CreateProjectContainer(AContext projectContext)
        {
            var container = ContextTree.Root = new Context("ProjectContainer");

            Application.quitting += () =>
            {
                ContextTree.Root = null;
                container.Dispose();
            };

            projectContext.InstallBindings(container);

            return container;
        }

        private static Context CreateSceneContainer(Scene scene, Context projectContext)
        {
            var container = projectContext.Scope(scene.name);

            var subscription = scene.OnUnload(() =>
            {
                container.Dispose();
#if UNITY_EDITOR
                EditorRuntimeContextAccess.Context = _projectContainer;
#endif
            });

            // If app is being closed, all containers will be disposed by depth first search starting from project container root, see UnityInjector.cs
            Application.quitting += () => { subscription.Dispose(); };

            if (scene.TryFindAtRootObjects<SceneContext>(out var sceneContext))
            {
                sceneContext.InstallBindings(container);
            }

            return container;
        }
    }
}
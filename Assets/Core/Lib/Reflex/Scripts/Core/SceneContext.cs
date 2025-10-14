using Reflex.Injectors;
using UnityEngine;
using Reflex.Scripts.Events;

namespace Reflex.Scripts.Core
{
    [DefaultExecutionOrder(-10000)]
    public class SceneContext : AContext
    {
        private void Awake()
        {
            UnityInjector.BeforeAwakeOfFirstSceneOnly(this);
            UnityStaticEvents.OnSceneEarlyAwake.Invoke(gameObject.scene);
        }

        public override void InstallBindings(Context context)
        {
            base.InstallBindings(context);

            Debug.Log(Application.isPlaying
                ? $"{GetType().Name} Bindings Installed"
                : $"Editor: {GetType().Name} Bindings Installed");
        }
    }
}
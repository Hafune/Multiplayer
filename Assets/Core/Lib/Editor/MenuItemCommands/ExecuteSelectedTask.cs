#if UNITY_EDITOR
using Core.Lib;
using Reflex.Injectors;
using UnityEditor;

namespace Core
{
    public static class ExecuteSelectedTask
    {
        [MenuItem("CONTEXT/MonoBehaviour/Execute Task", false, 12)]
        private static void Context(MenuCommand command)
        {
            if (command.context is IMyTask task)
                Run(task);
        }

        private static void Run(IMyTask task)
        {
            var payload = Payload.GetPooled();
            task.Begin(payload, () => payload.Dispose());
        }
    }
}
#endif
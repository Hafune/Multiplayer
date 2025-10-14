using System;
using Core.Lib;
using Core.Services;
using Reflex;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    public class TaskTutorialInstance : TaskSequence
    {
        [field: SerializeField, ReadOnly] public string uuid { get; private set; }

#if UNITY_EDITOR
        [MyButton]
        private void RegenerateUuid()
        {
            uuid = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
#endif

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            var service = context.Resolve<TutorialsService>();
            if (service.IsTutorialCompleted(uuid))
            {
                onComplete?.Invoke();
                return;
            }

            base.Begin(payload, () =>
            {
                service.SetTutorialComplete(uuid);
                InProgress = false;
                onComplete?.Invoke();
            });
        }
    }
}
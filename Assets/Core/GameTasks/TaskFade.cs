using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Tasks
{
    public class TaskFade : MonoConstruct, IMyTask
    {
        private enum Fade
        {
            In,
            Out
        }

        [SerializeField] private Fade _fade = Fade.In;
        private Action _onComplete;
        public bool InProgress => false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;
            
            name = $"Fade_{Enum.GetName(typeof(Fade), _fade)}";
            EditorUtility.SetDirty(this);
        }
#endif
        
        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _onComplete = onComplete;

            switch (_fade)
            {
                case Fade.In:
                    context.Resolve<DarkScreenService>().FadeIn(OnComplete);
                    break;
                case Fade.Out:
                    context.Resolve<DarkScreenService>().FadeOut(OnComplete);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnComplete() => _onComplete?.Invoke();
    }
}
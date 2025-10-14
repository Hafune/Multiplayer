using System;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskManagePathAnimation : MonoConstruct, IMyTask
    {
        [SerializeField] private ReferencePath _path;
        [SerializeField] private AnimationClip _clip;
        [SerializeField] private FloatValue _speed;

        public bool InProgress => false;

        public void Begin(
            Payload payload,
            Action onComplete = null)
        {
            // var pathAnimationStartLogic = payload
            //     .Get<ConvertToEntity>()
            //     .GetComponent<PathCache>()
            //     .GetByPath<PathAnimationStartLogic>(_path);
            //
            // pathAnimationStartLogic.clip = _clip;
            // pathAnimationStartLogic.speed = _speed.Value;
            // onComplete?.Invoke();
        }
    }
}
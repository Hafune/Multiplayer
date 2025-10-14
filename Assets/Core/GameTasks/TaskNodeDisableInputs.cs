using System;
using Core.Lib;
using Core.Services;
using Reflex;

namespace Core.Tasks
{
    public class TaskNodeDisableInputs : TaskSequence
    {
        private Action _onComplete;
        private GameplayStateService _gameplayStateService;

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _gameplayStateService = context.Resolve<GameplayStateService>();
            _gameplayStateService.PauseInputs(this);

            _onComplete = onComplete;
            base.Begin(payload, Callback);
        }

        private void Callback()
        {
            _gameplayStateService.ResumeInputs(this);
            _onComplete?.Invoke();
        }
    }
}
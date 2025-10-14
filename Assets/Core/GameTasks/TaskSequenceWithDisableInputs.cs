using System;
using Core.Lib;
using Core.Services;
using Reflex;

namespace Core.Tasks
{
    public class TaskSequenceWithDisableInputs : TaskSequence
    {
        private Action _onComplete;
        private GameplayStateService _playerStateService;

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _playerStateService = context.Resolve<GameplayStateService>();
            _playerStateService.PauseInputs(this);

            _onComplete = onComplete;
            base.Begin(payload, Callback);
        }

        private void Callback()
        {
            _playerStateService.ResumeInputs(this);
            _onComplete?.Invoke();
        }
    }
}
using System;
using Core.Lib;
using Reflex;

namespace Core.Tasks
{
    public class TaskSequenceWithTimeScaleZero : TaskSequence
    {
        private Action _onComplete;
        private TimeScaleService _timeScaleService;

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _timeScaleService = context.Resolve<TimeScaleService>();
            _timeScaleService.Pause();

            _onComplete = onComplete;
            base.Begin(payload, Callback);
        }

        private void Callback()
        {
            _timeScaleService.Resume();
            _onComplete?.Invoke();
        }
    }
}
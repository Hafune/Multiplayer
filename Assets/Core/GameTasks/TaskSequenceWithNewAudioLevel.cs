using System;
using Core.Lib;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskSequenceWithNewAudioLevel : TaskSequence
    {
        [SerializeField] private float _volume = .2f;
        private Action _onComplete;
        private AudioSourceService _audioService;
        private float _oldVolume;

        public override void Begin(
            Payload payload,
            Action onComplete = null)
        {
            _audioService = context.Resolve<AudioSourceService>();
            _oldVolume = _audioService.BackgroundTempScale;
            _audioService.BackgroundTempScale = _volume;

            _onComplete = onComplete;
            base.Begin(payload, Callback);
        }

        private void Callback()
        {
            _audioService.BackgroundTempScale = _oldVolume;
            _onComplete?.Invoke();
        }
    }
}
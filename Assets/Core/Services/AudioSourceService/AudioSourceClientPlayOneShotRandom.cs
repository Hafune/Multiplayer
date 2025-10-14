using System;
using Core.Services;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceClientPlayOneShotRandom : AbstractAudioSourceClient
    {
        [SerializeField] private GameObject _audioSourcesRoot;
        [SerializeField] private bool _playOnEnable = true;
        [SerializeField] private bool _stopOnDisable;
        private AudioSource[] _audioSources;
        private AudioSourceService _audioSourceService;
        private int _lastIndex = -1;

        private void OnValidate()
        {
            if (_audioSourcesRoot && _audioSourcesRoot.GetComponent<AudioSource>() == null)
                _audioSourcesRoot = null;
        }

        private void Awake()
        {
            _audioSourceService = context.Resolve<AudioSourceService>();
            _audioSources = _audioSourcesRoot ? _audioSourcesRoot.GetComponents<AudioSource>() : Array.Empty<AudioSource>();
#if UNITY_EDITOR
            if (_audioSources.Length == 0)
                Debug.LogWarning(name + ": не заполнен " + nameof(_audioSources));
#endif
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }

        private void OnDisable()
        {
            if (_stopOnDisable)
                Stop();
        }

        public override void Execute()
        {
            if (_audioSources.Length == 0)
                return;

            var index = _audioSources.RandomIndex();

            if (_audioSources.Length > 1 && _lastIndex == index)
                index = ++index % _audioSources.Length;

            _lastIndex = index;
            _audioSourceService.PlayOneShot(_audioSources[index], transform.position);
        }

        public override void Stop()
        {
            if (_lastIndex != -1)
                _audioSourceService.StopOneShot(_audioSources[_lastIndex]);
        }
    }
}
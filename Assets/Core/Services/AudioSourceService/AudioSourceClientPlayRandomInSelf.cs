using Core.Services;
using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceClientPlayRandomInSelf : AbstractAudioSourceClient
    {
        [SerializeField] private GameObject _audioSourcesRoot;
        [SerializeField] private bool _playOnEnable = true;
        private AudioSource[] _audioSourcesOrigin;
        private AudioSource[] _audioSources;
        private GameObject _instance;
        private AudioSource _currentSource;
        private int _lastIndex = -1;

        private void OnValidate()
        {
            if (_audioSourcesRoot && _audioSourcesRoot.GetComponent<AudioSource>() == null)
                _audioSourcesRoot = null;
        }

        private void Awake()
        {
            context.Resolve<AudioSourceService>();
            _audioSourcesOrigin = _audioSourcesRoot.GetComponents<AudioSource>();
            _instance = Instantiate(_audioSourcesRoot, transform);
            _audioSources = _instance.GetComponents<AudioSource>();
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }
        
        public override void Execute()
        {
            var index = _audioSources.RandomIndex();

            if (_audioSources.Length > 1 && _lastIndex == index)
                index = ++index % _audioSources.Length;

            _lastIndex = index;
            _currentSource = _audioSources[index]; 
            _currentSource.volume = _audioSourcesOrigin[index].volume;
            _currentSource.Play();
        }

        public override void Stop()
        {
            _currentSource?.Stop();
            _currentSource = null;
        }
    }
}
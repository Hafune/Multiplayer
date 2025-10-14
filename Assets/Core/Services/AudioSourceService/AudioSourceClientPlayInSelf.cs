using Core.Services;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceClientPlayInSelf : AbstractAudioSourceClient
    {
        [SerializeField] private GameObject _audioSourcesRoot;
        [SerializeField] private bool _playOnEnable = true;
        private AudioSource[] _audioSourcesOrigin;
        private AudioSource[] _audioSources;
        private GameObject _instance;

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
            for (int i = 0, iMax = _audioSources.Length; i < iMax; i++)
            {
                var source = _audioSources[i]; 
                source.volume = _audioSourcesOrigin[i].volume;
                source.Play();
            }
        }

        public override void Stop()
        {
            for (int i = 0, iMax = _audioSources.Length; i < iMax; i++)
                _audioSources[i].Stop();
        }
    }
}
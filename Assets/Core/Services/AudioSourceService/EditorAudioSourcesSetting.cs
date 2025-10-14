using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Lib
{
    public class EditorAudioSourcesSetting : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private AudioClip[] _pasteClips;
        [SerializeField] private AudioMixerGroup _output;
        [SerializeField] private Vector2Int _minMaxDistance = new(495, 500);

        private bool _mute;
        private bool _bypassEffects;
        [SerializeField] private bool _bypassListenerEffects;
        [SerializeField] private bool _bypassReverbZones;
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private bool _loop;
        [SerializeField, Range(0, 256)] private int _priority = 128;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(-3f, 3f)] private float _pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float _panStereo;
        [SerializeField, Range(0f, 1f)] private float _spatialBlend;
        [SerializeField, Range(0f, 1.1f)] private float _reverbZoneMix = 1;

        private void OnValidate()
        {
            if (_pasteClips.Length == 0)
                return;

            foreach (var clip in _pasteClips)
                gameObject.AddComponent<AudioSource>().clip = clip;

            _pasteClips = Array.Empty<AudioClip>();
            Apply();
        }

        [MyButton]
        private void RemoveSources()
        {
            foreach (var source in gameObject.GetComponents<AudioSource>())
                DestroyImmediate(source, true);
        }

        [MyButton]
        private void Read()
        {
            var source = GetComponent<AudioSource>();
            Debug.Log(source.priority);
            _mute = source.mute;
            _bypassEffects = source.bypassEffects;
            _bypassListenerEffects = source.bypassListenerEffects;
            _bypassReverbZones = source.bypassReverbZones;
            _playOnAwake = source.playOnAwake;
            _loop = source.loop;
            _priority = source.priority;
            _volume = source.volume;
            _pitch = source.pitch;
            _panStereo = source.panStereo;
            _spatialBlend = source.spatialBlend;
            _reverbZoneMix = source.reverbZoneMix;
            _minMaxDistance.x = (int)source.minDistance;
            _minMaxDistance.y = (int)source.maxDistance;
        }

        [MyButton]
        private void Apply()
        {
            foreach (var source in GetComponents<AudioSource>())
            {
                source.outputAudioMixerGroup = _output;
                source.mute = _mute;
                source.bypassEffects = _bypassEffects;
                source.bypassListenerEffects = _bypassListenerEffects;
                source.bypassReverbZones = _bypassReverbZones;
                source.playOnAwake = _playOnAwake;
                source.loop = _loop;
                source.priority = _priority;
                source.volume = _volume;
                source.pitch = _pitch;
                source.panStereo = _panStereo;
                source.spatialBlend = _spatialBlend;
                source.reverbZoneMix = _reverbZoneMix;
                source.minDistance = _minMaxDistance.x;
                source.maxDistance = _minMaxDistance.y;
            }
        }
#endif
    }
}
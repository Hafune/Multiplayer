using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lib
{
    [RequireComponent(typeof(Light))]
    public class LightFlickering : MonoBehaviour
    {
        [Header("Intensity Settings")]
        [SerializeField] private float _minIntensity = 0.5f;
        [SerializeField] private float _maxIntensity = 1.5f;
        [SerializeField] private float _flickerSpeed = 5f;
    
        [Header("Random Spikes (Optional)")]
        [SerializeField] private bool _enableRandomSpikes = true;
        [SerializeField] [Range(0f, 1f)] private float _spikeChance = 0.05f;
        [SerializeField] private float _spikeDuration = 0.1f;
    
        private Light _pointLight;
        private float _spikeTimer;
        private bool _isSpike;

        private void Awake() => _pointLight = GetComponent<Light>();

        private void Update()
        {
            if (_enableRandomSpikes && !_isSpike)
            {
                if (Random.value < _spikeChance * Time.deltaTime * 60f)
                {
                    _isSpike = true;
                    _spikeTimer = _spikeDuration;
                }
            }

            if (_isSpike)
            {
                _spikeTimer -= Time.deltaTime;
                if (_spikeTimer <= 0f)
                {
                    _isSpike = false;
                }
                _pointLight.intensity = Random.Range(_minIntensity * 0.3f, _minIntensity * 0.7f);
                return;
            }

            float noise = Mathf.PerlinNoise(Time.time * _flickerSpeed, 0f);
            _pointLight.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);
        }
    }
}
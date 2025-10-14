using System;
using Core.ExternalEntityLogics;
using Core.Lib;
using JetBrains.Annotations;
using UnityEngine;
using VInspector;

public class MyHovlLaser : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int Noise = Shader.PropertyToID("_Noise");
    private static readonly int Emission = Shader.PropertyToID("_Emission");

    [SerializeField] private GameObject _effContainer;
    [SerializeField] private GameObject _lineEffContainer;
    [SerializeField] private GameObject _hitEffContainer;
    [SerializeField] private GameObject _areaContainer;
    [SerializeField] private float _hitOffset;
    [SerializeField] private float _currentLength = 30;
    [SerializeField] private float _mainTextureLength = 1f;
    [SerializeField] private float _noiseTextureLength = 1f;
    [SerializeField] private AbstractAudioSourceClient _loopSfx;
    [SerializeField, CanBeNull] private AbstractEntityLogic _onStart;
    [SerializeField, CanBeNull] private AbstractEntityLogic _onStop;
    [SerializeField] private bool _changeEmission;

    [ShowIf(nameof(_changeEmission))] [SerializeField]
    private float _emissionMin;

    [SerializeField] private float _emissionMax;
    [SerializeField] private float _emissionDuration;
    [EndIf] private ParticleSystem[] _effects;
    private ParticleSystem[] _hit;
    private LineRenderer _laser;
    private Vector3 _endPos;
    private bool _hasHit;
    private int _hitMoveSkipFrame;
    private float _emissionValue;
    private int _emissionValueDirection = 1;
    private float _baseWidthMultiplier;

    public Area Area { get; private set; }

    private void Awake()
    {
        _laser = _effContainer.GetComponent<LineRenderer>();
        _effects = _lineEffContainer.GetComponentsInChildren<ParticleSystem>();
        _hit = _hitEffContainer.GetComponentsInChildren<ParticleSystem>();
        Area = GetComponentInChildren<Area>();
        Area.gameObject.SetActive(false);
        _emissionValue = _emissionMin;
        _baseWidthMultiplier = _laser.widthMultiplier;
    }

    private void Start() => enabled = false;

    private void OnEnable()
    {
        _laser.widthMultiplier = _baseWidthMultiplier * _effContainer.transform.lossyScale.x;
        _laser.enabled = true;
    }

    private void OnDisable()
    {
        _laser.enabled = false;
        enabled = false;
        _hasHit = false;
        _loopSfx.Stop();
        Area.gameObject.SetActive(false);
    }

    private void Update()
    {
        _laser.SetPosition(0, transform.position);
        _laser.SetPosition(1, _endPos);

        if (_hasHit && (_hitMoveSkipFrame == 0 || (_hitMoveSkipFrame -= 1) == 0))
            _hitEffContainer.transform.position = _endPos;
    }

    [MyButton]
    private void PlayLaserEditor() => PlayLaser(_currentLength - _hitOffset, -1);

    public void PlayLaser(float distance, int entity)
    {
        if (!enabled && Application.isPlaying)
        {
            _loopSfx.Execute();
            Area.gameObject.SetActive(true);
        }

        if (!enabled && _onStart)
            _onStart.Run(entity);

        enabled = true;
        _currentLength = Math.Max(distance + _hitOffset, 1);

        var position = _lineEffContainer.transform.position;
        _endPos = position + _lineEffContainer.transform.forward * _currentLength;
        _areaContainer.transform.localScale = new Vector3(1, _currentLength, 1);

        foreach (var AllPs in _effects)
            if (!AllPs.isPlaying)
                AllPs.Play();

        var mainTextureScaleX = _mainTextureLength * Vector3.Distance(position, _endPos);
        _laser.material.SetTextureScale(MainTex, new Vector2(mainTextureScaleX, 1));

        var noiseTextureScaleX = _noiseTextureLength * Vector3.Distance(position, _endPos);
        _laser.material.SetTextureScale(Noise, new Vector2(noiseTextureScaleX, 1));

        if (!_changeEmission)
            return;

        _emissionValue += _emissionValueDirection * (_emissionMax - _emissionMin) * Time.deltaTime / _emissionDuration *
                          2;

        if (_emissionValue > _emissionMax)
        {
            _emissionValue = _emissionMax;
            _emissionValueDirection = -1;
        }

        if (_emissionValue < _emissionMin)
        {
            _emissionValue = _emissionMin;
            _emissionValueDirection = 1;
        }

        _laser.material.SetFloat(Emission, _emissionValue);
    }

    [MyButton]
    public void StopLaser(int entity)
    {
        foreach (var AllPs in _effects)
            if (AllPs.isPlaying)
                AllPs.Stop();

        if (enabled && _onStop)
            _onStop.Run(entity);
        
        enabled = false;
    }

    [MyButton]
    private void PlayHitEditor() => PlayHit();

    public void PlayHit(Quaternion? rotation = null)
    {
        if (_hasHit)
            return;

        _hitEffContainer.transform.localPosition = Vector3.zero;
        _hitEffContainer.transform.rotation = rotation ?? transform.rotation;

        foreach (var AllPs in _hit)
            if (!AllPs.isPlaying)
                AllPs.Play();

        _hasHit = true;
        _hitMoveSkipFrame = 2;
    }

    [MyButton]
    public void StopHit()
    {
        if (!_hasHit)
            return;

        _hitEffContainer.transform.localPosition = Vector3.zero;
        _hasHit = false;

        foreach (var AllPs in _hit)
            if (AllPs.isPlaying)
                AllPs.Stop();
    }
}
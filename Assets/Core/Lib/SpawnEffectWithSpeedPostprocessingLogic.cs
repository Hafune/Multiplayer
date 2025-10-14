using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.ExternalEntityLogics;
using Core.Lib.Services;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Lib
{
    public class SpawnEffectWithSpeedPostprocessingLogic : AbstractEffect
    {
        [SerializeField] private ConvertToEntity _entityRef;
        [SerializeField, Min(0.1f)] private float _speedScale = 1f;
        [SerializeField] private Vector3 _globalOffset;
        [SerializeField] private bool _quaternionIdentity;
        [SerializeField] private ParticleSystem _prefab;
        [SerializeField] private bool _playOnEnable;
        [SerializeField] private SpawnEffect.AttachEffect _attachEffect;

        private PrefabPool<ParticleSystem> _effectPool;
        private Coroutine _updateCoroutine;
        private AbstractAnimationSpeedPostProcessing[] _speedPostProcessing;
        private readonly Glossary<(ParticleSystem[], float[])> _particleCache = new();
        private readonly HashSet<int> _validParticleIds = new();
        private float _lastSpeed;

        private void Awake()
        {
            if (!_prefab)
            {
#if UNITY_EDITOR
                Debug.LogError("Эффект не указан");
#endif
                return;
            }

            _speedPostProcessing = GetComponentsInChildren<AbstractAnimationSpeedPostProcessing>();
            _effectPool = context.Resolve<PoolService>().GetIsolatedPullByPrefab(_prefab);
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }

        private void OnDisable()
        {
            if (_updateCoroutine is not null)
                StopCoroutine(_updateCoroutine);

            _updateCoroutine = null;
        }

        private float CalculateSpeed(int entity)
        {
            var speed = 0f;
            for (var i = 0; i < _speedPostProcessing.Length; i++) 
                speed = _speedPostProcessing[i].CalculateValue(entity, speed);

            return math.clamp(speed * _speedScale, 0.1f, 5f);
        }

        public override void Execute()
        {
            int entity = _entityRef.RawEntity;
            var speed = CalculateSpeed(entity);

            if (_lastSpeed != speed)
                _validParticleIds.Clear();

            _lastSpeed = speed;

            var eff = _effectPool.GetInstance(transform.position + _globalOffset,
                _quaternionIdentity ? Quaternion.identity : transform.rotation);

            int id = eff.GetInstanceID();
            if (!_validParticleIds.Contains(id))
            {
                if (!_particleCache.TryGetValue(id, out var pair))
                {
                    pair.Item1 = eff.GetComponentsInChildren<ParticleSystem>();
                    pair.Item2 = pair.Item1.Select(i => i.main.simulationSpeed).ToArray();
                    _particleCache.Add(id, pair);
                }

                var particleSystems = pair.Item1;
                var originalSpeeds = pair.Item2;

                for (int i = 0, iMax = particleSystems.Length; i < iMax; i++)
                {
                    var mainModule = particleSystems[i].main;
                    mainModule.simulationSpeed = originalSpeeds[i] * speed;
                }

                _validParticleIds.Add(id);
            }

            switch (_attachEffect)
            {
                case SpawnEffect.AttachEffect.None:
                    eff.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
                    break;
                case SpawnEffect.AttachEffect.AsChild:
                    eff.transform.SetParent(transform);
                    eff.transform.localScale = _prefab.transform.localScale;
                    break;
                case SpawnEffect.AttachEffect.SynchronizePositions:
                    eff.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
                    _updateCoroutine = StartCoroutine(UpdateCoroutine(eff.transform));
                    break;
                default:
                    Debug.LogError("_attachEffect: " + _attachEffect, this);
                    break;
            }
        }

        private IEnumerator UpdateCoroutine(Transform eff)
        {
            while (eff!.gameObject.activeSelf)
            {
                eff!.position = transform.position;
                yield return null;
            }

            yield return null;
            _updateCoroutine = null;
        }
    }
}
using System;
using Core.Lib.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Lib
{
    [Obsolete("Использовать " + nameof(SpawnEffect))]
    public class SpawnEffectOld : AbstractEffect
    {
        [SerializeField] private Vector3 _globalOffset;
        [SerializeField] private bool _quaternionIdentity;
        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _playOnEnable;
        [SerializeField] private SpawnEffect.AttachEffect _attachEffect;

        private PrefabPool<Transform> _effectPool;

        private void Awake()
        {
            if (!_prefab)
            {
#if UNITY_EDITOR
                Debug.LogError("Эффект не указан");
#endif
                return;
            }

            _effectPool = context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab);
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }

        public override void Execute()
        {
            _effectPool ??= context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab);

            var eff = _effectPool.GetInstance(transform.position + _globalOffset,
                _quaternionIdentity ? Quaternion.identity : transform.rotation);

            switch (_attachEffect)
            {
                case SpawnEffect.AttachEffect.None:
                    eff.localScale = Vector3.Scale(transform.lossyScale, _prefab.localScale);
                    break;
                case SpawnEffect.AttachEffect.AsChild:
                    eff.transform.SetParent(transform);
                    eff.localScale = _prefab.localScale;
                    break;
                case SpawnEffect.AttachEffect.SynchronizePositions:
                    eff.localScale = Vector3.Scale(transform.lossyScale, _prefab.localScale);
                    UpdateCoroutine(eff).Forget();
                    break;
                default:
                    Debug.LogError("_attachEffect: " + _attachEffect, this);
                    break;
            }
        }

        private async UniTaskVoid UpdateCoroutine(Transform eff)
        {
#if UNITY_EDITOR //защита от ошибок в логе при выключении плей мода
            while (eff && eff.gameObject.activeSelf && enabled)
#else
            while (eff.gameObject.activeSelf && enabled)
#endif
            {
                eff.position = transform.position;
                await UniTask.Yield();
            }
        }

#if UNITY_EDITOR
        [SerializeField, Range(0f, 1f)] private float _editorTimeScale = 1;
        [MyButton]
        public GameObject EditorPlayParticle()
        {
            var instance = Instantiate(
                _prefab,
                transform.position,
                _quaternionIdentity ? Quaternion.identity : transform.rotation,
                transform.root
            ).gameObject;
            instance.transform.position = transform.position + _globalOffset;
            instance.transform.localScale = Vector3.Scale(transform.localScale, _prefab.localScale);
            instance.hideFlags = HideFlags.DontSave;

            foreach (var system in instance.GetComponentsInChildren<ParticleSystem>())
                system.gameObject.AddComponent<EditorParticlePlayer>().SetTimeScale(_editorTimeScale);

            return instance;
        }
#endif
    }
}
using Core.Lib.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Lib
{
    public class SpawnEffect : AbstractEffect
    {
        public enum AttachEffect
        {
            None,
            AsChild,
            SynchronizePositions
        }

        [SerializeField] private Vector3 _globalOffset;
        [SerializeField] private bool _quaternionIdentity;
        [SerializeField] private ParticleSystem _prefab;
        [SerializeField] private bool _playOnEnable;
        [SerializeField] private AttachEffect _attachEffect;

        [SerializeField, Tooltip("Только для " + nameof(AttachEffect.SynchronizePositions))]
        private bool _disableEffectOnDisable;

        private PrefabPool<ParticleSystem> _effectPool;
        private ParticleSystem _particle;
        private bool _isActive;

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

        private void OnDisable()
        {
#if UNITY_EDITOR //защита от ошибок в логе при выключении плей мода
            if (_disableEffectOnDisable && _particle)
                _particle.Stop();
#else
            if (_disableEffectOnDisable)
                _particle.Stop();
#endif
            _isActive = false;
        }

        public override void Execute()
        {
            _effectPool ??= context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab);

            var eff = _effectPool.GetInstance(transform.position + _globalOffset,
                _quaternionIdentity ? Quaternion.identity : transform.rotation);
            
            switch (_attachEffect)
            {
                case AttachEffect.None:
                    eff.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
                    break;
                case AttachEffect.AsChild:
                    eff.transform.SetParent(transform);
                    eff.transform.localScale = _prefab.transform.localScale;
                    break;
                case AttachEffect.SynchronizePositions:
                    eff.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
                    _particle = eff;
                    _isActive = true;
                    UpdateCoroutine(eff.transform).Forget();
                    break;
                default:
                    Debug.LogError("_attachEffect: " + _attachEffect, this);
                    break;
            }
        }

        private async UniTaskVoid UpdateCoroutine(Transform eff)
        {
#if UNITY_EDITOR //защита от ошибок в логе при выключении плей мода
            while (eff && eff.gameObject.activeSelf && _isActive)
#else
            while (eff.gameObject.activeSelf && _isActive)
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
            instance.transform.localScale = Vector3.Scale(transform.localScale, _prefab.transform.localScale);
            instance.hideFlags = HideFlags.DontSave;

            foreach (var system in instance.GetComponentsInChildren<ParticleSystem>())
                system.gameObject.AddComponent<EditorParticlePlayer>().SetTimeScale(_editorTimeScale);

            return instance;
        }
#endif
    }
}
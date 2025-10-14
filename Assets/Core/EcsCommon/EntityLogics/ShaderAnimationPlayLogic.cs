using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ShaderAnimationPlayLogic : AbstractEntityLogic, IActionSubCancelLogic
    {
        private static readonly int TimeOffset = Shader.PropertyToID("_TimeOffset");

        [SerializeField] private float _speed = 1;
        [SerializeField] private GameObject _skinnedRoot;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Animator _animator;
        private Material _material;

        private void OnValidate() => _animator = _animator ? _animator : GetComponentInParent<Animator>();

        private void Awake() => _material = _meshRenderer.material;

        public override void Run(int entity)
        {
            _animator.enabled = false;
            _skinnedRoot.SetActive(false);
            _meshRenderer.gameObject.SetActive(true);
            _material.SetFloat(TimeOffset, -Time.time);
        }

        public void SubCancel(int entity)
        {
            _animator.enabled = true;
            _skinnedRoot.SetActive(true);
            _meshRenderer.gameObject.SetActive(false);
        }
    }
}
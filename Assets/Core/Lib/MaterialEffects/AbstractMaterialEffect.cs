using UnityEngine;

namespace Core.Lib
{
    public abstract class AbstractMaterialEffect : MonoBehaviour
    {
        [field: SerializeField] public MaterialEffectData Data { get; private set; }
        private MaterialEffectController _effectManager;

        private void Awake() => _effectManager = GetComponent<MaterialEffectController>();

        public void Activate(Material material, float duration) => _effectManager.AddEffect(material, material.GetInstanceID(), duration);
    }
}
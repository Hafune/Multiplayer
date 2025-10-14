using Core.Generated;
using Core.Lib;
using Lib;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.ExternalEntityLogics
{
    public class MaterialEffectStartLogic : AbstractEntityLogic
    {
        [SerializeField] private Material _material;
        [SerializeField] private string key;
        private ComponentPools _pools;

        private void Awake() => _pools = context.Resolve<ComponentPools>();

        public override void Run(int entity)
        {
            if (_pools.MaterialEffectController.Has(entity))
                _pools.MaterialEffectController.Get(entity).controller.AddEffect(_material, key.GetHashCode(), float.MaxValue);
        }

        public int GetID() => key.GetHashCode();

#if UNITY_EDITOR
        [MyButton]
        private void SetPathAsKey()
        {
            key = transform.root.name + "/" + gameObject.GetPath();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
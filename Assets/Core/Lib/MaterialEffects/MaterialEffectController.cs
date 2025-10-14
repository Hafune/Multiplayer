using UnityEngine;

namespace Core.Lib
{
    public class MaterialEffectController : MonoBehaviour
    {
        private Material[][] _originalMaterials;
        private Renderer[] _meshRenderers;
        private readonly MyList<(Material material, int id, float endTime)> _activeEffects = new();
        private readonly MyList<Material> _uniqueMaterials = new();
        private readonly MyList<int> _currentIds = new();
        private readonly IntValueCache<Material[]> _arraysCache = new(i => new Material[i]);
        private bool _needsUpdate;

        private void Awake()
        {
            _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            _originalMaterials = new Material[_meshRenderers.Length][];

            for (int i = 0; i < _meshRenderers.Length; i++)
                _originalMaterials[i] = _meshRenderers[i].materials;

            enabled = false;
        }

        private void OnDisable()
        {
            _activeEffects.Clear();
            _needsUpdate = false;
            RestoreOriginalMaterials();
        }

        public void AddEffect(Material material, int id, float duration)
        {
            for (int i = 0, iMax = _activeEffects.Count; i < iMax; i++)
            {
                if (_activeEffects[i].id == id)
                {
                    _activeEffects[i].endTime = Time.time + duration;
                    return;
                }
            }

            _activeEffects.Add((material, id, Time.time + duration));
            _needsUpdate = true;
            enabled = true;
        }

        public void RemoveEffect(int id)
        {
            for (int i = 0, iMax = _activeEffects.Count; i < iMax; i++)
            {
                if (_activeEffects[i].id == id)
                {
                    _activeEffects.RemoveAt(i);
                    _needsUpdate = true;
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_needsUpdate)
            {
                _needsUpdate = false;
                UpdateMaterials();
            }
            
            var hasExpired = false;

            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                if (Time.time >= _activeEffects[i].endTime)
                {
                    _activeEffects.RemoveAt(i);
                    hasExpired = true;
                }
            }

            if (!hasExpired)
                return;

            if (_activeEffects.Count != 0)
            {
                _needsUpdate = true;
                return;
            }

            enabled = false;
            RestoreOriginalMaterials();
        }

        private void UpdateMaterials()
        {
            if (_activeEffects.Count == 0)
            {
                RestoreOriginalMaterials();
                return;
            }

            _uniqueMaterials.Clear();
            for (int i = 0; i < _activeEffects.Count; i++)
                if (!_uniqueMaterials.Contains(_activeEffects[i].material))
                    _uniqueMaterials.Add(_activeEffects[i].material);

            bool isSameLength = _currentIds.Count == _uniqueMaterials.Count;
            if (!isSameLength)
                _currentIds.Clear();
            
            bool isSameMaterials = isSameLength;
            for (int j = 0; j < _uniqueMaterials.Count; j++)
            {
                int instanceId = _uniqueMaterials[j].GetInstanceID();
            
                if (isSameLength)
                {
                    if (isSameMaterials && _currentIds[j] != instanceId)
                        isSameMaterials = false;
            
                    _currentIds[j] = instanceId;
                }
                else
                {
                    _currentIds.Add(instanceId);
                }
            }
            
            if (isSameMaterials)
                return;
            
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                var originalLength = _originalMaterials[i].Length;
                var newMaterials = _arraysCache.Get(originalLength + _uniqueMaterials.Count);
                _originalMaterials[i].CopyTo(newMaterials, 0);

                for (int j = 0; j < _uniqueMaterials.Count; j++) 
                    newMaterials[originalLength + j] = _uniqueMaterials[j];

                _meshRenderers[i].materials = newMaterials;
            }
        }

        private void RestoreOriginalMaterials()
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
                _meshRenderers[i].materials = _originalMaterials[i];
            
            _currentIds.Clear();
        }
    }
}
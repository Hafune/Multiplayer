using System;
using UnityEngine.UIElements;

namespace Core.Lib
{
    public class ElePool<T>
    {
        private readonly VisualTreeAsset _prefab;
        private readonly MyList<(VisualElement, T)> _pool = new();
        private readonly Func<VisualElement, T> _build;
        private readonly Action<VisualElement> _reset;

        public ElePool(VisualTreeAsset prefab, Func<VisualElement, T> build, Action<VisualElement> reset = null)
        {
            _prefab = prefab;
            _build = build;
            _reset = reset;
        }

        public bool TryGetFree(out (VisualElement, T) value)
        {
            if (_pool.Count > 0)
            {
                value = _pool.Pop();
                return true;
            }

            value = default;
            return false;
        }
        
        public (VisualElement, T) Instantiate()
        {
            var instance = _prefab.Instantiate();
            var view = _build(instance);
            instance.RegisterCallback<DetachFromPanelEvent>(_ =>
            {
                _pool.Add((instance, view));
                _reset?.Invoke(instance);
            });

            return (instance, view);
        }
    }
}
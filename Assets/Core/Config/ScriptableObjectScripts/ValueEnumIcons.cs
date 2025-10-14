using Core.Generated;
using JetBrains.Annotations;
using UnityEngine;
using VInspector;

namespace Core
{
    [CreateAssetMenu(menuName = "Game Config/UI/" + nameof(ValueEnumIcons))]
    public class ValueEnumIcons : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<ValueEnum, Sprite> _store;

        [CanBeNull] public Sprite GetSprite(ValueEnum key) => _store.TryGetValue(key, out var sprite) ? sprite : null;
    }
}
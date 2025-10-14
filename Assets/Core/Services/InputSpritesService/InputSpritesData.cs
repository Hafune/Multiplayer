using UnityEngine;
using VInspector;

namespace Core.Services.SpriteForInputService
{
    [CreateAssetMenu(menuName = "Game Config/UI/Input/" + nameof(InputSpritesData))]
    public class InputSpritesData : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<InputSpritesService.InputKeys, Sprite> _store;

        public Sprite GetSprite(InputSpritesService.InputKeys key) => _store[key];
    }
}
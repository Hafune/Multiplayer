using Lib;
using TMPro;
using UnityEngine;

namespace Core.Services.SpriteForInputService
{
    public class TMPInputSpritesController : MonoConstruct
    {
        [SerializeField] private TextMeshPro _textMesh;
        private InputSpritesService _service;

        private void OnValidate() => _textMesh = _textMesh ? _textMesh : GetComponent<TextMeshPro>();

        private void Awake() => _service = context.Resolve<InputSpritesService>();

        private void OnEnable()
        {
            _service.OnChange += ReloadData;
            ReloadData();
        }

        private void OnDisable() => _service.OnChange -= ReloadData;

        private void ReloadData() => _textMesh.spriteAsset = _service.GetSpriteAsset();
    }
}
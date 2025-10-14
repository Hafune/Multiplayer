using Core.Components;

namespace Core.Lib.MaterialEffects
{
    public class MaterialEffectControllerProvider : MonoProvider<MaterialEffectControllerComponent>
    {
        private void OnValidate()
        {
            value.controller = GetComponent<MaterialEffectController>();
        }
    }
}
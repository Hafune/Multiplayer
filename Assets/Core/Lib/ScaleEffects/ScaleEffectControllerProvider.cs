using Core.Components;

namespace Core.Lib.ScaleEffects
{
    public class ScaleEffectControllerProvider : MonoProvider<ScaleEffectControllerComponent>
    {
        private void OnValidate()
        {
            value.controller = GetComponent<ScaleEffectController>();
        }
    }
}



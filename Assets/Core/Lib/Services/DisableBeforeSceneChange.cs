using Lib;

namespace Core.Lib
{
    public class DisableBeforeSceneChange : MonoConstruct
    {
        private void Awake() => context.Resolve<AddressableService>().OnNextSceneWillBeLoad += Disable;

        private void Disable() => gameObject.SetActive(false);

        private void OnDestroy() => context.Resolve<AddressableService>().OnNextSceneWillBeLoad -= Disable;
    }
}
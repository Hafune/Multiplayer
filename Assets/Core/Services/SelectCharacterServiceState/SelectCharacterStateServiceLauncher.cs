using Core.Services;
using Lib;

namespace Core.Views
{
    public class SelectCharacterStateServiceLauncher : MonoConstruct
    {
        private void Start() => context.Resolve<SelectCharacterServiceState>().EnableState();
    }
}
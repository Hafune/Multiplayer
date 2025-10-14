using System;
using Core.Services;
using Lib;

namespace Core.Views
{
    [Obsolete("Использовать " + nameof(TaskGameplayStateLauncher))]
    public class GameplayScenePlayerStateLauncher : MonoConstruct
    {
        private void Start() => context.Resolve<GameplayStateService>().EnableState();
    }
}
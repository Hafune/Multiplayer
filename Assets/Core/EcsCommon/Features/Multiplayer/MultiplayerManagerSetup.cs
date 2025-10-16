using Lib;
using UnityEngine;

namespace Core
{
    public class MultiplayerManagerSetup : MonoConstruct
    {
        [SerializeField] private MultiplayerManager _manager;

        private void Awake() => _manager.SetupContext(context);
    }
}
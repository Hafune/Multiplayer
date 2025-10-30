using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core
{
    public class MultiplayerSendStateLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityLogic _logic;
        private MultiplayerState _multiplayerLogics;
        
        private void Awake() => _multiplayerLogics = GetComponentInParent<MultiplayerState>();

        public override void Run(int entity) => _multiplayerLogics.WriteState(this);

        public void RunMultiplayerLogic(int entity) => _logic.Run(entity);
    }
}
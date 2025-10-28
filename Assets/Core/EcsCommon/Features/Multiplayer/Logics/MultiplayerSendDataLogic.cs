using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core
{
    public class MultiplayerSendLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityLogic _logic;
        private MultiplayerLogics _multiplayerLogics;
        
        private void Awake() => _multiplayerLogics = GetComponentInParent<MultiplayerLogics>();

        public override void Run(int entity) => _multiplayerLogics.SendData(entity, this);

        public void RunMultiplayerLogic(int entity) => _logic.Run(entity);
    }
}
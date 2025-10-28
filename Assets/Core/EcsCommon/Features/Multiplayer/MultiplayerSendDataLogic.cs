using Core.ExternalEntityLogics;
using UnityEngine;

namespace Core
{
    public class MultiplayerSendDataLogic : AbstractEntityLogic
    {
        [SerializeField] private string key ;
        [SerializeField] private string data;

        public override void Run(int entity) => 
            _animancer.Play(_mixer);
    }
}
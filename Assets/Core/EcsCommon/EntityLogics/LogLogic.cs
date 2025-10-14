using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class LogLogic : AbstractEntityLogic
    {
        [SerializeField] private string _message;

        public override void Run(int entity) => Debug.Log($"{_message + " " + nameof(LogLogic)} " + entity, this);
    }
}
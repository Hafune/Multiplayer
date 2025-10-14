using System;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete("Не работает, обновить если понадобится!")]
    public class StartActionEventLogic : AbstractEntityLogic
    {
        // [SerializeField] private ActionEnum _actionEnum;
        // private ActionSystemsService _actionSystemsService;
        //
        // private void Awake() => _actionSystemsService = context.Resolve<ActionSystemsService>();

        public override void Run(int entity)
        {
            //_actionSystemsService.AddStartEvent(_actionEnum, entity);
        }
    }
}
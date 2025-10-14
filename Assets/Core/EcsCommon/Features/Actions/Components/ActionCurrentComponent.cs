using System;
using Core.ExternalEntityLogics;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct ActionCurrentComponent : IEcsAutoReset<ActionCurrentComponent>
    {
        public IAbstractActionSystem currentAction;
        public AbstractEntityActionStateful logic;
        public MyList<int> totalModules;

        public AbstractEntityActionStateful BTreeDesiredActionLogic;
        public Action BTreeOnActionStart;
        public Action BTreeOnActionStartFailed;
        public Action BTreeOnActionCompleted;

        public void AutoReset(ref ActionCurrentComponent c)
        {
            c.currentAction = null;
            c.logic = null;
            c.totalModules ??= new();
            c.totalModules.Clear();

            c.BTreeDesiredActionLogic = null;
            c.BTreeOnActionStart = null;
            c.BTreeOnActionStartFailed = null;
            c.BTreeOnActionCompleted = null;
        }
    }
}
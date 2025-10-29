using System;
using System.Collections.Generic;
using Core.Components;
using Core.ExternalEntityLogics;
using Core.ExternalEntityLogics.ActionAttributes;
using Core.Generated;
using Core.Lib;
using Core.Lib.Services;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Services
{
    public partial class ActionBarService : IInitializableService, ISerializableService
    {
        //public event Action OnGotNewAction;
        //public event Action OnToggleView;
        public event Action OnChange;
        private ServiceData _serviceData = new();
        private EcsWorld _world;
        private IDataService _dataService;
        private ComponentPools _pools;

        public const int MaxActiveActionsCount = 6;

        private readonly Dictionary<Type, int> _actionTypeIndexMap = new();
        private readonly Dictionary<Type, Action<int>> _defaultActionsSetup = new();
        private readonly Dictionary<Type, Action<int, AbstractEntityAction>> _actionsMapSetup = new();
        private readonly MyList<Type> _typeIndexes = new(8);

        private readonly MyList<Func<int, bool>> _coolDowns = new(8);
        //private readonly List<ActionRuneMarker> _tempMarkers = new();
        // private LabelNewService _labelNewService;
        //private ExperienceService _experienceService;

        public ActionBarService()
        {
            List<Type> actionTypes = new();

            void RegisterActionLink<T, B>()
                where T : struct, IEntityActionComponent
                where B : struct, IButtonComponent
            {
                var type = typeof(T);
                actionTypes.Add(type);
                _defaultActionsSetup[type] = entity => SetupActionLogicInternal<T, B>(entity, GetCurrentAction<T>(entity));
                _actionsMapSetup[type] = SetupActionLogicInternal<T, B>;
                _actionTypeIndexMap[type] = actionTypes.Count - 1;
                _typeIndexes.Add(type);
                _coolDowns.Add(IsInCooldown<T>);
            }

            RegisterActionLink<ActionLinkMouseLeftComponent, MouseLeftTag>();
            RegisterActionLink<ActionLinkMouseRightComponent, MouseRightTag>();
            RegisterActionLink<ActionLinkSpaceComponent, ButtonJumpTag>();
            RegisterActionLink<ActionLinkSpaceForwardComponent, ButtonJumpForwardTag>();
            // RegisterActionLink<ActionLinkButton1Component, Button1Tag>();
            // RegisterActionLink<ActionLinkButton2Component, Button2Tag>();
            // RegisterActionLink<ActionLinkButton3Component, Button3Tag>();
            // RegisterActionLink<ActionLinkButton4Component, Button4Tag>();
        }

        // public void ToggleView() => OnToggleView!.Invoke();

        public void InitializeService(Context context)
        {
            // _experienceService = context.Resolve<ExperienceService>();
            // _experienceService.OnLevelUp += entity =>
            // {
            //     if (!ExistNewActions(entity))
            //         return;
            //
            //     OnGotNewAction?.Invoke();
            // };

            // _labelNewService = context.Resolve<LabelNewService>();
            _dataService = context.Resolve<CharactersService>();
            _dataService.RegisterService(this);
            _world = context.Resolve<EcsWorld>();
            _pools = context.Resolve<ComponentPools>();
        }

        public void SaveData() => _dataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _dataService.DeserializeData<ServiceData>(this);
            OnChange?.Invoke();
        }

        public AbstractEntityAction GetCurrentAction<T>(int entity) where T : struct, IEntityActionComponent
        {
            var client = GetClientForEntity(entity);
            int actionIndex = GetActionTypeIndex<T>();
            return client.GetCurrentAction(actionIndex);
        }

        public void UnlockDefaultAction<T>(int entity) where T : struct, IEntityActionComponent
        {
            var client = GetClientForEntity(entity);
            int actionIndex = GetActionTypeIndex<T>();
            if (client.GetCurrentAction(actionIndex) != null)
                return;
        
            client.SetCurrentAction(actionIndex, client.DefaultLogics[actionIndex]);
        
            _defaultActionsSetup[typeof(T)](entity);
            _dataService.SetDirty(this);
        }

        public void InitializeActionsClient(ActionBarClient client, int entity)
        {
            var savedActionKeys = _serviceData.actionKeys;

            for (int i = 0; i < savedActionKeys.Length; i++)
            {
                string actionKey = savedActionKeys[i];

                if (string.IsNullOrEmpty(actionKey))
                {
                    client.SetCurrentAction(i, null);
                    continue;
                }

                client.SetCurrentActionFromTotal(i, actionKey);
            }

            foreach (var (_, action) in _defaultActionsSetup)
                action(entity);

            //TODO определиться с инициализацией начальных скилов !!!!!!!!!!!!!!!!!!!!!!!!-----------------------------------------
            //TODO определиться с инициализацией начальных скилов !!!!!!!!!!!!!!!!!!!!!!!!-----------------------------------------
            UnlockDefaultAction<ActionLinkMouseLeftComponent>(entity);
            UnlockDefaultAction<ActionLinkMouseRightComponent>(entity);
            UnlockDefaultAction<ActionLinkSpaceComponent>(entity);
            UnlockDefaultAction<ActionLinkSpaceForwardComponent>(entity);
        }

        public void SetupActionLogic<T>(int entity, AbstractEntityAction action)
            where T : struct, IEntityActionComponent
        {
            _actionsMapSetup[typeof(T)](entity, action);
            _dataService.SetDirty(this);
        }

        public ActionBarClient GetClientForEntity(int entity) =>
            _pools.ConvertToEntity.Get(entity).convertToEntity.GetComponent<ActionBarClient>();

        public bool IsInCooldown<T>(int entity) where T : struct, IEntityActionComponent
        {
            var pool = _world.GetPool<CooldownValueComponent<T>>();
            if (!pool.Has(entity))
                return false;

            var value = pool.Get(entity);
            if (Time.time - value.startTime < value.value)
                return true;

            return false;
        }

        public bool IsInCooldown(int entity, AbstractEntityAction action)
        {
            var client = GetClientForEntity(entity);
            for (var i = 0; i < client.CurrentActions.Count; i++)
                if (client.CurrentActions[i] == action)
                    return _coolDowns[i](entity);

            return false;
        }

        public string GetSelectedRuneKey(string key)
        {
            _serviceData.actionRuneKeys.TryGetValue(key, out var runeName);
            return runeName;
        }

        public void SetRune(string actionName, string key)
        {
            _serviceData.actionRuneKeys[actionName] = key;
            _dataService.SetDirty(this);
        }

        // public bool ExistNewRuneForAction(AbstractEntityAction action)
        // {
        //     action.GetComponentsInChildren(true, _tempMarkers);
        //     var requiredLevel = _experienceService.Level;
        //     for (var i = 0; i < _tempMarkers.Count; i++)
        //     {
        //         var runeMarker = _tempMarkers[i];
        //         if (runeMarker.RequiredLevel <= requiredLevel && !_labelNewService.ContainsKey(runeMarker.Key))
        //             return true;
        //     }
        //
        //     return false;
        // }
        //
        // private bool ExistNewActions(int entity)
        // {
        //     var currentLevel = _experienceService.Level;
        //     var client = GetClientForEntity(entity);
        //     foreach (var (action, key) in client.TotalActions)
        //     {
        //         if (action.GetAttribute<ActionAttrRequiredLevel>()?.Value <= currentLevel && !_labelNewService.ContainsKey(key) ||
        //             ExistNewRuneForAction(action))
        //             return true;
        //     }
        //
        //     return false;
        // }

        private int GetActionTypeIndex<T>() where T : struct, IEntityActionComponent => _actionTypeIndexMap[typeof(T)];

        private string GetCurrentActionKey<T>(int entity) where T : struct, IEntityActionComponent
        {
            var client = GetClientForEntity(entity);
            int actionIndex = GetActionTypeIndex<T>();
            return client.GetActionKey(actionIndex);
        }

        private void SetupActionLogicInternal<T, B>(int entity, AbstractEntityAction action)
            where T : struct, IEntityActionComponent
            where B : struct, IButtonComponent
        {
            var lastAction = GetCurrentAction<T>(entity);

            if (lastAction)
            {
                var lastAttrs = lastAction.GetAttributes();
                for (int i = 0, iMax = lastAttrs.Count; i < iMax; i++)
                    lastAttrs[i].Remove<T>(entity);

                lastAction.SetActionContext(null);
                lastAction.SetButtonContext(null);
            }

            var client = GetClientForEntity(entity);
            int index = GetActionTypeIndex<T>();
            client.SetCurrentAction(index, action);
            _world.GetPool<EventValueUpdated<T>>().AddIfNotExist(entity);

            if (!action)
                return;

            for (var i = 0; i < client.CurrentActions.Count; i++)
            {
                if (index == i || client.CurrentActions[i] != action)
                    continue;

                _actionsMapSetup[_typeIndexes[i]](entity, lastAction);
                break;
            }

            var attrs = action.GetAttributes();
            for (int i = 0, iMax = attrs.Count; i < iMax; i++)
                attrs[i].Setup<T>(entity);

            ref var component = ref _world.GetPool<T>().GetOrInitialize(entity);
            component.logic = action;
            _serviceData.actionKeys[index] = GetCurrentActionKey<T>(entity) ?? string.Empty;
            action.SetActionContext(ActionContext<T>);
            action.SetButtonContext(ButtonContext<B>);
        }

        private static void ActionContext<A>(IActionGenericLogic actionGeneric, int entity) => actionGeneric.GenericRun<A>(entity);

        private static void ButtonContext<B>(IButtonGenericLogic actionGeneric, int entity) where B : struct, IButtonComponent =>
            actionGeneric.GenericRun<B>(entity);
    }
}
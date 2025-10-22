using System.Collections.Generic;
using System.Linq;
using Core.ExternalEntityLogics;
using Core.Lib;
using Lib;
using UnityEngine;

namespace Core.Services
{
    public class ActionBarServiceClient : MonoConstruct
    {
        [field: SerializeField] public AbstractEntityAction[] DefaultLogics { get; private set; }

        private readonly AbstractEntityAction[] _currentActions = new AbstractEntityAction[ActionBarService.MaxActiveActionsCount];
        private readonly Dictionary<AbstractEntityAction, string> _totalActions = new();
        private ActionBarService _actionService;
        private ConvertToEntity _entityRef;

        public IReadOnlyDictionary<AbstractEntityAction, string> TotalActions => _totalActions;
        public IReadOnlyList<AbstractEntityAction> CurrentActions => _currentActions;

        private void Awake()
        {
            _actionService = context.Resolve<ActionBarService>();
            transform
                .GetComponentsInChildren<AbstractEntityAction>(true)
                .ForEach(actionLogic => _totalActions[actionLogic] = actionLogic.Key);
        }

        private void OnEnable()
        {
            _entityRef = GetComponent<ConvertToEntity>();
            _entityRef.RegisterInitializeCall(Refresh);
        }

        private void OnDisable()
        {
            _actionService.OnChange -= InitializeActions;
            _entityRef.UnRegisterInitializeCall(Refresh);
        }

        private void Refresh()
        {
            _actionService.OnChange += InitializeActions;
            InitializeActions();
        }

        private void InitializeActions() => _actionService.InitializeActionsClient(this, _entityRef.RawEntity);

        public void SetCurrentAction(int actionIndex, AbstractEntityAction actionLogic) => _currentActions[actionIndex] = actionLogic;

        public void SetCurrentActionFromTotal(int actionIndex, string uuid) =>
            _currentActions[actionIndex] = _totalActions.FirstOrDefault(pair => pair.Value == uuid).Key;

        public AbstractEntityAction GetCurrentAction(int actionIndex) => _currentActions[actionIndex];

        public string GetActionKey(int actionIndex) => _currentActions[actionIndex]?.Key;
    }
}
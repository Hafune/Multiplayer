using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Core.Lib;

namespace Core.Services
{
    [Serializable]
    public class SelectCharacterServiceState : MonoConstruct, IAbstractGlobalState
    {
        public event Action OnShowView;
        public event Action OnHideView;
        public event Action<int> OnSelectionChange;
        public readonly List<UserContext> UiUsers = new();

        [SerializeField] private SceneField _nextScene;
        [SerializeField] private SceneField _mainMenuScene;
        [field: SerializeField] public List<ConvertToEntity> CharacterPrefabs { get; private set; }
        public List<ConvertToEntity> SelectedCharacterPrefabs { get; private set; } = new();
        private AddressableService _addressableService;
        private GlobalStateService _globalStateService;

        private void Awake()
        {
            _globalStateService = context.Resolve<GlobalStateService>();
            _addressableService = context.Resolve<AddressableService>();
            SelectedCharacterPrefabs.AddRange(CharacterPrefabs);
        }

        private void Start()
        {
            var inputsService = context.Resolve<InputService>();
            int max = context.Resolve<PlayersService>().MaxPlayersCount;

            for (int i = 0; i < max; i++)
                UiUsers.Add(new UserContext(inputsService, i));

            while (SelectedCharacterPrefabs.Count < max)
                SelectedCharacterPrefabs.Add(CharacterPrefabs[0]);
        }

        public void EnableState()
        {
            if (!_globalStateService.ChangeActiveState(this))
                return;

            OnShowView?.Invoke();
        }

        public void DisableState() => OnHideView?.Invoke();

        public void LoadNextScene() => _addressableService.LoadSceneAsync(_nextScene);
        public void LoadMainMenu() => _addressableService.LoadSceneAsync(_mainMenuScene);

        public void SetSelectedCharacter(int userIndex, int prefabIndex)
        {
            SelectedCharacterPrefabs[userIndex] = CharacterPrefabs[prefabIndex];
            OnSelectionChange?.Invoke(userIndex);
        }
    }

    public class UserContext
    {
        public bool isReadyToPlay;

        public int prefabIndex;
        public readonly int Index;

        private readonly UiInputWrapper _wrapper;
        private Action<UserContext> _onSubmit;
        private Action<UserContext> _onCancel;
        private Action<UserContext> _onLeft;
        private Action<UserContext> _onRight;
        private readonly InputService _inputsService;
        private readonly InputUser _user;

        public bool IsEntered { get; private set; }

        public UserContext(
            InputService inputsService,
            int index)
        {
            Index = index;
            _user = inputsService.GetUser(index);
            _inputsService = inputsService;
            _wrapper = inputsService.BuildUiInput(_user);
            _wrapper.UI.Submit.performed += Submit;
            _wrapper.UI.Cancel.performed += Cancel;
            _wrapper.UI.Left.performed += Left;
            _wrapper.UI.Right.performed += Right;

            inputsService.OnUserEnter += MatchWithUser;
        }

        public void MatchWithUser(InputUser user)
        {
            if (_user != user)
                return;

            Submit(default);
            IsEntered = true;
        }

        public void EnableInput() => _wrapper.EnableInput();

        public void DisableInput()
        {
            ClearInputs();
            _wrapper.DisableInput();
        }

        public void SetSubmit(Action<UserContext> callback) => _onSubmit = callback;
        public void SetCancel(Action<UserContext> callback) => _onCancel = callback;
        public void SetLeft(Action<UserContext> callback) => _onLeft = callback;
        public void SetRight(Action<UserContext> callback) => _onRight = callback;
        private void Submit(InputAction.CallbackContext _) => _onSubmit?.Invoke(this);
        private void Cancel(InputAction.CallbackContext _) => _onCancel?.Invoke(this);
        private void Left(InputAction.CallbackContext _) => _onLeft?.Invoke(this);
        private void Right(InputAction.CallbackContext _) => _onRight?.Invoke(this);

        public void ClearInputs()
        {
            SetSubmit(null);
            SetCancel(null);
            SetLeft(null);
            SetRight(null);
        }

        public void UserLeave()
        {
            _inputsService.UserLeave(_user);
            IsEntered = false;
        }
    }
}
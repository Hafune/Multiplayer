using System;
using System.Collections.Generic;
using Lib;
#if !UNITY_EDITOR
using UnityEngine;
#endif
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Core.Services
{
    public class InputService : MonoConstruct
    {
        public Action<InputUser> OnUserEnter;
        public Action<InputUser, InputDevice> OnPairDevice;
        public Action<InputUser, InputDevice> OnUnPairDevice;
        public bool IsNewUsersAllowed;

        private InputUser _rootUser;
        private readonly List<InputUser> _totalUsers = new();
        private readonly HashSet<uint> _enteredUsers = new();
        private readonly List<PlayerInputs> _inputs = new();
        private readonly List<InputUser> _deviceListeners = new();
        private readonly List<InputDevice> _totalDevices = new();
        private readonly List<InputDevice> _usedDevices = new();

        private EventSystem _eventSystem;
        private readonly List<PlayerInputs.UIActions> _uiActions = new();
        private readonly List<bool> _restoreState = new();
        private bool _isInputsEnabled = true;

        private InputDevice _lastUsedDevice;

        public PlayerInputs.PlayerActions GetInputs(int index) => _inputs[index].Player;

        private void Awake()
        {
            _eventSystem = context.Resolve<EventSystem>();

            void AddUser()
            {
                var (inputUser, playerInputs) = BuildUser();
                _totalUsers.Add(inputUser);
                _inputs.Add(playerInputs);
            }

            int max = context.Resolve<PlayersService>().MaxPlayersCount;
            for (int i = 0; i < max; i++)
                AddUser();

            _rootUser = _totalUsers[0];

            InputSystem.onDeviceChange += OnDeviceChange;
            foreach (var device in InputSystem.devices)
                OnDeviceChange(device, default);
        }

        private void OnDestroy() => InputSystem.onDeviceChange -= OnDeviceChange;

        public void EnableGameplayInputs() => _inputs.ForEach(i => i.Enable());

        public void DisableGameplayInputs() => _inputs.ForEach(i => i.Disable());

        public void InitializeRootUserWithLastDevice()
        {
            if (_lastUsedDevice != null)
                PairingWithDevice(_lastUsedDevice, _rootUser);
        }

        public void UserLeave(InputUser user)
        {
            foreach (var device in user.pairedDevices)
            {
                if (!_usedDevices.Contains(device))
                    continue;

                if (_rootUser != user)
                {
                    foreach (var inputUser in _totalUsers)
                    {
                        inputUser.UnpairDevice(device);
                        OnUnPairDevice?.Invoke(inputUser, device);
                    }

                    InputUser.PerformPairingWithDevice(device, _rootUser);
                }

                _usedDevices.Remove(device);
                OnPairDevice?.Invoke(_rootUser, device);
            }

            _enteredUsers.Remove(user.id);
        }

        public UiInputWrapper BuildUiInput(int index) => BuildUiInput(_totalUsers[index]);

        public UiInputWrapper BuildUiInput(InputUser mainUser)
        {
            var (uiUser, playerInputs) = BuildUser();
            foreach (var device in mainUser.pairedDevices)
                InputUser.PerformPairingWithDevice(device, uiUser);

            var uiInput = playerInputs.UI;
            _uiActions.Add(uiInput);
            _restoreState.Add(false);

            return new UiInputWrapper(
                uiInput,
                this,
                uiUser,
                mainUser,
                _uiActions.Count - 1
            );
        }

        public void StopAll()
        {
            for (int i = 0, iMax = _uiActions.Count; i < iMax; i++)
            {
                _uiActions[i].Disable();
                _restoreState[i] = _uiActions[i].enabled;
            }

            _eventSystem.enabled = false;
            _isInputsEnabled = false;
        }

        public void RestoreAll()
        {
            for (int i = 0, iMax = _uiActions.Count; i < iMax; i++)
                if (_restoreState[i])
                    _uiActions[i].Enable();
                else
                    _uiActions[i].Disable();

            _eventSystem.enabled = true;
            _isInputsEnabled = true;
        }

        public InputUser GetUser(int index) => _totalUsers[index];

        public void EnableInput(int actionIndex) =>
            SetEnable(actionIndex, true);

        public void DisableInput(int actionIndex) =>
            SetEnable(actionIndex, false);

        private void SetEnable(int index, bool state)
        {
            if (!_isInputsEnabled)
                _restoreState[index] = state;
            else if (state)
                _uiActions[index].Enable();
            else
                _uiActions[index].Disable();
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange inputDeviceChange)
        {
            //Отключение мыши
            // if (device is not Gamepad and not Keyboard && device.enabled)
            // {
            //     InputSystem.DisableDevice(device);
            //     return;
            // }

            var index = _totalDevices.IndexOf(device);
            if (index == -1 && !device.added)
                return;

            if (!device.added)
            {
                _deviceListeners[index].UnpairDevicesAndRemoveUser();
                _deviceListeners.RemoveAt(index);
                _totalDevices.RemoveAt(index);

                foreach (var pair in _totalUsers)
                    pair.UnpairDevice(device);

                _usedDevices.Remove(device);

                return;
            }

            InputUser.PerformPairingWithDevice(device, _rootUser);

            var (inputUser, playerInputs) = BuildUser();
            InputUser.PerformPairingWithDevice(device, inputUser);
            _deviceListeners.Add(inputUser);
            _totalDevices.Add(device);

            playerInputs.UI.Enable();
            playerInputs.UI.Submit.performed += _ =>
            {
                _lastUsedDevice = device;

                if (_enteredUsers.Count == _totalUsers.Count)
                    return;

                if (!IsNewUsersAllowed)
                    return;

                PairingWithDevice(device, _totalUsers[_enteredUsers.Count]);
            };
        }

        private void PairingWithDevice(InputDevice device, InputUser user)
        {
            if (_usedDevices.Contains(device))
                return;

            if (_rootUser != user)
            {
                _rootUser.UnpairDevice(device);
                InputUser.PerformPairingWithDevice(device, user);
                OnUnPairDevice?.Invoke(_rootUser, device);
            }

            _usedDevices.Add(device);
            _enteredUsers.Add(user.id);
            OnPairDevice?.Invoke(user, device);
            OnUserEnter?.Invoke(user);
        }

        private (InputUser, PlayerInputs) BuildUser()
        {
            var user = InputUser.CreateUserWithoutPairedDevices();
            var playerInputs = new PlayerInputs();
            user.AssociateActionsWithUser(playerInputs);
            return (user, playerInputs);
        }
    }
}
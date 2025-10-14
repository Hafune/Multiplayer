using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Core.Services
{
    public class UiInputWrapper
    {
        public PlayerInputs.UIActions UI { get; }

        private readonly InputService _service;
        private InputUser _uiUser;
        private readonly InputUser _mainUser;
        private readonly int _actionIndex;

        public UiInputWrapper(
            PlayerInputs.UIActions ui,
            InputService service,
            InputUser uiUser,
            InputUser mainUser,
            int actionIndex
        )
        {
            UI = ui;
            _service = service;
            _uiUser = uiUser;
            _mainUser = mainUser;
            _actionIndex = actionIndex;

            service.OnPairDevice += OnPairDevice;
            service.OnUnPairDevice += OnUnPairDevice;
            SetupDevices(_mainUser.pairedDevices);
        }

        public void EnableInput() => _service.EnableInput(_actionIndex);
        public void DisableInput() => _service.DisableInput(_actionIndex);

        private void OnPairDevice(InputUser user, InputDevice device)
        {
            if (_mainUser != user)
                return;

            InputUser.PerformPairingWithDevice(device, _uiUser);
        }

        private void OnUnPairDevice(InputUser user, InputDevice device)
        {
            if (_mainUser != user)
                return;

            _uiUser.UnpairDevice(device);
        }

        private void SetupDevices(IEnumerable<InputDevice> devices)
        {
            _uiUser.UnpairDevices();

            foreach (var device in devices)
                if (device != null)
                    InputUser.PerformPairingWithDevice(device, _uiUser);
        }
    }
}
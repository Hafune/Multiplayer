using System;
using Core.Lib;
using Core.Lib.Services;
using Reflex;

namespace Core.Services
{
    public class SettingsService
    {
        public event Action OnToggleView;

        public void ToggleView() => OnToggleView!.Invoke();
    }
}
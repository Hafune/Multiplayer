using System;
using Core.Lib.Services;
using Reflex;

namespace Core.Services
{
    public abstract class AbstractResourceService : IInitializableService, ISerializableService
    {
        public event Action OnChange;

        protected ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;

        public int Count => _serviceData.count;

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public bool TryChangeValue(int value)
        {
            if (value < 0 && _serviceData.count + value < 0)
                return false;

            if (value < 0 || int.MaxValue - value >= _serviceData.count)
                _serviceData.count += value;
            else
                _serviceData.count = int.MaxValue;
            
            _playerDataService.SetDirty(this);
            OnChange?.Invoke();
            return true;
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
            OnChange?.Invoke();
        }

        protected class ServiceData
        {
            public int count;
        }
    }
}
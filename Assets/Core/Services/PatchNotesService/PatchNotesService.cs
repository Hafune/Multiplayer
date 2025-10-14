using System;
using Core.EcsCommon.ValueComponents;
using Core.Lib.Services;
using Reflex;

namespace Core.Services
{
    public class PatchNotesService : IInitializableService, ISerializableService
    {
        public event Action OnChange;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private const string currentVersion = "v0.4";
        
        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
            OnChange?.Invoke();
        }

        public bool IsNew() => _serviceData.version != currentVersion;

        public void MarkAsRead()
        {
            _serviceData.version = currentVersion;
            _playerDataService.SetDirty(this);
        }

        public string GetVersionText() => currentVersion;//MyEnumUtility<ReleaseVersion>.Name((int)MigrationService.ACTUAL_VERSION).Replace("_", ".");

        private class ServiceData
        {
            public string version = "";
        }
    }
}
using System.Collections.Generic;
using Core.Lib.Services;
using Reflex;

namespace Core.Services
{
    public class TutorialsService : IInitializableService, ISerializableService
    {
        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public bool IsTutorialCompleted(string uuid) => _serviceData.uuids.Contains(uuid);

        public void SetTutorialComplete(string uuid)
        {
            _serviceData.uuids.Add(uuid);
            _playerDataService.SetDirty(this);
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);
        
        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public HashSet<string> uuids = new();
        }
    }
}
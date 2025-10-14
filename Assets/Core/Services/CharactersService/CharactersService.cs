using System.Collections.Generic;
using Lib;

namespace Core.Services
{
    public class CharactersService : MonoConstruct, ISerializableService, IDataService
    {
        private PlayerDataService _dataService;
        private ServiceData _serviceData = new();
        private readonly List<ISerializableService> _subServices = new();

        private void Awake()
        {
            _dataService = context.Resolve<PlayerDataService>();
            _dataService.RegisterService(this);
        }

        public void RegisterService(ISerializableService service, bool ignoreReset = false, bool lazySave = false) =>
            _subServices.Add(service);

        public void SetDirty(ISerializableService _, bool lazy) => _dataService.SetDirty(this, lazy);

        public void SerializeData(object obj, object data) =>
            _serviceData.values[_serviceData.lastSelectedIndex].SerializeData(obj, data);

        public T DeserializeData<T>(object obj) where T : new() =>
            _serviceData.values[_serviceData.lastSelectedIndex].DeserializeData<T>(obj);

        public ReleaseVersion GetDataVersion() => _dataService.GetDataVersion();

        public void SaveData()
        {
            foreach (var service in _subServices)
                service.SaveData();

            _dataService.SerializeData(this, _serviceData);
        }

        public void ReloadData()
        {
            _serviceData = _dataService.DeserializeData<ServiceData>(this);
            foreach (var value in _serviceData.values)
                if (value.IsEmpty)
                    value.version = MigrationService.ACTUAL_VERSION;

            foreach (var service in _subServices)
                service.ReloadData();
        }

        private class ServiceData
        {
            public List<ServicesValues> values = new();
            public int lastSelectedIndex;

            public ServiceData() => values.Add(new());
        }
    }
}
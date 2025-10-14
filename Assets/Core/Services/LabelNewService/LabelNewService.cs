using System.Collections.Generic;
using Lib;

namespace Core.Services
{
    public class LabelNewService : MonoConstruct, ISerializableService
    {
        private ServiceData _serviceData = new();
        private IDataService _dataService;

        private void Awake()
        {
            _dataService = context.Resolve<CharactersService>();
            _dataService.RegisterService(this);
        }

        public void AddKey(string key)
        {
            if (_serviceData.alreadyShown.Add(key))
                _dataService.SetDirty(this);
        }

        public bool ContainsKey(string key) => _serviceData.alreadyShown.Contains(key);

        public void SaveData() => _dataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _dataService.DeserializeData<ServiceData>(this);

        public class ServiceData
        {
            public HashSet<string> alreadyShown = new();

            public ServiceData() => alreadyShown.Add("barb_bash");
        }
    }
}
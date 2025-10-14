namespace Core.Services
{
    public interface IDataService
    {
        public void RegisterService(ISerializableService service, bool ignoreReset = false, bool lazySave = false);
        public void SetDirty(ISerializableService service, bool lazy = false);
        public void SerializeData(object obj, object data);
        public T DeserializeData<T>(object obj) where T : new();
        public ReleaseVersion GetDataVersion();
    }
}
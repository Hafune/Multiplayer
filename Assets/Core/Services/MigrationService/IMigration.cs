namespace Core.Services
{
    public interface IMigration
    {
        public ReleaseVersion From();

        public object ReadFromData(IDataService dataService, object key);
        public object Migrate(object oldData);
    }
}
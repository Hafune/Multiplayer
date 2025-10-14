using Reflex;

namespace Core.Lib.Services
{
    public interface IInitializableService
    {
        public void InitializeService(Context context);

        public void Start()
        {
        }
    }
}
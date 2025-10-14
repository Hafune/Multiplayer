using System;

namespace Core.Lib
{
    public interface IMyTask
    {
        public bool InProgress { get; }

        public void Begin(
            Payload payload,
            Action onComplete = null);

        public void Cancel()
        {
            
        }
    }
}
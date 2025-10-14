namespace Core.Lib
{
    public class TaskSequenceStart : TaskSequence
    {
        private Payload _payload;

        private void Start() => RunSequence();

        private void OnDisable()
        {
            if (!InProgress)
                return;
            
            Cancel();
            _payload?.Dispose();
            _payload = null;
        }

        [MyButton]
        private void RunSequence() => Begin(_payload = Payload.GetPooled(), OnComplete);

        private void OnComplete()
        {
            _payload?.Dispose();
            _payload = null;
        }
    }
}
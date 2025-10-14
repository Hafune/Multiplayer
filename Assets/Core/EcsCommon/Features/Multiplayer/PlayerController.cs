using Lib;

namespace Core
{
    public class PlayerController : MonoConstruct
    {
        private void FixedUpdate()
        {
            var position = transform.position;
            
            MultiplayerManager.Instance.SendData("move", new()
            {
                { "x", position.x },
                { "y", position.z },
            });
        }
    }
}
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.EcsCommon
{
    public class ArcaneRandomRotateDirection : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.localScale = new(1, Random.value > .5f ? 1 : -1, 1);
            transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        }
    }
}
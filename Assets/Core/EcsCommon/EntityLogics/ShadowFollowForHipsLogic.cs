using UnityEngine;
using UnityEngine.Assertions;

namespace Core.ExternalEntityLogics
{
    public class ShadowFollowForHipsLogic : AbstractEntityLogic
    {
        [SerializeField] private Transform _shadow;
        [SerializeField] private Transform _hips;

#if UNITY_EDITOR
        private void Awake()
        {
            Assert.IsNotNull(_shadow);
            Assert.IsNotNull(_hips);
        }
#endif

        public override void Run(int entity)
        {
            var position = _shadow.transform.position;
            var hipsPos = _hips.transform.position;
            position.x = hipsPos.x;
            position.y = hipsPos.y;
            _shadow.transform.position = position;
        }
    }
}
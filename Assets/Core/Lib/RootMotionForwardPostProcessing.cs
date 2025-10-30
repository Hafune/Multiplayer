using UnityEngine;
using UnityEngine.Assertions;

namespace Core.Lib
{
    public class RootMotionForwardPostProcessing : MonoBehaviour, ITriggerDispatcherTarget
    {
        [SerializeField] private AnimatorRootMotion _rootMotion;
        private int _contactCount;

        private void OnValidate() =>
            _rootMotion = _rootMotion ? _rootMotion : GetComponentInParent<AnimatorRootMotion>();

#if UNITY_EDITOR
        private void OnEnable() => Assert.AreEqual(0, _contactCount, name);
#endif
        private void OnDisable() => _rootMotion.SetDeltaPostProcessing(null);

        public void OnTriggerEnter(Collider col)
        {
            TriggerDisableHandler.RegisterTrigger(this, col);
            _contactCount++;

            if (!enabled || _contactCount != 1)
                return;

            _rootMotion.SetDeltaPostProcessing(PostProcess);
        }

        public void OnTriggerExit(Collider col)
        {
            TriggerDisableHandler.UnRegisterTrigger(this, col);
            _contactCount--;

            if (!enabled || _contactCount != 0)
                return;

            _rootMotion.SetDeltaPostProcessing(null);
        }

        private static Vector3 PostProcess(Vector3 delta) => Vector3.zero;
    }
}
using UnityEngine;

namespace Core.Lib
{
    public class AnimateRotationController : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] public AnimateRotation rotation;
        [SerializeField] public AnimateRotationSpring spring;

        private void Awake() => OnDisable();

        private void OnEnable()
        {
            rotation.enabled = true;
            spring.enabled = false;
        }

        private void OnDisable()
        {
            rotation.enabled = false;
            spring.enabled = true;
        }

        private void Update() => rotation.speed = speed;
    }
}
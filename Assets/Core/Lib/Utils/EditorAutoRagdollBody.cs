using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
#if UNITY_EDITOR
    [RequireComponent(typeof(Rigidbody)), ExecuteAlways]
    public class EditorAutoRagdollBody : MonoBehaviour
    {
        public float mass = 6;

        private void Awake()
        {
            if (!gameObject.TryGetComponent<BoxCollider>(out _))
                gameObject.AddComponent<BoxCollider>();

            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = mass;

            EditorApplication.delayCall += () => DestroyImmediate(this);
        }
    }
#endif
}
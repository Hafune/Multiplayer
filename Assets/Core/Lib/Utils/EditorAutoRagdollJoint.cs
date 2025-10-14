using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
#if UNITY_EDITOR
    [RequireComponent(typeof(Rigidbody)), ExecuteAlways]
    public class EditorAutoRagdollJoint : MonoBehaviour
    {
        private void Awake()
        {
            if (transform.childCount == 0)
            {
                Debug.LogError("AutoJoint: Объект не имеет дочерних элементов, невозможно создать сустав.");
                return;
            }

            var child = transform.GetChild(0);
            var direction = child.position - transform.position;
            float length = direction.magnitude;

            if (length <= 0)
            {
                Debug.LogError("AutoJoint: Дочерний объект находится в той же точке, что и родитель.");
                return;
            }

            // Добавляем и настраиваем CapsuleCollider
            var capsule = gameObject.AddComponent<CapsuleCollider>();
            capsule.direction = 0;
            capsule.height = length;
            capsule.radius = length / 3f;
            capsule.center = new Vector3(-length / 2f, 0, 0);
            ;

            var rootRb = transform.root.GetComponentInChildren<Rigidbody>();
            // Добавляем Rigidbody
            var rb = GetComponent<Rigidbody>();
            rb.mass = rootRb?.mass * .5f ?? 1f;
            // rb.interpolation = RigidbodyInterpolation.Interpolate;
            // rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Добавляем и настраиваем CharacterJoint
            var joint = gameObject.AddComponent<CharacterJoint>();
            joint.connectedBody = transform.parent.GetComponentInParent<Rigidbody>();
            ;
            joint.anchor = Vector3.zero;
            joint.axis = Vector3.forward;
            joint.swing1Limit = new SoftJointLimit { limit = 30f };
            joint.swing2Limit = new SoftJointLimit { limit = 0f };
            // joint.twistLimitSpring = new SoftJointLimitSpring { spring = 10f, damper = 1f };
            joint.lowTwistLimit = new SoftJointLimit { limit = -30 };
            joint.highTwistLimit = new SoftJointLimit { limit = 30 };

            EditorApplication.delayCall += () => DestroyImmediate(this);
        }
    }
#endif
}
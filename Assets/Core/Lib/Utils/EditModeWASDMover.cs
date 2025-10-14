#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

namespace Core.Lib
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class EditModeWASDMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        private double _lastTimeSinceStartup;

        private void OnEnable()
        {
            EditorApplication.update += EditorUpdate;
            _lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        }

        private void OnDisable()
        {
            EditorApplication.update -= EditorUpdate;
        }
        
        void EditorUpdate()
        {
            var deltaTime = (float)(EditorApplication.timeSinceStartup - _lastTimeSinceStartup);
            _lastTimeSinceStartup = EditorApplication.timeSinceStartup;
            
            var e = Keyboard.current;
            if (e == null) return;
            if (Selection.activeTransform != transform) return;

            Vector3 step = Vector3.zero;
            if (e.sKey.isPressed) step += Vector3.back;
            if (e.wKey.isPressed) step += Vector3.forward;
            if (e.dKey.isPressed) step += Vector3.right;
            if (e.aKey.isPressed) step += Vector3.left;
            
            transform.position += step * (deltaTime * speed);
        }
    }
}
#endif
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Lib
{
    public class AnimatePingPongScale : MonoBehaviour
    {
        [SerializeField] public Axis axis;
        [SerializeField] public float speed;
        [SerializeField] public float step;
        
        private float _time;
        private Vector3 _startScale;

        private void Awake() => _startScale = transform.localScale;

        private void OnDisable()
        {
            _time = 0f;
            transform.localScale = _startScale;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetValue(float min, float max, float time, float speed) =>
            Mathf.SmoothStep(min, max, Mathf.PingPong(time * speed, 1f));

        private void Update()
        {
            _time += Time.deltaTime * speed;
            var scale = _startScale;

            switch (axis)
            {
                case Axis.X: scale.x += GetValue(0,step,_time,speed);break;
                case Axis.Y: scale.y += GetValue(0,step,_time,speed);break;
                case Axis.Z: scale.z += GetValue(0,step,_time,speed);break;
                default: throw new ArgumentOutOfRangeException();
            };

            transform.localScale = scale;
        }
    }
}
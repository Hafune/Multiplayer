using UnityEngine;

namespace Core.Lib
{
    public class TrailReset : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        
        private void OnValidate() => _trailRenderer = GetComponent<TrailRenderer>();

        private void OnDisable() => _trailRenderer.Clear();
    }
}
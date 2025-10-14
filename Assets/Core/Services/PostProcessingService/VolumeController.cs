using System.Linq;
using Lib;
using UnityEngine;
using UnityEngine.Rendering;

namespace Core.Services
{
    [RequireComponent(typeof(Volume))]
    public class VolumeController : MonoConstruct
    {
        [field: SerializeField] public Locations_Row Location { get; private set; }
        [SerializeField] private Volume _volume;
        [SerializeField] private GameObject _container;

        private void OnValidate()
        {
            _volume ??= GetComponent<Volume>();
            var loc = (Locations)Location;

            if (loc != null)
                name = loc.name;

            const string _containerName = "Container";
            if (!_container)
            {
                _container = GetComponents<Transform>().FirstOrDefault(t => t.name == _containerName)?.gameObject ??
                             new GameObject(_containerName);
                _container.transform.parent = transform;
                _container.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }

        private void Awake()
        {
            _volume.weight = 0;
            _container?.SetActive(false);
        }

        public void Activate()
        {
            _container?.SetActive(true);
            _volume.weight = 1;
        }

        public void DeActivate()
        {
            _container?.SetActive(false);
            _volume.weight = 0;
        }
    }
}
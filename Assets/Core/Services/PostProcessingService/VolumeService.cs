using System.Linq;
using Lib;
using UnityEngine;

namespace Core.Services
{
    public class VolumeService : MonoConstruct
    {
        [SerializeField] private VolumeController _defaultController;
        private VolumeController[] _controllers;
        [field: SerializeField] public VolumeController Current { get; private set; }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                Current = null;
        }

        private void Awake()
        {
            _controllers = GetComponentsInChildren<VolumeController>();
            var locationService = context.Resolve<MyLocationService>();
            locationService.OnChange += () => ChangeByLocation(locationService.CurrentLocations);
        }

        private void ChangeByLocation(Locations location)
        {
            var nextVolume = _controllers.FirstOrDefault(l => (Locations)l.Location == location);

            Current?.DeActivate();
            Current = nextVolume ?? _defaultController;
            Current?.Activate();
        }
    }
}
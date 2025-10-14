using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Services
{
    public class MyLocationService
    {
        public event Action OnChange;

        private readonly Dictionary<Entrances, Action<Action>> _locationConstructors = new();
        private readonly Dictionary<Entrances, Vector3> _spawnPoints = new();
        private readonly HashSet<Locations> _visitedLocations = new();

        public Locations CurrentLocations { get; private set; }

        public bool IsVisited(Locations location) => _visitedLocations.Contains(location);

        public void RegisterSpawnPoint(Entrances entrance, Vector3 point)
        {
#if UNITY_EDITOR
            if (_spawnPoints.ContainsKey(entrance))
                Debug.LogWarning($"{nameof(entrance)} уже зарегистрирован: " + entrance.name);
#endif

            _spawnPoints[entrance] = point;
        }
        
        public void ChangeLocation(Locations location)
        {
            CurrentLocations = location;
            _visitedLocations.Add(CurrentLocations);
            OnChange?.Invoke();
        }

        public void ChangeLocationByEntrance(Entrances entrance, Action<Vector3> callback)
        {
            var point = _spawnPoints[entrance];
            callback.Invoke(point);
            CurrentLocations = entrance.Location;
            _visitedLocations.Add(CurrentLocations);
            OnChange?.Invoke();
        }

        public void LoadLocationAndReturnPosition(Entrances from, Action<Vector3> callback)
        {
            var to = EntranceRelations.FindEntity(i => i.From == from)?.To;
            to ??= EntranceRelations.FindEntity(i => i.To == from && i.IsBidirectional).From;

            if (_spawnPoints.TryGetValue(to, out var point))
            {
                callback.Invoke(point);
                CurrentLocations = to.Location;
                _visitedLocations.Add(CurrentLocations);
                OnChange?.Invoke();
            }
            else
            {
                _locationConstructors[to].Invoke(() => LoadLocationAndReturnPosition(from, callback));
                _locationConstructors.Remove(to);
            }
        }

        public void RegisterLocationConstructor(IEnumerable<Entrances> entrances, Action<Action> action)
        {
#if UNITY_EDITOR
            if (_locationConstructors.Keys.Any(entrances.Contains))
                Debug.LogError($"Конструктор уже добавлен!!");

            foreach (var entrance in entrances)
                if (_spawnPoints.ContainsKey(entrance))
                    Debug.LogError("Конструктор не будет вызван так как точка уже существует: " + entrance.name);
#endif

            foreach (var entrance in entrances)
                _locationConstructors[entrance] = action;
        }
    }
}
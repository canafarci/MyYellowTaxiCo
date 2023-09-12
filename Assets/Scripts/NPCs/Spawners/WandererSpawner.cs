using System;
using TaxiGame.GameState;
using TaxiGame.Vehicles;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class WandererSpawner : MonoBehaviour
    {
        [SerializeField] private Waypoint[] _waypoints;
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _spawnTime;
        private float _currentTime;
        private Wanderer.Factory _factory;
        private ProgressionState _progressionState;
        //WandererCamera subscribes to this event to show spawned wanderer
        public event EventHandler<OnWandererSpawnedArgs> OnWandererSpawned;

        [Inject]
        private void Init(Wanderer.Factory factory, ProgressionState model)
        {
            _factory = factory;
            _progressionState = model;
        }

        private void Awake()
        {
            if (!_progressionState.IsVIPTutorialComplete())
            {
                _currentTime = 5f;
            }
            else
            {
                _currentTime = _spawnTime;
            }
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime < 0)
            {
                Spawn();
                _currentTime = _spawnTime;
            }
        }

        private void Spawn()
        {
            Wanderer wanderer = _factory.Create(_prefab, _spawnTransform, _waypoints);

            _progressionState.HandleVIPSpawned(wanderer);
            OnWandererSpawned?.Invoke(this, new OnWandererSpawnedArgs { Target = wanderer.transform });
        }
    }
    public class OnWandererSpawnedArgs : EventArgs
    {
        public Transform Target;
    }
}
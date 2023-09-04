using System.Collections;
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
        private VehicleProgressionModel _vehicleProgressModel;

        [Inject]
        private void Init(Wanderer.Factory factory, VehicleProgressionModel model)
        {
            _factory = factory;
            _vehicleProgressModel = model;
        }


        private void Awake()
        {
            if (!_vehicleProgressModel.IsVIPTutorialComplete())
            {
                _currentTime = 5f;
            }
            else
                _currentTime = _spawnTime;
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

            _vehicleProgressModel.HandleVIPSpawned(wanderer);
        }
    }
}
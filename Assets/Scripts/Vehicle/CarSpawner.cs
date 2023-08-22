using System;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _enterNode, _exitNode;
        [SerializeField] private Animator _parkAnimator;
        [SerializeField] private Enums.StackableItemType _hatType;
        [SerializeField] private CarSpawnerID _carSpawnerID;

        private Vehicle.Factory _factory;
        private VehicleSpot _spot;
        private CarSpawnDataProvider _provider;
        private MoneyStacker _stacker;

        public static event EventHandler<OnNewSpawnerActivatedEventArgs> OnNewSpawnerActivated;
        public event Action OnCarSpawned;

        [Inject]
        private void Init(Vehicle.Factory factory,
                            VehicleSpot spot, MoneyStacker stacker,
                            CarSpawnDataProvider provider)
        {
            _factory = factory;
            _spot = spot;
            _stacker = stacker;
            _provider = provider;
        }

        private void Awake()
        {
            OnNewSpawnerActivated?.Invoke(this, new OnNewSpawnerActivatedEventArgs { HatType = _hatType });
        }

        private void Start()
        {
            SpawnCar(true);
        }

        public void SpawnCar(bool isInitialSpawn = false)
        {
            SpawnedCarData spawnData = GetSpawnData(isInitialSpawn);

            VehicleConfiguration vehicleConfig = new VehicleConfiguration(_parkAnimator,
                                            _enterNode,
                                            _exitNode,
                                            _spot,
                                            this);

            _factory.Create(spawnData.Prefab, vehicleConfig, spawnData.VehicleInPlaceCallbacks);

            OnCarSpawned?.Invoke();
        }
        private SpawnedCarData GetSpawnData(bool isInitialSpawn)
        {
            if (isInitialSpawn)
                return _provider.GetInitialCarSpawnData(_hatType);
            else
                return _provider.GetCarSpawnData(_carSpawnerID, _hatType);
        }
    }

    public class OnNewSpawnerActivatedEventArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
    }
}
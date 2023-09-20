using System;
using System.Collections.Generic;
using TaxiGame.Installers;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Creation
{
    public class CarSpawnDataProvider
    {
        private ISpawnDataFactory _brokenCarFactory;
        private ISpawnDataFactory _regularCarFactory;
        private ISpawnDataFactory _specialCarFactory;
        private VehicleProgressionModel _vehicleProgressionModel;

        private const float BROKEN_CAR_SPAWN_CHANCE = 0.33f;

        public CarSpawnDataProvider([Inject(Id = VehicleFactoryID.RegularCarFactory)] ISpawnDataFactory regularCarFactory,
                                    [Inject(Id = VehicleFactoryID.BrokenCarFactory)] ISpawnDataFactory brokenCarFactory,
                                    [Inject(Id = VehicleFactoryID.ProgressionCarFactory)] ISpawnDataFactory specialCarFactory,
                                    VehicleProgressionModel model)
        {
            _brokenCarFactory = brokenCarFactory;
            _regularCarFactory = regularCarFactory;
            _specialCarFactory = specialCarFactory;
            _vehicleProgressionModel = model;
        }

        public SpawnedCarData GetInitialCarSpawnData(CarSpawnerID carSpawnerID)
        {
            return _regularCarFactory.CreateCarSpawnData(carSpawnerID);
        }

        public SpawnedCarData GetCarSpawnData(CarSpawnerID carSpawnerID)
        {
            // Check for special progression events
            if (CheckShouldSpawnSpecialProgressionCar(carSpawnerID))
            {
                return _specialCarFactory.CreateCarSpawnData(carSpawnerID);
            }

            // Return regular or broken car based on chance
            if (ShouldSpawnRandomBrokenCar(BROKEN_CAR_SPAWN_CHANCE))
            {
                return _brokenCarFactory.CreateCarSpawnData(carSpawnerID);
            }
            else
            {
                return _regularCarFactory.CreateCarSpawnData(carSpawnerID);
            }
        }

        private bool CheckShouldSpawnSpecialProgressionCar(CarSpawnerID carSpawnerID)
        {
            return _vehicleProgressionModel.ShouldSpawnSpecialProgressionCar(carSpawnerID);
        }

        private bool ShouldSpawnRandomBrokenCar(float chance)
        {
            if (chance <= 0.0f || chance >= 1.0f)
            {
                throw new ArgumentOutOfRangeException("percentile", "Percentile must be between 0 and 1 (exclusive).");
            }

            return UnityEngine.Random.value < chance;
        }
    }

    public struct SpawnedCarData
    {
        public List<Action> VehicleInPlaceCallbacks;
        public GameObject Prefab;
        public SpawnedCarData(GameObject prefab)
        {
            VehicleInPlaceCallbacks = new();
            Prefab = prefab;
        }
    }
}

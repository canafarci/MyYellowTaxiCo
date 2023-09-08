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
        private ICarFactory _brokenCarFactory;
        private ICarFactory _regularCarFactory;
        private ICarFactory _specialCarFactory;
        private VehicleProgressionModel _model;

        private const float BROKEN_CAR_SPAWN_CHANCE = 0.33f;

        public CarSpawnDataProvider([Inject(Id = VehicleFactoryID.RegularCarFactory)] ICarFactory regularCarFactory,
                                    [Inject(Id = VehicleFactoryID.BrokenCarFactory)] ICarFactory brokenCarFactory,
                                    [Inject(Id = VehicleFactoryID.ProgressionCarFactory)] ICarFactory specialCarFactory,
                                    VehicleProgressionModel model)
        {
            _brokenCarFactory = brokenCarFactory;
            _regularCarFactory = regularCarFactory;
            _specialCarFactory = specialCarFactory;
            _model = model;
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
            return _model.ShouldSpawnSpecialProgressionCar(carSpawnerID);
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

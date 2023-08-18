using System;
using System.Collections.Generic;
using TaxiGame.Scripts;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class CarSpawnDataProvider
    {
        private BrokenCarFactory _brokenCarFactory;
        private RegularCarFactory _regularCarFactory;
        private SpecialProgressionEventCarFactory _specialCarFactory;
        private VehicleProgressionModel _model;

        private const float BROKEN_CAR_SPAWN_CHANCE = 0.33f;

        public CarSpawnDataProvider(RegularCarFactory regularCarFactory,
                        BrokenCarFactory brokenCarFactory,
                        SpecialProgressionEventCarFactory specialCarFactory,
                        VehicleProgressionModel model)
        {
            _brokenCarFactory = brokenCarFactory;
            _regularCarFactory = regularCarFactory;
            _specialCarFactory = specialCarFactory;
            _model = model;
        }

        public SpawnedCarData GetInitialCarSpawnData(Enums.StackableItemType spawnerType)
        {
            return _regularCarFactory.CreateRegularCarSpawnData(spawnerType);
        }

        public SpawnedCarData GetCarSpawnData(CarSpawnerID carSpawnerID, Enums.StackableItemType spawnerType)
        {
            // Check for special progression events
            if (CheckShouldSpawnSpecialProgressionCar(carSpawnerID))
            {
                return _specialCarFactory.CreateSpecialProgressionCarSpawnData(carSpawnerID);
            }

            // Return regular or broken car based on chance
            if (ShouldSpawnRandomBrokenCar(BROKEN_CAR_SPAWN_CHANCE))
            {
                return _brokenCarFactory.CreateRandomBrokenCarSpawnData(spawnerType);
            }
            else
            {
                return _regularCarFactory.CreateRegularCarSpawnData(spawnerType);
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

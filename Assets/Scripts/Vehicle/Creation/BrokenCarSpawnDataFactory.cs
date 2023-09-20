using System;
using System.Collections.Generic;
using TaxiGame.GameState;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Creation
{
    public class BrokenCarSpawnDataFactory : ISpawnDataFactory
    {
        private BrokenCarsSO _brokenVehiclesSO;
        private ProgressionState _progressionState;

        //constants
        private const int DEFAULT_CAR_MAX_RANGE = 3;
        private const int SECOND_TUTORIAL_MAX_RANGE = 2;
        private const int FIRST_TUTORIAL_MAX_RANGE = 1;

        public BrokenCarSpawnDataFactory(BrokenCarsSO vehiclesSO, ProgressionState progressionState)
        {
            _brokenVehiclesSO = vehiclesSO;
            _progressionState = progressionState;
        }

        public SpawnedCarData CreateCarSpawnData(CarSpawnerID carSpawnerID)
        {
            int maxRange = GetMaxRangeForColoredCars(carSpawnerID);
            int randomIndex = GetRandomInt(maxRange);

            GameObject prefab = GetRandomBrokenCarPrefab(carSpawnerID, randomIndex);

            return new SpawnedCarData(prefab);
        }
        private int GetMaxRangeForColoredCars(CarSpawnerID carSpawnerID)
        {
            if (IsTaxiCar(carSpawnerID))
            {
                return GetYellowCarMaxRange();
            }
            else
            {
                return DEFAULT_CAR_MAX_RANGE;
            }
        }
        private int GetYellowCarMaxRange()
        {
            if (_progressionState.IsTutorialSequenceComplete(UnlockSequence.TireChangeTutorial))
            {
                return DEFAULT_CAR_MAX_RANGE;
            }
            else if (_progressionState.IsTutorialSequenceComplete(UnlockSequence.EngineRepairTutorial))
            {
                return SECOND_TUTORIAL_MAX_RANGE;
            }
            else
            {
                return FIRST_TUTORIAL_MAX_RANGE;
            }
        }
        private GameObject GetRandomBrokenCarPrefab(CarSpawnerID carSpawnerID, int index)
        {
            return carSpawnerID switch
            {
                CarSpawnerID.LimoSpawner => _brokenVehiclesSO.BrokenLimos[index],
                CarSpawnerID.SuberSpawner => _brokenVehiclesSO.BrokenSubers[index],
                //Rest of IDs all belong to taxi cars
                _ => _brokenVehiclesSO.BrokenTaxis[index],
            };
        }

        private int GetRandomInt(int range)
        {
            return UnityEngine.Random.Range(0, range);
        }

        private bool IsTaxiCar(CarSpawnerID carSpawnerID)
        {
            return carSpawnerID != CarSpawnerID.LimoSpawner && carSpawnerID != CarSpawnerID.SuberSpawner;
        }
    }
}

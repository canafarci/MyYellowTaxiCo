using System;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Creation
{
    public class BrokenCarFactory : ICarFactory
    {
        private BrokenCarsSO _brokenVehiclesSO;
        //constants
        private const int DEFAULT_CAR_MAX_RANGE = 3;

        public BrokenCarFactory(BrokenCarsSO vehiclesSO)
        {
            _brokenVehiclesSO = vehiclesSO;
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
            //If ID belongs to a taxi car
            if (carSpawnerID != CarSpawnerID.LimoSpawner || carSpawnerID != CarSpawnerID.SuberSpawner)
            {
                return GetYellowCarMaxRange();
            }
            else
            {
                return DEFAULT_CAR_MAX_RANGE;
            }
        }
        //TODO remove magic variables
        private int GetYellowCarMaxRange()
        {
            if (PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
            {
                return 3;
            }
            else if (PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
            {
                return 2;
            }
            else
            {
                return 1;
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
    }
}

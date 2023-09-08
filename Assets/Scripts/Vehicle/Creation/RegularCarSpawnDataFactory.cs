using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    public class RegularCarSpawnDataFactory : ISpawnDataFactory
    {
        private RegularCarsSO _regularCarsSO;
        public RegularCarSpawnDataFactory(RegularCarsSO regularCarsSO)
        {
            _regularCarsSO = regularCarsSO;
        }
        public SpawnedCarData CreateCarSpawnData(CarSpawnerID carSpawnerID)
        {
            return GetRegularCarSpawnData(carSpawnerID);
        }

        public SpawnedCarData GetRegularCarSpawnData(CarSpawnerID carSpawnerID)
        {
            GameObject prefab = GetRegularCarPrefab(carSpawnerID);
            return new SpawnedCarData(prefab);
        }
        private GameObject GetRegularCarPrefab(CarSpawnerID carSpawnerID)
        {
            return carSpawnerID switch
            {
                CarSpawnerID.SuberSpawner => _regularCarsSO.RegularSuber,
                CarSpawnerID.LimoSpawner => _regularCarsSO.RegularLimo,
                //rest of IDs all belong to Taxi cars
                _ => _regularCarsSO.RegularTaxi
            };
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    public class RegularCarFactory : MonoBehaviour
    {
        [SerializeField] RegularCarPrefabs _carPrefabs;

        public SpawnedCarData CreateRegularCarSpawnData(InventoryObjectType spawnerType)
        {
            return GetRegularCarSpawnData(spawnerType);
        }

        public SpawnedCarData GetRegularCarSpawnData(InventoryObjectType spawnerType)
        {
            GameObject prefab = GetRegularCarPrefab(spawnerType);
            return new SpawnedCarData(prefab);
        }
        private GameObject GetRegularCarPrefab(InventoryObjectType spawnerType)
        {
            return spawnerType switch
            {
                InventoryObjectType.YellowHat => _carPrefabs.RegularYellowCar,
                InventoryObjectType.PurpleHat => _carPrefabs.RegularPurpleCar,
                InventoryObjectType.BlackHat => _carPrefabs.RegularBlackCar,
                _ => null
            };
        }


        [Serializable]
        public struct RegularCarPrefabs
        {
            public GameObject RegularYellowCar;
            public GameObject RegularPurpleCar;
            public GameObject RegularBlackCar;
        }
    }
}

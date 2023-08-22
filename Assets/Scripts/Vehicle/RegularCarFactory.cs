using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles
{
    public class RegularCarFactory : MonoBehaviour
    {
        [SerializeField] RegularCarPrefabs _carPrefabs;

        public SpawnedCarData CreateRegularCarSpawnData(Enums.StackableItemType spawnerType)
        {
            return GetRegularCarSpawnData(spawnerType);
        }

        public SpawnedCarData GetRegularCarSpawnData(Enums.StackableItemType spawnerType)
        {
            GameObject prefab = GetRegularCarPrefab(spawnerType);
            return new SpawnedCarData(prefab);
        }
        private GameObject GetRegularCarPrefab(Enums.StackableItemType spawnerType)
        {
            return spawnerType switch
            {
                Enums.StackableItemType.YellowHat => _carPrefabs.RegularYellowCar,
                Enums.StackableItemType.PurpleHat => _carPrefabs.RegularPurpleCar,
                Enums.StackableItemType.BlackHat => _carPrefabs.RegularBlackCar,
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

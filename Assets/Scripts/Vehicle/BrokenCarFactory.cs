using System;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Vehicles
{
    public class BrokenCarFactory : MonoBehaviour
    {
        [SerializeField] private BrokenCarPrefabs _brokenCarPrefabs;
        private const int DEFAULT_CAR_MAX_RANGE = 3;

        public SpawnedCarData CreateRandomBrokenCarSpawnData(InventoryObjectType spawnerType)
        {
            int maxRange = GetMaxRangeForColoredCars(spawnerType);
            int randomIndex = GetRandomInt(maxRange);

            GameObject prefab = GetRandomBrokenCarPrefab(spawnerType, randomIndex);

            return new SpawnedCarData(prefab);
        }
        private int GetMaxRangeForColoredCars(InventoryObjectType spawnerType)
        {
            if (spawnerType == InventoryObjectType.YellowHat)
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
        private GameObject GetRandomBrokenCarPrefab(InventoryObjectType spawnerType, int index)
        {
            return spawnerType switch
            {
                InventoryObjectType.YellowHat => _brokenCarPrefabs.BrokenYellowCars[index],
                InventoryObjectType.PurpleHat => _brokenCarPrefabs.BrokenPurpleCars[index],
                _ => _brokenCarPrefabs.BrokenBlackCars[index]
            };
        }

        private int GetRandomInt(int range)
        {
            return UnityEngine.Random.Range(0, range);
        }
    }

    [Serializable]
    public struct BrokenCarPrefabs
    {
        public GameObject[] BrokenYellowCars;
        public GameObject[] BrokenPurpleCars;
        public GameObject[] BrokenBlackCars;
    }
}

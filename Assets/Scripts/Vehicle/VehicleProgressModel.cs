using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleProgressModel : MonoBehaviour
    {
        [SerializeField] CarPrefabs _carPrefabs;
        private GameProgressModel _progressModel;
        public CarSpawnData GetCarSpawnData(CarSpawnerID carSpawner, Enums.StackableItemType spawnerType)
        {
            if (spawnerType == Enums.StackableItemType.YellowHat)
            {
                if (carSpawner == CarSpawnerID.FirstYellowSpawner && !PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
                {
                    return new CarSpawnData(() => _progressModel.FirstReturnCarWithoutCharger(),
                                            _carPrefabs.NoChargeYellowCar);
                }
                else
                {
                    return new CarSpawnData(() => { },
                                             GetCarPrefab(spawnerType));
                }
            }
            else
            {
                Debug.LogError("NO MATCHING CAR SPAWN CONFIGURATION");
                return new CarSpawnData(null, null);
            }
        }

        public CarSpawnData GetFirstCarSpawnData(Enums.StackableItemType spawnerType)
        {
            return new CarSpawnData(() => { }, GetCarPrefab(spawnerType, true));
        }


        private GameObject GetCarPrefab(Enums.StackableItemType spawnerType, bool isFirstSpawn = false)
        {
            //there is a 33% change to get a broken car, unless it is the first spawn
            if (!isFirstSpawn)
            {
                int randInt = GetRandomInt(3);
                if (randInt == 0)
                {
                    return GetRandomBrokenCar(spawnerType);
                }
            }

            return GetRegularCar(spawnerType);
        }

        private GameObject GetRegularCar(Enums.StackableItemType spawnerType)
        {
            if (spawnerType == Enums.StackableItemType.YellowHat)
                return _carPrefabs.RegularYellowCar;
            else if (spawnerType == Enums.StackableItemType.PurpleHat)
                return _carPrefabs.RegularPurpleCar;
            else
                return _carPrefabs.RegularBlackCar;
        }

        private GameObject GetRandomBrokenCar(Enums.StackableItemType spawnerType)
        {
            if (spawnerType == Enums.StackableItemType.YellowHat)
            {
                int randIntRange = GetRandomIntRangeForYellowCars();
                int randInt = GetRandomInt(randIntRange);
                return _carPrefabs.BrokenYellowCars[randInt];
            }
            else
            {
                int randInt = GetRandomInt(3);

                if (spawnerType == Enums.StackableItemType.PurpleHat)
                    return _carPrefabs.BrokenPurpleCars[randInt];
                else
                    return _carPrefabs.BrokenBlackCars[randInt];

            }
        }

        private int GetRandomIntRangeForYellowCars()
        {
            int range = 0;

            if (PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
                range = 3;
            else if (PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
                range = 2;
            else
                range = 1;

            return range;
        }

        private int GetRandomInt(int range)
        {
            return UnityEngine.Random.Range(0, range);
        }

        //Initialization
        [Inject]
        private void Init(GameProgressModel progressModel)
        {
            _progressModel = progressModel;
        }


    }


    public enum CarSpawnerID
    {
        FirstYellowSpawner
    }


    public struct CarSpawnData
    {
        public Action VehicleInPlaceCallback;
        public GameObject Prefab;
        public CarSpawnData(Action callback, GameObject prefab)
        {
            VehicleInPlaceCallback = callback;
            Prefab = prefab;
        }
    }


    [Serializable]
    public struct CarPrefabs
    {
        public GameObject RegularYellowCar;
        public GameObject RegularPurpleCar;
        public GameObject RegularBlackCar;

        public GameObject NoChargeYellowCar;
        public GameObject BrokenEngineYellowCar;
        public GameObject FlatTireYellowCar;

        public GameObject[] BrokenYellowCars;
        public GameObject[] BrokenPurpleCars;
        public GameObject[] BrokenBlackCars;

    }
}

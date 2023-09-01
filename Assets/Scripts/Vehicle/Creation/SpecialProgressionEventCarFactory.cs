using System;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Creation
{
    public class SpecialProgressionEventCarFactory : MonoBehaviour
    {
        [SerializeField] SpecialEventBrokenCars _specialCarPrefabs;

        private GameProgressModel _progressModel;

        [Inject]
        private void Init(GameProgressModel progressModel)
        {
            _progressModel = progressModel;
        }

        public SpawnedCarData CreateSpecialProgressionCarSpawnData(CarSpawnerID carSpawner)
        {
            GameObject prefab = GetSpecialProgressionCarPrefab(carSpawner);
            Action progressionEvent = GetProgressionEvent(carSpawner);
            return GetCarSpawnDataWithProgressionEvent(prefab, progressionEvent);
        }

        private GameObject GetSpecialProgressionCarPrefab(CarSpawnerID carSpawner)
        {
            return carSpawner switch
            {
                CarSpawnerID.FirstYellowSpawner => _specialCarPrefabs.NoChargeYellowCar,
                CarSpawnerID.SecondYellowSpawner => _specialCarPrefabs.BrokenEngineYellowCar,
                CarSpawnerID.ThirdYellowSpawner => _specialCarPrefabs.FlatTireYellowCar,
                _ => null
            };
        }

        private Action GetProgressionEvent(CarSpawnerID carSpawner)
        {
            return carSpawner switch
            {
                CarSpawnerID.FirstYellowSpawner => _progressModel.FirstReturnCarWithoutCharger,
                CarSpawnerID.SecondYellowSpawner => _progressModel.SecondReturnBroken,
                CarSpawnerID.ThirdYellowSpawner => _progressModel.ThirdReturnBroken,
                _ => null
            };
        }

        private SpawnedCarData GetCarSpawnDataWithProgressionEvent(GameObject prefab, Action progressionEvent)
        {
            SpawnedCarData data = new SpawnedCarData(prefab);
            data.VehicleInPlaceCallbacks.Add(progressionEvent);
            return data;
        }
    }
    public enum CarSpawnerID
    {
        FirstYellowSpawner,
        SecondYellowSpawner,
        ThirdYellowSpawner
    }

    [Serializable]
    public struct SpecialEventBrokenCars
    {
        public GameObject NoChargeYellowCar;
        public GameObject BrokenEngineYellowCar;
        public GameObject FlatTireYellowCar;
    }
}

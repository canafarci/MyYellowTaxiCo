using System;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Creation
{
    public class ProgressionCarSpawnDataFactory : ISpawnDataFactory
    {
        private ProgressionCarsSO _progressionCarsSO;
        private GameProgressModel _progressModel;

        public ProgressionCarSpawnDataFactory(GameProgressModel progressModel, ProgressionCarsSO progressionCarsSO)
        {
            _progressModel = progressModel;
            _progressionCarsSO = progressionCarsSO;
        }

        public SpawnedCarData CreateCarSpawnData(CarSpawnerID carSpawner)
        {
            GameObject prefab = GetSpecialProgressionCarPrefab(carSpawner);
            Action progressionEvent = GetProgressionEvent(carSpawner);
            return GetCarSpawnDataWithProgressionEvent(prefab, progressionEvent);
        }

        private GameObject GetSpecialProgressionCarPrefab(CarSpawnerID carSpawner)
        {
            return carSpawner switch
            {
                CarSpawnerID.FirstYellowSpawner => _progressionCarsSO.NoChargeTaxi,
                CarSpawnerID.SecondYellowSpawner => _progressionCarsSO.BrokenEngineTaxi,
                CarSpawnerID.ThirdYellowSpawner => _progressionCarsSO.FlatTireTaxi,
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
        ThirdYellowSpawner,
        SuberSpawner,
        LimoSpawner
    }
}

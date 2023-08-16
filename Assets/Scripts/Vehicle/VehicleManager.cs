using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleManager
    {
        private Dictionary<VehicleSpot, int> _vehicleTripCountLookup = new();
        private LevelProgress _progresser;
        private VehicleProgressModel _model;

        [Inject]
        private void Init(LevelProgress progresser, VehicleProgressModel model)
        {
            _progresser = progresser;
            _model = model;
        }

        public void OnVehicleDeparted()
        {
            _progresser.OnLevelProgress();
        }

        public bool CanSpawnDriver(VehicleSpot spot)
        {
            _vehicleTripCountLookup.TryGetValue(spot, out int tripCount);
            _vehicleTripCountLookup[spot] = tripCount + 1;
            //On the initial spawn of the vehicle, no driver should return
            if (tripCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public CarSpawnData GetInitialCarSpawnData(Enums.StackableItemType spawnerType)
        {
            return _model.GetFirstCarSpawnData(spawnerType);
        }

        public CarSpawnData GetCarSpawnData(CarSpawnerID carSpawnerID, Enums.StackableItemType spawnerType)
        {
            return _model.GetCarSpawnData(carSpawnerID, spawnerType);
        }

    }
}

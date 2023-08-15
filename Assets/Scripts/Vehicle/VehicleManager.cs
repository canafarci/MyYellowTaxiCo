using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleManager
    {
        private Dictionary<VehicleSpot, int> _vehicleTripCountLookup = new();
        private LevelProgress _progresser;

        [Inject]
        private void Init(LevelProgress progresser)
        {
            _progresser = progresser;
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
    }
}

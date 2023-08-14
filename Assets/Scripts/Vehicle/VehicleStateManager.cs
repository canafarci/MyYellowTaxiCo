using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;

namespace Taxi.Vehicle
{
    public class VehicleStateManager
    {
        private Dictionary<VehicleSpot, int> _vehicleTripCountLookup = new();

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

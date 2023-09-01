using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles
{
    public interface IVehicleEvents
    {
        public event Action OnVehicleDeparted;
        public event Action<int> OnVehicleMoneyEarned;
    }
}

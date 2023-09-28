using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using TaxiGame.Vehicles;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class RepairCarState : MonoBehaviour, IHelperNPCState
    {
        private List<Transform> _brokenCarLocations;

        private void Start()
        {
            VehicleSpot.OnVehicleReturned += VehicleSpot_OnVehicleReturnedHandler;
        }

        private void VehicleSpot_OnVehicleReturnedHandler(object sender, OnVehicleReturnedArgs e)
        {
            if (e.IsBrokenCar)
            {
                _brokenCarLocations.Add(e.SpawnerTransform);
            }
        }

        public void Enter()
        {
            print("entering new state");
        }

        public void Exit()
        {
            print("exiting new state");
        }

        public void Tick()
        {
            print("ticking new state");
        }
    }
}

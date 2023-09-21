using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using TaxiGame.Vehicles;
using UnityEngine;

namespace TaxiGame.Scripts
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
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}

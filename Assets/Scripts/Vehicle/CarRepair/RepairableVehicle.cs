using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Repair
{
    public abstract class RepairableVehicle : MonoBehaviour
    {
        public static event EventHandler<OnVehicleRepairedArgs> OnVehicleRepaired;
        protected VehicleModel _vehicleModel;

        [Inject]
        private void Init(VehicleModel model)
        {
            _vehicleModel = model;
        }


        private void OnTriggerEnter(Collider other)
        {
            if ((other.CompareTag("Player") || other.CompareTag("HatHelperNPC")) && VehicleCanBeRepaired(other, out Inventory inventory))
            {
                Repair(inventory);
            }
        }

        protected abstract bool VehicleCanBeRepaired(Collider other, out Inventory inventory);
        protected abstract void Repair(Inventory inventory);
        protected void InvokeVehicleRepairedEvent()
        {
            OnVehicleRepaired?.Invoke(this, new OnVehicleRepairedArgs
            {
                HatType = _vehicleModel.GetHatType(),
                VehicleSpot = _vehicleModel.GetConfig().VehicleSpot
            });
        }

    }

    public class OnVehicleRepairedArgs : EventArgs
    {
        public InventoryObjectType HatType;
        public VehicleSpot VehicleSpot;
    }
}
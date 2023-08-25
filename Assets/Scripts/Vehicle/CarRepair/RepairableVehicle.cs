using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Characters;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Repair
{
    public abstract class RepairableVehicle : MonoBehaviour
    {
        [SerializeField] private InventoryObjectType _repairObjectType;
        public static event EventHandler<OnVehicleRepairedArgs> OnVehicleRepaired;
        protected VehicleModel _vehicleModel;

        [Inject]
        private void Init(VehicleModel model)
        {
            _vehicleModel = model;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!(other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))) return;

            Inventory inventory = other.GetComponent<IInventoryHolder>().GetInventory();

            if (VehicleCanBeRepaired(inventory, _repairObjectType))
            {
                StartCoroutine(Repair(inventory));
            }

        }
        protected bool VehicleCanBeRepaired(Inventory inventory, InventoryObjectType repairObjectType)
        {
            bool carIsNotRepaired = _vehicleModel.IsCarBroken();


            bool inventoryHasObject = inventory.HasInventoryObjectType(repairObjectType);

            return carIsNotRepaired && inventoryHasObject;
        }
        protected abstract IEnumerator Repair(Inventory inventory);
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
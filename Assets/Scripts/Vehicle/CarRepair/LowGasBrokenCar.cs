using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Characters;
using TaxiGame.Items;
using TaxiGame.Vehicles;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Repair
{
    public class LowGasBrokenCar : RepairableVehicle, IHandleHolder
    {
        [SerializeField] private Transform _handleAttachTransform;
        private Handle _handle;
        private VehicleProgressionModel _progressionModel;

        public static EventHandler<GasHandleAttachToCarEventArgs> OnGasHandleAttachedToCar;

        [Inject]
        private void Init(VehicleProgressionModel progressionModel)
        {
            _progressionModel = progressionModel;
        }

        protected override bool VehicleCanBeRepaired(Collider other, out Inventory inventory)
        {
            bool carIsNotRepaired = _vehicleModel.IsCarBroken();

            inventory = other.GetComponent<IInventoryHolder>().GetInventory();
            bool inventoryHasObject = inventory.HasInventoryObjectType(InventoryObjectType.GasHandle);

            return carIsNotRepaired && inventoryHasObject;
        }

        protected override void Repair(Inventory inventory)
        {
            StartCoroutine(LowGasCarRepair(inventory));

        }

        private IEnumerator LowGasCarRepair(Inventory inventory)
        {
            Handle handle = inventory.PopInventoryObject(InventoryObjectType.GasHandle) as Handle;
            handle.ChangeOwner(this);
            yield return new WaitForSeconds(Globals.LOW_GAS_CAR_ATTACH_HANDLE_DURATION);
            OnGasHandleAttachedToCar?.Invoke(this, new GasHandleAttachToCarEventArgs { GasHandle = _handle });
            yield return new WaitForSeconds(Globals.LOW_GAS_CAR_REPAIR_DURATION);

            InvokeVehicleRepairedEvent();
            _vehicleModel.SetCarNotBroken();
            _handle.ReturnHandleToGasStation();

            _progressionModel.HandleLowGasCarRepaired();
        }

        //HandleHolder contract
        public void ClearHandle()
        {
            _handle = null;
        }

        public Transform GetTransform()
        {
            return _handleAttachTransform;
        }

        public void SetHandle(Handle handle)
        {
            _handle = handle;
        }

    }
    public class GasHandleAttachToCarEventArgs : EventArgs
    {
        public Handle GasHandle;
    }
}

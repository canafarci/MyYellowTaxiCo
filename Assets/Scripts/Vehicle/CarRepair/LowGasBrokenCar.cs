using System;
using System.Collections;
using System.Collections.Generic;
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

        protected override bool ActorMeetsRepairConditions(Collider other, out Inventory inventory)
        {
            inventory = other.GetComponent<IInventoryHolder>().GetInventory();
            return inventory.HasHandle();
        }

        protected override void Repair(Inventory inventory)
        {
            StartCoroutine(LowGasCarRepair(inventory));

        }

        private IEnumerator LowGasCarRepair(Inventory inventory)
        {
            inventory.GetHandle().ChangeOwner(this);
            yield return new WaitForSeconds(Globals.LOW_GAS_CAR_ATTACH_HANDLE_DURATION);
            OnGasHandleAttachedToCar?.Invoke(this, new GasHandleAttachToCarEventArgs { GasHandle = _handle });
            yield return new WaitForSeconds(Globals.LOW_GAS_CAR_REPAIR_DURATION);
            InvokeVehicleRepairedEvent();
            _progressionModel.HandleLowGasCarRepaired();
            _handle.ReturnHandleToGasStation();
        }

        //HandleHolder contract
        public void Clear()
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

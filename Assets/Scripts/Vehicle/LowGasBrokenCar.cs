using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class LowGasBrokenCar : RepairableVehicle, IHandleHolder
    {
        [SerializeField] private Transform _handleAttachTransform;
        private Handle _handle;
        public static EventHandler<GasHandleAttachToCarEventArgs> OnGasHandleAttachedToCar;


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
            yield return new WaitForSeconds(1f); //tween duration
            OnGasHandleAttachedToCar?.Invoke(this, new GasHandleAttachToCarEventArgs { GasHandle = inventory.GetHandle() });

            InvokeVehicleRepairedEvent();
            inventory.GetHandle().Return();

        }

    }
    public class GasHandleAttachToCarEventArgs : EventArgs
    {
        public Handle GasHandle;
    }
}

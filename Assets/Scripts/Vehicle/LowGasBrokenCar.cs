using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
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
            yield return new WaitForSeconds(1f); //TODO remove magic var //move tween duration
            OnGasHandleAttachedToCar?.Invoke(this, new GasHandleAttachToCarEventArgs { GasHandle = inventory.GetHandle() });
            yield return new WaitForSeconds(3f);
            InvokeVehicleRepairedEvent();
            _progressionModel.HandleLowGasCarRepaired();
            _handle.Return();
        }

    }
    public class GasHandleAttachToCarEventArgs : EventArgs
    {
        public Handle GasHandle;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public abstract class RepairableVehicle : MonoBehaviour
    {
        public static event EventHandler<OnVehicleRepairedArgs> OnVehicleRepaired;

        private VehicleModel _model;

        [Inject]
        private void Init(VehicleModel model)
        {
            _model = model;
        }


        private void OnTriggerEnter(Collider other)
        {
            if ((other.CompareTag("Player") || other.CompareTag("HatHelperNPC")) && ActorMeetsRepairConditions(other, out Inventory inventory))
            {
                Repair(inventory);
            }
        }

        protected abstract bool ActorMeetsRepairConditions(Collider other, out Inventory inventory);
        protected abstract void Repair(Inventory inventory);
        protected void InvokeVehicleRepairedEvent()
        {
            OnVehicleRepaired?.Invoke(this, new OnVehicleRepairedArgs
            {
                HatType = _model.GetHatType(),
                VehicleSpot = _model.GetConfig().VehicleSpot
            });
        }

    }

    public class OnVehicleRepairedArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
        public VehicleSpot VehicleSpot;
    }
}
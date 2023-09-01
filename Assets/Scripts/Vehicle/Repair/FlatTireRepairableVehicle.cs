using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Characters;
using TaxiGame.Items;
using UnityEngine;
using Zenject;


namespace TaxiGame.Vehicles.Repair
{
    public class FlatTireRepairableVehicle : RepairableVehicle
    {
        private IInputReader _reader;
        private VehicleProgressionModel _progressionModel;

        public event EventHandler<OnPlayerEnteredWithTireArgs> OnPlayerEnteredWithTire;

        [Inject]
        private void Init(VehicleProgressionModel progressionModel)
        {
            _progressionModel = progressionModel;
        }

        protected override IEnumerator Repair(Inventory inventory)
        {
            StackableItem item = inventory.PopInventoryObject(InventoryObjectType.Tire) as StackableItem;

            OnPlayerEnteredWithTire?.Invoke(this, new OnPlayerEnteredWithTireArgs { Item = item });
            yield return new WaitForSeconds(Globals.TIRE_DROP_TWEEN_DURATION);

            InvokeVehicleRepairedEvent();
            _vehicleModel.SetCarNotBroken();

            _progressionModel.HandleFlatTireRepaired();
        }
    }

    public class OnPlayerEnteredWithTireArgs : EventArgs
    {
        public StackableItem Item;
    }
}

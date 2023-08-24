using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Characters;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Repair
{
    public class BrokenEngineRepairableVehicle : RepairableVehicle
    {
        private IInputReader _reader;
        private VehicleProgressionModel _progressionModel;

        public EventHandler<OnPlayerEnteredWithToolboxArgs> OnPlayerEnteredWithToolbox;

        [Inject]
        private void Init(IInputReader reader, VehicleProgressionModel progressionModel)
        {
            _reader = reader;
            _progressionModel = progressionModel;
        }

        protected override bool VehicleCanBeRepaired(Collider other, out Inventory inventory)
        {
            bool carIsNotRepaired = _vehicleModel.IsCarBroken();

            inventory = other.GetComponent<IInventoryHolder>().GetInventory();
            bool inventoryHasObject = inventory.HasInventoryObjectType(InventoryObjectType.ToolBox);

            return carIsNotRepaired && inventoryHasObject;
        }

        protected override void Repair(Inventory inventory)
        {
            StartCoroutine(RepairBrokenEngine(inventory));
        }

        private IEnumerator RepairBrokenEngine(Inventory inventory)
        {
            //triggers animation to play because it modifies inventory
            StackableItem item = inventory.PopInventoryObject(InventoryObjectType.ToolBox) as StackableItem;
            _reader.Disable();

            OnPlayerEnteredWithToolbox?.Invoke(this, new OnPlayerEnteredWithToolboxArgs { Item = item });
            yield return new WaitForSeconds(Globals.TOOLBOX_DROP_TWEEN_DURATION + Globals.TOOLBOX_DROP_REPAIR_ANIMATION_DURATION);
            _reader.Enable();

            InvokeVehicleRepairedEvent();
            _vehicleModel.SetCarNotBroken();

            _progressionModel.HandleBrokenEngineRepaired();
        }

    }

    public class OnPlayerEnteredWithToolboxArgs : EventArgs
    {
        public StackableItem Item;
    }

}

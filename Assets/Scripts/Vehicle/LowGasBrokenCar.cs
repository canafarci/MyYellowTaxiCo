using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;

namespace TaxiGame.Scripts
{
    public class LowGasBrokenCar : RepairableVehicle
    {
        protected override bool ActorMeetsRepairConditions(Collider other, out Inventory inventory)
        {
            inventory = other.GetComponent<IInventoryHolder>().GetInventory();
            return true;
        }

        protected override void Repair(Inventory inventory)
        {
            InvokeVehicleRepairedEvent();
        }
    }
}

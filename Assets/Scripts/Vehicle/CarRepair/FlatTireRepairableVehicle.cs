using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Vehicles.Repair
{
    public class FlatTireRepairableVehicle : RepairableVehicle
    {
        protected override bool VehicleCanBeRepaired(Collider other, out Inventory inventory)
        {
            throw new System.NotImplementedException();
        }

        protected override void Repair(Inventory inventory)
        {
            throw new System.NotImplementedException();
        }
    }

    public class OnPlayerEnteredWithTireArgs : EventArgs
    {
        public StackableItem Item;
    }
}

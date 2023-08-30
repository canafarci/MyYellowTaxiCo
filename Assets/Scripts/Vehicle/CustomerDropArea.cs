using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class CustomerDropArea : MonoBehaviour
    {
        [Inject]
        private void Init(VehicleSpot spot)
        {
            _vehicleSpot = spot;
        }

        private VehicleSpot _vehicleSpot;

        private void OnTriggerEnter(Collider other)
        {
            if (_vehicleSpot.HasVehicle() && (other.CompareTag("Player") || other.CompareTag("HatHelperNPC")))
            {
                Inventory inventory = other.GetComponent<Inventory>();

                if (inventory.HasInventoryObjectType(InventoryObjectType.Customer))
                {
                    Customer customer = inventory.PopInventoryObject(InventoryObjectType.Customer) as Customer;
                    customer.GetFollower().StopFollowing();
                    customer.GetController().GoToCar(transform, () => _vehicleSpot.HandleCustomerArrival());
                    _vehicleSpot.SetCustomerWaiting(true);
                }
            }
        }
    }


}


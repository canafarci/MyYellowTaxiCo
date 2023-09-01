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
        private void Init(VehicleSpot spot, VehicleProgressionModel progressionModel)
        {
            _vehicleSpot = spot;
            _vehicleProgressionMpdel = progressionModel;
        }

        private VehicleSpot _vehicleSpot;
        private VehicleProgressionModel _vehicleProgressionMpdel;

        private void OnTriggerEnter(Collider other)
        {
            if (_vehicleSpot.HasVehicle() && (other.CompareTag("Player") || other.CompareTag("HatHelperNPC")))
            {
                Inventory inventory = other.GetComponent<Inventory>();

                if (inventory.HasInventoryObjectType(InventoryObjectType.Customer))
                {
                    SendCustomerToVehicleSpot(inventory);

                    _vehicleSpot.SetCustomerWaiting(true);
                    //Update persistent game state
                    _vehicleProgressionMpdel.HandleCustomerDropped();
                }
            }
        }

        private void SendCustomerToVehicleSpot(Inventory inventory)
        {
            FollowingNPC customer = inventory.PopInventoryObject(InventoryObjectType.Customer) as FollowingNPC;
            customer.GetFollower().StopFollowing();
            customer.GetController().GoToVehicleSpot(transform, () => _vehicleSpot.HandleCustomerArrival());
        }
    }


}


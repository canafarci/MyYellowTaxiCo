using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using TaxiGame.Items;
using System;
using TaxiGame.NPC;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class HeliDropArea : MonoBehaviour
    {
        [SerializeField] Transform _getInLocation;
        private HeliSpot _heliSpot;

        [Inject]
        private void Init(HeliSpot heliSpot)
        {
            _heliSpot = heliSpot;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Inventory inventory = other.GetComponent<Inventory>();
                CheckCanDropVIP(inventory);
            }
        }

        private void CheckCanDropVIP(Inventory inventory)
        {
            if (inventory.HasInventoryObjectType(InventoryObjectType.VIP))
            {
                Wanderer vip = inventory.PopInventoryObject(InventoryObjectType.VIP) as Wanderer;
                vip.GetFollower().StopFollowing();
                vip.GetController().GoToVehicleSpot(_getInLocation, () => _heliSpot.HandleVIPArrival());
            }
        }
    }
}
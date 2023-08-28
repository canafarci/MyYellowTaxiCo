using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class Customer : RiderNPC, IInventoryObject
    {
        private Follower _follower;
        public Follower GetFollower() => _follower;

        [Inject]
        private void Init(Follower follower)
        {
            _follower = follower;
        }

        public void FollowPlayer(Inventory inventory)
        {
            inventory.AddObjectToInventory(this);
            _follower.FollowPlayer(inventory.transform);
        }


        public InventoryObjectType GetObjectType() => InventoryObjectType.Customer;

    }
}

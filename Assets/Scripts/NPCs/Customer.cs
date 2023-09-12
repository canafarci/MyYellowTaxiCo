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
        bool _isSeated = false;

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
        //Getters-Setters
        public Follower GetFollower() => _follower;
        public InventoryObjectType GetObjectType() => InventoryObjectType.Customer;
        public void SetCustomerAsSeated() => _isSeated = true;
        public bool IsCustomerSeated() => _isSeated;
    }
}

using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class FollowingNPC : RiderNPC, IInventoryObject
    {
        [SerializeField] private InventoryObjectType _npcType;
        private Follower _follower;

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
        public Follower GetFollower() => _follower;
        public InventoryObjectType GetObjectType() => _npcType;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Characters
{
    public class Player : MonoBehaviour, IInventoryHolder
    {
        private Inventory _inventory;

        //Initialization
        [Inject]
        private void Init(Inventory inventory)
        {
            _inventory = inventory;
        }
        //Getters-Setters
        public Inventory GetInventory() => _inventory;
    }


}

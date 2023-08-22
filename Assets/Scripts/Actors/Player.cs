using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Characters
{
    public class Player : MonoBehaviour, IHandleHolder, IInventoryHolder
    {
        [SerializeField] private Transform _handTransform;
        private Inventory _inventory;

        //Initialization
        [Inject]
        private void Init(Inventory inventory)
        {
            _inventory = inventory;
        }
        //Getters-Setters
        public Inventory GetInventory() => _inventory;

        public Transform GetHandTransform() => _handTransform;

        public void Clear() => _inventory.Clear();

        public void SetHandle(Handle handle) => _inventory.SetHandle(handle);

        public Transform GetTransform() => _handTransform;
    }


}

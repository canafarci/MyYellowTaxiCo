using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Characters
{
    public class Player : MonoBehaviour, IHandleHolder
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
        public void ClearHandle()
        {
            _inventory.PopInventoryObject(InventoryObjectType.GasHandle);
        }

        public void SetHandle(Handle handle) => _inventory.AddObjectToInventory(handle);
        public Transform GetTransform() => _handTransform;
    }


}

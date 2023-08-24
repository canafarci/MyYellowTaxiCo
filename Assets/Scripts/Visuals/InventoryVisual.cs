using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class InventoryVisual : MonoBehaviour
    {
        private StackPositionCalculator _positionCalculator;
        private Inventory _inventory;
        private List<IInventoryObject> _playerItems = new();

        [Inject]
        private void Init(StackPositionCalculator positionCalculator, Inventory inventory)
        {
            _positionCalculator = positionCalculator;
            _inventory = inventory;
        }

        private void Start()
        {
            _inventory.OnInventoryModified += Inventory_InventoryModifiedHandler;
        }

        private void Inventory_InventoryModifiedHandler(object sender, InventoryModifiedArgs e)
        {
            if (IsNotStackableItem(e.InventoryObject.GetObjectType())) return;

            StackableItem item = e.InventoryObject as StackableItem;

            if (e.ItemAddedToInventory)
            {
                item.transform.parent = _positionCalculator.transform;
                Vector3 endPos = _positionCalculator.CalculatePosition(_playerItems, item);
                StartCoroutine(DotweenFX.StackTweenItem(item, endPos));
                _playerItems.Add(item);
            }
            else
            {
                _playerItems.Remove(item);
                _positionCalculator.RecalculatePositions(_playerItems);
            }

        }


        // public void StackItem(StackableItem item)
        // {
        //     item.transform.parent = _positionCalculator.transform;

        //     Vector3 endPos = _positionCalculator.CalculatePosition(_linkedList, item);
        //    StartCoroutine(DotweenFX.StackTweenItem(item, endPos));

        //REMOVE
        //_positionCalculator.RecalculatePositions(_linkedList);

        private bool IsNotStackableItem(InventoryObjectType objectType)
        {
            return objectType == InventoryObjectType.Follower || objectType == InventoryObjectType.GasHandle;
        }
    }
}

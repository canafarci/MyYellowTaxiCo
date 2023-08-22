using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TaxiGame.Characters;
using TaxiGame.NPC;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items
{
    public class Inventory : MonoBehaviour
    {
        public int StackableItemCapacity { get { return _maxStackSize; } set { _maxStackSize = value; } }
        public int FollowerCapacity { get { return _maxFollowerSize; } }

        [SerializeField] private int _maxStackSize;
        [SerializeField] private int _maxFollowerSize;
        private ItemUtility _itemUtility;

        private Dictionary<InventoryObjectType, Stack<IInventoryObject>> _inventoryLookup = new();
        public EventHandler<InventoryModifiedArgs> OnInventoryModified;

        [Inject]
        private void Init(ItemUtility itemUtility)
        {
            _itemUtility = itemUtility;
        }

        private void Start()
        {
            InitializeInventoryLookup();
        }
        public void AddObjectToInventory(IInventoryObject inventoryObject)
        {
            ModifyInventory(inventoryObject, true);
        }

        public void RemoveObjectFromInventory(IInventoryObject inventoryObject)
        {
            ModifyInventory(inventoryObject, false);
        }

        private void ModifyInventory(IInventoryObject inventoryObject, bool isAdding)
        {
            InventoryObjectType objectType = inventoryObject.GetObjectType();
            if (_inventoryLookup.TryGetValue(objectType, out Stack<IInventoryObject> stack))
            {
                if (isAdding)
                {
                    stack.Push(inventoryObject);
                }
                else
                {
                    stack.Pop();
                }
                _inventoryLookup[objectType] = stack;
                InvokeInventoryModifiedEvent(inventoryObject, stack, isAdding);
            }
        }

        public IInventoryObject PopInventoryObject(InventoryObjectType objectType)
        {
            return AccessInventoryObject(objectType, false); // For getting and removing
        }

        public IInventoryObject PeekInventoryObject(InventoryObjectType objectType)
        {
            return AccessInventoryObject(objectType, true); // For peeking without removing
        }
        public bool HasInventoryObjectType(InventoryObjectType objectType)
        {
            return PeekInventoryObject(objectType) != null;
        }

        private IInventoryObject AccessInventoryObject(InventoryObjectType objectType, bool isPeeking)
        {
            if (_inventoryLookup.TryGetValue(objectType, out Stack<IInventoryObject> stack) && stack.Count > 0)
            {
                IInventoryObject inventoryObject = isPeeking ? stack.Peek() : stack.Pop();

                if (!isPeeking)
                {
                    InvokeInventoryModifiedEvent(inventoryObject, stack, true);
                }

                return inventoryObject;
            }
            return null;
        }

        private void InvokeInventoryModifiedEvent(IInventoryObject inventoryObject, Stack<IInventoryObject> stack, bool itemIsAdded)
        {
            bool itemIsStackableItem = _itemUtility.IsStackableObject(inventoryObject.GetObjectType());
            bool itemCountIsZero;

            if (itemIsStackableItem)
            {
                itemCountIsZero = GetStackableItemCountInInventory() == 0;
            }
            else
            {
                itemCountIsZero = stack.Count == 0;
            }

            OnInventoryModified?.Invoke(this, new InventoryModifiedArgs
            {
                InventoryObject = inventoryObject,
                ItemCountIsZero = itemCountIsZero,
                ItemAddedToInventory = itemIsAdded
            });
        }

        public int GetStackableItemCountInInventory()
        {

            InventoryObjectType[] stackableItemTypes = _itemUtility.GetStackableItemTypes();
            int count = 0;

            foreach (InventoryObjectType objType in stackableItemTypes)
            {
                count += GetObjectTypeCountInInventory(objType);
            }
            return count;
        }

        public int GetObjectTypeCountInInventory(InventoryObjectType objectType)
        {
            _inventoryLookup.TryGetValue(objectType, out Stack<IInventoryObject> stack);
            return stack.Count;
        }

        private void InitializeInventoryLookup()
        {
            InventoryObjectType[] allItemTypes = _itemUtility.GetAllInventoryObjectTypes();

            foreach (InventoryObjectType objType in allItemTypes)
            {
                _inventoryLookup[objType] = new Stack<IInventoryObject>();
            }
        }
    }

    public class InventoryModifiedArgs : EventArgs
    {
        public IInventoryObject InventoryObject;
        public bool ItemCountIsZero;
        public bool ItemAddedToInventory;

    }

}


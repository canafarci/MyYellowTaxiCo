using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _maxStackSize;
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

        public IInventoryObject PopInventoryObject(InventoryObjectType objectType)
        {
            return AccessInventoryObject(objectType, false);
        }

        public bool HasInventoryObjectType(InventoryObjectType objectType)
        {
            return AccessInventoryObject(objectType, true) != null;
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

        private IInventoryObject AccessInventoryObject(InventoryObjectType objectType, bool isPeeking)
        {
            if (_inventoryLookup.TryGetValue(objectType, out Stack<IInventoryObject> stack) && stack.Count > 0)
            {
                IInventoryObject inventoryObject = isPeeking ? stack.Peek() : stack.Pop();

                if (!isPeeking)
                {
                    InvokeInventoryModifiedEvent(inventoryObject, stack, false);
                }

                return inventoryObject;
            }

            return null;
        }

        private void InvokeInventoryModifiedEvent(IInventoryObject inventoryObject, Stack<IInventoryObject> stack, bool itemIsAdded)
        {
            bool itemIsStackableItem = _itemUtility.IsStackableObject(inventoryObject.GetObjectType());
            bool itemCountIsZero = itemIsStackableItem ? GetStackableItemCountInInventory() == 0 : stack.Count == 0;

            OnInventoryModified?.Invoke(this, new InventoryModifiedArgs
            {
                InventoryObject = inventoryObject,
                ItemCountIsZero = itemCountIsZero,
                ItemAddedToInventory = itemIsAdded
            });
        }

        //Getters-Setters
        public void SetMaxStackCapacity(int capacity) => _maxStackSize = capacity;
        public bool IsInventoryFull() => _maxStackSize == GetStackableItemCountInInventory();
    }

    public class InventoryModifiedArgs : EventArgs
    {
        public IInventoryObject InventoryObject;
        public bool ItemCountIsZero;
        public bool ItemAddedToInventory;
    }
}

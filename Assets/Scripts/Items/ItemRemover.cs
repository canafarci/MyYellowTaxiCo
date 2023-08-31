using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Items
{
    public class ItemRemover : MonoBehaviour
    {
        [SerializeField] ItemTargetPair[] _itemTargetPairs;

        public EventHandler<OnItemRemovedArgs> OnItemRemoved;

        public void RemoveItem(Inventory inventory, InventoryObjectType objectType)
        {
            while (inventory.HasInventoryObjectType(objectType))
            {
                StackableItem item = inventory.PopInventoryObject(objectType) as StackableItem;
                Transform target = GetTarget(objectType);

                OnItemRemoved?.Invoke(this, new OnItemRemovedArgs
                {
                    Target = target,
                    Item = item.transform
                });
            }
        }

        private Transform GetTarget(InventoryObjectType objectType)
        {
            foreach (ItemTargetPair itemTargetPair in _itemTargetPairs)
            {
                if (objectType == itemTargetPair.ItemType)
                    return itemTargetPair.Target;
            }

            return null;
        }


        // public void RemoveRecursive(Inventory inventory, InventoryObjectType itemType, Transform target)
        // {
        //     if (inventory.GetStackableItemCountInInventory() == 0) { return; }
        //     StackableItem item = inventory.GetItem(itemType);
        //     ItemRemove(inventory, item, target);
        //     RemoveRecursive(inventory, itemType, target);
        // }

        // void ItemRemove(Inventory inventory, StackableItem item, Transform target)
        // {
        //     inventory.RemoveItem(item);
        //     item.transform.parent = null;
        //     Tween(item, target);
        // }


    }

    public class OnItemRemovedArgs : EventArgs
    {
        public Transform Target;
        public Transform Item;
    }


    [Serializable]
    public struct ItemTargetPair
    {
        public Transform Target;
        public InventoryObjectType ItemType;
    }
}



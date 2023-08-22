using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TaxiGame.Items
{
    [RequireComponent(typeof(RemoveItem))]
    public class RemoveItemTrigger : MonoBehaviour
    {
        [SerializeField] Transform[] _targets;
        [SerializeField] InventoryObjectType[] _itemTypes;
        [SerializeField] InventoryObjectType _thisItemType;
        RemoveItem _remover;
        // private void Awake() => _remover = GetComponent<RemoveItem>();
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         Inventory inventory = GameManager.Instance.References.PlayerInventory;

        //         if (inventory.GetStackableItemCountInInventory() == 0) { return; }

        //         for (int i = 0; i < _itemTypes.Length; i++)
        //         {
        //             InventoryObjectType itemType = _itemTypes[i];
        //             StackableItem item = inventory.PopInventoryObject(itemType);

        //             if (item == null || item.GetObjectType() == _thisItemType) { continue; }
        //             Transform target = _targets[i];
        //             _remover.RemoveRecursive(inventory, itemType, target);
        //             break;
        //         }
        //     }
        // }
    }
}

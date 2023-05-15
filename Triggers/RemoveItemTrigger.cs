using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RemoveItem))]
public class RemoveItemTrigger : MonoBehaviour
{
    [SerializeField] Transform[] _targets;
    [SerializeField] Enums.StackableItemType[] _itemTypes;
    [SerializeField] Enums.StackableItemType _thisItemType;
    RemoveItem _remover;
    private void Awake() => _remover = GetComponent<RemoveItem>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = GameManager.Instance.References.PlayerInventory;

            if (inventory.ItemCount == 0) { return; }

            for (int i = 0; i < _itemTypes.Length; i++)
            {
                Enums.StackableItemType itemType = _itemTypes[i];
                StackableItem item = inventory.GetItem(itemType);

                if (item == null || item.Hat == _thisItemType) { continue; }
                Transform target = _targets[i];
                _remover.RemoveRecursive(inventory, itemType, target);
                break;
            }
        }
    }

}

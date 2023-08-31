
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items
{
    public class RemoveItemTrigger : MonoBehaviour
    {
        [SerializeField] private InventoryObjectType[] _skippedItems;
        private ItemRemover _remover;
        private ItemUtility _itemUtility;

        [Inject]
        private void Init(ItemRemover remover, ItemUtility itemUtility)
        {
            _remover = remover;
            _itemUtility = itemUtility;
        }


        // private void Awake() => _remover = GetComponent<RemoveItem>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
            {
                Inventory inventory = other.GetComponent<Inventory>();

                if (inventory.GetStackableItemCountInInventory() == 0) { return; }

                InventoryObjectType[] stackableItemTypes = _itemUtility.GetStackableItemTypes();

                foreach (InventoryObjectType objectType in stackableItemTypes)
                {
                    if (_skippedItems.Contains(objectType)) continue;

                    if (inventory.HasInventoryObjectType(objectType))
                    {
                        _remover.RemoveItem(inventory, objectType);
                    }
                }
            }
        }
    }
}

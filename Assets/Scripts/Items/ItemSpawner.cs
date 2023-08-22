using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _stackableItem;
        [SerializeField] protected Transform _startTransform;
        public void SpawnItem(Collider other)
        {
            if (CanSpawnItem(other))
            {
                StackableItem item = GameObject.Instantiate(_stackableItem, _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
                Inventory inventory = other.GetComponent<Inventory>();
                inventory.AddObjectToInventory(item);

                IUnlockable unlock = GetComponent<IUnlockable>();
                if (unlock == null || unlock.HasUnlockedBefore()) return;
                unlock.UnlockObject();
            }
        }
        private bool CanSpawnItem(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            return inventory.GetStackableItemCountInInventory() == 0;
        }
    }
}



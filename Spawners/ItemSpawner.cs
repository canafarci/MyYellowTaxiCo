using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour, IItemSpawner
{
    [SerializeField] protected GameObject _stackableItem;
    [SerializeField] protected Transform _startTransform;

    public bool CanSpawnItem(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        return inventory.ItemCount == 0;
    }

    public void SpawnItem(Collider other)
    {
        StackableItem item = GameObject.Instantiate(_stackableItem, _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
        Inventory inventory = other.GetComponent<Inventory>();
        inventory.StackItem(item);

        IUnlockable unlock = GetComponent<IUnlockable>();
        if (unlock == null || unlock.HasUnlockedBefore()) return;
        unlock.UnlockObject();
    }
}


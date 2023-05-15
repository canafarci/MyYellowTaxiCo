using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RemoveItem : MonoBehaviour
{
    public void RemoveRecursive(Inventory inventory, Enums.StackableItemType itemType, Transform target)
    {
        if (inventory.ItemCount == 0) { return; }
        StackableItem item = inventory.GetItem(itemType);
        ItemRemove(inventory, item, target);
        RemoveRecursive(inventory, itemType, target);
    }

    void ItemRemove(Inventory inventory, StackableItem item, Transform target)
    {
        inventory.RemoveItem(item);
        item.transform.parent = null;
        Tween(item, target);
    }

    void Tween(StackableItem item, Transform target)
    {
        Vector3[] path = {  item.transform.position,
                            (item.transform.position + target.position) / 2f + new Vector3(0f, 2f, 0f),
                            target.position };

        item.transform.DOPath(path, .5f, PathType.CatmullRom, PathMode.Full3D).onComplete = () => Destroy(item.gameObject, 0.1f);
    }
}

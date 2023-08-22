using UnityEngine;

public interface IInventoryHolder
{
    public Inventory GetInventory();
    public Transform GetHandTransform();
}
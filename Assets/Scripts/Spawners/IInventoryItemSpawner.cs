using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemSpawner
{
    bool CanSpawnItem(Collider other);
    void SpawnItem(Collider other);
}

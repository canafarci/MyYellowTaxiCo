using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemSpawner
{
    bool CanSpawnItem(Collider other);
    void SpawnItem(Collider other);
}

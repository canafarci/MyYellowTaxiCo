using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
namespace Taxi.WaitZones
{
    public class WaitToSpawnItemZoneTrigger : WaitZoneTrigger
    {
        private ItemSpawner _itemSpawner;
        protected override void Awake()
        {
            base.Awake();
            _itemSpawner = GetComponent<ItemSpawner>();

        }
        protected override Action GetSuccessAction(Collider other)
        {
            return () =>
            {
                _itemSpawner.SpawnItem(other);
            };
        }

    }
}
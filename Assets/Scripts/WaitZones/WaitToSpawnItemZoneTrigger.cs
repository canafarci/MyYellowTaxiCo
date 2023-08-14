using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.WaitZones
{
    public class WaitToSpawnItemZoneTrigger : WaitZoneTrigger
    {
        private ItemSpawner _itemSpawner;

        [Inject]
        private void Init(ItemSpawner itemSpawner)
        {
            _itemSpawner = itemSpawner;

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
using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class HelperNPC : MonoBehaviour
    {

        [Inject]
        private void Create(Transform spawnPoint)
        {
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }

        public class Factory : PlaceholderFactory<Transform, HelperNPC>
        {

        }
    }
}
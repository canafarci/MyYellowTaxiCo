using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class DriverSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _follower;
        [SerializeField] private Transform _spawnTransform;

        public Enums.StackableItemType HatType;
        private DriverQueue _queue;

        private NavMeshNPC.Factory _driverFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Driver)] NavMeshNPC.Factory driverFactory,
                        DriverQueue queue)
        {
            _queue = queue;

            _driverFactory = driverFactory;
        }
        private void Start() => SpawnDriver(_spawnTransform);
        public void SpawnDriver(Transform spawnTransform)
        {
            //spawn driver and downsize scale
            Driver driver = (Driver)_driverFactory.Create(spawnTransform.position, spawnTransform.rotation);

            //TODO move view to its own class

            //add driver to the queue
            _queue.AddDriverToQueue(driver);
        }
    }
}
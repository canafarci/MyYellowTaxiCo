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
        [SerializeField] private Transform _spawnTransform;

        public Enums.StackableItemType HatType;
        private INPCQueue _queue;

        private NavMeshMover.Factory _driverFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Driver)] NavMeshMover.Factory driverFactory,
                        [Inject(Id = NPCType.Driver)] INPCQueue queue)
        {
            _queue = queue;
            _driverFactory = driverFactory;
        }
        private void Start() => SpawnDriver(_spawnTransform);
        public void SpawnDriver(Transform spawnTransform)
        {
            //spawn driver and downsize scale
            RiderNPC driver = _driverFactory.Create(spawnTransform.position, spawnTransform.rotation).GetComponent<RiderNPC>();
            //add driver to the queue
            if (!_queue.QueueIsFull())
                _queue.AddToQueue(driver);
        }
    }
}
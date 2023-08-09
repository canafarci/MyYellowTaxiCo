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
        private INPCQueue _queue;

        private NavMeshNPC.Factory _driverFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Driver)] NavMeshNPC.Factory driverFactory,
                        [Inject(Id = NPCType.Driver)] INPCQueue queue)
        {
            _queue = queue;
            _driverFactory = driverFactory;
        }
        private void Start() => SpawnDriver(_spawnTransform);
        public void SpawnDriver(Transform spawnTransform)
        {
            //spawn driver and downsize scale
            NavMeshNPC driver = _driverFactory.Create(spawnTransform.position, spawnTransform.rotation);
            //add driver to the queue
            _queue.AddToQueue(driver);
        }
    }
}
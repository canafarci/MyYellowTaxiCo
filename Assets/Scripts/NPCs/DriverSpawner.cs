using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TaxiGame.Vehicle;
using TMPro;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class DriverSpawner : MonoBehaviour
    {
        public Enums.StackableItemType HatType;
        private INPCQueue _queue;
        private RiderNPC.Factory _driverFactory;
        [SerializeField] private GameObject _driverPrefab;

        [Inject]
        private void Init(RiderNPC.Factory driverFactory,
                        [Inject(Id = NPCType.Driver)] INPCQueue queue)
        {
            _queue = queue;
            _driverFactory = driverFactory;
        }
        private void Start()
        {
            CarSpawner.OnCarReturned += CarSpawner_CarReturnedHandler;
            DriverQueueSpot.OnNewDriverQueueSpotActivated += DriverQueueSpot_NewDriverQueueSpotActivatedHandler;
        }

        private void DriverQueueSpot_NewDriverQueueSpotActivatedHandler(object sender, OnNewQueueSpotActivatedEventArgs e)
        {
            if (e.HatType == HatType)
            {
                StartCoroutine(AddDriverWhenQueueHasAvaliableSpot(transform));
            }
        }
        private void CarSpawner_CarReturnedHandler(object sender, OnCarReturned e)
        {
            if (e.HatType == HatType)
            {
                StartCoroutine(AddDriverWhenQueueHasAvaliableSpot(e.SpawnerTransform));
            }
        }
        private IEnumerator AddDriverWhenQueueHasAvaliableSpot(Transform spot)
        {
            yield return new WaitUntil(() => !_queue.QueueIsFull());
            SpawnDriver(spot);
        }
        private void SpawnDriver(Transform spawnTransform)
        {
            //spawn driver and downsize scale
            RiderNPC driver = _driverFactory.Create(_driverPrefab,
                                                    spawnTransform.position,
                                                    spawnTransform.rotation);
            //add driver to the queue
            _queue.AddToQueue(driver);
        }
    }
}
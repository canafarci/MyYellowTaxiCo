using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ModestTree;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class DriverQueueCoordinator : MonoBehaviour, INPCQueue
    {
        [SerializeField] private InventoryObjectType _hatType;
        private List<DriverQueueSpot> _queueSpots = new List<DriverQueueSpot>();
        private List<Driver> _drivers = new List<Driver>();
        private DriverDispatcher _driverDispatcher;

        public EventHandler<OnDriverAddedToQueueArgs> OnDriverAddedToQueue;

        [Inject]
        private void Init(DriverDispatcher dispatcher)
        {
            _driverDispatcher = dispatcher;
        }

        private void Start()
        {
            DriverQueueSpot.OnNewDriverQueueSpotActivated += QueueSpot_NewQueueSpotActivatedHandler;
            _driverDispatcher.OnDriverDispatched += DriverDispatcher_DriverDispatchedHandler;
        }

        private void DriverDispatcher_DriverDispatchedHandler(object sender, OnDriverDispatchedArgs e)
        {
            RemoveDriverFromSpot(e.Driver);
        }

        public void AddToQueue(RiderNPC npc)
        {
            DriverQueueSpot spot = FindAvailableSpot();
            Assert.IsNotNull(spot);
            npc.GetController().MoveAndSit(spot.transform);
            spot.SetNPC(npc);

            OnDriverAddedToQueue?.Invoke(this, new OnDriverAddedToQueueArgs { Driver = npc as Driver });
        }


        private void RemoveDriverFromSpot(RiderNPC driverToRemove)
        {
            foreach (DriverQueueSpot spot in _queueSpots)
            {
                if (spot.GetNPC() != null && spot.GetNPC() == driverToRemove)
                {
                    spot.Clear();
                }
            }
        }

        private DriverQueueSpot FindAvailableSpot()
        {
            return _queueSpots.FirstOrDefault(spot => spot.IsEmpty());
        }

        private void QueueSpot_NewQueueSpotActivatedHandler(object sender, OnNewQueueSpotActivatedEventArgs e)
        {
            if (e.HatType == _hatType)
            {
                DriverQueueSpot spot = sender as DriverQueueSpot;
                Assert.IsNotNull(spot);
                _queueSpots.Add(spot);
            }
        }

        public bool QueueIsFull()
        {
            bool isFull = true;
            foreach (DriverQueueSpot spot in _queueSpots)
            {
                if (spot.IsEmpty())
                {
                    isFull = false;
                }
            }
            return isFull;
        }
        //Getters-Setters
        public InventoryObjectType GetHatType() => _hatType;

        //cleanup
        private void OnDisable()
        {
            DriverQueueSpot.OnNewDriverQueueSpotActivated -= QueueSpot_NewQueueSpotActivatedHandler;
        }
    }

    public class OnDriverAddedToQueueArgs : EventArgs
    {
        public Driver Driver;
    }
}

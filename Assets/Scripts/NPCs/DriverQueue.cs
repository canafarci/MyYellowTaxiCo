using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ModestTree;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class DriverQueue : MonoBehaviour, INPCQueue
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        private List<DriverQueueSpot> _queueSpots = new List<DriverQueueSpot>();

        public event EventHandler<OnDriverAddedToQueueArgs> OnDriverAddedToQueue;

        private void Start()
        {
            DriverQueueSpot.OnNewDriverQueueSpotActivated += QueueSpot_NewQueueSpotActivatedHandler;
        }

        public void AddToQueue(RiderNPC npc)
        {
            DriverQueueSpot spot = FindAvailableSpot();
            Assert.IsNotNull(spot);
            npc.GetView().MoveAndSit(spot.transform);
            spot.SetNPC(npc);

            OnDriverAddedToQueue?.Invoke(this, new OnDriverAddedToQueueArgs { Driver = npc as Driver });
        }


        public void Remove(RiderNPC driverToRemove)
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
        public Enums.StackableItemType GetHatType() => _hatType;

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

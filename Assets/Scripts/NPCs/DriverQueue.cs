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
        public Enums.StackableItemType HatType;
        private List<DriverQueueSpot> _queueSpots = new List<DriverQueueSpot>();

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
        }

        public List<Driver> GetDrivers()
        {
            return GetDriversByCondition(driver => true);
        }

        public List<Driver> GetDriversWithHat()
        {
            return GetDriversByCondition(driver => driver.HasHat());
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

        private List<Driver> GetDriversByCondition(Func<Driver, bool> condition)
        {
            List<Driver> drivers = new List<Driver>();
            foreach (DriverQueueSpot spot in _queueSpots)
            {
                if (spot.GetNPC() != null && condition(spot.GetNPC() as Driver))
                {
                    drivers.Add(spot.GetNPC() as Driver);
                }
            }
            return drivers;
        }

        private void QueueSpot_NewQueueSpotActivatedHandler(object sender, OnNewQueueSpotActivatedEventArgs e)
        {
            if (e.HatType == HatType)
            {
                DriverQueueSpot spot = sender as DriverQueueSpot;
                Assert.IsNotNull(spot);
                _queueSpots.Add(spot);
            }
        }

        private void OnDisable()
        {
            DriverQueueSpot.OnNewDriverQueueSpotActivated -= QueueSpot_NewQueueSpotActivatedHandler;
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
    }
}

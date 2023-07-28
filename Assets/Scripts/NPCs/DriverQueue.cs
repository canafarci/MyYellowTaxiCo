using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ModestTree;
using UnityEngine;
namespace Taxi.NPC
{
    public class DriverQueue : MonoBehaviour
    {
        public Enums.StackableItemType HatType;
        private List<QueueSpot> _queueSpots = new List<QueueSpot>();
        private void Start()
        {
            QueueSpot.OnNewQueueSpotActivated += NewQueueSpotActivatedHandler;
        }
        private void NewQueueSpotActivatedHandler(object sender, OnNewQueueSpotActivatedEventArgs e)
        {
            if (e.HatType == HatType)
            {
                QueueSpot spot = sender as QueueSpot;
                Assert.IsNotNull(spot);
                _queueSpots.Add(spot);
            }
        }
        public void AddDriverToQueue(Driver driver)
        {
            //get first avaliable spot
            QueueSpot spot = _queueSpots.Where(x => !x.HasDriver()).FirstOrDefault();
            Assert.IsNotNull(spot);
            //move driver to the spot and sit
            driver.MoveAndSit(spot);
            spot.SetDriver(driver);
        }
        public List<Driver> GetDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            foreach (QueueSpot spot in _queueSpots)
            {
                if (spot.TryGetDriver(out Driver driver))
                {
                    drivers.Add(driver);
                }
            }
            return drivers;
        }
        public List<Driver> GetDriversWithHat()
        {
            List<Driver> drivers = new List<Driver>();
            foreach (QueueSpot spot in _queueSpots)
            {
                if (spot.TryGetDriver(out Driver driver) && driver.DriverHasHat())
                {
                    drivers.Add(driver);
                }
            }
            return drivers;
        }
        public void Remove(Driver driverToRemove)
        {
            foreach (QueueSpot spot in _queueSpots)
            {
                if (spot.TryGetDriver(out Driver driver) && driver == driverToRemove)
                {
                    spot.Clear();
                }
            }
        }
        //Cleanup
        private void OnDisable()
        {
            QueueSpot.OnNewQueueSpotActivated -= NewQueueSpotActivatedHandler;
        }
    }
}
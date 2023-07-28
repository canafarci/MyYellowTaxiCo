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
            QueueSpot spot = sender as QueueSpot;
            Assert.IsNotNull(spot);
            _queueSpots.Add(spot);
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
        private IEnumerator CheckDrivers()
        {
            while (true)
            {
                yield return null;
                //check if stacker has avaliable hats
                //if so, make driver wear hat
                //if there are drivers with hats, check if there are avaliable cars
                //make drivers move to the cars
            }
        }
        //Cleanup
        private void OnDisable()
        {
            QueueSpot.OnNewQueueSpotActivated -= NewQueueSpotActivatedHandler;
        }
    }
}
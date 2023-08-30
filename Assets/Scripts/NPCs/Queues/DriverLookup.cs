using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    /// <summary>
    /// Stores driver objects which are in sitting state
    /// </summary>
    public class DriverLookup : MonoBehaviour
    {
        private HashSet<Driver> _driversWithoutHat = new HashSet<Driver>();
        private HashSet<Driver> _driversWithHat = new HashSet<Driver>();
        private DriverQueueCoordinator _driverQueue;
        [Inject]
        private void Init([Inject(Id = ModelType.Distributor)] DriverQueueCoordinator driverQueue)
        {
            _driverQueue = driverQueue;
        }

        private void Start()
        {
            _driverQueue.OnDriverAddedToQueue += DriverQueue_DriverAddedToQueueHandler;
        }

        private void DriverQueue_DriverAddedToQueueHandler(object sender, OnDriverAddedToQueueArgs e)
        {
            _driversWithoutHat.Add(e.Driver);
        }
        //Getters-Setters
        public HashSet<Driver> GetDriversWithoutHat() => _driversWithoutHat;
        public HashSet<Driver> GetDriversWithHat() => _driversWithHat;
    }
}

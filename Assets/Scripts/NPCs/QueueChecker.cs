using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.Vehicle;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taxi.NPC
{
    public class QueueChecker : MonoBehaviour
    {
        public Enums.StackableItemType HatType;
        private DriverQueue _driverQueue;
        private List<CarSpawner> _spawners = new List<CarSpawner>();
        private void Awake()
        {
            _driverQueue = GetComponent<DriverQueue>();
        }
        private void Start()
        {
            CarSpawner.OnNewSpawnerActivated += NewSpawnerActivatedHandler;
            StartCoroutine(CheckSpawners());
        }
        private IEnumerator CheckSpawners()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                //if there are drivers with hats, check if there are avaliable cars
                List<Driver> drivers = _driverQueue.GetDriversWithHat();

                if (drivers.Count == 0) { continue; }

                foreach (CarSpawner spawner in _spawners)
                {
                    if (spawner.CarIsReady)
                    {
                        spawner.DriverIsComing = true;
                        Driver driver = drivers[^1];
                        driver.GoToCar(spawner.transform, () => spawner.StartMove());
                        drivers.RemoveAt(drivers.Count - 1);
                        _driverQueue.Remove(driver);
                    }
                }
                //make drivers move to the cars
            }
        }
        private void NewSpawnerActivatedHandler(object sender, OnNewSpawnerActivatedEventArgs e)
        {
            if (HatType == e.HatType)
            {
                CarSpawner spawner = sender as CarSpawner;
                Assert.IsNotNull(spawner);
                _spawners.Add(spawner);
            }
        }
        private void OnDisable()
        {
            CarSpawner.OnNewSpawnerActivated -= NewSpawnerActivatedHandler;
        }
    }
}

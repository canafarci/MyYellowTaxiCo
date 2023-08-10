using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taxi.NPC
{
    public class QueueChecker : MonoBehaviour
    {
        public Enums.StackableItemType HatType;
        private DriverQueue _driverQueue;
        private List<Spawner> _spawners = new List<Spawner>();
        private void Awake()
        {
            _driverQueue = GetComponent<DriverQueue>();
        }
        private void Start()
        {
            Spawner.OnNewSpawnerActivated += NewSpawnerActivatedHandler;
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

                foreach (Spawner spawner in _spawners)
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
                Spawner spawner = sender as Spawner;
                Assert.IsNotNull(spawner);
                _spawners.Add(spawner);
            }
        }
        private void OnDisable()
        {
            Spawner.OnNewSpawnerActivated -= NewSpawnerActivatedHandler;
        }
    }
}

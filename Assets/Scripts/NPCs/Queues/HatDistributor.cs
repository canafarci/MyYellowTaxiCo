using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.NPC
{
    public class HatDistributor : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueueCoordinator _driverQueue;
        private HashSet<Driver> _driversWithoutHat = new HashSet<Driver>();
        private HashSet<Driver> _driversWithHat = new HashSet<Driver>();

        private Coroutine _distributeHatCoroutine;

        public event EventHandler<HatDistributedEventArgs> OnHatDistributed;

        [Inject]
        private void Init(Stacker stacker,
                        [Inject(Id = ModelType.Distributor)] DriverQueueCoordinator driverQueue)
        {
            _stacker = stacker;
            _driverQueue = driverQueue;
        }
        private void Start()
        {
            _driverQueue.OnDriverAddedToQueue += DriverQueue_DriverAddedToQueueHandler;
        }

        private void DriverQueue_DriverAddedToQueueHandler(object sender, OnDriverAddedToQueueArgs e)
        {
            _driversWithoutHat.Add(e.Driver);

            if (_distributeHatCoroutine == null)
                _distributeHatCoroutine = StartCoroutine(TryGiveHatLoop());
        }

        private IEnumerator TryGiveHatLoop()
        {
            while (_driversWithoutHat.Count > 0)
            {
                yield return new WaitForSeconds(1f);

                if (_stacker.ItemStack.Count > 0)
                {
                    Driver driver = _driversWithoutHat.FirstOrDefault();
                    StackableItem hat = _stacker.ItemStack.Pop();

                    driver.SetHasHat(true);
                    _driversWithoutHat.Remove(driver);
                    _driversWithHat.Add(driver);

                    InvokeHatDistributedEvent(driver, hat);
                }
            }

            _distributeHatCoroutine = null;
        }

        private void InvokeHatDistributedEvent(Driver driver, StackableItem hat)
        {
            OnHatDistributed?.Invoke(this, new HatDistributedEventArgs
            {
                Driver = driver,
                Item = hat.transform,
            });
        }
        //Getters-Setters
        public HashSet<Driver> GetDriversWithoutHat() => _driversWithoutHat;
        public HashSet<Driver> GetDriversWithHat() => _driversWithHat;
    }

    public class HatDistributedEventArgs : EventArgs
    {
        public Transform Item;

        public Driver Driver;
    }
}

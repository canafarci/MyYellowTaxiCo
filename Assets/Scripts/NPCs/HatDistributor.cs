using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.NPC
{
    public class HatDistributor : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueue _driverQueue;
        private List<Driver> _drivers = new List<Driver>();
        private Coroutine _distributeHatCoroutine;

        public event EventHandler<HatDistributedEventArgs> OnHatDistributed;

        [Inject]
        private void Init(Stacker stacker,
                        [Inject(Id = ModelType.Distributor)] DriverQueue driverQueue)
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
            _drivers.Add(e.Driver);

            if (_distributeHatCoroutine == null)
                _distributeHatCoroutine = StartCoroutine(TryGiveHatLoop());
        }

        private IEnumerator TryGiveHatLoop()
        {
            while (_drivers.Count > 0)
            {
                yield return new WaitForSeconds(1f);

                if (_stacker.ItemStack.Count > 0)
                {
                    Driver driver = _drivers[^1];
                    StackableItem hat = _stacker.ItemStack.Pop();

                    driver.SetHasHat(true);
                    _drivers.Remove(driver);

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
    }

    public class HatDistributedEventArgs : EventArgs
    {
        public Transform Item;

        public Driver Driver;
    }
}

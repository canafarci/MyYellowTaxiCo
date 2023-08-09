using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Taxi.NPC
{
    public class HatDistributor : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueue _driverQueue;
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
            StartCoroutine(TryGiveHatLoop());
        }
        private IEnumerator TryGiveHatLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                List<Driver> drivers = _driverQueue.GetDrivers();
                if (_stacker.ItemStack.Count > 0 && drivers.Count > 0)
                {
                    TryDistributeHatToDrivers(drivers);
                }
            }
        }
        private void TryDistributeHatToDrivers(List<Driver> drivers)
        {
            foreach (Driver driver in drivers)
            {
                if (driver.DriverHasHat()) { continue; }

                if (_stacker.ItemStack.TryPop(out StackableItem hat))
                {
                    driver.SetHasHat(true);
                    InvokeHatDistributedEvent(driver, hat);
                }
                else
                {
                    break;
                }
            }
        }

        private void InvokeHatDistributedEvent(Driver driver, StackableItem hat)
        {
            OnHatDistributed?.Invoke(this, new HatDistributedEventArgs
            {
                Item = hat.transform,
                Target = driver.GetHatTransform()
            });
        }
    }

    public class HatDistributedEventArgs : EventArgs
    {
        public Transform Item;

        public Transform Target;
    }
}

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
    /// <summary>
    /// periodically  checks for drivers in wait spots and tries to distribute them hats
    /// </summary>
    public class DriverHatDistributor : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverLookup _driverLookup;
        public event EventHandler<HatDistributedEventArgs> OnHatDistributed;

        [Inject]
        private void Init(Stacker stacker, DriverLookup driverLookup)
        {
            _stacker = stacker;
            _driverLookup = driverLookup;
        }

        private void Start()
        {
            InvokeRepeating(nameof(DistributeHatsToDrivers), 0f, 0.5f);
        }
        //Invoked repeatedly every 0.5 seconds
        private void DistributeHatsToDrivers()
        {
            HashSet<Driver> driversWithoutHat = _driverLookup.GetDriversWithoutHat();
            HashSet<Driver> driversWithHat = _driverLookup.GetDriversWithHat();

            if (driversWithoutHat.Count > 0 && _stacker.ItemStack.Count > 0)
            {

                Driver driver = driversWithoutHat.FirstOrDefault();
                StackableItem hat = _stacker.ItemStack.Pop();

                driver.SetHasHat(true);
                driversWithoutHat.Remove(driver);
                driversWithHat.Add(driver);

                InvokeHatDistributedEvent(driver, hat);
            }
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

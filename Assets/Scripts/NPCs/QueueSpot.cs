using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class QueueSpot : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        public static event EventHandler<OnNewQueueSpotActivatedEventArgs> OnNewQueueSpotActivated;
        private Driver _driver = null;
        private void Awake()
        {
            OnNewQueueSpotActivated?.Invoke(this, new OnNewQueueSpotActivatedEventArgs { HatType = _hatType });
        }
        public bool HasDriver() => _driver != null;
        public bool DriverHasHat()
        {
            if (_driver == null)
                return false;
            else
                return _driver.DriverHasHat();
        }
        public void SetDriver(Driver driver) => _driver = driver;
    }
    public class OnNewQueueSpotActivatedEventArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
    }
}

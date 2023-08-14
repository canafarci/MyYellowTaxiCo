using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class DriverQueueSpot : RiderNPCSpot
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        public static event EventHandler<OnNewQueueSpotActivatedEventArgs> OnNewDriverQueueSpotActivated;
        private Driver _driver = null;
        private void Awake()
        {
            OnNewDriverQueueSpotActivated?.Invoke(this, new OnNewQueueSpotActivatedEventArgs { HatType = _hatType });
        }
        public bool DriverHasHat()
        {
            if (_driver == null)
                return false;
            else
                return _driver.HasHat();
        }
    }
    public class OnNewQueueSpotActivatedEventArgs : EventArgs
    {
        public Enums.StackableItemType HatType;
    }
}

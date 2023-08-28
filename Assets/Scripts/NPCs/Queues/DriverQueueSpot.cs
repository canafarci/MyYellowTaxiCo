using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class DriverQueueSpot : RiderNPCSpot
    {
        [SerializeField] private InventoryObjectType _hatType;
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
        public InventoryObjectType HatType;
    }
}

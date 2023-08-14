using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class TaxiSpot : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        public static event EventHandler<OnTaxiReturned> OnTaxiReturned;














        //Getters-Setters
        public Enums.StackableItemType GetHatType() => _hatType;



        //TODO move to Vehicle base class
        private Taxi _taxi;
        public bool HasTaxi() => _taxi != null;
        public void SetTaxi(Taxi taxi)
        {
            _taxi = taxi;
            InvokeTaxiReturnedEvent();
        }
        public void Clear() => _taxi = null;
        private void InvokeTaxiReturnedEvent()
        {
            OnTaxiReturned?.Invoke(this, new OnTaxiReturned
            {
                HatType = _hatType,
                SpawnerTransform = transform
            });
        }
    }

    public class OnTaxiReturned : EventArgs
    {
        public Enums.StackableItemType HatType;
        public Transform SpawnerTransform;
    }
}

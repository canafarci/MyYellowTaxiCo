using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.NPC
{
    public class QueueChecker : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        private List<TaxiSpot> _spots = new List<TaxiSpot>();
        private List<Driver> _driversWithHat = new List<Driver>();
        private Coroutine _checkSpotsCoroutine;
        private DriverQueue _driverQueue;
        private HatDistributor _hatDistributor;

        [Inject]
        private void Init(
                        [Inject(Id = ModelType.Distributor)] DriverQueue driverQueue,
                        HatDistributor hatDistributor)
        {
            _hatDistributor = hatDistributor;
            _driverQueue = driverQueue;
        }
        private void Start()
        {
            TaxiSpot.OnTaxiReturned += TaxiSpot_TaxiReturnedHandler;
            _hatDistributor.OnHatDistributed += HatDistributor_HatDistributedHandler;
        }

        private void HatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
            _driversWithHat.Add(e.Driver);
        }

        private void TaxiSpot_TaxiReturnedHandler(object sender, OnTaxiReturned e)
        {
            if (_hatType != e.HatType) return;

            TaxiSpot spot = sender as TaxiSpot;
            Assert.IsNotNull(spot);
            _spots.Add(spot);

            if (_checkSpotsCoroutine == null)
                _checkSpotsCoroutine = StartCoroutine(CheckTaxiSpots());
        }

        private IEnumerator CheckTaxiSpots()
        {
            while (_spots.Count > 0)
            {
                if (_driversWithHat.Count > 0)
                {
                    Driver driver = _driversWithHat[^1];
                    TaxiSpot spot = _spots[^1];

                    driver.GetView().GoToCar(spot.transform, () => { });

                    _driversWithHat.Remove(driver);
                    _spots.Remove(spot);
                    _driverQueue.Remove(driver);
                }
                yield return new WaitForSeconds(.75f);
            }

            _checkSpotsCoroutine = null;
        }
        //Getters-Setters
        public Enums.StackableItemType GetHatType() => _hatType;
        //Cleanup
        private void OnDisable()
        {
            TaxiSpot.OnTaxiReturned -= TaxiSpot_TaxiReturnedHandler;
        }
    }
}

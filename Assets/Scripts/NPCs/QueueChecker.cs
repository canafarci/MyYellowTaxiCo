using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.Items;
using TaxiGame.Vehicles;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.NPC
{
    public class QueueChecker : MonoBehaviour
    {
        [SerializeField] private InventoryObjectType _hatType;
        private HashSet<VehicleSpot> _spots = new HashSet<VehicleSpot>();
        private HashSet<Driver> _driversWithHat = new HashSet<Driver>();
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
            VehicleSpot.OnVehicleReturned += VehicleSpot_VehicleReturnedHandler;
            RepairableVehicle.OnVehicleRepaired += RepairableVehicle_VehicleRepairedHandler;
            _hatDistributor.OnHatDistributed += HatDistributor_HatDistributedHandler;
        }
        private void HatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
            _driversWithHat.Add(e.Driver);
            StartChecking();
        }

        private void RepairableVehicle_VehicleRepairedHandler(object sender, OnVehicleRepairedArgs e)
        {
            if (_hatType != e.HatType) return;

            AddVehicleSpotToList(e.VehicleSpot);
        }
        private void VehicleSpot_VehicleReturnedHandler(object sender, OnVehicleReturnedArgs e)
        {
            if (e.IsBrokenCar || _hatType != e.HatType) return;

            AddVehicleSpotToList(sender as VehicleSpot);
        }
        private void AddVehicleSpotToList(VehicleSpot spot)
        {
            _spots.Add(spot);
            StartChecking();
        }
        private void StartChecking()
        {

            if (_checkSpotsCoroutine == null)
                _checkSpotsCoroutine = StartCoroutine(CheckTaxiSpots());
        }

        private IEnumerator CheckTaxiSpots()
        {
            while (_spots.Count > 0)
            {
                if (_driversWithHat.Count > 0)
                {
                    Driver driver = _driversWithHat.LastOrDefault();
                    VehicleSpot spot = _spots.LastOrDefault();

                    driver.GetView().GoToCar(spot.transform, () => spot.DepartVehicle());

                    _driversWithHat.Remove(driver);
                    _spots.Remove(spot);
                    _driverQueue.Remove(driver);
                }
                yield return new WaitForSeconds(.75f);
            }

            _checkSpotsCoroutine = null;
        }
        //Getters-Setters
        public InventoryObjectType GetHatType() => _hatType;
        //Cleanup
        private void OnDisable()
        {
            VehicleSpot.OnVehicleReturned -= VehicleSpot_VehicleReturnedHandler;
        }
    }
}

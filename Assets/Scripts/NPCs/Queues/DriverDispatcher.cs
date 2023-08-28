using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaxiGame.Items;
using TaxiGame.Vehicles;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class DriverDispatcher : MonoBehaviour
    {
        [SerializeField] private InventoryObjectType _hatType;
        private HashSet<VehicleSpot> _spots = new HashSet<VehicleSpot>();
        private Coroutine _checkSpotsCoroutine;
        private HatDistributor _hatDistributor;
        public event EventHandler<OnDriverDispatchedArgs> OnDriverDispatched;

        [Inject]
        private void Init(HatDistributor hatDistributor)
        {
            _hatDistributor = hatDistributor;
        }

        private void Start()
        {
            VehicleSpot.OnVehicleReturned += VehicleSpot_VehicleReturnedHandler;
            RepairableVehicle.OnVehicleRepaired += RepairableVehicle_VehicleRepairedHandler;
            _hatDistributor.OnHatDistributed += HatDistributor_HatDistributedHandler;
        }

        private void HatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
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
                // Create a copy of the spots to avoid modification while iterating
                foreach (VehicleSpot spot in _spots.ToList())
                {
                    HashSet<Driver> driversWithoutHat = _hatDistributor.GetDriversWithoutHat();
                    HashSet<Driver> driversWithHat = _hatDistributor.GetDriversWithHat();

                    if (spot.IsCustomerWaiting())
                    {
                        DispatchDriverWithoutHat(spot, driversWithoutHat);
                    }
                    else
                    {
                        DispatchDriverWithHat(spot, driversWithHat);
                    }
                }

                yield return new WaitForSeconds(.75f);
            }

            _checkSpotsCoroutine = null;
        }

        private void DispatchDriverWithoutHat(VehicleSpot spot, HashSet<Driver> driversWithoutHat)
        {
            if (driversWithoutHat.Count > 0)
            {
                Driver driver = driversWithoutHat.LastOrDefault();
                DispatchDriverToSpot(driver, spot, driversWithoutHat);
                spot.SetCustomerWaiting(false);
            }
        }

        private void DispatchDriverWithHat(VehicleSpot spot, HashSet<Driver> driversWithHat)
        {
            if (driversWithHat.Count > 0)
            {
                Driver driver = driversWithHat.LastOrDefault();
                DispatchDriverToSpot(driver, spot, driversWithHat);
            }
        }

        private void DispatchDriverToSpot(Driver driver, VehicleSpot spot, HashSet<Driver> drivers)
        {
            driver.GetController().GoToCar(spot.transform, () => spot.DepartVehicle());

            drivers.Remove(driver);
            _spots.Remove(spot);

            OnDriverDispatched?.Invoke(this, new OnDriverDispatchedArgs { Driver = driver });
        }

        // Getters-Setters
        public InventoryObjectType GetHatType() => _hatType;

        // Cleanup
        private void OnDisable()
        {
            VehicleSpot.OnVehicleReturned -= VehicleSpot_VehicleReturnedHandler;
        }
    }

    public class OnDriverDispatchedArgs : EventArgs
    {
        public Driver Driver;
    }
}

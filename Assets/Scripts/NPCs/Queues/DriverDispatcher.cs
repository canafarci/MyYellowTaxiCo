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
        /// <summary>
        /// Dispatches drivers to VehicleSpots in certain states
        /// </summary>
        [SerializeField] private InventoryObjectType _hatType;
        private HashSet<VehicleSpot> _spots = new HashSet<VehicleSpot>();
        private Coroutine _checkSpotsCoroutine;
        private DriverLookup _driverLookup;
        //subbed from DriverQueueCoordinator to remove dispatched driver from driverSpot
        public event EventHandler<OnDriverDispatchedArgs> OnDriverDispatched;

        [Inject]
        private void Init(DriverLookup driverLookup)
        {
            _driverLookup = driverLookup;
        }
        private void Start()
        {
            SubscribeToEvents();

            InvokeRepeating(nameof(CheckTaxiSpots), 0f, 0.5f);
        }

        private void SubscribeToEvents()
        {
            VehicleSpot.OnVehicleReturned += VehicleSpot_VehicleReturnedHandler;
            RepairableVehicle.OnVehicleRepaired += RepairableVehicle_VehicleRepairedHandler;
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
        }

        //Invoked repeatedly every 0.5 seconds
        private void CheckTaxiSpots()
        {
            // Create a copy of the spots to avoid modification while iterating
            foreach (VehicleSpot spot in _spots.ToList())
            {
                HashSet<Driver> driversWithoutHat = _driverLookup.GetDriversWithoutHat();
                HashSet<Driver> driversWithHat = _driverLookup.GetDriversWithHat();

                if (spot.IsCustomerWaiting())
                {
                    DispatchDriverWithoutHat(spot, driversWithoutHat);
                }
                else
                {
                    DispatchDriverWithHat(spot, driversWithHat);
                }
            }
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
            driver.GetController().GoToVehicleSpot(spot.GetInPosition(), () => spot.HandleDriverArrival());

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
            RepairableVehicle.OnVehicleRepaired -= RepairableVehicle_VehicleRepairedHandler;
        }
    }

    public class OnDriverDispatchedArgs : EventArgs
    {
        public Driver Driver;
    }
}

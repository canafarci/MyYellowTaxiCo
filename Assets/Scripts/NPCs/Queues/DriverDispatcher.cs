using System;
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
        private DriverLookup _driverLookup;

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
            if (_hatType == e.HatType)
                AddVehicleSpotToList(e.VehicleSpot);
        }

        private void VehicleSpot_VehicleReturnedHandler(object sender, OnVehicleReturnedArgs e)
        {
            if (!e.IsBrokenCar && _hatType == e.HatType)
                AddVehicleSpotToList(sender as VehicleSpot);
        }

        private void AddVehicleSpotToList(VehicleSpot spot)
        {
            _spots.Add(spot);
        }

        private void CheckTaxiSpots()
        {
            foreach (VehicleSpot spot in _spots.ToList())
            {
                HashSet<Driver> driversWithoutHat = _driverLookup.GetDriversWithoutHat();
                HashSet<Driver> driversWithHat = _driverLookup.GetDriversWithHat();

                if (spot.IsCustomerWaiting())
                    DispatchDriver(spot, driversWithoutHat, driversWithHat);
                else
                    DispatchDriverWithHat(spot, driversWithHat);
            }
        }

        private void DispatchDriver(VehicleSpot spot, HashSet<Driver> driversWithoutHat, HashSet<Driver> driversWithHat)
        {
            if (driversWithoutHat.Count > 0)
            {
                DispatchDriverToSpot(driversWithoutHat, spot);
                spot.SetCustomerWaiting(false);
            }
            else if (DispatchDriverWithHat(spot, driversWithHat))
            {
                spot.SetCustomerWaiting(false);
            }
        }
        // Bool return type is required to set customer waiting in VehicleSpot to false when a driver is dispatched
        private bool DispatchDriverWithHat(VehicleSpot spot, HashSet<Driver> driversWithHat)
        {
            if (driversWithHat.Count > 0)
            {
                DispatchDriverToSpot(driversWithHat, spot);
                return true;
            }
            return false;
        }

        private void DispatchDriverToSpot(HashSet<Driver> drivers, VehicleSpot spot)
        {
            Driver driver = drivers.LastOrDefault();
            driver.GetController().GoToVehicleSpot(spot.GetInPosition(), () => spot.HandleDriverArrival());

            drivers.Remove(driver);
            _spots.Remove(spot);

            OnDriverDispatched?.Invoke(this, new OnDriverDispatchedArgs { Driver = driver });
        }

        public InventoryObjectType GetHatType() => _hatType;

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

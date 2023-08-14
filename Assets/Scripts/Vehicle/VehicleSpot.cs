using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleSpot : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        private Vehicle _vehicle;
        private VehicleStateManager _vehicleManager;
        public static event EventHandler<OnVehicleReturned> OnVehicleReturned;

        [Inject]
        private void Init(VehicleStateManager manager)
        {
            _vehicleManager = manager;
        }
        public void DepartVehicle()
        {
            _vehicle.Depart();
            Clear();
        }
        private void InvokeVehicleReturnedEvent()
        {
            OnVehicleReturned?.Invoke(this, new OnVehicleReturned
            {
                HatType = _hatType,
                SpawnerTransform = transform,
                CanSpawnDriver = _vehicleManager.CanSpawnDriver(this)
            });

        }
        //Getters-Setters
        public Enums.StackableItemType GetHatType() => _hatType;
        public bool HasVehicle() => _vehicle != null;
        public void SetVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            InvokeVehicleReturnedEvent();
        }
        private void Clear() => _vehicle = null;
    }

    public class OnVehicleReturned : EventArgs
    {
        public Enums.StackableItemType HatType;
        public Transform SpawnerTransform;
        public bool CanSpawnDriver;
    }
}

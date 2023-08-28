using System;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class VehicleSpot : MonoBehaviour
    {
        [SerializeField] private InventoryObjectType _hatType;
        private Vehicle _vehicle;
        private VehicleManager _vehicleManager;
        private bool _isCustomerWaiting;
        public static event EventHandler<OnVehicleReturnedArgs> OnVehicleReturned;
        public event Action<int> OnVehicleDeparted;
        public void DepartVehicle()
        {
            _vehicle.GetController().InitiateDeparture();
            _vehicleManager.OnVehicleDeparted();
            OnVehicleDeparted?.Invoke(_vehicle.GetModel().GetMoneyStackCount());
            Clear();
        }
        private void InvokeVehicleReturnedEvent(Vehicle vehicle)
        {
            OnVehicleReturned?.Invoke(this, new OnVehicleReturnedArgs
            {
                HatType = _hatType,
                SpawnerTransform = transform,
                CanSpawnDriver = _vehicleManager.CanSpawnDriver(this),
                IsBrokenCar = vehicle.GetModel().IsCarBroken()
            });

        }
        //Getters-Setters
        public InventoryObjectType GetHatType() => _hatType;
        public bool HasVehicle() => _vehicle != null;
        public void SetVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            InvokeVehicleReturnedEvent(vehicle);
        }
        public bool IsCustomerWaiting() => _isCustomerWaiting;
        public void SetCustomerWaiting(bool value) => _isCustomerWaiting = value;
        private void Clear() => _vehicle = null;
        //initialization
        [Inject]
        private void Init(VehicleManager manager)
        {
            _vehicleManager = manager;
        }
    }

    public class OnVehicleReturnedArgs : EventArgs
    {
        public InventoryObjectType HatType;
        public Transform SpawnerTransform;
        public bool CanSpawnDriver;
        public bool IsBrokenCar;
    }
}

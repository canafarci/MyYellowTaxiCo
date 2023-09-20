using System;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class VehicleSpot : MonoBehaviour, IVehicleEvents
    {
        [SerializeField] private InventoryObjectType _hatType;
        [SerializeField] private Transform _getInPosition;
        private Vehicle _vehicle;
        private VehicleManager _vehicleManager;
        private bool _isCustomerWaiting;
        //events
        //Subscribed from DriverDispatcher, DriverSpawner
        public static event EventHandler<OnVehicleReturnedArgs> OnVehicleReturned;
        //Subscribed from CarSpawnerVisual and HeliSpotVisual, and is part of the contract of IVehicleEvents
        public event Action OnVehicleDeparted;
        //Subscribed from MoneyStacker, and is part of the contract of IVehicleEvents
        public event Action<int> OnVehicleMoneyEarned;
        //initialization
        [Inject]
        private void Init(VehicleManager manager)
        {
            _vehicleManager = manager;
        }

        public void HandleDriverArrival()
        {
            _vehicle.GetController().InitiateDeparture();

            UpdateGameState();
        }
        public void HandleCustomerArrival()
        {
            Assert.IsNotNull(_vehicle);
            _vehicle.GetController().HandleCustomerArrival();
            OnVehicleMoneyEarned?.Invoke(_vehicle.GetModel().GetMoneyStackCount());
        }
        private void UpdateGameState()
        {
            OnVehicleDeparted?.Invoke();
            OnVehicleMoneyEarned?.Invoke(_vehicle.GetModel().GetMoneyStackCount());
            Clear();
        }
        private void InvokeVehicleReturnedEvent(Vehicle vehicle)
        {
            OnVehicleReturned?.Invoke(this, new OnVehicleReturnedArgs
            {
                HatType = _hatType,
                SpawnerTransform = _getInPosition,
                CanSpawnDriver = _vehicleManager.CanSpawnDriver(this),
                IsBrokenCar = vehicle.GetModel().IsCarBroken()
            });
        }
        //Getters-Setters
        public Transform GetInPosition() => _getInPosition;
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

    }

    public class OnVehicleReturnedArgs : EventArgs
    {
        public InventoryObjectType HatType;
        public Transform SpawnerTransform;
        public bool CanSpawnDriver;
        public bool IsBrokenCar;
    }
}

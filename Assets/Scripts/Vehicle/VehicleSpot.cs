using System;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleSpot : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        private Vehicle _vehicle;
        private VehicleManager _vehicleManager;
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
        public Enums.StackableItemType GetHatType() => _hatType;
        public bool HasVehicle() => _vehicle != null;
        public void SetVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            InvokeVehicleReturnedEvent(vehicle);
        }
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
        public Enums.StackableItemType HatType;
        public Transform SpawnerTransform;
        public bool CanSpawnDriver;
        public bool IsBrokenCar;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class VehicleModel : MonoBehaviour
    {
        [SerializeField] private bool _isBroken;
        [SerializeField] private int _moneyToGain;
        private VehicleConfig _config;
        // Callback for notifying Vehicle spot when the vehicle is in place
        private Action _vehicleInPlaceCallback;

        public int GetMoneyStackCount() => _moneyToGain;
        public bool IsCarBroken() => _isBroken;
        public void SetConfig(VehicleConfig config) => _config = config;
        public VehicleConfig GetConfig() => _config;
        public void SetVehicleInPlaceCallback(Action callback) => _vehicleInPlaceCallback = callback;
        public Action GetVehicleInPlaceCallback() => _vehicleInPlaceCallback;
    }
}

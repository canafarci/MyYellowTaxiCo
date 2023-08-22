using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles
{
    public class VehicleModel : MonoBehaviour
    {
        [SerializeField] private Enums.StackableItemType _hatType;
        [SerializeField] private bool _isBroken;
        [SerializeField] private int _moneyToGain;
        private VehicleConfiguration _config;
        // Callback for notifying Vehicle spot when the vehicle is in place
        private List<Action> _vehicleInPlaceCallbacks;

        public int GetMoneyStackCount() => _moneyToGain;
        public bool IsCarBroken() => _isBroken;
        public void SetConfig(VehicleConfiguration config) => _config = config;
        public VehicleConfiguration GetConfig() => _config;
        public void SetVehicleInPlaceCallbacks(List<Action> callback) => _vehicleInPlaceCallbacks = callback;
        public List<Action> GetVehicleInPlaceCallbacks() => _vehicleInPlaceCallbacks;
        public Enums.StackableItemType GetHatType() => _hatType;
    }
}

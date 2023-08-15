using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private int _moneyToGain;
        private CarConfig _config;
        public event Action OnDepart;
        [Inject]
        private void Create(CarConfig config)
        {
            _config = config;
        }

        public void OnTaxiReachedParkSpot()
        {
            VehicleSpot spot = _config.TaxiSpot;
            spot.SetVehicle(this);
        }

        public void Depart()
        {
            OnDepart?.Invoke();
        }
        //Accessed from an animation event (CarExit - End frame)
        public void OnCarMovedAway()
        {
            _config.Spawner.SpawnCar();
            Destroy(gameObject);
        }

        //Getters-Setters
        public CarConfig GetConfig() => _config;
        public int GetMoneyStackCount() => _moneyToGain;
        public class Factory : PlaceholderFactory<UnityEngine.Object, CarConfig, Vehicle>
        {

        }
    }
}

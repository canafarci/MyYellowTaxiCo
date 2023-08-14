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
        private CarView _view;

        [Inject]
        private void Create(CarConfig config)
        {
            _config = config;
        }
        [Inject]
        private void Init(CarView view)
        {
            _view = view;
        }

        public void Depart()
        {
            GainMoneyOnVehicleDeparture();
            _view.PlayDepartAnimation();
        }
        private void GainMoneyOnVehicleDeparture()
        {
            MoneyStacker moneyStacker = _config.Stacker;
            moneyStacker.StackItems(_moneyToGain);
        }

        public void OnCarMovedAway()
        {
            _config.Spawner.SpawnCar();
            Destroy(gameObject);
        }

        //Getters-Setters
        public CarConfig GetConfig() => _config;
        public CarView GetView() => _view;

        public class Factory : PlaceholderFactory<Object, CarConfig, Vehicle>
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class Taxi : MonoBehaviour
    {
        private CarConfig _config;
        public CarConfig GetConfig() => _config;
        [Inject]
        private void Create(CarConfig config)
        {
            _config = config;
        }

        public class Factory : PlaceholderFactory<Object, CarConfig, Taxi>
        {

        }
    }
}

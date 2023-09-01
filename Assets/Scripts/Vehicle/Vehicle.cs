using System;
using System.Collections.Generic;
using TaxiGame.Vehicles.Creation;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        private VehicleController _controller;
        private VehicleModel _model;

        [Inject]
        private void Create(VehicleController controller,
                            VehicleModel model,
                            VehicleConfiguration config,
                            List<Action> vehicleInPlaceCallbacks)
        {
            _controller = controller;
            _model = model;
            _model.SetConfig(config);
            vehicleInPlaceCallbacks.Add(() => config.VehicleSpot.SetVehicle(this));

            _model.SetVehicleInPlaceCallbacks(vehicleInPlaceCallbacks);
        }

        //Getters-Setters
        public VehicleController GetController() => _controller;
        public VehicleModel GetModel() => _model;

        public class Factory : PlaceholderFactory<UnityEngine.Object, VehicleConfiguration, List<Action>, Vehicle>
        {

        }
    }
    [Serializable]
    public struct VehicleConfiguration
    {
        public Animator ParkAnimator;
        public Transform EnterParkNode;
        public Transform ExitParkNode;
        public VehicleSpot VehicleSpot;
        public CarSpawner Spawner;

        public VehicleConfiguration(Animator parkAnimator,
                Transform enterParkNode,
                Transform exitNode,
                VehicleSpot spot,
                CarSpawner carSpawner)
        {
            EnterParkNode = enterParkNode;
            ExitParkNode = exitNode;
            ParkAnimator = parkAnimator;
            VehicleSpot = spot;
            Spawner = carSpawner;
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class Vehicle : MonoBehaviour
    {
        private VehicleController _controller;
        private VehicleModel _model;

        [Inject]
        private void Create(VehicleController controller,
                            VehicleModel model,
                            VehicleConfig config,
                            Action vehicleInPlaceCallback)
        {
            _controller = controller;
            _model = model;
            _model.SetConfig(config);
            _model.SetVehicleInPlaceCallback(() =>
            {
                config.VehicleSpot.SetVehicle(this);
                vehicleInPlaceCallback();
            });
        }

        //Getters-Setters
        public VehicleController GetController() => _controller;
        public VehicleModel GetModel() => _model;

        public class Factory : PlaceholderFactory<UnityEngine.Object, VehicleConfig, Action, Vehicle>
        {

        }
    }
    [Serializable]
    public struct VehicleConfig
    {
        public Animator ParkAnimator;
        public Transform EnterParkNode;
        public Transform ExitParkNode;
        public VehicleSpot VehicleSpot;
        public CarSpawner Spawner;

        public VehicleConfig(Animator parkAnimator,
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

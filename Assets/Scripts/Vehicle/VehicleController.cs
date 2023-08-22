using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class VehicleController : MonoBehaviour
    {
        // Dependencies
        private VehicleAnimator _vehicleAnimator;
        private CarFX _carFX;
        private VehicleTweener _vehicleTweener;
        private VehicleModel _model;



        [Inject]
        private void Init(
            VehicleTweener tweener,
            CarFX carFX,
            VehicleAnimator animator,
            VehicleModel model)
        {
            _vehicleAnimator = animator;
            _carFX = carFX;
            _vehicleTweener = tweener;
            _model = model;
        }

        // Called when the vehicle enter animation finishes
        public void HandleCarEnterAnimationCompleted()
        {
            _vehicleAnimator.DisableAnimator();
            Tween enterTween = _vehicleTweener.CreateEnterTween(_model.GetConfig().EnterParkNode);
            enterTween.onComplete = () => MoveToParkingSpot();
        }

        // Animates the vehicle to the parking spot
        private void MoveToParkingSpot()
        {
            _vehicleAnimator.PlayParkingAnimation(_model.GetConfig().ParkAnimator);
            Invoke(nameof(HandleCarParked), AnimationValues.PARK_ANIM_LENGTH);
        }

        // Called when the vehicle finishes parking animation
        private void HandleCarParked()
        {
            _vehicleTweener.ShrinkDriver();

            // Invoke the callbacks to notify the vehicle spot and change the game state
            foreach (Action callback in _model.GetVehicleInPlaceCallbacks())
            {
                callback?.Invoke();
            }
        }

        // Initiates the departure sequence
        public void InitiateDeparture()
        {
            _vehicleTweener.EnlargeDriver();
            _carFX.PlayTakeOffFX();
            _vehicleAnimator.PlayDepartAnimation(_model.GetConfig().ParkAnimator);

            Invoke(nameof(CompleteDeparture), AnimationValues.PARK_ANIM_LENGTH);
        }

        // Initiates the exit sequence after departure
        private void CompleteDeparture()
        {
            Tween exitTween = _vehicleTweener.CreateExitTween(_model.GetConfig().ExitParkNode);

            exitTween.onComplete = () => HandleExitFromParkingLane();
        }

        // Called when the vehicle exits the parking lane
        private void HandleExitFromParkingLane()
        {
            _vehicleAnimator.PlayExitAnimation();
        }

        // Called from an animation event (CarExit - End frame)
        public void HandleCarMovedAway()
        {
            CarSpawner spawner = _model.GetConfig().Spawner;
            spawner.SpawnCar();
            Destroy(gameObject);
        }

        // Set the data and callback for notifying vehicle spot
    }
}

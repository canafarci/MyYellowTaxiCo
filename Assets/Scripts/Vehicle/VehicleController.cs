using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleController : MonoBehaviour
    {
        // Dependencies
        private VehicleAnimator _vehicleAnimator;
        private CarFX _carFX;
        private VehicleTweener _vehicleTweener;
        private VehicleData _data;

        // Callback for notifying Vehicle spot when the vehicle is in place
        private Action _vehicleInPlaceCallback;

        [Inject]
        private void Init(
            VehicleTweener tweener,
            CarFX carFX,
            VehicleAnimator animator)
        {
            _vehicleAnimator = animator;
            _carFX = carFX;
            _vehicleTweener = tweener;
        }

        // Called when the vehicle enter animation finishes
        public void HandleCarEnterAnimationCompleted()
        {
            _vehicleAnimator.DisableAnimator();
            Tween enterTween = _vehicleTweener.CreateEnterTween(_data.EnterParkNode);
            enterTween.onComplete = () => MoveToParkingSpot();
        }

        // Animates the vehicle to the parking spot
        private void MoveToParkingSpot()
        {
            _vehicleAnimator.PlayParkingAnimation(_data.ParkAnimator);
            Invoke(nameof(HandleCarParked), AnimationValues.PARK_ANIM_LENGTH);
        }

        // Called when the vehicle finishes parking animation
        private void HandleCarParked()
        {
            _vehicleTweener.ShrinkDriver();

            // Invoke the callback to notify the vehicle spot
            _vehicleInPlaceCallback?.Invoke();
        }

        // Initiates the departure sequence
        public void InitiateDeparture()
        {
            _vehicleTweener.EnlargeDriver();
            _carFX.PlayTakeOffFX();
            _vehicleAnimator.PlayDepartAnimation(_data.ParkAnimator);

            Invoke(nameof(CompleteDeparture), AnimationValues.PARK_ANIM_LENGTH);
        }

        // Initiates the exit sequence after departure
        private void CompleteDeparture()
        {
            Tween exitTween = _vehicleTweener.CreateExitTween(_data.ExitParkNode);

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
            CarSpawner spawner = _data.Spawner;
            spawner.SpawnCar();
            Destroy(gameObject);
        }

        // Set the data and callback for notifying vehicle spot
        public void SetData(VehicleData data) => _data = data;
        public void SetVehicleInPlaceCallback(Action callback) => _vehicleInPlaceCallback = callback;
    }
}

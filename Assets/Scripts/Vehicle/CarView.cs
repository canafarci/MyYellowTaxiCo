using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class CarView : MonoBehaviour
    {
        private VehicleAnimator _vehicleAnimator;
        private CarFX _carFX;
        private VehicleTweener _vehicleTweener;
        private VehicleManager _manager;
        private Vehicle _taxi;

        [Inject]
        private void Init(Vehicle taxi,
                        VehicleTweener tweener,
                        CarFX carFX,
                        VehicleAnimator animator)
        {
            _taxi = taxi;
            _vehicleAnimator = animator;
            _carFX = carFX;
            _vehicleTweener = tweener;
        }
        private void Start()
        {
            _taxi.OnDepart += () => PlayDepartVisual();
        }
        //Accessed from an animation event (CarEnter - End frame)
        public void OnCarEnterAnimationFinished()
        {
            _vehicleAnimator.DisableAnimator();

            Tween enterTween = _vehicleTweener.CreateEnterTween(_taxi.GetConfig().EnterParkNode);
            enterTween.onComplete = () => GetToParkSpot();
        }
        private void GetToParkSpot()
        {
            _vehicleAnimator.PlayParkingAnimation(_taxi.GetConfig().ParkAnimator);
            Invoke(nameof(OnCarParked), AnimationValues.PARK_ANIM_LENGTH);
        }
        private void OnCarParked()
        {
            _vehicleTweener.ShrinkDriver();
            _taxi.OnTaxiReachedParkSpot();
        }

        private void PlayDepartVisual()
        {
            _vehicleTweener.EnlargeDriver();
            _carFX.PlayTakeOffFX();
            _vehicleAnimator.PlayDepartAnimation(_taxi.GetConfig().ParkAnimator);

            Invoke(nameof(Exit), AnimationValues.PARK_ANIM_LENGTH);
        }
        private void Exit()
        {
            Tween exitTween = _vehicleTweener.CreateExitTween(_taxi.GetConfig().ExitParkNode);

            exitTween.onComplete = () => ExitParkingLane();
        }
        private void ExitParkingLane()
        {
            _vehicleAnimator.PlayExitAnimation();
        }

    }
}

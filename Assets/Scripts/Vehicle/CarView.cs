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
        [SerializeField] private Animator _wobbleAnimator;
        [SerializeField] GameObject _driver;
        private Animator _animator;
        private CarFX _carFX;
        private Vehicle _taxi;
        private Vector3 _driverBaseScale;
        private void Awake()
        {
            _driverBaseScale = _driver.transform.lossyScale;
        }
        [Inject]
        private void Init(Vehicle taxi, Animator animator, CarFX carFX)
        {
            _taxi = taxi;
            _animator = animator;
            _carFX = carFX;
        }
        public void OnCarEnterAnimationFinished()
        {
            _animator.enabled = false;
            Transform enterNode = _taxi.GetConfig().EnterParkNode;

            Tween move = transform
                            .DOMove(enterNode.position, 0.6f)
                            .SetEase(Ease.Linear);

            move.onComplete = () => GetToParkSpot();
        }
        public void PlayDepartAnimation()
        {
            _driver.SetActive(true);
            _driver.transform.DOScale(_driverBaseScale, .4f);

            Animator parkAnimator = _taxi.GetConfig().ParkAnimator;
            parkAnimator.Play(AnimationValues.PARK_OUT);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_OUT);
            _carFX.TakeOffFX();

            Invoke(nameof(Exit), AnimationValues.PARK_ANIM_LENGTH);
        }
        private void Exit()
        {
            transform.SetParent(null);

            Transform exitNode = _taxi.GetConfig().ExitParkNode;
            float time = GetMovementTime(exitNode);
            Tween move = transform.DOMove(exitNode.position, time).SetEase(Ease.Linear);

            move.onComplete = () => ExitParkingLane();
        }
        private float GetMovementTime(Transform exitNode)
        {
            float referenceVelocity = 50f / 3f;
            float distance = Vector3.Distance(transform.position, exitNode.position);
            return distance / referenceVelocity;
        }
        private void GetToParkSpot()
        {
            Animator parkAnimator = _taxi.GetConfig().ParkAnimator;

            parkAnimator.Play(AnimationValues.PARK_IN);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_IN);

            transform.SetParent(parkAnimator.transform);
            transform.localPosition = Vector3.zero;

            Invoke(nameof(OnCarParked), AnimationValues.PARK_ANIM_LENGTH);
        }
        private void ExitParkingLane()
        {
            _animator.enabled = true;
            _animator.Play(AnimationValues.CAR_EXIT);
            FindObjectOfType<LevelProgress>().OnLevelProgress();
        }
        private void OnCarParked()
        {
            _taxi.GetConfig().TaxiSpot.SetVehicle(_taxi);

            Tween scale = _driver.transform.DOScale(0.00001f, .5f);
            TweenCallback callback = () => _driver.SetActive(false);
            scale.onComplete = callback;
        }
    }
}

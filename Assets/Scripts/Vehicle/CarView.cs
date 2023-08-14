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
        private Taxi _taxi;

        [Inject]
        private void Init(Taxi taxi, Animator animator)
        {
            _taxi = taxi;
            _animator = animator;
        }
        public void OnCarEnterAnimationFinished()
        {
            _animator.enabled = false;
            Tween move = transform.DOMove(_taxi.GetConfig().EnterParkNode.position, 0.6f)
                                    .SetEase(Ease.Linear);
            move.onComplete = () => GetToParkSpot();
        }
        private void GetToParkSpot()
        {
            _taxi.GetConfig().ParkAnimator.Play(AnimationValues.PARK_IN);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_IN);

            transform.parent = _taxi.GetConfig().ParkAnimator.transform;
            transform.localPosition = Vector3.zero;

            Invoke(nameof(OnCarParked), AnimationValues.PARK_IN_ANIM_LENGTH);
        }

        public void OnCarParked()
        {
            _taxi.GetConfig().TaxiSpot.SetTaxi(_taxi);

            Tween scale = _driver.transform.DOScale(0.00001f, .5f);
            TweenCallback callback = () => _driver.SetActive(false);
            scale.onComplete = callback;
        }
    }
}

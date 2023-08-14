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
        private Animator _animator;
        private CarConfig _config;

        [Inject]
        private void Init(Animator animator, CarConfig config)
        {
            _config = config;
            _animator = animator;
        }
        public void OnCarEnterAnimationFinished()
        {
            _animator.enabled = false;
            Tween move = transform.DOMove(_config.EnterParkNode.position, 0.6f).SetEase(Ease.Linear);
            move.onComplete = () => GetToParkSpot();
        }
        private void GetToParkSpot()
        {
            _config.ParkAnimator.Play(AnimationValues.PARK_IN);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_IN);

            transform.parent = _config.ParkAnimator.transform;
            transform.localPosition = Vector3.zero;
        }

        public class Factory : PlaceholderFactory<Object, CarConfig, CarView>
        {

        }
    }
}

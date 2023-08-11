using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Taxi.Vehicle
{
    public class CarMover : MonoBehaviour
    {
        private Animator _animator;
        private Transform _parkEnterNode;
        private Animator _parkAnimator;

        [Inject]
        private void Init(Animator animator,
                        CarConfig config)
        {
            _animator = animator;
            _parkEnterNode = config.EnterParkNode;
            _parkAnimator = config.ParkAnimator;
        }
        public void OnCarEnterAnimationFinished()
        {
            _animator.enabled = false;
            Tween move = transform.DOMove(_parkEnterNode.position, 0.6f).SetEase(Ease.Linear);
            move.onComplete = () => GetToParkSpot();
        }
        private void GetToParkSpot()
        {
            _parkAnimator.Play("ParkIn");
            //_animator.Play("WobbleParkIn");

            transform.parent = _parkAnimator.transform;
            transform.localPosition = Vector3.zero;
        }

        public class Factory : PlaceholderFactory<Object, CarConfig, CarMover>
        {

        }
    }
}

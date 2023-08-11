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
        private Transform _destinationEnterNode;

        [Inject]
        private void Init(Animator animator)
        {
            _animator = animator;
        }
        public void OnCarEnterAnimationFinished()
        {
            _animator.enabled = false;
            Tween move = transform.DOMove(_destinationEnterNode.position, 0.6f).SetEase(Ease.Linear);
        }
    }
}

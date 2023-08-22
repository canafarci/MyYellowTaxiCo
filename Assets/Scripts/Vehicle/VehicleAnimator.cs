using System.Collections;
using System.Collections.Generic;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class VehicleAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _wobbleAnimator;
        private Animator _animator;

        [Inject]
        private void Init(Animator animator)
        {
            _animator = animator;
        }

        public void PlayParkingAnimation(Animator parkAnimator)
        {

            parkAnimator.Play(AnimationValues.PARK_IN);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_IN);

            transform.SetParent(parkAnimator.transform);
            transform.localPosition = Vector3.zero;
        }
        public void PlayDepartAnimation(Animator parkAnimator)
        {
            parkAnimator.Play(AnimationValues.PARK_OUT);
            _wobbleAnimator.Play(AnimationValues.WOBBLE_OUT);
        }
        public void PlayExitAnimation()
        {
            _animator.enabled = true;
            _animator.Play(AnimationValues.CAR_EXIT);
        }

        public void DisableAnimator() => _animator.enabled = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class RiderNPCController : NavMeshMover
    {
        public void MoveAndSit(Transform destination)
        {
            _scheduler.AddToActionQueue(GoAndSit(destination));
        }
        public void GoToVehicleSpot(Transform destination, Action onNPCReachedCar = null)
        {
            _scheduler.AddToActionQueue(MoveToCar(destination, onNPCReachedCar));
        }

        private IEnumerator GoAndSit(Transform trans)
        {
            _scheduler.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            yield return StartCoroutine(MoveToPosition(trans.position));

            Tween move = GetToExactPosition(trans);
            yield return move.WaitForCompletion();

            _scheduler.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, true);
        }

        private Tween GetToExactPosition(Transform destination)
        {
            Tween move = transform.DOMove(destination.position, .5f);
            transform.DORotate(destination.rotation.eulerAngles, .5f);
            return move;
        }

        private IEnumerator MoveToCar(Transform destination, Action onNPCReachedCar)
        {
            _scheduler.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            yield return StartCoroutine(MoveToPosition(destination.position));
            _scheduler.InvokeAnimationStateChangedEvent(AnimationValues.CAR_ENTER, true);

            yield return new WaitForSeconds(.5f);

            onNPCReachedCar?.Invoke();

            Destroy(gameObject, 0.1f);
        }
    }
}

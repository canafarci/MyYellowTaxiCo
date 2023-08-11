using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Taxi.Animations;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class RiderNPC : NavMeshMover
    {
        public void Move(Transform destination)
        {
            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            Move(destination.position);
        }

        public void MoveAndSit(Transform destination)
        {
            _npc.AddToActionQueue(GoAndSit(destination));
        }
        public void GoToCar(Transform destination, Action onNPCReachedCar)
        {
            _npc.AddToActionQueue(MoveToCar(destination, onNPCReachedCar));
        }

        private IEnumerator GoAndSit(Transform trans)
        {
            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            yield return StartCoroutine(MoveToPosition(trans.position));
            Tween move = GetToExactPosition(trans);
            yield return move.WaitForCompletion();

            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, true);
        }

        private Tween GetToExactPosition(Transform destination)
        {
            Tween move = transform.DOMove(destination.position, .5f);
            transform.DORotate(destination.rotation.eulerAngles, .5f);
            return move;
        }

        private IEnumerator MoveToCar(Transform destination, Action onNPCReachedCar)
        {
            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            yield return StartCoroutine(MoveToPosition(destination.position));
            _npc.InvokeAnimationStateChangedEvent(AnimationValues.ENTERING_CAR, true);

            yield return new WaitForSeconds(.5f);

            onNPCReachedCar();
            Destroy(gameObject, 0.6f);
        }
    }
}

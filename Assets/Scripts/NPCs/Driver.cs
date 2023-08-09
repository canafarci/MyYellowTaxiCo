using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Taxi.Animations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class Driver : NavMeshNPC
    {
        [SerializeField] private Transform _hatTransform;
        private bool _hasHat = false;
        public void MoveAndSit(QueueSpot spot)
        {
            StartAction(MoveAndSit(spot.transform));
        }
        public void GoToCarAndGetIn(Spawner spawner)
        {
            StartAction(GoToCarAndMove(spawner));
        }
        private IEnumerator MoveAndSit(Transform trans)
        {
            yield return StartCoroutine(MoveToPosition(trans.position));

            Tween move = GetToExactPosition(trans);
            yield return move.WaitForCompletion();

            InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, true);
        }

        private Tween GetToExactPosition(Transform trans)
        {
            Tween move = transform.DOMove(trans.position, .5f);
            transform.DORotate(trans.rotation.eulerAngles, .5f);
            return move;
        }

        private IEnumerator GoToCarAndMove(Spawner spawner) //refactor
        {
            spawner.DriverIsComing = true;
            InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            yield return StartCoroutine(MoveToPosition(spawner.transform.position));

            InvokeAnimationStateChangedEvent(AnimationValues.ENTERING_CAR, true);

            yield return new WaitForSeconds(.5f);

            spawner.StartMove();
            Destroy(gameObject, 0.6f);
        }
        public void SetHasHat(bool hasHat)
        {
            _hasHat = hasHat;
        }
        //Getter-Setters
        public Transform GetHatTransform() => _hatTransform;
        public bool DriverHasHat() => _hasHat;
    }
}
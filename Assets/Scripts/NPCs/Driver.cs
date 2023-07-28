using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Taxi.NPC
{
    public class Driver : NavMeshNPC
    {
        [SerializeField] private Transform _hatTransform;
        [SerializeField] private ParticleSystem _fx;
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
            Tween move = transform.DOMove(trans.position, .5f);
            Tween rot = transform.DORotate(trans.rotation.eulerAngles, .5f);

            yield return move.WaitForCompletion();
            GetComponentInChildren<Animator>().SetBool("IsSitting", true);
        }
        private IEnumerator GoToCarAndMove(Spawner spawner) //refactor
        {
            spawner.DriverIsComing = true;
            GetComponentInChildren<Animator>().SetBool("IsSitting", false);
            yield return StartCoroutine(MoveToPosition(spawner.transform.position));

            GetComponentInChildren<Animator>().Play("Car Door Open");

            Tween tween = transform.DOScale(Vector3.one * 0.0001f, .5f);

            yield return tween.WaitForCompletion();

            spawner.StartMove();
            Destroy(gameObject, 0.6f);
        }
        public void GiveHat(StackableItem item)
        {
            _hasHat = true;
            DotweenFX.MoveObjectInArc(item.transform, _hatTransform);
        }
        // public void ActivateHat()
        // {
        //     _hasHat = true;

        //     //TODO seperate view
        //     _hatTransform.GetChild(0).GetComponent<Renderer>().enabled = true;
        //     Sequence seq = DOTween.Sequence();
        //     Vector3 baseScale = _hatTransform.localScale;
        //     seq.Append(_hatTransform.DOScale(baseScale * 1.2f, .1f));
        //     seq.Append(_hatTransform.DOScale(baseScale, .1f));
        // }


        //Getter-Setters
        public bool DriverHasHat() => _hasHat;


    }
}
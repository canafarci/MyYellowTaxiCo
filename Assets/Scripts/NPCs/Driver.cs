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
        public DriverInQueue DriverInQueue;
        [SerializeField] private Transform _hatTransform;
        [SerializeField] private ParticleSystem _fx;
        public IEnumerator GetToPosAndSit(Transform trans)
        {
            yield return StartCoroutine(GetToPosCoroutine(trans.position));
            Tween move = transform.DOMove(trans.position, .5f);
            Tween rot = transform.DORotate(trans.rotation.eulerAngles, .5f);

            yield return move.WaitForCompletion();
            GetComponentInChildren<Animator>().SetBool("IsSitting", true);
        }
        public void ActivateHat()
        {
            _hatTransform.GetChild(0).GetComponent<Renderer>().enabled = true;

            Sequence seq = DOTween.Sequence();
            Vector3 baseScale = _hatTransform.localScale;
            seq.Append(_hatTransform.DOScale(baseScale * 1.2f, .1f));
            seq.Append(_hatTransform.DOScale(baseScale, .1f));
        }
        public IEnumerator GetHatAndGoToCar(StackableItem item, Action callback)
        {
            yield return StartCoroutine(DotweenFX.WearHatTween(item, _hatTransform, _fx));
            while (true)
            {
                DropZone dropZone = FindObjectsOfType<DropZone>().Where(x => x.HatType == Hat && x.CarIsReady && !x.DriverOnWay).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).LastOrDefault();

                if (dropZone != null)
                {
                    dropZone.DriverOnWay = true;
                    GetComponentInChildren<Animator>().SetBool("IsSitting", false);
                    dropZone.CallMoveDriver(this);
                    callback();
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(.5f);
                }
            }
        }
    }
}
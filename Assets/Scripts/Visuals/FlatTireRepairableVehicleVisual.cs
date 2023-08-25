using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class FlatTireRepairableVehicleVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _image;
        [SerializeField] private Transform _target;
        private FlatTireRepairableVehicle _repairableVehicle;


        [Inject]
        private void Init(FlatTireRepairableVehicle repairableVehicle)
        {
            _repairableVehicle = repairableVehicle;
        }

        private void Start()
        {
            _repairableVehicle.OnPlayerEnteredWithTire += FlatTireRepairableVehicle_PlayerEnteredWithTireHandler;
        }

        private void FlatTireRepairableVehicle_PlayerEnteredWithTireHandler(object sender, OnPlayerEnteredWithTireArgs e)
        {
            StartCoroutine(RepairTireSequence(e.Item, _target));
        }


        private IEnumerator RepairTireSequence(StackableItem item, Transform target)
        {
            ClearItemTransform(item, transform);
            Tween pathTween = GeneratePathTween(item, target);

            yield return pathTween.WaitForCompletion();

            _image.SetActive(false);

            PlayAnimationEndingSequence(item);
        }

        private Tween GeneratePathTween(StackableItem item, Transform target)
        {
            ClearItemTransform(item, target);

            Vector3[] path = GenerateTweenPath(item, target);

            Tween pathTween = item.transform.DOLocalPath(path,
                                     Globals.TIRE_DROP_TWEEN_DURATION,
                                     PathType.CatmullRom,
                                     PathMode.Full3D).
                                     SetEase(Ease.InOutSine);

            item.transform.DOLocalRotate(target.localRotation.eulerAngles, Globals.TIRE_DROP_TWEEN_DURATION);
            return pathTween;
        }


        private Vector3[] GenerateTweenPath(StackableItem item, Transform target)
        {
            Vector3 endPos = target.localPosition;
            Vector3 startPos = item.transform.localPosition;
            Vector3 intermediatePos = new Vector3((startPos.x + endPos.x) / 2f,
                                                5f,
                                                (startPos.z + endPos.z) / 2f);
            Vector3[] path = { startPos, intermediatePos, endPos };
            return path;
        }

        private void ClearItemTransform(StackableItem item, Transform target)
        {
            item.transform.DOKill();
            item.transform.parent = target.parent;
        }

        private void PlayAnimationEndingSequence(StackableItem item)
        {
            Sequence seq = DOTween.Sequence();
            Vector3 endBaseScale = item.transform.localScale;
            seq.Append(item.transform.DOScale(endBaseScale * 1.2f, .1f));
            seq.onComplete = () => Destroy(item.gameObject);
        }
    }
}

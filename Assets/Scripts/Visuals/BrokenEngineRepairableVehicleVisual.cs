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
    public class BrokenEngineRepairableVehicleVisual : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] GameObject _image;
        [SerializeField] GameObject _fx;
        private BrokenEngineRepairableVehicle _repairableVehicle;

        [Inject]
        private void Init(BrokenEngineRepairableVehicle repairableVehicle)
        {
            _repairableVehicle = repairableVehicle;
        }

        private void Start()
        {
            BrokenEngineRepairableVehicle.OnPlayerEnteredWithToolbox += BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler;
        }

        private void BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler(object sender, OnPlayerEnteredWithToolboxArgs e)
        {
            if (sender as BrokenEngineRepairableVehicle == _repairableVehicle)
            {
                StartCoroutine(RepairEngineSequence(e.Item, _target));
            }
        }

        private IEnumerator RepairEngineSequence(StackableItem item, Transform target)
        {
            item.transform.parent = transform;

            Tween moveTween = CreateMoveTween(item, target);

            yield return moveTween.WaitForCompletion();
            _fx.SetActive(true);
            yield return new WaitForSeconds(Globals.TOOLBOX_DROP_REPAIR_ANIMATION_DURATION);
            _fx.SetActive(false);
            _image.SetActive(false);

            PlayEndSequence(item, target);
        }

        private void PlayEndSequence(StackableItem item, Transform target)
        {
            Sequence seq = DOTween.Sequence();
            Vector3 baseScale = item.transform.localScale;

            seq.Append(item.transform.DOScale(baseScale * 0.95f, .15f));
            seq.Append(item.transform.DOScale(target.localScale,
                                            Globals.TOOLBOX_DROP_TWEEN_DURATION));

            seq.onComplete = () => Destroy(item.gameObject);
        }

        private Tween CreateMoveTween(StackableItem item, Transform target)
        {
            Vector3[] path = GeneratePath(target);

            Tween moveTween = item.transform.DOLocalPath(path,
                                                    Globals.TOOLBOX_DROP_TWEEN_DURATION,
                                                    PathType.CatmullRom, PathMode.Full3D);

            item.transform.DOLocalRotate(target.localRotation.eulerAngles,
                                        Globals.TOOLBOX_DROP_TWEEN_DURATION);
            return moveTween;
        }

        private Vector3[] GeneratePath(Transform target)
        {
            Vector3 endPos = target.localPosition;
            Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 4f, endPos.z / 2f);
            Vector3[] path = { intermediatePos, endPos };
            return path;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class HatDistributorVisual : MonoBehaviour
    {
        private DriverHatDistributor _hatDistributor;

        [Inject]
        private void Init(DriverHatDistributor hatDistributor)
        {
            _hatDistributor = hatDistributor;
        }

        private void Start()
        {
            _hatDistributor.OnHatDistributed += HatDistributor_HatDistributedHandler;
        }

        private void HatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
            MoveObjectInArc(e.Item, e.Driver.GetHatTransform());
        }

        private void MoveObjectInArc(Transform item, Transform target)
        {
            item.parent = target;

            Vector3[] path = GeneratePath(item, target);

            item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);
            item.transform.DOLocalRotate(target.localRotation.eulerAngles, 0.5f);
        }

        private static Vector3[] GeneratePath(Transform item, Transform target)
        {
            Vector3 endPos = target.localPosition;
            Vector3 startPos = item.transform.localPosition;
            Vector3 intermediatePos = new Vector3((endPos.x + startPos.x) / 2f,
                                                    endPos.y + 2f,
                                                    (endPos.z + startPos.z) / 2f);

            Vector3[] path = { startPos, intermediatePos, endPos };
            return path;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class FlatTireRepairableVehicleVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _image;
        [SerializeField] private Transform _target;

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
                                     Globals.TOOLBOX_DROP_TWEEN_DURATION,
                                     PathType.CatmullRom,
                                     PathMode.Full3D);

            item.transform.DOLocalRotate(target.localRotation.eulerAngles, Globals.TOOLBOX_DROP_TWEEN_DURATION);
            return pathTween;
        }


        private Vector3[] GenerateTweenPath(StackableItem item, Transform target)
        {
            Vector3 endPos = target.localPosition;
            Vector3 startPos = item.transform.localPosition;
            Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
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
            seq.onComplete = () => item.gameObject.SetActive(false);
        }
    }
}

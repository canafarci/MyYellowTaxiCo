using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TaxiGame.Visuals
{
    public class TweeningService
    {
        public Sequence GenerateMoveSequence(Transform item, Transform target, float duration)
        {
            return TweenStackableItemToTransformInternal(item, target.localPosition, duration, target.localRotation.eulerAngles.y);
        }
        public Sequence GenerateMoveSequence(Transform item, Vector3 target, float duration)
        {
            return TweenStackableItemToTransformInternal(item, target, duration, item.localRotation.eulerAngles.y);
        }
        public Sequence GenerateMoveSequenceWithRandomRotation(Transform item, Vector3 target, float duration)
        {
            float randomRotation = UnityEngine.Random.Range(0f, 360f);
            return TweenStackableItemToTransformInternal(item, target, duration, randomRotation);
        }
        public Sequence GenerateChangeScaleSequence(Transform item, float startScale, float finishScale, float duration)
        {
            Vector3 endScale = item.localScale;

            Sequence endSequence = DOTween.Sequence();
            endSequence.Append(item.DOScale(endScale * startScale, duration));
            endSequence.Append(item.DOScale(endScale * finishScale, duration));

            return endSequence;
        }
        private Sequence TweenStackableItemToTransformInternal(Transform item, Vector3 target, float duration, float rotationValue)
        {
            Vector3[] path = GeneratePath(item, target);

            Sequence moveSequence = DOTween.Sequence();
            moveSequence.Join(item.DOLocalPath(path, duration, PathType.CatmullRom, PathMode.Full3D));
            moveSequence.Join(item.DOLocalRotate(new Vector3(0, rotationValue, 0f), duration));

            return moveSequence;
        }
        private static Vector3[] GeneratePath(Transform item, Vector3 endPos)
        {
            Vector3 startPos = item.localPosition;
            Vector3 intermediatePos = new Vector3((endPos.x + startPos.x) / 2f,
                                                  endPos.y + 2f,
                                                  (endPos.z + startPos.z) / 2f);

            Vector3[] path = { startPos, intermediatePos, endPos };
            return path;
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIPulseEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Pulse());
    }
    IEnumerator Pulse()
    {
        Vector3 startScale = transform.localScale;

        while (true)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(startScale * 1.3f, .15f));
            seq.Append(transform.DOScale(startScale, .35f));
            yield return seq.WaitForCompletion();
        }
    }
}

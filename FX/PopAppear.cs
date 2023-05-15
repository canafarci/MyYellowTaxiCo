using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopAppear : MonoBehaviour
{
    private void Start()
    {
        Vector3 startScale = transform.localScale;

        transform.localScale = Vector3.one * 0.000001f;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(startScale * 1.2f, 0.25f));
        seq.Append(transform.DOScale(startScale, 0.35f));


    }
}

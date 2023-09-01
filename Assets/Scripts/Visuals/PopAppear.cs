using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TaxiGame.Visuals
{
    public class PopAppear : MonoBehaviour
    {
        /// <summary>
        /// Transforms which have this class attached to them will have a
        /// Growing and "Popping" animation when enabled
        /// </summary>
        private void Start()
        {
            Vector3 startScale = transform.localScale;

            transform.localScale = Vector3.one * 0.0001f;

            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(startScale * 1.2f, 0.25f));
            seq.Append(transform.DOScale(startScale, 0.35f));
        }
    }
}
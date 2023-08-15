using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicle
{
    public class VehicleTweener : MonoBehaviour
    {
        [SerializeField] GameObject _driver;
        [SerializeField] Transform[] _passengers;
        private Vector3 _driverBaseScale;

        private void Awake()
        {
            _driverBaseScale = _driver.transform.lossyScale;
        }
        public Tween CreateEnterTween(Transform enterNode)
        {
            Tween move = transform
                .DOMove(enterNode.position, 0.6f)
                .SetEase(Ease.Linear);

            return move;
        }
        public Tween CreateExitTween(Transform exitNode)
        {
            //detach from TaxiSpot animator's transform
            transform.SetParent(null);

            float time = GetMovementTime(exitNode);
            Tween move = transform
                            .DOMove(exitNode.position, time)
                            .SetEase(Ease.Linear);

            return move;
        }
        public void ShrinkDriver()
        {
            Tween scale = _driver.transform.DOScale(0.00001f, .5f);
            TweenCallback callback = () => _driver.SetActive(false);
            scale.onComplete = callback;
        }
        public void EnlargePassengers()
        {
            foreach (Transform tr in _passengers)
            {
                tr.gameObject.SetActive(true);
                Vector3 baseScale = tr.localScale;
                tr.localScale = Vector3.one * 0.00001f;
                tr.DOScale(baseScale, .4f);
            }
        }
        public void EnlargeDriver()
        {
            _driver.SetActive(true);
            _driver.transform.DOScale(_driverBaseScale, .4f);
        }

        private float GetMovementTime(Transform exitNode)
        {
            float referenceVelocity = 50f / 3f;
            float distance = Vector3.Distance(transform.position, exitNode.position);
            return distance / referenceVelocity;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items
{
    [RequireComponent(typeof(SphereCollider))]
    public class WandererMoney : MonoBehaviour
    {
        public static event Action MoneyPickupHandler;
        public event EventHandler<OnWandererMoneyPickedUpArgs> OnWandererMoneyPickedUp;

        [Inject]
        private void Create(Transform target)
        {
            transform.position = target.position;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }

            OnWandererMoneyPickedUp?.Invoke(this, new OnWandererMoneyPickedUpArgs { Target = other.transform });
            MoneyPickupHandler.Invoke();
            Destroy(gameObject, 0.25f);
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, Transform, WandererMoney>
        {
        }
    }
    public class OnWandererMoneyPickedUpArgs : EventArgs
    {
        public Transform Target;
    }
}
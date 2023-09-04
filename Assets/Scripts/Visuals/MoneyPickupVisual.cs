using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class MoneyPickupVisual : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        [Inject]
        private void Init(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }
        private void OnEnable()
        {
            MoneyStacker.MoneyPickupHandler += OnMoneyPickup;
            WandererMoney.WandererMoneyPickupHandler += OnMoneyPickup;
        }

        private void OnDisable()
        {
            MoneyStacker.MoneyPickupHandler -= OnMoneyPickup;
            WandererMoney.WandererMoneyPickupHandler -= OnMoneyPickup;
        }
        private void OnMoneyPickup()
        {
            _particleSystem.Play();
        }
    }
}
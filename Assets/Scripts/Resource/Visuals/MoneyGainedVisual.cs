using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource.Visuals
{
    public class MoneyGainedVisual : MonoBehaviour
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
            WandererMoney.MoneyPickupHandler += OnMoneyPickup;
        }

        private void OnDisable()
        {
            MoneyStacker.MoneyPickupHandler -= OnMoneyPickup;
            WandererMoney.MoneyPickupHandler -= OnMoneyPickup;
        }
        private void OnMoneyPickup()
        {
            _particleSystem.Play();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Taxi.WaitZones
{
    public class BuyableWaitingZone : WaitingEngine
    {
        [SerializeField] private float _moneyToUnlock;
        private float _remainingMoney;
        private PayMoneyProcessor _payCalculator;

        [Inject]
        private void Init(PayMoneyProcessor payCalculator)
        {
            _payCalculator = payCalculator;
        }
        private void Awake()
        {
            _remainingMoney = _moneyToUnlock;
        }
        protected override void Iterate(ref float remainingTime, GameObject instigator)
        {
            bool isSuccessful = _payCalculator.ProcessPay(ref remainingTime, ref _remainingMoney);

            if (!isSuccessful)
                Cancel(instigator);
            else
                RaiseIterationEvent(instigator, _remainingMoney, _moneyToUnlock);
        }

        protected override bool CheckCanContinue(float remainingTime)
        {
            return remainingTime > 0f && _remainingMoney > 0f;
        }
        //Getters-Setters
        public float GetCost() => _moneyToUnlock;
    }
}

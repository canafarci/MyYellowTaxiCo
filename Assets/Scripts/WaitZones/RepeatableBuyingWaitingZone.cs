using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using TMPro;
using UnityEngine;
using Zenject;

namespace TaxiGame.WaitZones
{
    public class RepeatableBuyingWaitingZone : WaitingEngine
    {
        private float _moneyToUnlock;
        private float _remainingMoney;
        private PayMoneyProcessor _payCalculator;

        [Inject]
        private void Init(PayMoneyProcessor payCalculator)
        {
            _payCalculator = payCalculator;
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
        public void SetPrice(float cost)
        {
            _moneyToUnlock = cost;
            _remainingMoney = cost;
        }
        public float GetCost() => _moneyToUnlock;

#if UNITY_INCLUDE_TESTS
        // Getters-Setters for testing purpose
        public void SetPayProcessor(PayMoneyProcessor payProcessor)
        {
            _payCalculator = payProcessor;
        }
        public float GetRemainingMoney() => _remainingMoney;
        public void SetRemainingMoney(float money) => _remainingMoney = money;
#endif
    }
}


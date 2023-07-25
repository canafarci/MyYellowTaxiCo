using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Taxi.WaitZones
{
    [RequireComponent(typeof(PayMoneyProcessor))]
    public class BuyableWaitingZone : WaitingEngine
    {
        [SerializeField] private float _moneyToUnlock;
        private float _remainingMoney;
        private float _moneyStep;
        private PayMoneyProcessor _payCalculator;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            _payCalculator = GetComponent<PayMoneyProcessor>();

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

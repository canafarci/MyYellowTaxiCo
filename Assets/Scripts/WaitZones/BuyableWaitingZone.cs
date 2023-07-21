using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Taxi.WaitZones
{
    [RequireComponent(typeof(BuyableWaitingZoneVisual), typeof(PayMoneyProcessor))]
    public class BuyableWaitingZone : WaitingEngine
    {
        [SerializeField] private float _moneyToUnlock;
        private float _remainingMoney;
        private float _moneyStep;
        private BuyableWaitingZoneVisual _visual;
        private PayMoneyProcessor _payCalculator;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            _payCalculator = GetComponent<PayMoneyProcessor>();

            _remainingMoney = _moneyToUnlock;

            _visual = GetComponent<BuyableWaitingZoneVisual>();
            _visual.Initialize(_moneyToUnlock);
        }
        protected override void Iterate(ref float remainingTime, GameObject instigator)
        {
            bool isSuccessful = _payCalculator.ProcessPay(ref remainingTime, ref _remainingMoney);

            if (!isSuccessful)
            {
                Cancel(instigator);
            }

            _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);
        }

        private void UpdateState()
        {
            _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);
        }

        protected override bool CheckCanContinue(float remainingTime)
        {
            return remainingTime > 0f && _remainingMoney > 0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using TMPro;
using UnityEngine;

namespace Taxi.WaitZones
{
    public class RepeatableBuyingWaitingZone : WaitingEngine
    {
        private float _moneyToUnlock;
        private IUpgradeCommand _upgradeCommand;
        private float _remainingMoney;
        private PayMoneyProcessor _payCalculator;
        private void Awake()
        {
            _payCalculator = GetComponent<PayMoneyProcessor>();
        }
        public override void Begin(WaitZoneConfigSO config, GameObject other)
        {
            base.Begin(config, other);
        }
        protected override void OnSuccess(GameObject instigator)
        {
            _upgradeCommand.Execute();

            base.OnSuccess(instigator);
        }

        protected override bool CheckCanContinue(float remainingTime)
        {
            return remainingTime > 0f && _remainingMoney > 0f;
        }

        protected override void Iterate(ref float remainingTime, GameObject instigator)
        {
            bool isSuccessful = _payCalculator.ProcessPay(ref remainingTime, ref _remainingMoney);

            if (!isSuccessful)
            {
                Cancel(instigator);
            }
            RaiseIterationEvent(instigator, _remainingMoney, _moneyToUnlock);
        }

        //Getters-Setters
        public void SetPrice(float cost)
        {
            _moneyToUnlock = cost;
            _remainingMoney = cost;
        }
        public void SetUpgradeCommand(IUpgradeCommand command) => _upgradeCommand = command;
        public float GetCost() => _moneyToUnlock;
    }
}


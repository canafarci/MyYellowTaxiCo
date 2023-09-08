using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Resource;
using UnityEngine;
using Zenject;

namespace TaxiGame.WaitZones
{
    public class PayMoneyProcessor
    {
        public static Action<float> MoneyPayHandler;
        private ResourceTracker _resourceTracker;
        [Inject]
        private void Init(ResourceTracker tracker)
        {
            _resourceTracker = tracker;
        }
        public bool ProcessPay(ref float remainingTime, ref float remainingMoney)
        {
            float playerMoney = _resourceTracker.PlayerMoney;
            float moneyStep = remainingMoney / remainingTime * Globals.TIME_STEP;

            float precalculatedPlayerMoneyAfterStep = playerMoney - moneyStep;
            float preCalculatedRemainingPayMoney = remainingMoney - moneyStep;

            if (preCalculatedRemainingPayMoney <= 0f)
            {
                MoneyPayHandler?.Invoke(remainingMoney);
                remainingMoney = 0f;
                return true;
            }
            if (precalculatedPlayerMoneyAfterStep < 0)
            {
                OnNotEnoughMoney(ref remainingMoney, playerMoney);
                return false;
            }

            remainingTime -= Globals.TIME_STEP;
            remainingMoney -= moneyStep;
            MoneyPayHandler?.Invoke(moneyStep);
            return true;
        }

        private void OnNotEnoughMoney(ref float remainingMoney, float playerMoney)
        {
            remainingMoney -= playerMoney;
            _resourceTracker.ZeroMoney();
        }
    }
}
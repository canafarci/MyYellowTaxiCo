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

            bool canPay = false;

            if (CanRegularPay(precalculatedPlayerMoneyAfterStep, preCalculatedRemainingPayMoney))
            {
                OnRegularPay(ref remainingTime, ref remainingMoney, moneyStep);
                canPay = true;
            }
            //player has excess money
            else if (preCalculatedRemainingPayMoney < 0f)
            {
                remainingMoney = OnPlayerHasExcessMoney(remainingMoney);
                canPay = true;
            }
            else
            {
                OnNotEnoughMoney(ref remainingMoney, playerMoney);
            }

            return canPay;
        }

        private static void OnRegularPay(ref float remainingTime, ref float remainingMoney, float moneyStep)
        {
            remainingTime -= Globals.TIME_STEP;
            remainingMoney -= moneyStep;
            MoneyPayHandler?.Invoke(moneyStep);
        }

        private static float OnPlayerHasExcessMoney(float remainingMoney)
        {
            MoneyPayHandler?.Invoke(remainingMoney);
            remainingMoney = 0f;
            return remainingMoney;
        }

        private static bool CanRegularPay(float precalculatedPlayerMoneyAfterStep, float preCalculatedRemainingPayMoney)
        {
            return precalculatedPlayerMoneyAfterStep > 0 && preCalculatedRemainingPayMoney > 0;
        }

        private void OnNotEnoughMoney(ref float remainingMoney, float playerMoney)
        {
            remainingMoney -= playerMoney;
            _resourceTracker.ZeroMoney();
        }
    }
}
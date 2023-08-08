using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.WaitZones
{
    public class PayMoneyProcessor
    {
        public static Action<float> MoneyPayHandler;
        public bool ProcessPay(ref float remainingTime, ref float remainingMoney)
        {
            float playerMoney = ResourceTracker.Instance.PlayerMoney;
            float moneyStep = remainingMoney / remainingTime * Globals.WAIT_ZONES_TIME_STEP;

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

            remainingTime -= Globals.WAIT_ZONES_TIME_STEP;
            remainingMoney -= moneyStep;
            MoneyPayHandler?.Invoke(moneyStep);
            return true;
        }

        private void OnNotEnoughMoney(ref float remainingMoney, float playerMoney)
        {
            remainingMoney -= playerMoney;
            ResourceTracker.Instance.ZeroMoney();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.WaitZones
{
    public class PayMoneyProcessor : MonoBehaviour
    {
        public static Action<float> MoneyPayHandler;
        public bool ProcessPay(ref float remainingTime, ref float remainingMoney)
        {
            float playerMoney = ResourceTracker.Instance.PlayerMoney;
            float moneyStep = remainingMoney / remainingTime * Globals.WAIT_ZONES_TIME_STEP;

            float precalculatedPlayerMoneyAfterStep = playerMoney - moneyStep;
            float preCalculatedRemainingPayMoney = remainingMoney - moneyStep;
            remainingTime -= Globals.WAIT_ZONES_TIME_STEP;

            if (preCalculatedRemainingPayMoney <= 0f)
            {
                MoneyPayHandler?.Invoke(remainingMoney);
                return true;
            }
            if (precalculatedPlayerMoneyAfterStep < 0)
            {
                remainingMoney -= playerMoney;
                ResourceTracker.Instance.ZeroMoney();
                return false;
            }

            remainingMoney -= moneyStep;
            MoneyPayHandler?.Invoke(moneyStep);
            return true;
        }
    }
}
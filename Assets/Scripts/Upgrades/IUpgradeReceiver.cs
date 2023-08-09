using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.Upgrades
{
    public interface IUpgradeReceiver
    {
        public void ReceiveUpgradeCommand(UpgradeType upgradeType, int index);
    }
}
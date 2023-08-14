using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Upgrades
{
    public interface IUpgradeReceiver
    {
        public void ReceiveUpgradeCommand(UpgradeType upgradeType, int index);
    }
}

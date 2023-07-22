using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.Upgrades
{
    public class LoadUpgradeCommand : IUpgradeCommand
    {
        private Enums.UpgradeType _upgradeType;
        public LoadUpgradeCommand(Enums.UpgradeType upgradeType)
        {
            _upgradeType = upgradeType;
        }
        public void Execute()
        {
            int index = GetUpgradeIndex();

            UpdateGameState(index);
        }

        private int GetUpgradeIndex()
        {
            string upgradeKey = UpgradeUtility.Instance.GetTypeKey(_upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }

        private void UpdateGameState(int index)
        {
            UpgradeReceiver.Instance.ReceiveUpgradeCommand(_upgradeType, index);
            PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, index);
        }

    }

}
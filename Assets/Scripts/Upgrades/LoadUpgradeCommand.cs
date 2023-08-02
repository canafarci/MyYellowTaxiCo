using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.Upgrades
{
    public class LoadUpgradeCommand : IUpgradeCommand
    {
        private UpgradeUtility _upgradeUtility;
        private UpgradeReceiver _upgradeReceiver;
        public LoadUpgradeCommand(UpgradeUtility upgradeUtility, UpgradeReceiver upgradeReceiver)
        {
            _upgradeUtility = upgradeUtility;
            _upgradeReceiver = upgradeReceiver;
        }
        private Enums.UpgradeType[] _upgradeTypes = new Enums.UpgradeType[] {
            Enums.UpgradeType.PlayerSpeed,
            Enums.UpgradeType.HatStackerSpeed,
            Enums.UpgradeType.HelperNPCCount,
            Enums.UpgradeType.HelperNPCInventorySize,
            Enums.UpgradeType.PlayerIncome,
            Enums.UpgradeType.PlayerInventorySize,
        };
        public void Execute()
        {
            foreach (Enums.UpgradeType upgradeType in _upgradeTypes)
            {
                LoadUpgrade(upgradeType);
            }
        }

        private void LoadUpgrade(Enums.UpgradeType upgradeType)
        {
            int index = GetUpgradeIndex(upgradeType);

            UpdateGameState(index, upgradeType);
        }

        private int GetUpgradeIndex(Enums.UpgradeType upgradeType)
        {
            string upgradeKey = _upgradeUtility.GetTypeKey(upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }

        private void UpdateGameState(int index, Enums.UpgradeType upgradeType)
        {
            _upgradeReceiver.ReceiveUpgradeCommand(upgradeType, index);
            PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, index);
        }

    }

}
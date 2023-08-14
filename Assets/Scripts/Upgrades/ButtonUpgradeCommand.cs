using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class ButtonUpgradeCommand : IUpgradeCommand
    {
        private UpgradeCardVisual _upgradeVisual;
        private UpgradeType _upgradeType;
        private UpgradeUtility _upgradeUtility;
        private IUpgradeReceiver _upgradeReceiver;
        public ButtonUpgradeCommand(UpgradeCardVisual upgradeVisual,
                                    UpgradeType upgradeType,
                                    UpgradeUtility upgradeUtility,
                                    IUpgradeReceiver upgradeReceiver)
        {
            _upgradeVisual = upgradeVisual;
            _upgradeType = upgradeType;
            _upgradeUtility = upgradeUtility;
            _upgradeReceiver = upgradeReceiver;
        }
        public void Execute()
        {
            int index = GetUpgradeIndex();

            PayMoney(index);
            index++;

            UpdateGameState(index);
        }

        private int GetUpgradeIndex()
        {
            string upgradeKey = _upgradeUtility.GetTypeKey(_upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }

        private void UpdateGameState(int index)
        {
            _upgradeReceiver.ReceiveUpgradeCommand(_upgradeType, index);
            string upgradeKey = _upgradeUtility.GetTypeKey(_upgradeType);
            _upgradeVisual.UpdateDotUI(index);
            PlayerPrefs.SetInt(upgradeKey, index);
        }

        private void PayMoney(int index)
        {
            float cost = _upgradeUtility.GetUpgradeCost(index, _upgradeType);
            ResourceTracker.Instance.OnPayMoney(cost);
        }
    }
}

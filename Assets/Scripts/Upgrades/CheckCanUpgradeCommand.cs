using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.Upgrades
{
    public class CheckCanUpgradeCommand : IUpgradeCommand
    {
        private UpgradeCardVisual _upgradeVisual;
        private UpgradeType _upgradeType;
        private UpgradeUtility _upgradeUtility;
        public CheckCanUpgradeCommand(UpgradeCardVisual upgradeVisual,
                                      UpgradeType upgradeType,
                                      UpgradeUtility upgradeUtility)
        {
            _upgradeVisual = upgradeVisual;
            _upgradeType = upgradeType;
            _upgradeUtility = upgradeUtility;
        }

        public void Execute()
        {
            string upgradeKey = _upgradeUtility.GetTypeKey(_upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            bool isAtMaxIndex = _upgradeUtility.IsIndexAtMaxLength(index, _upgradeType);

            if (isAtMaxIndex)
            {
                UpdateVisualForAtMaxUpgrade();
            }
            else
            {
                UpdateVisualByUpgradeIndex(index);
            }
            _upgradeVisual.UpdateDotUI(index);
        }

        private void UpdateVisualByUpgradeIndex(int index)
        {
            float cost = _upgradeUtility.GetUpgradeCost(index, _upgradeType);
            float playerMoney = ResourceTracker.Instance.PlayerMoney;

            if (cost > playerMoney)
            {
                _upgradeVisual.SetButtonInteractable(false);
            }
            //player can buy the upgrade
            else
            {
                _upgradeVisual.SetButtonInteractable(true);
            }
            _upgradeVisual.SetCostText(cost);
        }

        private void UpdateVisualForAtMaxUpgrade()
        {
            _upgradeVisual.SetCostTextToMax();
            _upgradeVisual.SetButtonInteractable(false);
        }
    }
}
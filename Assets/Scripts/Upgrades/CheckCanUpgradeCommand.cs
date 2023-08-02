using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Taxi.Upgrades
{
    public class CheckCanUpgradeCommand : IUpgradeCommand
    {
        private UpgradeCardVisual _upgradeVisual;
        private Enums.UpgradeType _upgradeType;
        public CheckCanUpgradeCommand(UpgradeCardVisual upgradeVisual, Enums.UpgradeType upgradeType)
        {
            _upgradeVisual = upgradeVisual;
            _upgradeType = upgradeType;
        }

        public void Execute()
        {
            UnityEngine.Debug.Log("asdasdasdsad");
            string upgradeKey = UpgradeUtility.Instance.GetTypeKey(_upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            bool isAtMaxIndex = UpgradeUtility.Instance.IsIndexAtMaxLength(index, _upgradeType);

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
            float cost = UpgradeUtility.Instance.GetUpgradeCost(index, _upgradeType);
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
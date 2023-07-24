using System;
using Taxi.UI;
using Taxi.WaitZones;
using UnityEngine;

namespace Taxi.Upgrades
{
    class WaitZoneUpgradeCommand : IUpgradeCommand
    {
        private RepeatableBuyingWaitingZone _waitZone;
        private RepeatableBuyableWaitingZoneVisual _visual;
        private bool _isLoading;

        public WaitZoneUpgradeCommand(RepeatableBuyingWaitingZone waitZone, RepeatableBuyableWaitingZoneVisual visual, bool isLoading = false)
        {
            _waitZone = waitZone;
            _visual = visual;
            _isLoading = isLoading;
        }
        public void Execute()
        {
            int index = GetUpgradeIndex();
            if (!_isLoading)
            {
                index++;
            }

            bool isAtMaxIndex = UpgradeUtility.Instance.IsIndexAtMaxLength(index, Enums.UpgradeType.HatStackerSpeed);

            UpdateGameState(index);

            if (isAtMaxIndex)
            {
                _visual.gameObject.SetActive(false);
            }
            else
            {
                float cost = UpgradeUtility.Instance.GetUpgradeCost(index, Enums.UpgradeType.HatStackerSpeed);
                UpdateVisual(index, cost);
                _waitZone.SetPrice(cost);
            }
        }

        private void UpdateVisual(int index, float cost)
        {
            _visual.SetLevelText(index);
            _visual.Initialize(cost);
        }

        private void UpdateGameState(int index)
        {
            string upgradeKey = UpgradeUtility.Instance.GetTypeKey(Enums.UpgradeType.HatStackerSpeed);
            PlayerPrefs.SetInt(upgradeKey, index);
            UpgradeReceiver.Instance.ReceiveUpgradeCommand(Enums.UpgradeType.HatStackerSpeed, index);
        }

        private int GetUpgradeIndex()
        {
            string upgradeKey = UpgradeUtility.Instance.GetTypeKey(Enums.UpgradeType.HatStackerSpeed);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }
    }
}
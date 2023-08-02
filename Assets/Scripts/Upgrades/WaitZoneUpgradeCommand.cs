using System;
using Taxi.UI;
using Taxi.WaitZones;
using UnityEngine;

namespace Taxi.Upgrades
{
    public class WaitZoneUpgradeCommand : IUpgradeCommand
    {
        private RepeatableBuyingWaitingZone _waitZone;
        private RepeatableBuyableWaitingZoneVisual _visual;
        private UpgradeUtility _upgradeUtility;
        private UpgradeReceiver _upgradeReceiver;
        private bool _isLoading;

        public WaitZoneUpgradeCommand(RepeatableBuyingWaitingZone waitZone,
                                    RepeatableBuyableWaitingZoneVisual visual,
                                    UpgradeUtility upgradeUtility,
                                    UpgradeReceiver upgradeReceiver,
                                    bool isLoading = false)
        {
            _waitZone = waitZone;
            _visual = visual;
            _isLoading = isLoading;
            _upgradeUtility = upgradeUtility;
            _upgradeReceiver = upgradeReceiver;
        }
        public void Execute()
        {
            int index = GetUpgradeIndex();
            if (!_isLoading)
            {
                index++;
            }

            bool isAtMaxIndex = _upgradeUtility.IsIndexAtMaxLength(index, Enums.UpgradeType.HatStackerSpeed);

            UpdateGameState(index);

            if (isAtMaxIndex)
            {
                _visual.gameObject.SetActive(false);
            }
            else
            {
                float cost = _upgradeUtility.GetUpgradeCost(index, Enums.UpgradeType.HatStackerSpeed);
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
            string upgradeKey = _upgradeUtility.GetTypeKey(Enums.UpgradeType.HatStackerSpeed);
            PlayerPrefs.SetInt(upgradeKey, index);
            _upgradeReceiver.ReceiveUpgradeCommand(Enums.UpgradeType.HatStackerSpeed, index);
        }

        private int GetUpgradeIndex()
        {
            string upgradeKey = _upgradeUtility.GetTypeKey(Enums.UpgradeType.HatStackerSpeed);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }
    }
}
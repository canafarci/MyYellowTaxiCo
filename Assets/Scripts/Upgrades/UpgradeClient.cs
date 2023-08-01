using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.UI;
using Taxi.WaitZones;
using UnityEngine;
namespace Taxi.Upgrades
{
    public class UpgradeClient : MonoBehaviour
    {
        public static UpgradeClient Instance;
        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
            InitializeUpgradeCards();
            InitializeWaitZoneUpgrade();
        }
        public IUpgradeCommand GetLoadUpgradeCommand(Enums.UpgradeType upgradeType)
        {
            if (upgradeType == Enums.UpgradeType.HatStackerSpeed)
            {
                IUpgradeCommand command = CreateWaitZoneCommand(true);
                return command;
            }
            else
            {
                return new LoadUpgradeCommand(upgradeType);
            }
        }

        private void InitializeWaitZoneUpgrade()
        {
            RepeatableBuyableWaitingZoneTrigger trigger = FindObjectOfType<RepeatableBuyableWaitingZoneTrigger>(true);
            IUpgradeCommand command = CreateWaitZoneCommand(false);
            trigger.SetUpgradeCommand(command);
        }

        private void InitializeUpgradeCards()
        {
            UpgradeCardButton[] upgrades = FindObjectsOfType<UpgradeCardButton>(true);

            foreach (UpgradeCardButton upgrade in upgrades)
            {
                UpgradeCardVisual visual = upgrade.GetComponent<UpgradeCardVisual>();

                SetUpCheckCommand(upgrade, visual);
            }
        }

        private void SetUpCheckCommand(UpgradeCardButton upgrade, UpgradeCardVisual visual)
        {
            IUpgradeCommand checkCommand = GetCheckCommand(upgrade.GetUpgradeType(), visual);
            upgrade.SetCheckCanUpgradeCommand(checkCommand);
        }


        private IUpgradeCommand GetUpgradeCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
        {
            return new ButtonUpgradeCommand(visual, upgradeType);
        }
        private IUpgradeCommand GetCheckCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
        {
            return new CheckCanUpgradeCommand(visual, upgradeType);
        }

        private IUpgradeCommand CreateWaitZoneCommand(bool isLoading)
        {
            RepeatableBuyingWaitingZone zone = FindObjectOfType<RepeatableBuyingWaitingZone>(true);
            RepeatableBuyableWaitingZoneVisual visual = zone.GetComponent<RepeatableBuyableWaitingZoneVisual>();
            IUpgradeCommand command = new WaitZoneUpgradeCommand(zone, visual, isLoading);
            return command;
        }
    }
}
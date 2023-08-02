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
            InitializeWaitZoneUpgrade();
        }
        public IUpgradeCommand GetLoadUpgradeCommand(Enums.UpgradeType upgradeType)
        {
            IUpgradeCommand command = CreateWaitZoneCommand(true);
            return command;
        }

        private void InitializeWaitZoneUpgrade()
        {
            RepeatableBuyableWaitingZoneTrigger trigger = FindObjectOfType<RepeatableBuyableWaitingZoneTrigger>(true);
            IUpgradeCommand command = CreateWaitZoneCommand(false);
            trigger.SetUpgradeCommand(command);
        }
        private IUpgradeCommand CreateWaitZoneCommand(bool isLoading)
        {
            RepeatableBuyingWaitingZone zone = FindObjectOfType<RepeatableBuyingWaitingZone>(true);
            RepeatableBuyableWaitingZoneVisual visual = zone.GetComponent<RepeatableBuyableWaitingZoneVisual>();
            IUpgradeCommand command = new WaitZoneUpgradeCommand(zone, visual, null, null, isLoading);
            return command;
        }
    }
}
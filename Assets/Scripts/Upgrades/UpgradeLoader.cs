using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.UI;
using Taxi.WaitZones;
using UnityEngine;

namespace Taxi.Upgrades
{
    public class UpgradeLoader : MonoBehaviour
    {
        private void Start() => Load();
        private void Load()
        {
            LoadUpgrade(Enums.UpgradeType.PlayerSpeed);
            LoadUpgrade(Enums.UpgradeType.PlayerIncome);
            LoadUpgrade(Enums.UpgradeType.PlayerInventorySize);
            LoadUpgrade(Enums.UpgradeType.HelperNPCCount);
            LoadUpgrade(Enums.UpgradeType.HelperNPCInventorySize);
            LoadUpgrade(Enums.UpgradeType.HelperNPCSpeed);
            LoadUpgrade(Enums.UpgradeType.HatStackerSpeed);
        }
        private void LoadUpgrade(Enums.UpgradeType type)
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(type);
            command.Execute();
        }
    }
}
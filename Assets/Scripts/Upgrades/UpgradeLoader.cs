using System;
using System.Collections;
using System.Collections.Generic;
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
        }

        private void LoadUpgrade(Enums.UpgradeType type)
        {
            string upgradeKey = UpgradeUtility.Instance.GetTypeKey(type);
            int index = GetIndex(upgradeKey);

            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(type);
            command.Execute();
        }

        private int GetIndex(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
                return 0;
            }
        }
    }
}
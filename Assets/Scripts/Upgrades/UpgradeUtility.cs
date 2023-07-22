using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.Upgrades
{
    public class UpgradeUtility : MonoBehaviour
    {
        public static UpgradeUtility Instance;
        [SerializeField] private UpgradeDataSO _upgradeData;
        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public string GetTypeKey(Enums.UpgradeType type)
        {
            switch (type)
            {
                case (Enums.UpgradeType.PlayerSpeed):
                    return Globals.PLAYER_SPEED_KEY;
                case (Enums.UpgradeType.PlayerIncome):
                    return Globals.PLAYER_INCOME_KEY;
                case (Enums.UpgradeType.PlayerInventorySize):
                    return Globals.PLAYER_INVENTORY_KEY;
                case (Enums.UpgradeType.HelperNPCCount):
                    return Globals.NPC_COUNT_KEY;
                case (Enums.UpgradeType.HelperNPCInventorySize):
                    return Globals.NPC_INVENTORY_KEY;
                case (Enums.UpgradeType.HelperNPCSpeed):
                    return Globals.NPC_SPEED_KEY;
                default:
                    return null;
            }
        }

        public float GetUpgradeCost(int index, Enums.UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case (Enums.UpgradeType.PlayerSpeed):
                    return _upgradeData.SpeedModifiers[index + 1].Cost;
                case (Enums.UpgradeType.PlayerIncome):
                    return _upgradeData.IncomeModifiers[index + 1].Cost;
                case (Enums.UpgradeType.PlayerInventorySize):
                    return _upgradeData.PlayerInventorySizes[index + 1].Cost;
                case (Enums.UpgradeType.HelperNPCCount):
                    return _upgradeData.HelperNPCCount[index + 1].Cost;
                case (Enums.UpgradeType.HelperNPCInventorySize):
                    return _upgradeData.HelperNPCInventorySizes[index + 1].Cost;
                case (Enums.UpgradeType.HelperNPCSpeed):
                    return _upgradeData.HelperNPCSpeeds[index + 1].Cost;
                default:
                    return 0f;
            }
        }

        public bool IsIndexAtMaxLength(int index, Enums.UpgradeType upgradeType)
        {
            bool isAtMax;

            if (upgradeType == Enums.UpgradeType.HelperNPCCount)
                isAtMax = index >= _upgradeData.HelperNPCCount.Length - 1;
            else
            {
                //return  true if index is at the end of upgrades, reference list is arbitary, all of them has the same length
                isAtMax = index >= _upgradeData.SpeedModifiers.Length - 1;
            }

            return isAtMax;
        }

    }

}
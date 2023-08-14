using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class UpgradeUtility
    {
        private UpgradeDataSO _upgradeData;

        [Inject]
        private void Init(UpgradeDataSO upgradeData)
        {
            _upgradeData = upgradeData;
        }
        public string GetTypeKey(UpgradeType type)
        {
            switch (type)
            {
                case (UpgradeType.PlayerSpeed):
                    return Globals.PLAYER_SPEED_KEY;
                case (UpgradeType.PlayerIncome):
                    return Globals.PLAYER_INCOME_KEY;
                case (UpgradeType.PlayerInventorySize):
                    return Globals.PLAYER_INVENTORY_KEY;
                case (UpgradeType.HelperNPCCount):
                    return Globals.NPC_COUNT_KEY;
                case (UpgradeType.HelperNPCInventorySize):
                    return Globals.NPC_INVENTORY_KEY;
                case (UpgradeType.HelperNPCSpeed):
                    return Globals.NPC_SPEED_KEY;
                case (UpgradeType.HatStackerSpeed):
                    return Globals.STACKER_UPGRADE_KEY;
                default:
                    return null;
            }
        }

        public float GetUpgradeCost(int index, UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case (UpgradeType.PlayerSpeed):
                    return _upgradeData.SpeedModifiers[index + 1].Cost;
                case (UpgradeType.PlayerIncome):
                    return _upgradeData.IncomeModifiers[index + 1].Cost;
                case (UpgradeType.PlayerInventorySize):
                    return _upgradeData.PlayerInventorySizes[index + 1].Cost;
                case (UpgradeType.HelperNPCCount):
                    return _upgradeData.HelperNPCCount[index + 1].Cost;
                case (UpgradeType.HelperNPCInventorySize):
                    return _upgradeData.HelperNPCInventorySizes[index + 1].Cost;
                case (UpgradeType.HelperNPCSpeed):
                    return _upgradeData.HelperNPCSpeeds[index + 1].Cost;
                case (UpgradeType.HatStackerSpeed):
                    return _upgradeData.StackSpeeds[index + 1].Cost;
                default:
                    return 0f;
            }
        }

        public bool IsIndexAtMaxLength(int index, UpgradeType upgradeType)
        {
            bool isAtMax;

            if (upgradeType == UpgradeType.HelperNPCCount)
            {
                isAtMax = index >= _upgradeData.HelperNPCCount.Length - 1;
            }
            else if (upgradeType == UpgradeType.HatStackerSpeed)
            {
                isAtMax = index >= _upgradeData.StackSpeeds.Length - 1;
            }
            else
            {
                //return  true if index is at the end of upgrades, reference list is arbitary, all of them has the same length
                isAtMax = index >= _upgradeData.SpeedModifiers.Length - 1;
            }

            return isAtMax;
        }
        public float GetItemGeneratorSpeed(int index)
        {
            return _upgradeData.StackSpeeds[index].SpawnRate;
        }

#if UNITY_INCLUDE_TESTS
        public void SetUpgradeData(UpgradeDataSO so) => _upgradeData = so;
#endif

    }

}
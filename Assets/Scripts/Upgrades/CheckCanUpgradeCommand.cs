using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private Enums.UpgradeType _upgradeType;
    public CheckCanUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, Enums.UpgradeType upgradeType)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _upgradeType = upgradeType;
    }

    public void Execute()
    {
        int index = PlayerPrefs.GetInt(GetTypeKey(_upgradeType));
        bool isAtMaxIndex = IsIndexAtMaxLength(index);

        if (isAtMaxIndex)
        {
            _upgradeVisual.SetCostTextToMax();
            _upgradeVisual.SetButtonActive(false);
        }
        else
        {
            float cost = GetCost(index);
            float playerMoney = GameManager.Instance.Resources.PlayerMoney;

            if (cost > playerMoney)
            {
                _upgradeVisual.SetButtonActive(false);
            }
            //player can buy the upgrade
            else
            {
                _upgradeVisual.SetButtonActive(true);
            }

            _upgradeVisual.SetCostText(cost);
        }
        _upgradeVisual.ActivateDots(index);
    }

    bool IsIndexAtMaxLength(int index)
    {
        bool isAtMax;

        if (_upgradeType == Enums.UpgradeType.HelperNPCCount)
            isAtMax = index >= _upgradeData.HelperNPCCount.Length - 1;
        else
        {
            //return  true if index is at the end of upgrades, reference list is arbitary, all of them has the same length
            isAtMax = index >= _upgradeData.SpeedModifiers.Length - 1;
        }

        return isAtMax;
    }
    private string GetTypeKey(Enums.UpgradeType type)
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
    private float GetCost(int index)
    {
        switch (_upgradeType)
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
}

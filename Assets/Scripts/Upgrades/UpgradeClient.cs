using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeClient : MonoBehaviour
{
    [SerializeField] private UpgradeData _upgradeData;
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

    public IUpgradeCommand GetUpgradeCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
    {
        switch (upgradeType)
        {
            case (Enums.UpgradeType.PlayerSpeed):
                return new PlayerSpeedUpgradeCommand(_upgradeData, visual);
            case (Enums.UpgradeType.PlayerIncome):
                return new PlayerIncomeUpgradeCommand(_upgradeData, visual);
            case (Enums.UpgradeType.PlayerInventorySize):
                return new PlayerInventoryUpgradeCommand(_upgradeData, visual);
            case (Enums.UpgradeType.HelperNPCCount):
                return new BuyNPCUpgradeCommand(_upgradeData, visual);
            case (Enums.UpgradeType.HelperNPCInventorySize):
                return new NPCInventorySizeUpgradeCommand(_upgradeData, visual);
            case (Enums.UpgradeType.HelperNPCSpeed):
                return new NPCSpeedUpgradeCommand(_upgradeData, visual);
            default:
                return null;
        }
    }
    public IUpgradeCommand GetLoadUpgradeCommand(Enums.UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case (Enums.UpgradeType.PlayerSpeed):
                return new PlayerSpeedUpgradeCommand(_upgradeData, null, true);
            case (Enums.UpgradeType.PlayerIncome):
                return new PlayerIncomeUpgradeCommand(_upgradeData, null, true);
            case (Enums.UpgradeType.PlayerInventorySize):
                return new PlayerInventoryUpgradeCommand(_upgradeData, null, true);
            case (Enums.UpgradeType.HelperNPCCount):
                return new BuyNPCUpgradeCommand(_upgradeData, null, true);
            case (Enums.UpgradeType.HelperNPCInventorySize):
                return new NPCInventorySizeUpgradeCommand(_upgradeData, null, true);
            case (Enums.UpgradeType.HelperNPCSpeed):
                return new NPCSpeedUpgradeCommand(_upgradeData, null, true);
            default:
                return null;
        }
    }
    public IUpgradeCommand GetCheckCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
    {
        return new CheckCanUpgradeCommand(_upgradeData, visual, upgradeType);
    }

    private string GetTypeName(Enums.UpgradeType type)
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
}

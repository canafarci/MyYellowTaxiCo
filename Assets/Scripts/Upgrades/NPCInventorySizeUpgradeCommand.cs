using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventorySizeUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private bool _isLoading = false;
    public NPCInventorySizeUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, bool isLoading = false)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _isLoading = isLoading;
    }
    public void Execute()
    {
        int index = PlayerPrefs.GetInt(Globals.NPC_INVENTORY_KEY);

        if (!_isLoading)
            index++;

        //npc state change
        Upgrades.Instance.NPCInventorySizeUpgrade(index);

        //update resources
        if (!_isLoading)
        {
            float cost = _upgradeData.HelperNPCInventorySizes[index].Cost;
            GameManager.Instance.Resources.OnPayMoney(cost);
        }
        //update visuals
        _upgradeVisual?.ActivateDots(index);

        //save progression
        PlayerPrefs.SetInt(Globals.NPC_INVENTORY_KEY, index);
        //TODO PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
}

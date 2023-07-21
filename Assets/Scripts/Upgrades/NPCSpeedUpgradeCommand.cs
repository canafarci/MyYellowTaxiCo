using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeedUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private bool _isLoading = false;
    public NPCSpeedUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, bool isLoading = false)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _isLoading = isLoading;
    }
    public void Execute()
    {
        int index = PlayerPrefs.GetInt(Globals.NPC_SPEED_KEY);

        if (!_isLoading)
            index++;

        //game state change
        Upgrades.Instance.NPCSpeedUpgrade(index);

        if (!_isLoading)
        {
            //update resources
            float cost = _upgradeData.HelperNPCSpeeds[index].Cost;
            GameManager.Instance.Resources.OnPayMoney(cost);
        }

        //update visuals
        _upgradeVisual?.ActivateDots(index);

        //save progression
        PlayerPrefs.SetInt(Globals.NPC_SPEED_KEY, index);
        //TODO PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
}

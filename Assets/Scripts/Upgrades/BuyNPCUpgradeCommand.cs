using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNPCUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private bool _isLoading = false;
    public BuyNPCUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, bool isLoading = false)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _isLoading = isLoading;
    }
    public void Execute()
    {
        int index = PlayerPrefs.GetInt(Globals.NPC_COUNT_KEY);

        if (!_isLoading)
            index++;

        Console.WriteLine($"index at command: {index}");

        //player state change
        Upgrades.Instance.SpawnHelperNPC(index);

        //update resources
        if (!_isLoading)
        {
            float cost = _upgradeData.HelperNPCCount[index].Cost;
            GameManager.Instance.Resources.OnPayMoney(cost);
        }

        //update visuals
        _upgradeVisual?.ActivateDots(index);

        //save progression
        PlayerPrefs.SetInt(Globals.NPC_COUNT_KEY, index);
        //TODO PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
}

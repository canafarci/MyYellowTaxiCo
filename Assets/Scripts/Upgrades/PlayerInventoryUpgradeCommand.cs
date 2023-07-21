using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private bool _isLoading = false;
    public PlayerInventoryUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, bool isLoading = false)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _isLoading = isLoading;
    }
    public void Execute()
    {
        int index = PlayerPrefs.GetInt(Globals.PLAYER_INVENTORY_KEY);

        if (!_isLoading)
            index++;

        //player state change
        Inventory playerInventory = GameManager.Instance.References.PlayerInventory;
        playerInventory.MaxStackSize = _upgradeData.PlayerInventorySizes[index].Size;

        if (!_isLoading)
        {
            //update resources
            float cost = _upgradeData.PlayerInventorySizes[index].Cost;
            GameManager.Instance.Resources.OnPayMoney(cost);
        }
        //update visuals
        _upgradeVisual?.ActivateDots(index);

        //save progression
        PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, index);
        //TODO PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
}

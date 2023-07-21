using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSpeedUpgradeCommand : IUpgradeCommand
{
    private UpgradeData _upgradeData;
    private UpgradeCardVisual _upgradeVisual;
    private bool _isLoading = false;
    public PlayerSpeedUpgradeCommand(UpgradeData upgradeData, UpgradeCardVisual upgradeVisual, bool isLoading = false)
    {
        _upgradeData = upgradeData;
        _upgradeVisual = upgradeVisual;
        _isLoading = isLoading;
    }
    public void Execute()
    {
        int index = PlayerPrefs.GetInt(Globals.PLAYER_SPEED_KEY);

        if (!_isLoading)
            index++;

        //player state change
        Upgrades.Instance.UpgradePlayerSpeed(index);

        if (!_isLoading)
        {
            //update resources
            float cost = _upgradeData.SpeedModifiers[index].Cost;
            GameManager.Instance.Resources.OnPayMoney(cost);
        }
        //update visuals
        _upgradeVisual?.ActivateDots(index);

        //save progression
        PlayerPrefs.SetInt(Globals.PLAYER_SPEED_KEY, index);
        //TODO PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
}

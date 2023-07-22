using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using UnityEngine;

public class ButtonUpgradeCommand : IUpgradeCommand
{
    private UpgradeCardVisual _upgradeVisual;
    private Enums.UpgradeType _upgradeType;
    public ButtonUpgradeCommand(UpgradeCardVisual upgradeVisual, Enums.UpgradeType upgradeType)
    {
        _upgradeVisual = upgradeVisual;
        _upgradeType = upgradeType;
    }
    public void Execute()
    {
        int index = GetUpgradeIndex();

        PayMoney(index);
        index++;

        UpdateGameState(index);
    }

    private int GetUpgradeIndex()
    {
        string upgradeKey = UpgradeUtility.Instance.GetTypeKey(_upgradeType);
        int index = PlayerPrefs.GetInt(upgradeKey);
        return index;
    }

    private void UpdateGameState(int index)
    {
        UpgradeReceiver.Instance.ReceiveUpgradeCommand(_upgradeType, index);
        string upgradeKey = UpgradeUtility.Instance.GetTypeKey(_upgradeType);
        _upgradeVisual.UpdateDotUI(index);
        PlayerPrefs.SetInt(upgradeKey, index);
    }

    private void PayMoney(int index)
    {
        float cost = UpgradeUtility.Instance.GetUpgradeCost(index, _upgradeType);
        GameManager.Instance.Resources.OnPayMoney(cost);
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLoader : MonoBehaviour
{
    void Start()
    {
        Load();
    }
    void Load()
    {
        // TODO NULL OBJECT
        //IUpgradeCommand command;

        //player income
        if (PlayerPrefs.HasKey(Globals.PLAYER_INCOME_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.PlayerIncome);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.PLAYER_INCOME_KEY, 0);

        //player speed
        if (PlayerPrefs.HasKey(Globals.PLAYER_SPEED_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.PlayerSpeed);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.PLAYER_SPEED_KEY, 0);
        //player capacity
        if (PlayerPrefs.HasKey(Globals.PLAYER_INVENTORY_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.PlayerInventorySize);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, 0);
        //bot buy
        if (PlayerPrefs.HasKey(Globals.NPC_COUNT_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.HelperNPCCount);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.NPC_COUNT_KEY, 0);
        //bot speed
        if (PlayerPrefs.HasKey(Globals.NPC_SPEED_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.PlayerSpeed);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.NPC_SPEED_KEY, 0);
        //bot capacity
        if (PlayerPrefs.HasKey(Globals.NPC_INVENTORY_KEY))
        {
            IUpgradeCommand command = UpgradeClient.Instance.GetLoadUpgradeCommand(Enums.UpgradeType.HelperNPCInventorySize);
            UpgradeInvoker.Instance.InvokeUpgradeCommand(command);
        }
        else
            PlayerPrefs.SetInt(Globals.NPC_INVENTORY_KEY, 0);
    }
}

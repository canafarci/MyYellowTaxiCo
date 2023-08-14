using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class LoadUpgradeCommand : IUpgradeCommand
    {
        private UpgradeUtility _upgradeUtility;
        private IUpgradeReceiver _modifierUpgradeReceiver;
        private IUpgradeReceiver _npcSpawnReceiver;
        public LoadUpgradeCommand(UpgradeUtility upgradeUtility,
                                  [Inject(Id = UpgradeReceiverType.Modifier)] IUpgradeReceiver modifierUpgradeReceiver,
                                  [Inject(Id = UpgradeReceiverType.NPCSpawner)] IUpgradeReceiver npcSpawnReceiver)
        {
            _upgradeUtility = upgradeUtility;
            _modifierUpgradeReceiver = modifierUpgradeReceiver;
            _npcSpawnReceiver = npcSpawnReceiver;
        }

        private UpgradeType[] _modifierUpgrades = new UpgradeType[] {
            UpgradeType.PlayerSpeed,
            UpgradeType.HatStackerSpeed,
            UpgradeType.HelperNPCCount,
            UpgradeType.HelperNPCInventorySize,
            UpgradeType.PlayerIncome,
            UpgradeType.PlayerInventorySize,
        };
        public void Execute()
        {
            foreach (UpgradeType upgradeType in _modifierUpgrades)
            {
                LoadUpgrade(upgradeType);
            }
        }

        private void LoadUpgrade(UpgradeType upgradeType)
        {
            int index = GetUpgradeIndex(upgradeType);

            UpdateGameState(index, upgradeType);
        }
        private void UpdateGameState(int index, UpgradeType upgradeType)
        {
            if (upgradeType == UpgradeType.HelperNPCCount)
                _npcSpawnReceiver.ReceiveUpgradeCommand(upgradeType, index);
            else
                _modifierUpgradeReceiver.ReceiveUpgradeCommand(upgradeType, index);

            PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, index);
        }

        private int GetUpgradeIndex(UpgradeType upgradeType)
        {
            string upgradeKey = _upgradeUtility.GetTypeKey(upgradeType);
            int index = PlayerPrefs.GetInt(upgradeKey);
            return index;
        }


    }

}
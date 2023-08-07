using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.Upgrades
{
    public class ModifierUpgradesReceiver : MonoBehaviour, IUpgradeReceiver
    {
        [SerializeField] private UpgradeDataSO _upgradeData;

        private UpgradesFacade _upgradesFacade;
        private References _references;
        private Mover _mover;

        [Inject]
        private void Init(UpgradesFacade upgradesFacade, References references, Mover mover)
        {
            _upgradesFacade = upgradesFacade;
            _references = references;
            _mover = mover;
        }
        public void ReceiveUpgradeCommand(UpgradeType upgradeType, int index)
        {
            switch (upgradeType)
            {
                case (UpgradeType.PlayerSpeed):
                    UpgradePlayerSpeed(index);
                    break;
                case (UpgradeType.PlayerIncome):
                    UpgradePlayerIncome(index);
                    break;
                case (UpgradeType.PlayerInventorySize):
                    UpgradePlayerInventorySize(index);
                    break;
                case (UpgradeType.HelperNPCInventorySize):
                    UpgradeNPCInventorySize(index);
                    break;
                case (UpgradeType.HelperNPCSpeed):
                    UpgradeNPCSpeed(index);
                    break;

                default:
                    break;
            }
        }
        private void UpgradePlayerInventorySize(int index)
        {
            Inventory playerInventory = _references.PlayerInventory;
            playerInventory.MaxStackSize = _upgradeData.PlayerInventorySizes[index].Size;
        }
        private void UpgradePlayerIncome(int index)
        {
            float incomeModifier = _upgradeData.IncomeModifiers[index].IncomeMultiplier;
            _upgradesFacade.SetIncomeModifier(incomeModifier);
        }
        private void UpgradeNPCSpeed(int index)
        {
            float npcSpeed = _upgradeData.HelperNPCSpeeds[index].Speed;
            _upgradesFacade.SetNPCSpeed(npcSpeed);
        }
        private void UpgradeNPCInventorySize(int index)
        {
            int inventorySize = _upgradeData.HelperNPCInventorySizes[index].Size;
            _upgradesFacade.SetNPCInventorySize(inventorySize);
        }
        private void UpgradePlayerSpeed(int index)
        {
            float speedModifier = _upgradeData.SpeedModifiers[index].SpeedMultiplier;
            _mover.IncreaseSpeed(speedModifier);
        }

    }
}
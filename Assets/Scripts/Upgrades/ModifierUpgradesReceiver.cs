using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class ModifierUpgradesReceiver : MonoBehaviour, IUpgradeReceiver
    {
        [SerializeField] private UpgradeDataSO _upgradeData;
        private UpgradesFacade _upgradesFacade;
        private Inventory _playerInventory;

        public event Action<int> OnNPCInventorySizeUpgrade;
        public event Action<float> OnNPCSpeedUpgrade;
        public event Action<float> OnPlayerSpeedUpgrade;

        [Inject]
        private void Init(UpgradesFacade upgradesFacade, [Inject(Id = "PlayerInventory")] Inventory playerInventory)
        {
            _upgradesFacade = upgradesFacade;
            _playerInventory = playerInventory;
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
            int MaxStackSize = _upgradeData.PlayerInventorySizes[index].Size;
            _playerInventory.SetMaxStackCapacity(MaxStackSize);
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
            OnNPCSpeedUpgrade?.Invoke(npcSpeed);
        }
        private void UpgradeNPCInventorySize(int index)
        {
            int inventorySize = _upgradeData.HelperNPCInventorySizes[index].Size;
            _upgradesFacade.SetNPCInventorySize(inventorySize);
            OnNPCInventorySizeUpgrade?.Invoke(inventorySize);

        }
        private void UpgradePlayerSpeed(int index)
        {
            float speedModifier = _upgradeData.SpeedModifiers[index].SpeedMultiplier;
            float speed = _upgradeData.BaseMoveSpeed * speedModifier;
            OnPlayerSpeedUpgrade?.Invoke(speed);
        }

    }
}
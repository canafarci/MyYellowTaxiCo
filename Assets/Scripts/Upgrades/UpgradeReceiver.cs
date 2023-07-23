using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.Upgrades
{
    public class UpgradeReceiver : MonoBehaviour
    {
        [SerializeField] private UpgradeDataSO _upgradeData;
        [SerializeField] GameObject _helperNPCPrefab;
        [SerializeField] Transform _spawnPoint;
        private int _npcMaxCount, _currentNPCCount;
        int _npcInventorySizeIndex;
        public static UpgradeReceiver Instance;
        public event Action<int> OnNPCInventorySizeUpgrade;
        public event Action<float> OnNPCSpeedUpgrade;
        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void ReceiveUpgradeCommand(Enums.UpgradeType upgradeType, int index)
        {
            switch (upgradeType)
            {
                case (Enums.UpgradeType.PlayerSpeed):
                    UpgradePlayerSpeed(index);
                    break;
                case (Enums.UpgradeType.PlayerIncome):
                    UpgradePlayerIncome(index);
                    break;
                case (Enums.UpgradeType.PlayerInventorySize):
                    UpgradePlayerInventorySize(index);
                    break;
                case (Enums.UpgradeType.HelperNPCCount):
                    SpawnHelperNPC(index);
                    break;
                case (Enums.UpgradeType.HelperNPCInventorySize):
                    NPCInventorySizeUpgrade(index);
                    break;
                case (Enums.UpgradeType.HelperNPCSpeed):
                    NPCSpeedUpgrade(index);
                    break;
                default:
                    break;
            }
        }

        private void UpgradePlayerInventorySize(int index)
        {
            Inventory playerInventory = GameManager.Instance.References.PlayerInventory;
            playerInventory.MaxStackSize = _upgradeData.PlayerInventorySizes[index].Size;
        }

        private void UpgradePlayerIncome(int index)
        {
            float incomeModifier = _upgradeData.IncomeModifiers[index].IncomeMultiplier;
            UpgradesFacade.Instance.SetIncomeModifier(incomeModifier);
        }
        private void NPCSpeedUpgrade(int index)
        {
            float npcSpeed = _upgradeData.HelperNPCSpeeds[index].Speed;
            UpgradesFacade.Instance.SetNPCSpeed(npcSpeed);
            OnNPCSpeedUpgrade?.Invoke(npcSpeed);
        }
        private void NPCInventorySizeUpgrade(int index)
        {
            int inventorySize = _upgradeData.HelperNPCInventorySizes[index].Size;
            UpgradesFacade.Instance.SetNPCInventorySize(inventorySize);
            OnNPCInventorySizeUpgrade?.Invoke(inventorySize);
        }

        private void UpgradePlayerSpeed(int index)
        {
            float speedModifier = _upgradeData.SpeedModifiers[index].SpeedMultiplier;
            FindObjectOfType<Mover>().IncreaseSpeed(speedModifier);
        }
        private void SpawnHelperNPC(int index)
        {
            _npcMaxCount = index;

            StartCoroutine(SpawnNPCs());
        }
        private IEnumerator SpawnNPCs()
        {
            while (_currentNPCCount < _npcMaxCount)
            {
                Instantiate(_helperNPCPrefab, _spawnPoint.position, Quaternion.identity);
                _currentNPCCount++;
                yield return new WaitForSeconds(1f);
            }
        }
        public void SetUpgradeDataSO(UpgradeDataSO dataSO)
        {

        }
    }
}
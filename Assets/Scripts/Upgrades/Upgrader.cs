using System;
using System.Collections;
using Ketchapp.MayoSDK;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public float IncomeModifier { get { return _upgrades.IncomeModifiers[_incomeModifierIndex].IncomeMultiplier; } }
    public float SpeedModifier { get { return _upgrades.SpeedModifiers[_playerSpeedIndex].SpeedMultiplier; } }
    public float NPCSpeed { get { return _upgrades.HelperNPCSpeeds[_npcSpeedIndex].Speed; } }
    public int NPCInventorySize { get { return _upgrades.HelperNPCInventorySizes[_npcInventorySizeIndex].Size; } }
    [SerializeField] UpgradeData _upgrades;
    [SerializeField] GameObject _helperNPCPrefab;
    [SerializeField] Transform _spawnPoint;
    int _incomeModifierIndex, _playerSpeedIndex, _playerInventorySizeIndex;
    int _npcInventorySizeIndex, _npcSpeedIndex;
    int _npcMaxCount, _currentNPCCount;
    public event Action<int> OnNPCInventorySizeUpgrade;
    public event Action<float> OnNPCSpeedUpgrade;
    private void Start()
    {
        Load();
        HatHelperNPC._hatStackers = null;
    }
    public void Upgrade(Enums.UpgradeType upgradeType, int index)
    {
        if (upgradeType == Enums.UpgradeType.PlayerIncome)
            IncreaseIncome(index, true);
        else if (upgradeType == Enums.UpgradeType.PlayerSpeed)
            IncreaseSpeed(index, true);
        else if (upgradeType == Enums.UpgradeType.PlayerInventorySize)
            IncreasePlayerInventorySize(index, true);
        else if (upgradeType == Enums.UpgradeType.HelperNPCCount)
            SpawnHelperNPC(index, true);
        else if (upgradeType == Enums.UpgradeType.HelperNPCInventorySize)
            IncreaseNPCInventorySize(index, true);
        else if (upgradeType == Enums.UpgradeType.HelperNPCSpeed)
            IncreaseNPCSpeed(index, true);
    }
    void IncreaseIncome(int index, bool saveChanges = false)
    {
        if (index < _upgrades.IncomeModifiers.Length)
            _incomeModifierIndex = index;

        if (!saveChanges) return;
        Save(Globals.PLAYER_INCOME_KEY, _incomeModifierIndex);
        PostProgression(Globals.PLAYER_INCOME_KEY, _incomeModifierIndex);
    }
    void IncreaseSpeed(int index, bool saveChanges = false)
    {
        if (index < _upgrades.SpeedModifiers.Length)
            _playerSpeedIndex = index;

        FindObjectOfType<Mover>().IncreaseSpeed(SpeedModifier);

        if (!saveChanges) return;
        Save(Globals.PLAYER_SPEED_KEY, _playerSpeedIndex);
        PostProgression(Globals.PLAYER_SPEED_KEY, _playerSpeedIndex);
    }

    private void IncreasePlayerInventorySize(int index, bool saveChanges = false)
    {
        if (index >= _upgrades.PlayerInventorySizes.Length) return;
        GameManager.Instance.References.PlayerInventory.MaxStackSize = _upgrades.PlayerInventorySizes[index].Size;

        if (!saveChanges) return;
        Save(Globals.PLAYER_INVENTORY_KEY, index);
        PostProgression(Globals.PLAYER_INVENTORY_KEY, index);
    }
    private void IncreaseNPCInventorySize(int index, bool saveChanges = false)
    {
        if (index >= _upgrades.HelperNPCInventorySizes.Length) return;

        _npcInventorySizeIndex = index;
        OnNPCInventorySizeUpgrade?.Invoke(NPCInventorySize);

        if (!saveChanges) return;
        Save(Globals.NPC_INVENTORY_KEY, index);
        PostProgression(Globals.NPC_INVENTORY_KEY, index);
    }

    private void IncreaseNPCSpeed(int index, bool saveChanges = false)
    {
        if (index >= _upgrades.HelperNPCSpeeds.Length) return;
        _npcSpeedIndex = index;
        OnNPCSpeedUpgrade?.Invoke(NPCSpeed);

        if (!saveChanges) return;
        Save(Globals.NPC_SPEED_KEY, index);
        PostProgression(Globals.NPC_SPEED_KEY, index);
    }

    void SpawnHelperNPC(int count, bool saveChanges = false)
    {
        if (count < _upgrades.HelperNPCCount.Length)
            _npcMaxCount = count;

        StartCoroutine(SpawnNPCs());

        if (!saveChanges) return;
        Save(Globals.NPC_COUNT_KEY, _npcMaxCount);
        PostProgression(Globals.NPC_COUNT_KEY, _npcMaxCount);
    }

    IEnumerator SpawnNPCs()
    {
        while (_currentNPCCount < _npcMaxCount)
        {
            Instantiate(_helperNPCPrefab, _spawnPoint.position, Quaternion.identity);
            _currentNPCCount++;
            yield return new WaitForSeconds(1f);
        }
    }

    void Save(string identifier, int value)
    {
        PlayerPrefs.SetInt(identifier, value);
    }

    void Load()
    {
        //player income
        if (PlayerPrefs.HasKey(Globals.PLAYER_INCOME_KEY))
            IncreaseIncome(PlayerPrefs.GetInt(Globals.PLAYER_INCOME_KEY));
        else
            PlayerPrefs.SetInt(Globals.PLAYER_INCOME_KEY, 0);
        //player speed
        if (PlayerPrefs.HasKey(Globals.PLAYER_SPEED_KEY))
            IncreaseSpeed(PlayerPrefs.GetInt(Globals.PLAYER_SPEED_KEY));
        else
            PlayerPrefs.SetInt(Globals.PLAYER_SPEED_KEY, 0);
        //player capacity
        if (PlayerPrefs.HasKey(Globals.PLAYER_INVENTORY_KEY))
            IncreasePlayerInventorySize(PlayerPrefs.GetInt(Globals.PLAYER_INVENTORY_KEY));
        else
            PlayerPrefs.SetInt(Globals.PLAYER_INVENTORY_KEY, 0);
        //bot buy
        if (PlayerPrefs.HasKey(Globals.NPC_COUNT_KEY))
            SpawnHelperNPC(PlayerPrefs.GetInt(Globals.NPC_COUNT_KEY));
        else
            PlayerPrefs.SetInt(Globals.NPC_COUNT_KEY, 0);
        //bot speed
        if (PlayerPrefs.HasKey(Globals.NPC_SPEED_KEY))
            IncreaseNPCSpeed(PlayerPrefs.GetInt(Globals.NPC_SPEED_KEY));
        else
            PlayerPrefs.SetInt(Globals.NPC_SPEED_KEY, 0);
        //bot capacity
        if (PlayerPrefs.HasKey(Globals.NPC_INVENTORY_KEY))
            IncreaseNPCInventorySize(PlayerPrefs.GetInt(Globals.NPC_INVENTORY_KEY));
        else
            PlayerPrefs.SetInt(Globals.NPC_INVENTORY_KEY, 0);
    }

    void PostProgression(string identifier, int upgradeLevel)
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("UpgradeName", identifier);
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent("UpgradeBought", data);
    }
}
using System;
using System.Collections;
using Ketchapp.MayoSDK;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public float IncomeModifier { get { return _upgrades.IncomeModifiers[_incomeModifierIndex].IncomeMultiplier; } }
    public float NPCSpeed { get { return _upgrades.HelperNPCSpeeds[_npcSpeedIndex].Speed; } }
    public int NPCInventorySize { get { return _upgrades.HelperNPCInventorySizes[_npcInventorySizeIndex].Size; } }
    [SerializeField] UpgradeData _upgrades;
    [SerializeField] GameObject _helperNPCPrefab;
    [SerializeField] Transform _spawnPoint;
    int _incomeModifierIndex;
    int _npcInventorySizeIndex, _npcSpeedIndex;
    int _npcMaxCount, _currentNPCCount;
    public event Action<int> OnNPCInventorySizeUpgrade;
    public event Action<float> OnNPCSpeedUpgrade;
    public static Upgrades Instance;
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void Start()
    {
        HatHelperNPC._hatStackers = null; //??????
    }

    public void SetIncomeIndex(int index)
    {
        _incomeModifierIndex = index;
    }
    public void UpgradePlayerSpeed(int index)
    {
        float speedModifier = _upgrades.SpeedModifiers[index].SpeedMultiplier;
        FindObjectOfType<Mover>().IncreaseSpeed(speedModifier);
    }
    public void NPCInventorySizeUpgrade(int index)
    {
        _npcInventorySizeIndex = index;
        OnNPCInventorySizeUpgrade?.Invoke(NPCInventorySize);
    }

    public void NPCSpeedUpgrade(int index)
    {
        _npcSpeedIndex = index;
        OnNPCSpeedUpgrade?.Invoke(NPCSpeed);
    }

    public void SpawnHelperNPC(int index)
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


    //TODO progression subsystem
    // void PostProgression(string identifier, int upgradeLevel)
    // {
    //     var data = new Ketchapp.MayoSDK.Analytics.Data();
    //     data.AddValue("UpgradeName", identifier);
    //     data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
    //     KetchappSDK.Analytics.CustomEvent("UpgradeBought", data);
    // }
}
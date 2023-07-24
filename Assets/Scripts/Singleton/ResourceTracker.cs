using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taxi.WaitZones;
using Taxi.Upgrades;

public class ResourceTracker : MonoBehaviour
{
    public float PlayerMoney { get { return _currentMoney; } }
    private float _currentMoney;
    public event Action<float> OnPlayerMoneyChanged;
    public static ResourceTracker Instance { get; private set; }
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }
        Load();
    }
    private void OnEnable()
    {
        MoneyStacker.MoneyPickupHandler += OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler += OnMoneyPickup;
        PayMoneyProcessor.MoneyPayHandler += OnPayMoney;
    }

    private void OnDisable()
    {
        MoneyStacker.MoneyPickupHandler -= OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler -= OnMoneyPickup;
        PayMoneyProcessor.MoneyPayHandler -= OnPayMoney;
    }
    public void ZeroMoney()
    {
        PlayerPrefs.SetInt("Money", 0);
        _currentMoney = 0;
        OnPlayerMoneyChanged?.Invoke(_currentMoney);
    }
    public void OnPayMoney(float change)
    {
        _currentMoney -= change;
        OnMoneyChange();
    }
    public void OnCheatMoneyGain(float gain)
    {
        _currentMoney += gain;
        OnMoneyChange();
    }
    private void OnMoneyPickup()
    {
        _currentMoney += (UpgradesFacade.Instance.GetIncomeModifier() * GameManager.Instance.References.GameConfig.MoneyPerStack);
        OnMoneyChange();
    }
    private void OnMoneyChange()
    {
        PlayerPrefs.SetInt("Money", (int)_currentMoney);
        OnPlayerMoneyChanged?.Invoke(_currentMoney);
    }
    private void Load()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", GameManager.Instance.References.GameConfig.StartMoney);
            _currentMoney = PlayerPrefs.GetInt("Money");
        }
        else if (PlayerPrefs.GetInt("TutorialComplete") == 1)
        {
            PlayerPrefs.SetInt("Money", GameManager.Instance.References.GameConfig.StartMoney);
            _currentMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetInt("TutorialComplete", 2);
        }
        else
            _currentMoney = PlayerPrefs.GetInt("Money");
    }
}

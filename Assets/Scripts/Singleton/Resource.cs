using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taxi.WaitZones;
using Taxi.Upgrades;

public class Resource : MonoBehaviour
{
    public float PlayerMoney { get { return _currentMoney; } }
    public event Action<float> OnPlayerMoneyChanged;
    float _currentMoney;
    private void Awake()
    {
        ReadPrefs();
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
    void OnMoneyChange()
    {
        PlayerPrefs.SetInt("Money", (int)_currentMoney);
        OnPlayerMoneyChanged?.Invoke(_currentMoney);
    }
    void ReadPrefs()
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

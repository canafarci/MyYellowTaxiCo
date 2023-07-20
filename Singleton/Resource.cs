using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float Money { get { return _currentMoney; } }
    public event Action<float> MoneyChangeHandler;
    float _currentMoney;
    Upgrader _upgrader;
    private void Awake()
    {
        ReadPrefs();
        _upgrader = FindObjectOfType<Upgrader>();
    }

    private void OnEnable()
    {
        MoneyStacker.MoneyPickupHandler += OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler += OnMoneyPickup;
        BuyableWaitingZone.MoneyPayHandler += OnPayMoney;
    }

    private void OnDisable()
    {
        MoneyStacker.MoneyPickupHandler -= OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler -= OnMoneyPickup;
        BuyableWaitingZone.MoneyPayHandler -= OnPayMoney;
    }
    public void ZeroMoney()
    {
        PlayerPrefs.SetInt("Money", 0);
        _currentMoney = 0;
        MoneyChangeHandler?.Invoke(_currentMoney);
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
        _currentMoney += (_upgrader.IncomeModifier * GameManager.Instance.References.GameConfig.MoneyPerStack);
        OnMoneyChange();
    }
    void OnMoneyChange()
    {
        PlayerPrefs.SetInt("Money", (int)_currentMoney);
        MoneyChangeHandler?.Invoke(_currentMoney);
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

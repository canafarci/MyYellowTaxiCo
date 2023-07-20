using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BuyableWaitingZoneVisual))]
public class RepeatableBuyingWaitingZone : WaitingEngine
{
    [SerializeField] UpgradeData _upgradeData;
    [SerializeField] GameObject _spawnerObject;
    [SerializeField] float _moneyToUnlock;
    private float _remainingMoney;
    private BuyableWaitingZoneVisual _visual;
    private float _moneyStep;

    [SerializeField] TextMeshProUGUI _levelText;
    private IItemSpawner _itemSpawner;
    private int _currentUpgradeIndex;
    public static Action<float> MoneyPayHandler;
    private void Awake()
    {
        _itemSpawner = _spawnerObject.GetComponent<IItemSpawner>();
    }
    void Start()
    {
        Initialize(LoadUpgradeIndex());
    }

    private int LoadUpgradeIndex()
    {
        if (PlayerPrefs.HasKey(Globals.STACKER_UPGRADE_KEY))
            _currentUpgradeIndex = PlayerPrefs.GetInt(Globals.STACKER_UPGRADE_KEY);
        else
            PlayerPrefs.SetInt(Globals.STACKER_UPGRADE_KEY, 1);
        return _currentUpgradeIndex;
    }

    protected override void ResetLoop()
    {
        Initialize(_currentUpgradeIndex);
    }
    protected override void OnSuccess()
    {
        _currentUpgradeIndex++;
        PlayerPrefs.SetInt(Globals.STACKER_UPGRADE_KEY, _currentUpgradeIndex);
        SetStackerSpeed(_currentUpgradeIndex);

        base.OnSuccess();
    }
    private void SetStackerSpeed(int index)
    {
        if (_currentUpgradeIndex < _upgradeData.StackSpeeds.Length)
            _itemSpawner.SetSpawnRate(_upgradeData.StackSpeeds[_currentUpgradeIndex].SpawnRate);
    }
    private void Initialize(int currentIndex)
    {
        SetStackerSpeed(currentIndex);
        _moneyStep = _remainingMoney / _remainingTime * Globals.WAIT_ZONES_TIME_STEP;

        if (GetIndexIsAtMaxLength())
            return;

        _visual = GetComponent<BuyableWaitingZoneVisual>();
        _visual.Initialize(_moneyToUnlock);
        _visual.SetLevelText(_currentUpgradeIndex);


        _remainingTime = _timeToUnlock;
        _remainingMoney = _upgradeData.StackSpeeds[_currentUpgradeIndex + 1].Cost;
    }
    bool GetIndexIsAtMaxLength()
    {
        bool retVal = _currentUpgradeIndex >= _upgradeData.StackSpeeds.Length - 1;
        if (retVal)
            gameObject.SetActive(false);

        return retVal;
    }

    protected override bool CheckCanContinue()
    {
        return _remainingTime > 0f && _remainingMoney > 0f;
    }

    protected override void Execute()
    {
        float playerMoney = GameManager.Instance.Resources.Money;
        float precalculatedPlayerMoneyAfterStep = playerMoney - _moneyStep;
        float preCalculatedRemainingPayMoney = _remainingMoney - _moneyStep;

        if (preCalculatedRemainingPayMoney <= 0f)
        {
            FinishPaying();
            return;
        }

        if (precalculatedPlayerMoneyAfterStep < 0)
        {
            OnNotEnoughMoney(playerMoney);
            return;
        }

        UpdateState();
    }
    private void UpdateState()
    {
        _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);

        _remainingMoney -= _moneyStep;
        MoneyPayHandler?.Invoke(_moneyStep);

        _remainingTime -= Globals.WAIT_ZONES_TIME_STEP;
    }

    private void OnNotEnoughMoney(float playerMoney)
    {
        _remainingMoney -= playerMoney;
        _remainingTime -= Globals.WAIT_ZONES_TIME_STEP;
        //Visual
        _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);

        GameManager.Instance.Resources.ZeroMoney();
        _currentConfig.OnFail();
        Cancel();
    }

    private void FinishPaying()
    {
        _remainingMoney = 0;
        _remainingTime -= Globals.WAIT_ZONES_TIME_STEP;

        _visual.UpdateVisual(0, 1);
    }
}

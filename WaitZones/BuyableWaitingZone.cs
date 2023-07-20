using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(BuyableWaitingZoneVisual))]
public class BuyableWaitingZone : WaitingEngine
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] float _moneyToUnlock;
    private float _remainingMoney;
    private float _moneyStep;
    private BuyableWaitingZoneVisual _visual;
    public static Action<float> MoneyPayHandler;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _remainingTime = _timeToUnlock;
        _remainingMoney = _moneyToUnlock;

        _moneyStep = _remainingMoney / _remainingTime * Globals.WAIT_ZONES_TIME_STEP;

        _visual = GetComponent<BuyableWaitingZoneVisual>();
        _visual.Initialize(_moneyToUnlock);
    }

    protected override void ResetLoop()
    {
        _remainingMoney = _moneyToUnlock;
        _visual.Reset(_moneyToUnlock);

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
        _text.DOColor(Color.white, 0.001f);
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

    protected override bool CheckCanContinue()
    {
        return _remainingTime > 0f && _remainingMoney > 0f;
    }
}

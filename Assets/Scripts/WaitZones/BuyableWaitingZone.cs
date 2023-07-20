using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(BuyableWaitingZoneVisual), typeof(PayMoneyProcessor))]
public class BuyableWaitingZone : WaitingEngine
{
    [SerializeField] float _moneyToUnlock;
    private float _remainingMoney;
    private float _moneyStep;
    private BuyableWaitingZoneVisual _visual;
    private PayMoneyProcessor _payCalculator;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _payCalculator = GetComponent<PayMoneyProcessor>();

        _remainingTime = _timeToUnlock;
        _remainingMoney = _moneyToUnlock;

        //_moneyStep = _remainingMoney / _remainingTime * Globals.WAIT_ZONES_TIME_STEP;

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
        bool isSuccessful = _payCalculator.ProcessPay(ref _remainingTime, ref _remainingMoney);

        if (!isSuccessful)
        {
            _currentConfig.OnFail();
            Cancel();
        }

        _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);
    }

    private void UpdateState()
    {
        _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);


        _remainingTime -= Globals.WAIT_ZONES_TIME_STEP;
    }

    protected override bool CheckCanContinue()
    {
        return _remainingTime > 0f && _remainingMoney > 0f;
    }
}

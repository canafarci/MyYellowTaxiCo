using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PayUnlockLoop : MonoBehaviour
{
    [SerializeField] protected float _timeToUnlock, _moneyToUnlock;
    [SerializeField] protected TextMeshProUGUI _text;
    protected float _remainingMoney, _remainingTime;
    protected IFillableUI _fillable;
    public static Action<float> MoneyPayHandler;
    Coroutine _moneyCoroutine;
    private void Awake()
    {
        _fillable = GetComponent<IFillableUI>();
    }

    private void Start()
    {
        _remainingTime = _timeToUnlock;
        _remainingMoney = _moneyToUnlock;
        if (_text != null)
            FormatText(_moneyToUnlock);
    }
    public IEnumerator PayLoop(float time, TextMeshProUGUI text = null, Action successCallback = null, Action failCallback = null, GameObject slider = null)
    {
        float step = StaticVariables.WAIT_ZONES_TIME_STEP;
        float moneyStep = _remainingMoney / _remainingTime * step;
        _moneyCoroutine = StartCoroutine(DotweenFX.MoneyArcTween(transform.position));

        while (_remainingTime > 0f && _remainingMoney > 0f)
        {
            LoopCycle(step, moneyStep, failCallback);
            yield return new WaitForSeconds(step);
        }
        OnSuccess(successCallback);
    }
    protected virtual void OnSuccess(Action successCallback)
    {
        ResetLoop();
        StopCoroutine(_moneyCoroutine);
        successCallback();
    }
    virtual protected void ResetLoop()
    {
        _remainingMoney = _moneyToUnlock;
        FormatText(_moneyToUnlock);
        if (_fillable != null)
            _fillable.SetFill(0, 1);
    }
    private void LoopCycle(float step, float moneyStep, Action failCallback)
    {
        if (_fillable != null)
            _fillable.SetFill(_timeToUnlock - _remainingTime, _timeToUnlock);

        float playerMoney = GameManager.Instance.Resources.Money;
        float precalculatedPlayerMoneyAfterStep = playerMoney - moneyStep;
        float preCalculatedRemainingPayMoney = _remainingMoney - moneyStep;


        if (preCalculatedRemainingPayMoney <= 0f)
        {
            MoneyPayHandler?.Invoke(_remainingMoney);
            _remainingMoney = 0;
            _text.text = "0";
            _remainingTime -= step;
            return;
        }

        if (precalculatedPlayerMoneyAfterStep < 0)
        {
            _text.DOColor(Color.white, 0.001f);
            _remainingMoney -= playerMoney;
            _remainingTime -= step;
            FormatText(_remainingMoney);

            GameManager.Instance.Resources.ZeroMoney();
            StopCoroutine(_moneyCoroutine);
            failCallback();
            return;
        }

        _text.DOColor(Color.green, 0.1f);
        FormatText(_remainingMoney);

        _remainingMoney -= moneyStep;
        MoneyPayHandler?.Invoke(moneyStep);

        _remainingTime -= step;
    }
    protected void FormatText(float value)
    {
        if (value >= 1000)
        {
            if (value % 1000 == 0)
                _text.text = (value / 1000).ToString("F0") + "K";
            else
                _text.text = (value / 1000).ToString("F1") + "K";
        }
        else
            _text.text = value.ToString("F0");
    }
}

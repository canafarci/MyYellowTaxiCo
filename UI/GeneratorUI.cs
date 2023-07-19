using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class GeneratorUI : MonoBehaviour, IWaitLoop
{
    [SerializeField] protected int _price;
    public bool HasInitialized { set { _hasInitialized = value; } }
    protected float _remainingMoney, _moneyStep, _remainingTime, _maxTime;
    protected bool _hasInitialized = false;
    protected float _step = .025f;
    protected IFillableUI _fillable;
    public static Action<float> MoneyPayHandler;
    protected void Awake()
    {
        _fillable = GetComponent<IFillableUI>();
    }
    private void Start() => _hasInitialized = false;
    virtual public IEnumerator WaitLoop(float time, TextMeshProUGUI text = null, Action successCallback = null, Action failCallback = null, GameObject slider = null)
    {
        if (!_hasInitialized)
            InitializeValues(text, _step, time);

        while (_remainingTime > 0f)
        {

            if (_fillable != null)
                _fillable.SetFill(1 - _remainingTime, _maxTime);

            if (text != null)
            {
                float playerMoney = GameManager.Instance.Resources.Money;
                float precalculatedPlayerMoneyAfterStep = playerMoney - _moneyStep;
                float preCalculatedRemainingPayMoney = _remainingMoney - _moneyStep;

                if (preCalculatedRemainingPayMoney <= 0f)
                {
                    MoneyPayHandler?.Invoke(_remainingMoney);
                    text.text = "0";
                    yield break;
                }

                if (precalculatedPlayerMoneyAfterStep < 0)
                {
                    text.DOColor(Color.white, 0.001f);
                    _remainingMoney -= playerMoney;
                    FormatText(text, _remainingMoney);

                    GameManager.Instance.Resources.ZeroMoney();
                    failCallback();
                    yield break;
                }

                text.DOColor(Color.green, 0.1f);
                FormatText(text, _remainingMoney);

                _remainingMoney -= _moneyStep;
                MoneyPayHandler?.Invoke(_moneyStep);
            }

            _remainingTime -= _step;
            yield return new WaitForSeconds(_step);
        }

        if (text != null)
            text.text = "";

        yield return null;
    }

    protected void FormatText(TextMeshProUGUI text, float value)
    {
        if (value >= 1000)
        {
            int lastChar = (int)(value / 100) % 10;
            if (lastChar == 0)
                text.text = (value / 1000f).ToString("F0") + "K";
            else
                text.text = (value / 1000f).ToString("F1") + "K";

        }
        else
            text.text = value.ToString("F0");
    }

    protected void InitializeValues(TextMeshProUGUI text, float step, float time)
    {
        _hasInitialized = true;
        _remainingTime = time;
        _maxTime = time;

        if (text != null)
        {
            _remainingMoney = _price;
            _moneyStep = _remainingMoney / _maxTime * step;
            FormatText(text, _price);
        }
    }
}

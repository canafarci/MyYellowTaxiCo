using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BuyableWaitingZoneVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _levelText;
    private IFillableUI _fillable;


    public void Initialize(float moneyToUnlock)
    {
        _fillable = GetComponent<IFillableUI>();

        if (_text != null)
            FormatText(moneyToUnlock);
    }
    private void FormatText(float value)
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
    public void Reset(float moneyToUnlock)
    {
        if (_text != null)
            FormatText(moneyToUnlock);

        if (_fillable != null)
            _fillable.SetFill(0, 1);
    }
    public void UpdateVisual(float value, float maxValue)
    {
        FormatText(value);
        _text.DOColor(Color.green, 0.1f);
        _fillable.SetFill(value, maxValue);
    }
    public void SetLevelText(int currentIndex)
    {
        _levelText.text = "LEVEL " + (currentIndex + 1).ToString();
    }
}


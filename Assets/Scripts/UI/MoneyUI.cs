using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        _text.text = ResourceTracker.Instance.PlayerMoney.ToString("F0");
        ResourceTracker.Instance.OnPlayerMoneyChanged += OnMoneyChange;
    }

    private void OnDisable()
    {
        ResourceTracker.Instance.OnPlayerMoneyChanged -= OnMoneyChange;
    }

    void OnMoneyChange(float value)
    {
        _text.text = value.ToString("F0");
    }

}

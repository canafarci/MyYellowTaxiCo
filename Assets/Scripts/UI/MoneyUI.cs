using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using TaxiGame.Resource;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI _text;

    private ResourceTracker _resourceTracker;

    [Inject]
    private void Init(ResourceTracker tracker)
    {
        _resourceTracker = tracker;
    }
    //TODO DI
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        _text.text = _resourceTracker.PlayerMoney.ToString("F0");
        _resourceTracker.OnPlayerMoneyChanged += OnMoneyChange;
    }

    private void OnDisable()
    {
        _resourceTracker.OnPlayerMoneyChanged -= OnMoneyChange;
    }

    void OnMoneyChange(float value)
    {
        _text.text = value.ToString("F0");
    }

}

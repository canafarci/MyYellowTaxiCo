using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PayUnlockLoop : BasePayLoop
{
    private void Start()
    {
        _remainingTime = _timeToUnlock;
        _remainingMoney = _moneyToUnlock;
        if (_text != null)
            FormatText(_moneyToUnlock);
    }
    protected override void ResetLoop()
    {
        _remainingMoney = _moneyToUnlock;
        if (_text != null)
            FormatText(_moneyToUnlock);
        if (_fillable != null)
            _fillable.SetFill(0, 1);
    }
}

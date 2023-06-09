using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContiniousPayUnlockLoop : PayUnlockLoop
{
    [SerializeField] UpgradeData _upgradeData;
    [SerializeField] ItemGenerator _stacker;
    [SerializeField] TextMeshProUGUI _levelText;
    int _currentIndex;
    void Start()
    {
        if (PlayerPrefs.HasKey(StaticVariables.STACKER_UPGRADE_KEY))
            _currentIndex = PlayerPrefs.GetInt(StaticVariables.STACKER_UPGRADE_KEY);
        else
            PlayerPrefs.SetInt(StaticVariables.STACKER_UPGRADE_KEY, 1);

        SetVariables(_currentIndex);
    }
    protected override void ResetLoop()
    {
        SetVariables(_currentIndex);
    }
    protected override void OnSuccess(Action successCallback)
    {
        _currentIndex++;
        PlayerPrefs.SetInt(StaticVariables.STACKER_UPGRADE_KEY, _currentIndex);
        SetStackerSpeed(_currentIndex);
        base.OnSuccess(successCallback);
    }
    void SetStackerSpeed(int index)
    {
        if (_currentIndex < _upgradeData.StackSpeeds.Length)
            _stacker.SpawnRate = _upgradeData.StackSpeeds[_currentIndex].SpawnRate;
    }
    private void SetVariables(int currentIndex)
    {
        SetStackerSpeed(currentIndex);

        if (CheckIndexAtMaxLength())
            return;

        if (_fillable != null)
            _fillable.SetFill(0, 1);

        _remainingTime = _timeToUnlock;
        _remainingMoney = _upgradeData.StackSpeeds[_currentIndex + 1].Cost;

        _levelText.text = "LEVEL " + (currentIndex + 1).ToString();

        if (_text != null)
            FormatText(_remainingMoney);
    }
    bool CheckIndexAtMaxLength()
    {
        bool retVal = _currentIndex >= _upgradeData.StackSpeeds.Length - 1;
        if (retVal)
            gameObject.SetActive(false);

        return retVal;
    }
}

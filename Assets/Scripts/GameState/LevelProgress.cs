using System;
using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    StarTween _tweener;
    int _levelProgressValue, _currentLevel, _levelIncreaseTreshold;
    public event Action<int> LevelIncreaseHandler;
    private void Awake()
    {
        _tweener = GetComponent<StarTween>();
    }

    private void Start() => InitValues();
    public void OnLevelProgress() => _tweener.OnLevelProgress(() => ProgressLevel());
    public void ProgressLevel()
    {
        _levelProgressValue += 1;
        PlayerPrefs.SetInt("LevelProgressValue", _levelProgressValue);

        if (_levelProgressValue % _levelIncreaseTreshold == 0)
            OnLevelIncrease();

        _tweener.UpdateUI(_currentLevel, _levelProgressValue, _levelIncreaseTreshold);
    }
    private void OnLevelIncrease()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("StarLevel", _currentLevel);
        data.AddValue("ProgressionStatus", "Completed");
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent("StarLevel", data);


        _levelProgressValue = 1;
        _currentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        LevelIncreaseHandler?.Invoke(_currentLevel);
        UpdateTreshold(_currentLevel);

        LevelStartEvent(_currentLevel);
    }
    void UpdateTreshold(int currentLevel)
    {
        if (currentLevel == 2)
            _levelIncreaseTreshold = 15;
        else if (currentLevel == 3)
            _levelIncreaseTreshold = 20;
        else if (currentLevel == 9)
            _levelIncreaseTreshold = 25;

        PlayerPrefs.SetInt("LevelIncreaseValue", _levelIncreaseTreshold);
    }
    void InitValues()
    {
        if (PlayerPrefs.HasKey("LevelProgressValue"))
            _levelProgressValue = PlayerPrefs.GetInt("LevelProgressValue");
        else
        {
            _levelProgressValue = 1;
            PlayerPrefs.SetInt("LevelProgressValue", _levelProgressValue);
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        else
        {
            _currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        }

        if (PlayerPrefs.HasKey("LevelIncreaseValue"))
            _levelIncreaseTreshold = PlayerPrefs.GetInt("LevelIncreaseValue");
        else
        {
            _levelIncreaseTreshold = 10;
            PlayerPrefs.SetInt("LevelIncreaseValue", _levelIncreaseTreshold);
        }

        _tweener.UpdateUI(_currentLevel, _levelProgressValue, _levelIncreaseTreshold);
        LevelIncreaseHandler?.Invoke(_currentLevel);

        LevelStartEvent(_currentLevel);
    }

    private void LevelStartEvent(int level)
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("StarLevel", _currentLevel);
        data.AddValue("ProgressionStatus", "Started");
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent("StarLevel", data);
    }

    protected void OnApplicationFocus(bool isFocused)
    {
        if (!isFocused)
        {
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("StarLevel", _currentLevel);
            data.AddValue("ProgressionStatus", "Faied");
            data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
            KetchappSDK.Analytics.CustomEvent("StarLevel", data);
        }
    }
}

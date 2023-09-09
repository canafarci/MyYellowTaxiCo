using System;
using TaxiGame.Resource;
using UnityEngine;
using Zenject;

#if UNITY_ANDROID
using Ketchapp.MayoSDK;
#endif
public class LevelProgress : MonoBehaviour
{
    StarTween _tweener;
    int _levelProgressValue, _currentLevel, _levelIncreaseTreshold;
    private ResourceTracker _resourceTracker;

    public event Action<int> LevelIncreaseHandler;

    [Inject]
    private void Init(ResourceTracker tracker)
    {
        _resourceTracker = tracker;
    }
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
#if UNITY_ANDROID

        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("StarLevel", _currentLevel);
        data.AddValue("ProgressionStatus", "Completed");
        data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent("StarLevel", data);


        _levelProgressValue = 1;
        _currentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
#endif
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
#if UNITY_ANDROID

        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("StarLevel", _currentLevel);
        data.AddValue("ProgressionStatus", "Started");
        data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent("StarLevel", data);
#endif
    }

    protected void OnApplicationFocus(bool isFocused)
    {
#if UNITY_ANDROID
        if (!isFocused)
        {

            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("StarLevel", _currentLevel);
            data.AddValue("ProgressionStatus", "Faied");
            data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
            KetchappSDK.Analytics.CustomEvent("StarLevel", data);
        }
#endif
    }
}

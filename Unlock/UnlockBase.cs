using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnlockBase : MonoBehaviour, IUnlockable
{
    public bool HasUnlockedBefore { get { return HasUnlocked(); } }
    [SerializeField] protected UnityEvent _onUnlock;
    [SerializeField] protected string _identifier;
    protected bool _hasUnlocked = false;
    public void UnlockObject()
    {
        if (_hasUnlocked) return;
        _hasUnlocked = true;
        _onUnlock.Invoke();

        if (!HasUnlockedBefore)
            SendOnUnlockCompleteAnalytics();

        Save();
    }

    private void SendOnUnlockCompleteAnalytics()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Completed");
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }
    protected void SendAnalyticsDataForProgressionStart()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Started");
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(_identifier, 1);
    }
    protected bool HasUnlocked()
    {
        return PlayerPrefs.HasKey(_identifier);
    }
    private void OnApplicationFocus(bool isFocused)
    {
        if (!isFocused && !HasUnlockedBefore)
        {
            SendOnQuitAnalytics();
        }
    }

    private void SendOnQuitAnalytics()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Failed");
        data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }
}
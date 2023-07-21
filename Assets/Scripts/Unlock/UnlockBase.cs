using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnlockBase : MonoBehaviour, IUnlockable
{
    [SerializeField] protected UnityEvent _onUnlock;
    [SerializeField] protected string _identifier;
    public virtual void UnlockObject()
    {
        _onUnlock.Invoke();

        if (!HasUnlockedBefore())
            SendOnUnlockCompleteAnalytics();

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(_identifier, 1);
    }
    public bool HasUnlockedBefore()
    {
        return PlayerPrefs.HasKey(_identifier);
    }

    //TODO violates SRP
    private void OnApplicationFocus(bool isFocused)
    {
        if (!isFocused && !HasUnlockedBefore())
        {
            SendOnQuitAnalytics();
        }
    }
    private void SendOnQuitAnalytics()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Failed");
        data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }
    private void SendOnUnlockCompleteAnalytics()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Completed");
        data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }
    protected void SendAnalyticsDataForProgressionStart()
    {
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Started");
        data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
    }
}
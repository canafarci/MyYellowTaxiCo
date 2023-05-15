using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public class Unlock : MonoBehaviour
{
    [SerializeField] protected UnityEvent _onUnlock;
    [SerializeField] protected string _identifier;
    public bool HasUnlockedBefore { get { return HasUnlocked(); } }
    protected bool _hasUnlocked = false;
    private void Start()
    {

        if (HasUnlockedBefore)
            UnlockObject();
        else
        {
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Started");
            data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
            KetchappSDK.Analytics.CustomEvent(_identifier, data);
        }
    }
    public virtual void UnlockObject()
    {
        if (_hasUnlocked) return;
        _hasUnlocked = true;
        _onUnlock.Invoke();

        if (!HasUnlockedBefore)
        {
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Completed");
            data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
            KetchappSDK.Analytics.CustomEvent(_identifier, data);
        }
        Save();
    }
    void Save()
    {
        PlayerPrefs.SetInt(_identifier, 1);
    }
    protected bool HasUnlocked()
    {
        return PlayerPrefs.HasKey(_identifier);
    }

    protected void OnApplicationFocus(bool isFocused)
    {
        if (!isFocused && !HasUnlockedBefore)
        {
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Failed");
            data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
            KetchappSDK.Analytics.CustomEvent(_identifier, data);
        }
    }
}

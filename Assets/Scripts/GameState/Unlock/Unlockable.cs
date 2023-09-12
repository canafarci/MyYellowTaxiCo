using TaxiGame.Resource;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace TaxiGame.GameState.Unlocking
{
    public class Unlockable : MonoBehaviour, IUnlockable
    {
        [SerializeField] protected UnityEvent _onUnlock;
        [SerializeField] protected UnityEvent _persistentUnlock;
        [SerializeField] protected string _identifier;
        private ResourceTracker _resourceTracker;

        [Inject]
        private void Init(ResourceTracker tracker)
        {
            _resourceTracker = tracker;
        }

        private void Start()
        {
            if (HasUnlockedBefore())
            {
                HandleUnlockedBefore();
            }
            else
            {
                SendAnalyticsDataForProgressionStart();
            }
        }

        protected virtual void HandleUnlockedBefore()
        {
            _persistentUnlock.Invoke();
        }

        public virtual void UnlockObject()
        {
            if (!HasUnlockedBefore())
            {
                _onUnlock.Invoke();
                _persistentUnlock.Invoke();
                SendOnUnlockCompleteAnalytics();
                Save();
            }
        }

        private void Save()
        {
            PlayerPrefs.SetInt(_identifier, 1);
        }
        protected bool HasUnlockedBefore()
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
#if UNITY_ANDROID
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Failed");
        data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
#endif
        }
        private void SendOnUnlockCompleteAnalytics()
        {
#if UNITY_ANDROID
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Completed");
        data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
#endif
        }
        protected void SendAnalyticsDataForProgressionStart()
        {
#if UNITY_ANDROID
        var data = new Ketchapp.MayoSDK.Analytics.Data();
        data.AddValue("ProgressionStatus", "Started");
        data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
        KetchappSDK.Analytics.CustomEvent(_identifier, data);
#endif
        }
    }
}
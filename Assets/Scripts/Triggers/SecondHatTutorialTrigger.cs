using TaxiGame.Resource;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

#if UNITY_ANDROID
using Ketchapp.MayoSDK;
#endif

public class SecondHatTutorialTrigger : TutorialTrigger
{
    public bool SecondHatTutorialStarted = false;
    [SerializeField] UnityEvent _onSecondHatTutorialUnlock;
    private ResourceTracker _resourceTracker;
    [Inject]
    private void Init(ResourceTracker tracker)
    {
        _resourceTracker = tracker;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (SecondHatTutorialStarted && other.CompareTag("Player"))
        {
            _onSecondHatTutorialUnlock.Invoke();
            SecondHatTutorialStarted = false;
#if UNITY_ANDROID
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Completed");
            data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
            KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialGiveHatToDrivers", data);
#endif
        }
    }
}
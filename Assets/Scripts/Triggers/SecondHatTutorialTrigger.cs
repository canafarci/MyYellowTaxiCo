using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public class SecondHatTutorialTrigger : TutorialTrigger
{
    public bool SecondHatTutorialStarted = false;
    [SerializeField] UnityEvent _onSecondHatTutorialUnlock;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (SecondHatTutorialStarted && other.CompareTag("Player"))
        {
            _onSecondHatTutorialUnlock.Invoke();
            SecondHatTutorialStarted = false;

            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Completed");
            data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
            KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialGiveHatToDrivers", data);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;

public class TutorialUnlock : Unlock
{
    [SerializeField] bool _isFirst;
    [SerializeField] TutorialUnlock _nextUnlocker;
    private void Start()
    {
        if (_isFirst && HasUnlocked())
        {
            SequentialUnlock();
        }
        else if (!HasUnlockedBefore)
        {
            var data = new Ketchapp.MayoSDK.Analytics.Data();
            data.AddValue("ProgressionStatus", "Started");
            data.AddValue("Money", (int)GameManager.Instance.Resources.Money);
            KetchappSDK.Analytics.CustomEvent(_identifier, data);
        }
    }

    public void SequentialUnlock()
    {
        if (HasUnlocked())
            StartCoroutine(UnlockCoroutine());
    }

    IEnumerator UnlockCoroutine()
    {
        yield return new WaitForEndOfFrame();
        UnlockObject();
        if (_nextUnlocker != null)
        {
            _nextUnlocker.SequentialUnlock();
        }
    }
}

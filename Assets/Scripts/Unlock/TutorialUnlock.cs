using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public class TutorialUnlock : UnlockBase
{
    [SerializeField] bool _isFirst;
    [SerializeField] TutorialUnlock _nextUnlocker;
    private void Start()
    {
        if (_isFirst && HasUnlockedBefore())
        {
            SequentialUnlock();
        }
        else if (!HasUnlockedBefore())
        {
            SendAnalyticsDataForProgressionStart();
        }
    }

    public void SequentialUnlock()
    {
        if (HasUnlockedBefore())
            StartCoroutine(UnlockCoroutine());
    }

    IEnumerator UnlockCoroutine()
    {
        yield return new WaitForEndOfFrame();
        UnlockObject();
        if (_nextUnlocker != null)
        {
            _nextUnlocker.gameObject.SetActive(true);
            _nextUnlocker.SequentialUnlock();
        }
    }

}

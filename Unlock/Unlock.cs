using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using UnityEngine;
using UnityEngine.Events;

public class Unlock : UnlockBase
{
    private void Start()
    {
        if (HasUnlockedBefore)
            UnlockObject();
        else
            SendAnalyticsDataForProgressionStart();
    }
}

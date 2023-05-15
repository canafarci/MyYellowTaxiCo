using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IWaitLoop
{
    public IEnumerator WaitLoop(float time, TextMeshProUGUI text = null, Action successCallback = null, Action failCallback = null, GameObject slider = null);
}

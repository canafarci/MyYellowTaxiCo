using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IPayLoop
{
    IEnumerator PayLoop(Action successCallback = null, Action failCallback = null);
    public void StopAllCoroutines();
}

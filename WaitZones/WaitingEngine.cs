using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class WaitingEngine : MonoBehaviour, IWaitingEngine
{
    [SerializeField] protected float _timeToUnlock;
    protected float _remainingTime;
    protected Coroutine _currentRun;
    protected WaitZoneConfigSO _currentConfig = null;

    public void Begin(WaitZoneConfigSO config)
    {
        _currentConfig = config;
        _currentRun = StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (CheckCanContinue())
        {
            Execute();
            yield return new WaitForSeconds(Globals.WAIT_ZONES_TIME_STEP);
        }
        OnSuccess();
    }
    public void Cancel()
    {
        StopCoroutine(_currentRun);
    }
    protected abstract bool CheckCanContinue();
    protected abstract void ResetLoop();
    protected abstract void Execute();
    protected virtual void OnSuccess()
    {
        _currentConfig.OnSuccess();
        ResetLoop();
    }
}

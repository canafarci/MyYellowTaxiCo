using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitZoneConfigSO", menuName = "NewYork Taxi/WaitZoneConfigSO", order = 0)]
public class WaitZoneConfigSO : ScriptableObject
{
    public Action OnSuccess { get { return _onSuccess; } }
    public Action OnFail { get { return _onFail; } }
    private Action _onSuccess;
    private Action _onFail;
    public WaitZoneConfigSO(Action OnSuccess, Action OnFail)
    {
        _onSuccess = OnSuccess;
        _onFail = OnFail;
    }
}
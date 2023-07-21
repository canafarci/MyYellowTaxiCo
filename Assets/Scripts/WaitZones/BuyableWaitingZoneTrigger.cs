using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System;

public class BuyableWaitingZoneTrigger : MonoBehaviour
{
    private IWaitingEngine _payingZoneEngine;
    private IUnlockable _unlockable;
    private Action _successAction;
    private void Awake()
    {
        _payingZoneEngine = GetComponent<IWaitingEngine>();
        _unlockable = GetComponent<IUnlockable>();
    }
    private void Start()
    {
        _successAction = () =>
            {
                if (_unlockable != null)
                    _unlockable.UnlockObject();
            };
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaitZoneConfigSO config = new WaitZoneConfigSO(_successAction);
            _payingZoneEngine.Begin(config, other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _payingZoneEngine.Cancel(other.gameObject);
        }
    }
}
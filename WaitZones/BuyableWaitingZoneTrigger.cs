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
    private Action _failAction;
    private void Awake()
    {
        _payingZoneEngine = GetComponent<BuyableWaitingZone>();
        _unlockable = GetComponent<IUnlockable>();
    }
    private void Start()
    {
        _successAction = () =>
            {
                if (_unlockable != null)
                    _unlockable.UnlockObject();
            };
        _failAction = () => StopAllCoroutines();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaitZoneConfigSO config = new WaitZoneConfigSO(_successAction,
                                                            _failAction
                                                                            );
            _payingZoneEngine.Begin(config);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _payingZoneEngine.Cancel();
        }
    }
}
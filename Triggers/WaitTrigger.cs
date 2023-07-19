﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class WaitTrigger : MonoBehaviour
{
    private PayUnlockLoop _payUnlockLoop;
    private Coroutine _payRoutine;
    IUnlockable _unlockable;

    private void Awake()
    {
        _payUnlockLoop = GetComponent<PayUnlockLoop>();
        _unlockable = GetComponent<IUnlockable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _payRoutine = StartCoroutine(UnlockItemLoop());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            _payUnlockLoop.StopAllCoroutines();
        }
    }


    private IEnumerator UnlockItemLoop()
    {
        //prevent accidental player walking and paying
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(_payUnlockLoop.PayLoop(() => { if (_unlockable != null) _unlockable.UnlockObject(); },
                                                            () => StopAllCoroutines()));
    }


}
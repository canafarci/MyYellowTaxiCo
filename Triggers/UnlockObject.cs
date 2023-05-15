using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class UnlockObject : MonoBehaviour
{
    private PayUnlockLoop _payUnlockLoop;
    private Coroutine _payRoutine;
    Unlock _unlocker;

    private void Awake()
    {
        _payUnlockLoop = GetComponent<PayUnlockLoop>();
        _unlocker = GetComponent<Unlock>();
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
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(_payUnlockLoop.WaitLoop(0f, null,
                                                            () => { if (_unlocker != null) _unlocker.UnlockObject(); },
                                                            () => StopAllCoroutines()));
    }


}
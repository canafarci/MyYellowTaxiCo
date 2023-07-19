using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class PayLoopTrigger : MonoBehaviour
{
    private IPayLoop _payUnlockLoop;
    private Coroutine _payRoutine;
    private IUnlockable _unlockable;

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
        // Prevent accidental player walking and paying
        yield return new WaitForSeconds(1f);

        yield return _payUnlockLoop.PayLoop(() =>
            {
                if (_unlockable != null)
                    _unlockable.UnlockObject();
            },
            () => StopAllCoroutines());
    }


}
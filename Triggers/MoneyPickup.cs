using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    MoneyStacker _stacker;
    private void Awake() => _stacker = GetComponent<MoneyStacker>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup());
            Unlock unlocker = GetComponent<Unlock>();
            if (unlocker != null && !unlocker.HasUnlockedBefore)
                unlocker.UnlockObject();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StopAllCoroutines();
    }
    IEnumerator Pickup()
    {
        while (true)
        {
            _stacker.StartEmptyStack();
            yield return new WaitForSeconds(0.55f);
        }
    }
}

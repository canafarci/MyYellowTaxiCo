using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    IUnlockable _unlocker;

    private void Awake()
    {
        _unlocker = GetComponent<IUnlockable>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _unlocker != null && !_unlocker.HasUnlockedBefore())
        {
            _unlocker.UnlockObject();
        }
    }



}

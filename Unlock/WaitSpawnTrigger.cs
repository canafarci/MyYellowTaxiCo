using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSpawnTrigger : MonoBehaviour
{
    WaitSpawnLoop _waitLoop;
    Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
    private void Awake()
    {
        _waitLoop = GetComponent<WaitSpawnLoop>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            _coroutines[other] = StartCoroutine(_waitLoop.SpawnLoop(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            if (_coroutines[other] != null)
            {
                StopCoroutine(_coroutines[other]);
                _waitLoop.Sliders[other].SetActive(false);
            }
        }
    }

}

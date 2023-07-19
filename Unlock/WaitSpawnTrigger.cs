using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSpawnTrigger : MonoBehaviour
{
    private WaitSpawnLoop _waitLoop;
    private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
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
            if (_coroutines.TryGetValue(other, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                _waitLoop.Sliders[other].SetActive(false);
                _coroutines.Remove(other);
            }
        }
    }

}

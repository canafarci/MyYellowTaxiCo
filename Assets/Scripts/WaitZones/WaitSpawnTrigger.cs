using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSpawnTrigger : MonoBehaviour
{
    private WaitSpawnLoop _waitLoop;
    private ISliderVisual _sliderVisual;
    private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
    private void Awake()
    {
        _waitLoop = GetComponent<WaitSpawnLoop>();
        _sliderVisual = GetComponent<ISliderVisual>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            OnWaitZoneEnter(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            OnWaitZoneExit(other);
        }
    }
    private void OnWaitZoneEnter(Collider other)
    {
        GameObject slider = other.GetComponent<ComponentReference>().Slider;
        _coroutines[other] = StartCoroutine(_waitLoop.SpawnLoop(other, slider));
    }
    private void OnWaitZoneExit(Collider other)
    {
        if (_coroutines.TryGetValue(other, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            GameObject slider = other.GetComponent<ComponentReference>().Slider;
            _sliderVisual.HideSlider(slider);
            _coroutines.Remove(other);
        }
    }
}

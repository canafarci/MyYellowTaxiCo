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
            GameObject slider = other.GetComponent<ComponentReference>().Slider;
            _coroutines[other] = StartCoroutine(_waitLoop.SpawnLoop(other, slider));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
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

}

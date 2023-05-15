using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingGeneratorUI : MonoBehaviour, IWaitLoop
{
    float _money, _moneyStep, _remainingTime, _maxTime;

    public IEnumerator WaitLoop(float time, TextMeshProUGUI text = null, Action failCallback = null, Action successCallback = null, GameObject slider = null)
    {
        Material mat = slider.transform.GetChild(1).GetComponent<Renderer>().material;
        float step = .025f;
        mat.SetFloat("_ClipUvUp", 1f);

        InitializeValues(step, time);
        slider.SetActive(true);


        while (_remainingTime > 0f)
        {
            _remainingTime -= step;
            yield return new WaitForSeconds(step);

            mat.SetFloat("_ClipUvUp", (_remainingTime / _maxTime));
        }

        slider.SetActive(false);
    }

    private void InitializeValues(float step, float time)
    {
        _remainingTime = time;
        _maxTime = time;
    }
}

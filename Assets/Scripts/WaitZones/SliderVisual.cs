using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderVisual : MonoBehaviour, ISliderVisual
{
    public void ShowSlider(GameObject slider)
    {
        slider.SetActive(true);
    }

    public void UpdateSlider(GameObject slider, float remainingTime, float totalTime)
    {
        float progress = remainingTime / totalTime;
        Material mat = slider.transform.GetChild(1).GetComponent<Renderer>().material;
        mat.SetFloat("_ClipUvUp", progress);
    }

    public void HideSlider(GameObject slider)
    {
        slider.SetActive(false);
    }
}


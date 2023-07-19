using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISliderVisual
{
    void ShowSlider(GameObject slider);
    void UpdateSlider(GameObject slider, float remainingTime, float totalTime);
    void HideSlider(GameObject slider);
}


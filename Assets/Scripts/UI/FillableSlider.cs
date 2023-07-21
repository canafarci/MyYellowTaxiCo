using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillableSlider : MonoBehaviour, IFeedbackVisual
{
    [SerializeField] protected Slider _slider;
    public void SetValue(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }

}

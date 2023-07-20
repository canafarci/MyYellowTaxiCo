using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillableSlider : MonoBehaviour, IFillableUI
{
    [SerializeField] protected Slider _slider;
    public void SetFill(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }

}

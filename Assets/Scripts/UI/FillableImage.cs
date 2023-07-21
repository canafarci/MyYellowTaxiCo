using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillableImage : MonoBehaviour, IFeedbackVisual
{
    [SerializeField] Image _image;
    public void SetValue(float currentValue, float maxValue)
    {
        _image.fillAmount = currentValue / maxValue;
    }

}

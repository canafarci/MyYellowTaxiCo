using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillableImage : MonoBehaviour, IFillableUI
{
    [SerializeField] Image _image;
    public void SetFill(float currentValue, float maxValue)
    {
        _image.fillAmount = currentValue / maxValue;
    }

}

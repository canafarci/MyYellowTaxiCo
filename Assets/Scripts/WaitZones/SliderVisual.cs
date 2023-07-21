using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Taxi.WaitZones
{
    public class SliderVisual : MonoBehaviour
    {
        public void Show(GameObject parent)
        {
            GameObject slider = parent.GetComponent<ComponentReference>().Slider;
            slider.SetActive(true);
        }

        public void SetValue(GameObject parent, float remainingTime, float totalTime)
        {
            GameObject slider = parent.GetComponent<ComponentReference>().Slider;
            float progress = remainingTime / totalTime;
            Material mat = slider.transform.GetChild(1).GetComponent<Renderer>().material;
            mat.SetFloat("_ClipUvUp", progress);
        }

        public void Hide(GameObject parent)
        {
            GameObject slider = parent.GetComponent<ComponentReference>().Slider;
            slider.SetActive(false);
        }
    }
}